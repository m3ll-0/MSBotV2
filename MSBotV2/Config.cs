using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSBotV2.TemplateMatching;

namespace MSBotV2
{
    public static class Config
    {
        public static class TemplateMatchingConfig
        {
            public static List<(TemplateMatchingAction, OrchestratorMode, OrchestratorMode?)> TemplateMatchingOrchestratorModes = new List<(TemplateMatchingAction, OrchestratorMode, OrchestratorMode?)>()
            {
                new (TemplateMatchingAction.DEATH_SCREEN, OrchestratorMode.MODE_ATTACK, null),
                new (TemplateMatchingAction.PENALTY, OrchestratorMode.MODE_ATTACK, null),
                new (TemplateMatchingAction.SPECTER_GAUGE_FULL, OrchestratorMode.MODE_ATTACK_SPECTER, null),
            };

            // TemplateMatchingActions that interrupt the main cycle
            public static List<TemplateMatchingAction> templateActionsInterruptingOrchestrator = new List<TemplateMatchingAction>()
            {
                TemplateMatchingAction.DEATH_SCREEN,
                TemplateMatchingAction.PENALTY,
                TemplateMatchingAction.SPECTER_GAUGE_FULL
            };

            public static Dictionary<TemplateMatchingAction, TemplateMatchingMouseClickType> TemplateMatchingMouseClicks = new Dictionary<TemplateMatchingAction, TemplateMatchingMouseClickType>()
            {
                { TemplateMatchingAction.DEATH_SCREEN, TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE },
                { TemplateMatchingAction.PENALTY, TemplateMatchingMouseClickType.NONE },
                { TemplateMatchingAction.INVENTORY_CASH, TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE},
                { TemplateMatchingAction.INVENTORY_PET, TemplateMatchingMouseClickType.MOUSE_CLICK_DOUBLE},
                { TemplateMatchingAction.SPECTER_GAUGE_FULL, TemplateMatchingMouseClickType.NONE},
            };

            public static List<(TemplateMatchingAction, DynamicScript?, DynamicScript?)> TemplateMatchingResults = new List<(TemplateMatchingAction, DynamicScript?, DynamicScript?)>()
            {
                new (
                    TemplateMatchingAction.DEATH_SCREEN,
                     DynamicScriptBuilder.BuildOpenPetDynamicScript(),
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
                    )
            };

            public static Dictionary<TemplateMatchingAction, string> templateMatchingActionFiles { get; set; } = new Dictionary<TemplateMatchingAction, string>()
            {
                { TemplateMatchingAction.DEATH_SCREEN, "needle_deathscreen.png" },
                { TemplateMatchingAction.PENALTY, "needle_penalty.png" },
                { TemplateMatchingAction.INVENTORY_CASH, "needle_cash.png" },
                { TemplateMatchingAction.INVENTORY_PET, "needle_pet.png" },
                { TemplateMatchingAction.SPECTER_GAUGE_FULL, "needle_specter_gauge_full.png" },
            };
        }
    }
}
