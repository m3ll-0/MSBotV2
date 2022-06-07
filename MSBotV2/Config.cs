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
                // Multi-direction
                { OrchestratorMode.MODE_BUFF, OrchestratorMode.MODE_ATTACK },
                { OrchestratorMode.MODE_ATTACK, OrchestratorMode.MODE_CC },
                { OrchestratorMode.MODE_CC, OrchestratorMode.MODE_ATTACK },

                // Single-direction
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
                { TemplateMatchingAction.QUEST_COMPLETED, new DateTime(1999, 8, 11, 08, 15, 20)},
            };

            public static Dictionary<TemplateMatchingAction, int> TemplateMatchingActionTimeouts = new Dictionary<TemplateMatchingAction, int>()
            {
                { TemplateMatchingAction.SPECTER_GAUGE_FULL, 30000},
                { TemplateMatchingAction.PENALTY, 40000},
                { TemplateMatchingAction.DEATH_SCREEN, 0},
                { TemplateMatchingAction.BOSS_CURSE, 40000},
                { TemplateMatchingAction.QUEST_COMPLETED, 0},
            };

            public static List<(TemplateMatchingAction, OrchestratorMode, OrchestratorMode?)> TemplateMatchingOrchestratorModes = new List<(TemplateMatchingAction, OrchestratorMode, OrchestratorMode?)>()
            {
                new (TemplateMatchingAction.SPECTER_GAUGE_FULL, OrchestratorMode.MODE_ATTACK_SPECTER, null),
                new (TemplateMatchingAction.DEATH_SCREEN, OrchestratorMode.MODE_ATTACK, null),
                new (TemplateMatchingAction.PENALTY, OrchestratorMode.MODE_ATTACK, null),
                new (TemplateMatchingAction.BOSS_CURSE, OrchestratorMode.MODE_ATTACK, null),
                new (TemplateMatchingAction.QUEST_COMPLETED, OrchestratorMode.MODE_ATTACK, null),
            };

            // TemplateMatchingActions that interrupt the main cycle
            public static List<TemplateMatchingAction> TemplateActionsInterruptingOrchestrator = new List<TemplateMatchingAction>()
            {
                TemplateMatchingAction.QUEST_COMPLETED,
                TemplateMatchingAction.DEATH_SCREEN,
                TemplateMatchingAction.PENALTY,
                TemplateMatchingAction.BOSS_CURSE,
                TemplateMatchingAction.SPECTER_GAUGE_FULL,
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

                // Quests
                { TemplateMatchingAction.QUEST_MASTER_LYCK,TemplateMatchingMouseClickType.MOUSE_CLICK_DOUBLE},
                { TemplateMatchingAction.QUEST_200_BIGHORN_PINEDEERS,TemplateMatchingMouseClickType.NONE},
                { TemplateMatchingAction.QUEST_200_GREEN_CATFISH,TemplateMatchingMouseClickType.NONE},
                { TemplateMatchingAction.QUEST_50_LYCKS_RECIPES,TemplateMatchingMouseClickType.NONE},
                { TemplateMatchingAction.QUEST_COMPLETED,TemplateMatchingMouseClickType.NONE},

                { TemplateMatchingAction.QUEST_50_TRANQUIL_ERDAS_SAMPLES,TemplateMatchingMouseClickType.NONE},
                { TemplateMatchingAction.QUEST_50_STONE_ERDAS_SAMPLES,TemplateMatchingMouseClickType.NONE},
                { TemplateMatchingAction.QUEST_200_TRANQUIL_ERDAS,TemplateMatchingMouseClickType.NONE},
                { TemplateMatchingAction.QUEST_50_JOYFUL_ERDAS_SAMPLES,TemplateMatchingMouseClickType.NONE},
                { TemplateMatchingAction.QUEST_RONA,TemplateMatchingMouseClickType.MOUSE_CLICK_DOUBLE},
                { TemplateMatchingAction.QUEST_RONA_DIALOG,TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE},
                // Map
                { TemplateMatchingAction.MAP_SEARCH_BAR,TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE},
                { TemplateMatchingAction.MAP_SEARCH_BUTTON,TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE},
                { TemplateMatchingAction.MAP_SELECTED,TemplateMatchingMouseClickType.MOUSE_CLICK_DOUBLE},
                { TemplateMatchingAction.MAP_SEARCH_FOUND_ICON,TemplateMatchingMouseClickType.MOUSE_CLICK_DOUBLE},
                { TemplateMatchingAction.MAP_CONFIRM,TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE},
            };

            public static List<(TemplateMatchingAction, DynamicScript?, DynamicScript?)> TemplateMatchingResults = new List<(TemplateMatchingAction, DynamicScript?, DynamicScript?)>()
            {
                new (
                    TemplateMatchingAction.DEATH_SCREEN,
                     DynamicScriptBuilder.BuildNavigationDynamicScript(FinishedScripts.SearchMapDealieBobberForest1),
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
                ),
                new (
                    TemplateMatchingAction.QUEST_COMPLETED,
                    DynamicScriptBuilder.BuildQuestArcaneRiverCompletedDynamicScript(),// todo: make variable by proxy function so that it can give callback to any bot
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

                // Quests
                { TemplateMatchingAction.QUEST_COMPLETED, "needle_quest_completed.png" },

                { TemplateMatchingAction.QUEST_50_LYCKS_RECIPES, "quest_50_lycks_recipes.png" },
                { TemplateMatchingAction.QUEST_200_BIGHORN_PINEDEERS, "quest_200_bighorn_pinedeers.png" },
                { TemplateMatchingAction.QUEST_200_GREEN_CATFISH, "quest_200_green_catfish.png" },
                { TemplateMatchingAction.QUEST_MASTER_LYCK, "quest_master_lyck.png" },

                { TemplateMatchingAction.QUEST_50_TRANQUIL_ERDAS_SAMPLES, "quest_50_tranquil_erdas_samples.png" },
                { TemplateMatchingAction.QUEST_50_STONE_ERDAS_SAMPLES, "quest_50_stone_erdas_samples.png" },
                { TemplateMatchingAction.QUEST_200_TRANQUIL_ERDAS, "quest_200_tranquil_erdas.png" },
                { TemplateMatchingAction.QUEST_50_JOYFUL_ERDAS_SAMPLES, "quest_50_joyful_erdas_samples.png" },
                { TemplateMatchingAction.QUEST_RONA, "quest_rona.png" },
                { TemplateMatchingAction.QUEST_RONA_DIALOG, "quest_rona_dialog.png" },

                // Map
                { TemplateMatchingAction.MAP_SEARCH_BAR, "map_search_bar.png" },
                { TemplateMatchingAction.MAP_SEARCH_BUTTON, "map_search_button.png" },
                { TemplateMatchingAction.MAP_SELECTED, "map_selected.png" },
                { TemplateMatchingAction.MAP_SEARCH_FOUND_ICON, "map_search_found_icon.png" },
                { TemplateMatchingAction.MAP_CONFIRM, "needle_map_confirm.png" },

            };

            public static Dictionary<TemplateMatchingAction, (int, int)> TemplateMatchingActionRelativeOffset { get; set; } = new Dictionary<TemplateMatchingAction, (int, int)>()
            {
                { TemplateMatchingAction.MAP_SEARCH_BAR, (100, 0) },
                { TemplateMatchingAction.MAP_SEARCH_BUTTON, (200, 0) },
                { TemplateMatchingAction.MAP_SELECTED, (-8, -4) },
            };

            public static Dictionary<TemplateMatchingAction, double> TemplateMatchingActionThreshold { get; set; } = new Dictionary<TemplateMatchingAction, double>()
            {
                { TemplateMatchingAction.MAP_SELECTED, 0.99 },
                { TemplateMatchingAction.QUEST_COMPLETED, 0.7 },
            };
        }

        public static class MouseConfig
        {
            public static bool UseScreenScaling = false;

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
