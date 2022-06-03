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

        public static void Orchestrate() {

            // Start stopwatch
            sw.Start();

            for (; ; ) {

                if (orchestratorMode == OrchestratorMode.MODE_ATTACK)
                {
                    HandleAttackMode();
                }
                else if (orchestratorMode == OrchestratorMode.MODE_CC)
                {
                    handleChangeChannelMode();
                }
                else if (orchestratorMode == OrchestratorMode.MODE_BUFF) {
                    handleBuffMode();
                }           

                // Poll switch mode according to Cycle definition
                PollOrchestratorModeCycleStrategy();
            }
        }

        private static void HandleAttackMode() {

            // Compose random attack script based on direction and run it
            Script attackScript = ScriptComposer.Compose(GetRandomAttack());
            core.RunScript(attackScript);

            // Toggle attack type mode to change direction
            currentAttackTypeMode = (currentAttackTypeMode == ScriptItemAttackType.RIGHT_TO_LEFT ? ScriptItemAttackType.LEFT_TO_RIGHT : ScriptItemAttackType.RIGHT_TO_LEFT);
            Logger.Log(nameof(Orchestrator), $"Changing ScriptItemAttackType to [{currentAttackTypeMode}]", Logger.LoggerPriority.MEDIUM);
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

        private static List<ScriptItem>? GetRandomAttack()
        {
            // Attack scripts HAVE to be symmetric
            int relevantKeySpace = (int) (attackPool.Count * 0.5);

            var attackPoolEnumerator = attackPool
                .Where(x => x.Value == (currentAttackTypeMode == ScriptItemAttackType.RIGHT_TO_LEFT ? ScriptItemAttackType.LEFT_TO_RIGHT : ScriptItemAttackType.RIGHT_TO_LEFT))
                .GetEnumerator();

            int attackMoveCounter = 0;
            int randomAttackMove = new Random().Next(0, relevantKeySpace);

            while (attackPoolEnumerator.MoveNext())
            {
                if (attackMoveCounter++ == randomAttackMove) {
                    return attackPoolEnumerator.Current.Key;
                }
            }

            return null;
        }

        /*
         * Does TemplateMatching on a specified TemplateMatchingAction and invokes a corresponding script. Returns the corresponding OrchestratorMode that
         * will be set afterwards which might interrupt the current cycle.
         */
        private static OrchestratorMode? PollTemplateMatching(TemplateMatchingAction templateMatchingAction) {

            var TemplateMatchingResult = TemplateMatch(templateMatchingAction);

            // Find corresponding dynamic script
            var templateMatchingScriptTriple = TemplateMatchingConfig.TemplateMatchingResults.Where(x => x.Item1 == templateMatchingAction).First();
            DynamicScript dynamicScript = TemplateMatchingResult.Item1 ? templateMatchingScriptTriple.Item2 : templateMatchingScriptTriple.Item3;

            if (dynamicScript != null) {
                // Invoke corresponding script
                Logger.Log(nameof(TemplateMatching), $"Invoking dynamic script for TemplateMatchingAction {templateMatchingAction})", Logger.LoggerPriority.MEDIUM);
                dynamicScript.Invoke();
            }

            // Find corresponding action
            var templateMatchingOrchestratorModeTriple = TemplateMatchingConfig.TemplateMatchingOrchestratorModes.Where(x => x.Item1 == templateMatchingAction).First();
            OrchestratorMode? orchestratorMode = TemplateMatchingResult.Item1 ? templateMatchingOrchestratorModeTriple.Item2 : templateMatchingOrchestratorModeTriple.Item3;

            return orchestratorMode;
        }

        private static void PollOrchestratorModeCycleStrategy()
        {
            switch (orchestratorModeCycleStrategy) {

                case OrchestratorModeCycleStrategy.SIMPLE:

                    // todo: put in config
                    // poll template matching for each defined action
                    // use predefined list for actions to test during this phase
                    // potential problem, only the latest o-mode will be set afterwards because it is sequential, priority?
                    // Could make a priorityqueue of items found and get best one
                    foreach (TemplateMatching.TemplateMatchingAction templateMatchingAction in TemplateMatchingConfig.templateActionsInterruptingOrchestrator) {
                        OrchestratorMode? polledOrchestratorMode = PollTemplateMatching(templateMatchingAction);

                        if (polledOrchestratorMode != null)
                        {
                            Logger.Log(nameof(Orchestrator), $"Interrupting cycle by TemplateMatching, changing Orchestrator mode from [{orchestratorMode}] to [{(OrchestratorMode)polledOrchestratorMode}]", Logger.LoggerPriority.HIGH);

                            orchestratorMode = (OrchestratorMode)polledOrchestratorMode;
                            return;
                        }
                    }

                    int switchTime = OrchestratorModeCycleStrategyConfig.cycleConfigTime[orchestratorMode];
                    if (sw.Elapsed.TotalMilliseconds > switchTime)
                    {
                        Logger.Log(nameof(Orchestrator), $"Normal Cycle, changing OrchestratorMode from [{orchestratorMode}] to [{OrchestratorModeCycleStrategyConfig.cycleConfigSequence[orchestratorMode]}]", Logger.LoggerPriority.MEDIUM);

                        orchestratorMode = OrchestratorModeCycleStrategyConfig.cycleConfigSequence[orchestratorMode];
                        sw.Restart();
                    }
                    else {
                        Logger.Log(nameof(Orchestrator), $"Lapsed {sw.Elapsed.TotalMilliseconds} / {switchTime} ", Logger.LoggerPriority.LOW);
                    }

                    break;
            }
        }
    }

    public enum OrchestratorMode
    { 
        MODE_ATTACK,
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
            { OrchestratorMode.MODE_CC, 0 },
            { OrchestratorMode.MODE_BUFF, 0 },
        };

        public static Dictionary<OrchestratorMode, OrchestratorMode> cycleConfigSequence { get; set; } = new Dictionary<OrchestratorMode, OrchestratorMode>()
        {
            { OrchestratorMode.MODE_BUFF, OrchestratorMode.MODE_ATTACK },
            { OrchestratorMode.MODE_ATTACK, OrchestratorMode.MODE_CC },
            { OrchestratorMode.MODE_CC, OrchestratorMode.MODE_BUFF },
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
