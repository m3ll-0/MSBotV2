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
            public static List<(TemplateMatching.TemplateMatchingAction, OrchestratorMode, OrchestratorMode?)> TemplateMatchingOrchestratorModes = new List<(TemplateMatching.TemplateMatchingAction, OrchestratorMode, OrchestratorMode?)>()
            {
                new (TemplateMatching.TemplateMatchingAction.DEATH_SCREEN, OrchestratorMode.MODE_ATTACK, null),
                new (TemplateMatching.TemplateMatchingAction.PENALTY, OrchestratorMode.MODE_ATTACK, null)
            };

            public static List<TemplateMatching.TemplateMatchingAction> templateActionsInterruptingOrchestrator = new List<TemplateMatching.TemplateMatchingAction>()
            {
                TemplateMatching.TemplateMatchingAction.DEATH_SCREEN,
                TemplateMatching.TemplateMatchingAction.PENALTY
            };

            public static Dictionary<TemplateMatchingAction, TemplateMatchingMouseClickType> TemplateMatchingMouseClicks = new Dictionary<TemplateMatchingAction, TemplateMatchingMouseClickType>()
            {
                { TemplateMatchingAction.DEATH_SCREEN, TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE },
                { TemplateMatchingAction.PENALTY, TemplateMatchingMouseClickType.NONE },
                { TemplateMatchingAction.INVENTORY_CASH, TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE},
                { TemplateMatchingAction.INVENTORY_PET, TemplateMatchingMouseClickType.MOUSE_CLICK_DOUBLE},
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
                    )
            };

            public static Dictionary<TemplateMatchingAction, string> templateMatchingActionFiles { get; set; } = new Dictionary<TemplateMatchingAction, string>()
            {
                { TemplateMatchingAction.DEATH_SCREEN, "needle_deathscreen.png" },
                { TemplateMatchingAction.PENALTY, "needle_penalty.png" },
                { TemplateMatchingAction.INVENTORY_CASH, "needle_cash.png" },
                { TemplateMatchingAction.INVENTORY_PET, "needle_pet.png" }, 
            };
        }
    }
}
