using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSBotV2.FinishedScripts;
using static MSBotV2.TemplateMatching;

namespace MSBotV2
{
    public static class Config
    {

        public static class OrchestratorConfig
        {
            public static Dictionary<OrchestratorMode, int> CycleConfigTime { get; set; } = new Dictionary<OrchestratorMode, int>()
            {
                { OrchestratorMode.MODE_ATTACK, 1000 * 120 },
                { OrchestratorMode.MODE_ATTACK_SPECTER, 1000 * 29 },
                { OrchestratorMode.MODE_CC, 0 },
                { OrchestratorMode.MODE_BUFF, 0 },
            };

            public static Dictionary<OrchestratorMode, OrchestratorMode> CycleConfigSequence { get; set; } = new Dictionary<OrchestratorMode, OrchestratorMode>()
            {
                { OrchestratorMode.MODE_BUFF, OrchestratorMode.MODE_ATTACK },
                { OrchestratorMode.MODE_ATTACK, OrchestratorMode.MODE_CC },
                { OrchestratorMode.MODE_CC, OrchestratorMode.MODE_ATTACK },

                // TemplateMatching OrchestratorModes (one way)
                { OrchestratorMode.MODE_ATTACK_SPECTER, OrchestratorMode.MODE_BUFF },
            };

            public static Dictionary<Map, ScriptItemAttackType> MapChangeChannelStartingDirections { get; set; } = new Dictionary<Map, ScriptItemAttackType>()
            {
                { Map.CAVE_OF_REPOSE, ScriptItemAttackType.LEFT_TO_RIGHT },
                { Map.MOTTLED_FOREST_3, ScriptItemAttackType.RIGHT_TO_LEFT },
            };

            public static Dictionary<Map, List<ScriptItem>> MapChangeChannelScripts { get; set; } = new Dictionary<Map, List<ScriptItem>>()
            {
                { Map.CAVE_OF_REPOSE, ExitMapRightCaveOfRepose.Concat(ChangeChannel).Concat(EnterMapLeft).ToList() },
                { Map.MOTTLED_FOREST_3, PreExitMottled.Concat(ChangeChannel).ToList() },
            };
        }

        public static class TemplateMatchingConfig
        {
            public static string ImageDirectory = "../../../img/";

            public static Dictionary<TemplateMatchingAction, DateTime> TemplateMatchingActionEventTimes = new Dictionary<TemplateMatchingAction, DateTime>()
            {
                { TemplateMatchingAction.SPECTER_GAUGE_FULL, new DateTime(1999, 8, 11, 08, 15, 20)},
                { TemplateMatchingAction.PENALTY, new DateTime(1999, 8, 11, 08, 15, 20)},
                { TemplateMatchingAction.DEATH_SCREEN, new DateTime(1999, 8, 11, 08, 15, 20)},
                { TemplateMatchingAction.BOSS_CURSE, new DateTime(1999, 8, 11, 08, 15, 20)},
            };

            public static Dictionary<TemplateMatchingAction, int> TemplateMatchingActionTimeouts = new Dictionary<TemplateMatchingAction, int>()
            {
                { TemplateMatchingAction.SPECTER_GAUGE_FULL, 30000},
                { TemplateMatchingAction.PENALTY, 40000},
                { TemplateMatchingAction.DEATH_SCREEN, 0},
                { TemplateMatchingAction.BOSS_CURSE, 40000},
            };

            public static List<(TemplateMatchingAction, OrchestratorMode, OrchestratorMode?)> TemplateMatchingOrchestratorModes = new List<(TemplateMatchingAction, OrchestratorMode, OrchestratorMode?)>()
            {
                new (TemplateMatchingAction.SPECTER_GAUGE_FULL, OrchestratorMode.MODE_ATTACK_SPECTER, null),
                new (TemplateMatchingAction.DEATH_SCREEN, OrchestratorMode.MODE_ATTACK, null),
                new (TemplateMatchingAction.PENALTY, OrchestratorMode.MODE_ATTACK, null),
                new (TemplateMatchingAction.BOSS_CURSE, OrchestratorMode.MODE_ATTACK, null),
            };

            // TemplateMatchingActions that interrupt the main cycle
            public static List<TemplateMatchingAction> TemplateActionsInterruptingOrchestrator = new List<TemplateMatchingAction>()
            {
                TemplateMatchingAction.SPECTER_GAUGE_FULL,
                TemplateMatchingAction.DEATH_SCREEN,
                TemplateMatchingAction.PENALTY,
                TemplateMatchingAction.BOSS_CURSE,
            };

            public static Dictionary<TemplateMatchingAction, TemplateMatchingMouseClickType> TemplateMatchingMouseClicks = new Dictionary<TemplateMatchingAction, TemplateMatchingMouseClickType>()
            {
                { TemplateMatchingAction.DEATH_SCREEN, TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE },
                { TemplateMatchingAction.PENALTY, TemplateMatchingMouseClickType.NONE },
                { TemplateMatchingAction.INVENTORY_CASH, TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE},
                { TemplateMatchingAction.INVENTORY_PET, TemplateMatchingMouseClickType.MOUSE_CLICK_DOUBLE},
                { TemplateMatchingAction.SPECTER_GAUGE_FULL, TemplateMatchingMouseClickType.NONE},
                { TemplateMatchingAction.BOSS_CURSE,TemplateMatchingMouseClickType.NONE},
                { TemplateMatchingAction.INVENTORY_HYPER_ROCK,TemplateMatchingMouseClickType.MOUSE_CLICK_DOUBLE},
                { TemplateMatchingAction.HYPER_ROCK_MAP,TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE},
                { TemplateMatchingAction.HYPER_ROCK_MOVE_BUTTON,TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE},
                { TemplateMatchingAction.HYPER_ROCK_CONFIRM_BUTTON,TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE},
            };

            public static List<(TemplateMatchingAction, DynamicScript?, DynamicScript?)> TemplateMatchingResults = new List<(TemplateMatchingAction, DynamicScript?, DynamicScript?)>()
            {
                new (
                    TemplateMatchingAction.DEATH_SCREEN,
                     DynamicScriptBuilder.BuildMoveToTrainingMapAfterDeathDynamicScript(),
                    null
                    ),
                new (
                    TemplateMatchingAction.PENALTY,
                    DynamicScriptBuilder.BuildChangeChannelDynamicScript(),
                    null
                    ),
                new (
                    TemplateMatchingAction.SPECTER_GAUGE_FULL,
                    DynamicScriptBuilder.BuildSpecterGaugeFullDynamicScript(),
                    null
                    ),
                    new (
                    TemplateMatchingAction.BOSS_CURSE,
                    DynamicScriptBuilder.BuildChangeChannelDynamicScript(),
                    null
                    )
            };

            public static Dictionary<TemplateMatchingAction, string> TemplateMatchingActionFiles { get; set; } = new Dictionary<TemplateMatchingAction, string>()
            {
                { TemplateMatchingAction.DEATH_SCREEN, "needle_deathscreen.png" },
                { TemplateMatchingAction.PENALTY, "needle_penalty.png" },
                { TemplateMatchingAction.INVENTORY_CASH, "needle_cash.png" },
                { TemplateMatchingAction.INVENTORY_PET, "needle_pet.png" },
                { TemplateMatchingAction.SPECTER_GAUGE_FULL, "needle_specter_gauge_full.png" },
                { TemplateMatchingAction.BOSS_CURSE, "needle_boss_curse.png" },
                { TemplateMatchingAction.INVENTORY_HYPER_ROCK, "needle_hyper_rock.png" },
                { TemplateMatchingAction.HYPER_ROCK_MAP, "needle_hyper_rock_map.png" },
                { TemplateMatchingAction.HYPER_ROCK_MOVE_BUTTON, "needle_hyper_rock_move.png" },
                { TemplateMatchingAction.HYPER_ROCK_CONFIRM_BUTTON, "needle_hyper_rock_confirm.png" },
            };
        }

        public static class MouseConfig
        {
            public static bool UseScreenScaling = true;

            public static double x_coordinate_scaling_factor = 0.793650794;
            public static double y_coordinate_scaling_factor = 0.8;

            public static int x_coordinate_result_padding = 15;
            public static int y_coordinate_result_padding = 10;
        }

        public static class LogConfig {
            public static bool LogToConsole = true;
            public static bool LogToFile = true;
        }
    }
}
