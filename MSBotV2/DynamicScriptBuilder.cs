using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSBotV2.TemplateMatching;

namespace MSBotV2
{
    public class DynamicScriptBuilder
    {
        public static DynamicScript BuildChangeChannelDynamicScript()
        {
            DynamicScript dynamicScriptChangeChannelRoot = new DynamicScript(
                null,
                null,
                FinishedScripts.ChangeChannel,
                null,
                null
                );
            
            return dynamicScriptChangeChannelRoot;
        }

        public static DynamicScript BuildSpecterGaugeFullDynamicScript()
        {
            DynamicScript dynamicScriptChangeChannelRoot = new DynamicScript(
                null,
                null,
                FinishedScripts.ToggleSpecterMode,
                null,
                null
                );

            return dynamicScriptChangeChannelRoot;
        }

        public static void DynamicScriptHelperChangeDirectionFunction() {
            Orchestrator.currentAttackTypeMode = (Orchestrator.currentAttackTypeMode == FinishedScripts.ScriptItemAttackType.RIGHT_TO_LEFT ? FinishedScripts.ScriptItemAttackType.LEFT_TO_RIGHT : FinishedScripts.ScriptItemAttackType.RIGHT_TO_LEFT);
        }

        public static DynamicScript BuildMoveToTrainingMapFailsafeDynamicScript()
        {
            // Get orchestrator type, go to designated map based on type. If questing, get from questingbot and run changemapservice, probably change implementation to always do that.
            // if not, normal script

            // Close all windows
            DynamicScript dynamicScriptCloseAllWindows = new DynamicScript(
                null,
                null,
                FinishedScripts.CloseInventory,
                null,
                null
                );

            // Click confirm button
            DynamicScript dynamicScriptClickConfirmButton = new DynamicScript(
                null,
                TemplateMatchingAction.HYPER_ROCK_CONFIRM_BUTTON,
                FinishedScripts.PauseLong,
                dynamicScriptCloseAllWindows,
                null
                );

            // Click move button
            DynamicScript dynamicScriptClickMoveButton = new DynamicScript(
                null,
                TemplateMatchingAction.HYPER_ROCK_MOVE_BUTTON,
                FinishedScripts.PauseLong,
                dynamicScriptClickConfirmButton,
                null
                );

            // Click on map
            DynamicScript dynamicScriptClickMap = new DynamicScript(
                null,
                TemplateMatchingAction.HYPER_ROCK_MAP,
                FinishedScripts.PauseLong,
                dynamicScriptClickMoveButton,
                null
                );

            // Click hyper rock
            DynamicScript dynamicScriptOpenHyperRock = new DynamicScript(
                null,
                TemplateMatchingAction.INVENTORY_HYPER_ROCK,
                FinishedScripts.PauseLong,
                dynamicScriptClickMap,
                null
                );

            // Click pet
            DynamicScript dynamicScriptOpenPet = new DynamicScript(
                null,
                TemplateMatchingAction.INVENTORY_PET,
                FinishedScripts.PauseLong,
                dynamicScriptOpenHyperRock,
                null
                );

            // Open inventory
            DynamicScript dynamicScriptOpenInventoryCash = new DynamicScript(
                null,
                TemplateMatchingAction.INVENTORY_CASH,
                FinishedScripts.PauseLong,
                dynamicScriptOpenPet,
                null
                );

            // Sleep after death (root)
            DynamicScript dynamicScriptSleepAfterDeathRoot = new DynamicScript(
                null,
                null,
                FinishedScripts.PauseAfterDeathAndOpenInventory,
                dynamicScriptOpenInventoryCash,
                null
                );

            return dynamicScriptSleepAfterDeathRoot;
        }

        public static DynamicScript BuildStartChuChuQuestStartingDialog()
        {
            // Click next by using enter command, quests are listed after this
            DynamicScript dynamicScriptNext2 = new DynamicScript(
                null,
                null,
                FinishedScripts.Return,
                null,
                null
                );

            // Click next by using enter command
            DynamicScript dynamicScriptNext1 = new DynamicScript(
                null,
                null,
                FinishedScripts.Return,
                dynamicScriptNext2,
                null
                );

            // Open dialog
            DynamicScript dynamicScriptOpenDialogRoot = new DynamicScript(
                null,
                TemplateMatchingAction.QUEST_MASTER_LYCK,
                null,
                dynamicScriptNext1,
                null
                );

            return dynamicScriptOpenDialogRoot;
        }

        public static DynamicScript BuildFinishChuChuQuestStartingDialog()
        {
            // Move to confirm button and hit enter
            DynamicScript dynamicScriptNext = new DynamicScript(
                null,
                null,
                FinishedScripts.ConfirmQuest,
                null,
                null
                );

            // Move to confirm button and hit enter
            DynamicScript dynamicScriptConfirmRoot = new DynamicScript(
                null,
                null,
                FinishedScripts.ConfirmQuest,
                dynamicScriptNext,
                null
                );

            return dynamicScriptConfirmRoot;
        }

        public static DynamicScript BuildQuestChuChuCompletedDynamicScript()
        {
            // Tell ChuChuQuestBot that the quest has been completed

            // Placeholder for questcompleted callback
            DynamicScript dynamicScriptPlaceholderRoot = new DynamicScript(
                ChuChuQuestBot.QuestChuChuCompletedCallback,
                null,
                null,
                null,
                null
                );

            return dynamicScriptPlaceholderRoot;
        }

        public static DynamicScript BuildQuestArcaneRiverCompletedDynamicScript()
        {
            // Placeholder for questcompleted callback
            DynamicScript dynamicScriptPlaceholderRoot = new DynamicScript(
                ArcaneRiverQuestBot.QuestArcaneRiverCompletedCallback,
                null,
                null,
                null,
                null
                );

            return dynamicScriptPlaceholderRoot;
        }

        public static DynamicScript BuildNavigationDynamicScript(List<ScriptItem> scriptItems)
        {
            //// Click okay button
            DynamicScript dynamicScriptConfirm = new DynamicScript(
                null,
                TemplateMatchingAction.MAP_CONFIRM,
                FinishedScripts.PauseLong,
                null,
                BuildMoveToTrainingMapFailsafeDynamicScript()
                );

            // Detect blinking icon and double click it
            DynamicScript dynamicScriptClickMap = new DynamicScript(
                null,
                TemplateMatchingAction.MAP_SELECTED,
                FinishedScripts.PauseLong,
                dynamicScriptConfirm,
                BuildMoveToTrainingMapFailsafeDynamicScript()
                );

            // Click first item
            DynamicScript dynamicScriptClickMapSearchBar = new DynamicScript(
                null,
                TemplateMatchingAction.MAP_SEARCH_FOUND_ICON,
                FinishedScripts.PauseLong,
                dynamicScriptClickMap,
                BuildMoveToTrainingMapFailsafeDynamicScript()
                );

            // Click search
            DynamicScript dynamicScriptClickSearch = new DynamicScript(
                null,
                TemplateMatchingAction.MAP_SEARCH_BUTTON,
                FinishedScripts.PauseLong,
                dynamicScriptClickMapSearchBar,
                BuildMoveToTrainingMapFailsafeDynamicScript()
                );

            // Type in searchbar using script
            DynamicScript dynamicScriptTypeMapname = new DynamicScript(
                null,
                null,
                scriptItems,
                dynamicScriptClickSearch,
                BuildMoveToTrainingMapFailsafeDynamicScript()
                );

            // Click searchbar
            DynamicScript dynamicScriptClickSearchbar = new DynamicScript(
                null,
                TemplateMatchingAction.MAP_SEARCH_BAR,
                FinishedScripts.PauseLong,
                dynamicScriptTypeMapname,
                BuildMoveToTrainingMapFailsafeDynamicScript()
                );

            // Open the map
            DynamicScript dynamicScriptOpenMap = new DynamicScript(
                null,
                null,
                FinishedScripts.OpenMap,
                dynamicScriptClickSearchbar,
                BuildMoveToTrainingMapFailsafeDynamicScript()
                );

            // Sleep after death
            DynamicScript dynamicScriptPauseAfterDeathRoot = new DynamicScript(
                null,
                null,
                FinishedScripts.PauseAfterDeath,
                dynamicScriptOpenMap,
                BuildMoveToTrainingMapFailsafeDynamicScript()
                );

            return dynamicScriptPauseAfterDeathRoot;
        }


        public static DynamicScript BuildStartArcaneRiverQuestStartingDialog()
        {
            DynamicScript dynamicScriptNext3 = new DynamicScript(
                null,
                null,
                FinishedScripts.PauseLong,
                null,
                null
                );

            // Click next by using enter command, quests are listed after this
            DynamicScript dynamicScriptNext2 = new DynamicScript(
                null,
                TemplateMatchingAction.QUEST_RONA_DIALOG,
                FinishedScripts.PauseLong,
                dynamicScriptNext3,
                null
                );

            // Click next by using enter command
            DynamicScript dynamicScriptNext1 = new DynamicScript(
                null,
                null,
                FinishedScripts.Return,
                dynamicScriptNext2,
                null
                );

            // Open dialog
            DynamicScript dynamicScriptOpenDialogRoot = new DynamicScript(
                null,
                TemplateMatchingAction.QUEST_RONA,
                FinishedScripts.PauseLong,
                dynamicScriptNext1,
                null
                );

            return dynamicScriptOpenDialogRoot;
        }

        public static DynamicScript BuildFinishArcaneRiverQuestStartingDialog()
        {
            // Move to confirm button and hit enter
            DynamicScript dynamicScriptNext = new DynamicScript(
                null,
                null,
                FinishedScripts.PauseLong.Concat(FinishedScripts.Return).ToList(),
                null,
                null
                );

            // Move to confirm button and hit enter
            DynamicScript dynamicScriptConfirmRoot = new DynamicScript(
                null,
                null,
                FinishedScripts.ConfirmQuest,
                dynamicScriptNext,
                null
                );

            return dynamicScriptConfirmRoot;
        }

        public static DynamicScript BuildRuneDetectionDynamicScript()
        {
            // Helper function for dynamicScript
            static void RuneSolverBotActivator()
            {
                // Activate RuneSolverBot
                RuneSolverBot runeSolverBot = new RuneSolverBot();
                RuneSolverResult runeSolverResult = runeSolverBot.Run();

                // Handle RuneSolverBot result
                switch (runeSolverResult) {
                    case RuneSolverResult.SUCCESS:
                        break;

                    default:
                        // Change channel is rune activation failed
                        BuildChangeChannelDynamicScript().Invoke();
                        break;
                }
            }   

            // Rune detection script
            // If rune timeout is NOT detected
            // Run rune activation bot
            DynamicScript dynamicScriptActivateRuneSolverBot = new DynamicScript(
                RuneSolverBotActivator,
                null,
                null,
                null,
                null
                );

            DynamicScript dynamicScriptCheckTimeout2 = new DynamicScript(
                null,
                TemplateMatchingAction.RUNE_TIMEOUT2,
                null,
                null,
                dynamicScriptActivateRuneSolverBot
    );

            DynamicScript dynamicScriptPlaceholderRoot = new DynamicScript(
                null,
                TemplateMatchingAction.RUNE_TIMEOUT,
                null,
                null,
                dynamicScriptCheckTimeout2
                );

            return dynamicScriptPlaceholderRoot;
        }



    }

}
