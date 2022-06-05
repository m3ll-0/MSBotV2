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
        public static DynamicScript BuildOpenPetDynamicScript() {

   
            // always return to map

            DynamicScript dynamicScriptReturnToMap = new DynamicScript(
                null,
                FinishedScripts.CloseInventory
                    .Concat(FinishedScripts.NavigateChuChuToFiveColorHillPath)
                    .Concat(FinishedScripts.NavigateFiveColorHillPathToMottledForest1)
                    .Concat(FinishedScripts.NavigateMottledForest1ToMottledForest2)
                    .Concat(FinishedScripts.NavigateMottledForest2ToMottledForest3)
                    .ToList(),
                    null, 
                    null
                    );

            DynamicScript dynamicScriptOpenPet = new DynamicScript(
                TemplateMatchingAction.INVENTORY_PET,
                FinishedScripts.OpenPet,
                dynamicScriptReturnToMap,
                dynamicScriptReturnToMap
                );
           
            DynamicScript dynamicScriptOpenInventoryCashRoot = new DynamicScript(
                TemplateMatchingAction.INVENTORY_CASH,
                FinishedScripts.OpenInventory,
                dynamicScriptOpenPet,
                dynamicScriptReturnToMap
                );

            return dynamicScriptOpenInventoryCashRoot;
        }

        public static DynamicScript BuildChangeChannelDynamicScript()
        {
            DynamicScript dynamicScriptChangeChannelRoot = new DynamicScript(
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
                FinishedScripts.ToggleSpecterMode,
                null,
                null
                );

            return dynamicScriptChangeChannelRoot;
        }

        public static DynamicScript BuildMoveToTrainingMapAfterDeathDynamicScript()
        {
            ///// FAILSAFE
            DynamicScript dynamicScriptReturnToMapOnFoot = new DynamicScript(
            null,
            FinishedScripts.CloseInventory
                .Concat(FinishedScripts.NavigateChuChuToFiveColorHillPath)
                .Concat(FinishedScripts.NavigateFiveColorHillPathToMottledForest1)
                .Concat(FinishedScripts.NavigateMottledForest1ToMottledForest2)
                .Concat(FinishedScripts.NavigateMottledForest2ToMottledForest3)
                .ToList(),
                null,
                null
                );

            ////// END

            // Close all windows
            DynamicScript dynamicScriptCloseAllWindows = new DynamicScript(
                null,
                FinishedScripts.CloseInventory,
                null,
                null
                );

            // Click confirm button
            DynamicScript dynamicScriptClickConfirmButton = new DynamicScript(
                TemplateMatchingAction.HYPER_ROCK_CONFIRM_BUTTON,
                FinishedScripts.PauseLong,
                dynamicScriptCloseAllWindows,
                dynamicScriptReturnToMapOnFoot
                );

            // Click move button
            DynamicScript dynamicScriptClickMoveButton = new DynamicScript(
                TemplateMatchingAction.HYPER_ROCK_MOVE_BUTTON,
                FinishedScripts.PauseLong,
                dynamicScriptClickConfirmButton,
                dynamicScriptReturnToMapOnFoot
                );

            // Click on map
            DynamicScript dynamicScriptClickMap = new DynamicScript(
                TemplateMatchingAction.HYPER_ROCK_MAP,
                FinishedScripts.PauseLong,
                dynamicScriptClickMoveButton,
                dynamicScriptReturnToMapOnFoot
                );

            // Click hyper rock
            DynamicScript dynamicScriptOpenHyperRock = new DynamicScript(
                TemplateMatchingAction.INVENTORY_HYPER_ROCK,
                FinishedScripts.PauseLong,
                dynamicScriptClickMap,
                dynamicScriptReturnToMapOnFoot
                );

            // Click pet
            DynamicScript dynamicScriptOpenPet = new DynamicScript(
                TemplateMatchingAction.INVENTORY_PET,
                FinishedScripts.PauseLong,
                dynamicScriptOpenHyperRock,
                dynamicScriptReturnToMapOnFoot
                );

            // Open inventory
            DynamicScript dynamicScriptOpenInventoryCash = new DynamicScript(
                TemplateMatchingAction.INVENTORY_CASH,
                FinishedScripts.PauseLong,
                dynamicScriptOpenPet,
                dynamicScriptReturnToMapOnFoot
                );

            // Sleep after death (root)
            DynamicScript dynamicScriptSleepAfterDeathRoot = new DynamicScript(
                null,
                FinishedScripts.PauseAfterDeathAndOpenInventory,
                dynamicScriptOpenInventoryCash,
                dynamicScriptReturnToMapOnFoot
                );

            return dynamicScriptSleepAfterDeathRoot;
        }
    }
}
