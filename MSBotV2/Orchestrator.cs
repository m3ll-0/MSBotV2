using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSBotV2.Config;
using static MSBotV2.FinishedScripts;
using static MSBotV2.TemplateMatching;

namespace MSBotV2
{
    public static class Orchestrator
    {
        private static OrchestratorMode orchestratorMode = OrchestratorMode.MODE_BUFF;
        private static ScriptItemAttackType currentAttackTypeMode = ScriptItemAttackType.LEFT_TO_RIGHT;
        private static Dictionary<List<ScriptItem>, ScriptItemAttackType> attackPool = CreateAttackScriptsPool();
        private static Core core = new Core();
        private static Stopwatch sw = new Stopwatch();
        private static OrchestratorModeCycleStrategy orchestratorModeCycleStrategy = OrchestratorModeCycleStrategy.SIMPLE; // todo: conf
        private static Map currentMap = Map.MOTTLED_FOREST_3;
        private static bool ORCHESTRATOR_INTERRUPTED = false;

        public static void Orchestrate() {

            // Start stopwatch
            sw.Start();

            for (; ; ) {

                if (ORCHESTRATOR_INTERRUPTED)
                {
                    Logger.Log(nameof(Script), $"Orchestrator interrupted to call DynamicScript, in idle mode", Logger.LoggerPriority.HIGH);
                    Thread.Sleep(3000);
                    continue;
                }

                if (orchestratorMode == OrchestratorMode.MODE_ATTACK)
                {
                    HandleAttackMode();
                }
                else if (orchestratorMode == OrchestratorMode.MODE_CC)
                {
                    handleChangeChannelMode();
                }
                else if (orchestratorMode == OrchestratorMode.MODE_BUFF)
                {
                    handleBuffMode();
                }
                // Special OM not part of cycle
                else if (orchestratorMode == OrchestratorMode.MODE_ATTACK_SPECTER) {
                    HandleAttackSpecterMode(); 
                }

                // Poll switch mode according to Cycle definition
                PollOrchestratorModeCycleStrategy();
            }
        }

        private static void PollOrchestratorModeCycleStrategy()
        {
            // Get configured switch time
            int switchTime = OrchestratorModeCycleStrategyConfig.cycleConfigTime[orchestratorMode];

            // See if elapsed time > switch time
            if (sw.Elapsed.TotalMilliseconds > switchTime)
            {
                Logger.Log(nameof(Orchestrator), $"Normal Cycle, changing OrchestratorMode from [{orchestratorMode}] to [{OrchestratorModeCycleStrategyConfig.cycleConfigSequence[orchestratorMode]}]", Logger.LoggerPriority.MEDIUM);

                orchestratorMode = OrchestratorModeCycleStrategyConfig.cycleConfigSequence[orchestratorMode];

                // Reset timer after changing modes
                sw.Restart();
            }
            else
            {
                Logger.Log(nameof(Orchestrator), $"Lapsed {sw.Elapsed.TotalMilliseconds} / {switchTime} ", Logger.LoggerPriority.LOW);
            }
        }

        private static void HandleAttackMode() {

            // Compose random attack script based on direction and run it
            Script attackScript = ScriptComposer.Compose(Helper.GetRandomAttack(currentAttackTypeMode));
            core.RunScript(attackScript);

            // Toggle attack type mode to change direction
            currentAttackTypeMode = (currentAttackTypeMode == ScriptItemAttackType.RIGHT_TO_LEFT ? ScriptItemAttackType.LEFT_TO_RIGHT : ScriptItemAttackType.RIGHT_TO_LEFT);
            Logger.Log(nameof(Orchestrator), $"Changing ScriptItemAttackType to [{currentAttackTypeMode}]", Logger.LoggerPriority.MEDIUM);
        }

        private static void HandleAttackSpecterMode()
        {
            // Problem: First time enter
            // Problem: What if spectermode is deactivated, attacks will continue
            // Problem: Specter mode only needs to be activated in combat mode, not anywhere else
            
            // Compose random attack script based on direction and run it
            Script attackScript = ScriptComposer.Compose(Helper.GetRandomSpecterAttack(currentAttackTypeMode));
            core.RunScript(attackScript);

            // Toggle attack type mode to change direction
            currentAttackTypeMode = (currentAttackTypeMode == ScriptItemAttackType.RIGHT_TO_LEFT ? ScriptItemAttackType.LEFT_TO_RIGHT : ScriptItemAttackType.RIGHT_TO_LEFT);
            Logger.Log(nameof(Orchestrator), $"Changing ScriptItemSpecterAttackType to [{currentAttackTypeMode}]", Logger.LoggerPriority.MEDIUM);

            // Need to handle exit spectermode somewhere
            int switchTime = OrchestratorModeCycleStrategyConfig.cycleConfigTime[orchestratorMode];

            if (sw.Elapsed.TotalMilliseconds > switchTime)
            {
                Logger.Log(nameof(Orchestrator), $"Detected switchtime in [{orchestratorMode}], exiting specter mode. ", Logger.LoggerPriority.MEDIUM);
                core.RunScript(ScriptComposer.Compose(ToggleSpecterMode));

                Logger.Log(nameof(Orchestrator), $"End of SpecterMode reached, changing Orchestrator mode from [{orchestratorMode}] to [{OrchestratorModeCycleStrategyConfig.cycleConfigSequence[orchestratorMode]}]", Logger.LoggerPriority.HIGH);
                orchestratorMode = OrchestratorModeCycleStrategyConfig.cycleConfigSequence[orchestratorMode];

                sw.Restart();
            }
        }

        private static void handleChangeChannelMode() {
            Logger.Log(nameof(Orchestrator), $"Changing channel {currentAttackTypeMode}", Logger.LoggerPriority.MEDIUM);

            // Run the script
            core.RunScript(ScriptComposer.Compose(MapChangeChannelScripts.mapChangeChannelScripts[currentMap]));
            
            // Change Attack direction respectively
            currentAttackTypeMode = MapChangeChannelStartingDirections.mapChangeChannelStartingDirections[currentMap];
        }

        private static void handleBuffMode()
        {
            Logger.Log(nameof(Orchestrator), $"Buffing", Logger.LoggerPriority.MEDIUM);

            core.RunScript(ScriptComposer.Compose(Buff));
        }

        /*
         * Does TemplateMatching on a specified TemplateMatchingAction and invokes a corresponding script. Returns the corresponding OrchestratorMode that
         * will be set afterwards which might interrupt the current cycle.
         */
        private static OrchestratorMode? ExecuteTemplateMatching(TemplateMatchingAction templateMatchingAction) {

            // Get templateMatchingResult
            var TemplateMatchingResult = TemplateMatch(templateMatchingAction);

            // Get corresponding DynamicScript from templateMatchingResult
            var templateMatchingScriptTriple = TemplateMatchingConfig.TemplateMatchingResults.Where(x => x.Item1 == templateMatchingAction).First();
            DynamicScript? dynamicScript = TemplateMatchingResult.Item1 ? templateMatchingScriptTriple.Item2 : templateMatchingScriptTriple.Item3;

            // If DynamicScript is not null, invoke
            if (dynamicScript != null) {

                // Invoke corresponding script
                Logger.Log(nameof(TemplateMatching), $"Invoking dynamic script for TemplateMatchingAction {templateMatchingAction})", Logger.LoggerPriority.MEDIUM);
                
                Core.CORE_INTERRUPTED = true; // Set false immediately, else the DynamicScript won't execute
                ORCHESTRATOR_INTERRUPTED = true; // Set false after DynamicScript has finished

                dynamicScript.Invoke();

                ORCHESTRATOR_INTERRUPTED = false; // After DynamicScript is done, continue orchestrator

                // Set time when action is completed for timeout purposes
                TemplateMatchingConfig.TemplateMatchingActionEventTimes[templateMatchingAction] = DateTime.Now;
            }

            // Find corresponding action
            var templateMatchingOrchestratorModeTriple = TemplateMatchingConfig.TemplateMatchingOrchestratorModes.Where(x => x.Item1 == templateMatchingAction).First();
            OrchestratorMode? orchestratorMode = TemplateMatchingResult.Item1 ? templateMatchingOrchestratorModeTriple.Item2 : templateMatchingOrchestratorModeTriple.Item3;

            // Set orchestratorMode from thread
            return orchestratorMode;
        }

        // Runs on different thread
        public static void PollTemplateMatchingThread()
        {
            for (; ; )
            {
                foreach (TemplateMatchingAction templateMatchingAction in TemplateMatchingConfig.templateActionsInterruptingOrchestrator)
                {
                    // Poll template match, unless it has just been called. Need to define stopwatch + timeOut.
                    // Stopwatch needed per TMaction........., or set time in dictionary<Action, OldTime>, then get 
                    // Current_time - old_time > Config[action]

                    var timeBetweenEventsInMilliseconds = (DateTime.Now - TemplateMatchingConfig.TemplateMatchingActionEventTimes[templateMatchingAction]).TotalMilliseconds;
                    var templateMatchingActionTimeout = TemplateMatchingConfig.TemplateMatchingActionTimeouts[templateMatchingAction];

                    if (timeBetweenEventsInMilliseconds < templateMatchingActionTimeout) {
                        Logger.Log(nameof(Orchestrator), $"Timeout for {templateMatchingAction} has not been reached: {timeBetweenEventsInMilliseconds / templateMatchingActionTimeout}", Logger.LoggerPriority.INFO);
                        continue;
                    }

                    OrchestratorMode? polledOrchestratorMode = ExecuteTemplateMatching(templateMatchingAction);

                    if (polledOrchestratorMode != null)
                    {
                        Logger.Log(nameof(Orchestrator), $"Interrupting cycle by TemplateMatching, changing Orchestrator mode from [{orchestratorMode}] to [{(OrchestratorMode)polledOrchestratorMode}]", Logger.LoggerPriority.HIGH);
                        orchestratorMode = (OrchestratorMode)polledOrchestratorMode;

                        // Restart orchistrator mode seperately
                        sw.Restart();
                    }
                }

                // Sleep after cycle
                Logger.Log(nameof(Orchestrator), $"PollTemplateMatchingThread goes to sleep", Logger.LoggerPriority.LOW);
                Thread.Sleep(5000);
            }
        }
    }

    public enum OrchestratorMode
    { 
        MODE_ATTACK,
        MODE_ATTACK_SPECTER,
        MODE_CC,
        MODE_BUFF,
    }

    internal enum OrchestratorModeCycleStrategy
    {
        SIMPLE
    }

    internal static class OrchestratorModeCycleStrategyConfig {
        public static Dictionary<OrchestratorMode, int> cycleConfigTime { get; set; } = new Dictionary<OrchestratorMode, int>()
        {
            { OrchestratorMode.MODE_ATTACK, 1000 * 120 },
            { OrchestratorMode.MODE_ATTACK_SPECTER, 1000 * 30 },
            { OrchestratorMode.MODE_CC, 0 },
            { OrchestratorMode.MODE_BUFF, 0 },
        };

        public static Dictionary<OrchestratorMode, OrchestratorMode> cycleConfigSequence { get; set; } = new Dictionary<OrchestratorMode, OrchestratorMode>()
        {
            { OrchestratorMode.MODE_BUFF, OrchestratorMode.MODE_ATTACK },
            { OrchestratorMode.MODE_ATTACK, OrchestratorMode.MODE_CC },
            { OrchestratorMode.MODE_CC, OrchestratorMode.MODE_ATTACK },

            // TemplateMatching OrchestratorModes (one way)
            { OrchestratorMode.MODE_ATTACK_SPECTER, OrchestratorMode.MODE_BUFF },
        };
    }

    internal enum Map
    {
        CAVE_OF_REPOSE, 
        MOTTLED_FOREST_3
    }

    internal static class MapChangeChannelScripts
    {
        public static Dictionary<Map, List<ScriptItem>> mapChangeChannelScripts { get; set; } = new Dictionary<Map, List<ScriptItem>>()
        {
            { Map.CAVE_OF_REPOSE, ExitMapRightCaveOfRepose.Concat(ChangeChannel).Concat(EnterMapLeft).ToList() },
            { Map.MOTTLED_FOREST_3, PreExitMottled.Concat(ChangeChannel).ToList() },
        };
    }

    internal static class MapChangeChannelStartingDirections
    {
        public static Dictionary<Map, ScriptItemAttackType> mapChangeChannelStartingDirections { get; set; } = new Dictionary<Map, ScriptItemAttackType>()
        {
            { Map.CAVE_OF_REPOSE, ScriptItemAttackType.LEFT_TO_RIGHT },
            { Map.MOTTLED_FOREST_3, ScriptItemAttackType.RIGHT_TO_LEFT },
        };
    }
}
