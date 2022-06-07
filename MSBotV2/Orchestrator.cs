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
        private static Dictionary<List<ScriptItem>, ScriptItemAttackType> attackPool = CreateAttackScriptsPool();
        private static Core core = new Core();
        private static Stopwatch sw = new Stopwatch();
        private static Map currentMap = Map.MOTTLED_FOREST_3;
        private static bool ORCHESTRATOR_INTERRUPTED = false;
        public static bool ORCHESTRATOR_QUIT = false;
        public static OrchestratorType orchestratorType = OrchestratorType.MOBBING;
        public static ScriptItemAttackType currentAttackTypeMode = ScriptItemAttackType.LEFT_TO_RIGHT;

        public static void Orchestrate() {

            // Start stopwatch
            sw.Start();

            for (; ; ) {

                if (ORCHESTRATOR_QUIT) {
                    Logger.Log(nameof(Script), $"Detected [ORCHESTRATOR_QUIT], quitting Orchestrator thread.");
                    return;
                }

                if (ORCHESTRATOR_INTERRUPTED)
                {
                    Logger.Log(nameof(Script), $"Orchestrator interrupted to call DynamicScript, in idle mode");
                    Thread.Sleep(1000);
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
            int switchTime = OrchestratorConfig.CycleConfigTime[orchestratorMode];

            if (switchTime == -1) {
                return; // One time usage - returns after executing script and cycles
            }
            else if (sw.Elapsed.TotalMilliseconds > switchTime) // See if elapsed time > switch time

            {
                Logger.Log(nameof(Orchestrator), $"Normal Cycle, changing OrchestratorMode from [{orchestratorMode}] to [{OrchestratorConfig.CycleConfigSequence[orchestratorMode]}]");

                orchestratorMode = OrchestratorConfig.CycleConfigSequence[orchestratorMode];

                // Reset timer after changing modes
                sw.Restart();
            }
            else
            {
                Logger.Log(nameof(Orchestrator), $"Lapsed {sw.Elapsed.TotalMilliseconds} / {switchTime} ");
            }
        }

        private static void HandleAttackMode() {

            // Compose random attack script based on direction and run it
            Script attackScript = ScriptComposer.Compose(Helper.GetRandomAttack(currentAttackTypeMode));
            core.RunScript(attackScript);

            // Toggle attack type mode to change direction
            currentAttackTypeMode = (currentAttackTypeMode == ScriptItemAttackType.RIGHT_TO_LEFT ? ScriptItemAttackType.LEFT_TO_RIGHT : ScriptItemAttackType.RIGHT_TO_LEFT);
            Logger.Log(nameof(Orchestrator), $"Changing ScriptItemAttackType to [{currentAttackTypeMode}]");
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
            Logger.Log(nameof(Orchestrator), $"Changing ScriptItemSpecterAttackType to [{currentAttackTypeMode}]");

            // Need to handle exit spectermode somewhere
            int switchTime = OrchestratorConfig.CycleConfigTime[orchestratorMode];

            if (sw.Elapsed.TotalMilliseconds > switchTime)
            {
                Logger.Log(nameof(Orchestrator), $"Detected switchtime in [{orchestratorMode}], exiting specter mode. ");
                core.RunScript(ScriptComposer.Compose(ToggleSpecterMode));

                // Set orchestrator mode manually because buff-mode is one way
                Logger.Log(nameof(Orchestrator), $"End of SpecterMode reached, changing Orchestrator mode from [{orchestratorMode}] to [{OrchestratorConfig.CycleConfigSequence[orchestratorMode]}]");
                orchestratorMode = OrchestratorConfig.CycleConfigSequence[orchestratorMode];

                sw.Restart();
            }
        }

        private static void handleChangeChannelMode() {
            Logger.Log(nameof(Orchestrator), $"Changing channel {currentAttackTypeMode}");

            // Run the script
            core.RunScript(ScriptComposer.Compose(OrchestratorConfig.MapChangeChannelScripts[currentMap]));
            
            // Change Attack direction respectively
            currentAttackTypeMode = OrchestratorConfig.MapChangeChannelStartingDirections[currentMap];
        }

        private static void handleBuffMode()
        {
            Logger.Log(nameof(Orchestrator), $"Buffing");

            core.RunScript(ScriptComposer.Compose(Buff));

            // Need to set orchestrator mode manually because -1 in main thread will keep in loop
            orchestratorMode = OrchestratorConfig.CycleConfigSequence[orchestratorMode];
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
                Logger.Log(nameof(TemplateMatching), $"Invoking dynamic script for TemplateMatchingAction {templateMatchingAction})");
                
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
                if (ORCHESTRATOR_QUIT)
                {
                    Logger.Log(nameof(Script), $"Detected [ORCHESTRATOR_QUIT], quitting PollTemplateMatching thread.");
                    return;
                }

                foreach (TemplateMatchingAction templateMatchingAction in TemplateMatchingConfig.TemplateActionsInterruptingOrchestrator)
                {
                    // Check if quest mode is not enable and in case quest completed needle was found, skip
                    if (orchestratorType != OrchestratorType.QUEST && templateMatchingAction == TemplateMatchingAction.QUEST_COMPLETED) {
                        continue;
                    }

                    // Check if TemplateMatchingAction timeout has been expired since last event
                    var timeBetweenEventsInMilliseconds = (DateTime.Now - TemplateMatchingConfig.TemplateMatchingActionEventTimes[templateMatchingAction]).TotalMilliseconds;
                    var templateMatchingActionTimeout = TemplateMatchingConfig.TemplateMatchingActionTimeouts[templateMatchingAction];

                    if (timeBetweenEventsInMilliseconds < templateMatchingActionTimeout) {
                        Logger.Log(nameof(Orchestrator), $"Timeout for {templateMatchingAction} has not been reached: {timeBetweenEventsInMilliseconds} / {templateMatchingActionTimeout}");
                        continue;
                    }

                    OrchestratorMode? polledOrchestratorMode = ExecuteTemplateMatching(templateMatchingAction);

                    if (polledOrchestratorMode != null)
                    {
                        Logger.Log(nameof(Orchestrator), $"Interrupting cycle by TemplateMatching, changing Orchestrator mode from [{orchestratorMode}] to [{(OrchestratorMode)polledOrchestratorMode}]");
                        orchestratorMode = (OrchestratorMode)polledOrchestratorMode;

                        // Restart orchistrator mode seperately
                        sw.Restart();
                    }
                }

                // Sleep after cycle
                Logger.Log(nameof(Orchestrator), $"PollTemplateMatchingThread goes to sleep");
                Thread.Sleep(3000);
            }
        }
    }

    // Determines the cycle of the orchestrator
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

    public enum Map
    {
        CAVE_OF_REPOSE, 
        MOTTLED_FOREST_3
    }


    // Determines the type of the orchestrator, if quest, on death return to quest map, otherwise to mobbing map
    public enum OrchestratorType { 
        MOBBING,
        QUEST
    }
}
