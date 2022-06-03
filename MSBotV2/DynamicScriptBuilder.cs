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

            DynamicScript dynamicScriptOpenPet = new DynamicScript(TemplateMatchingAction.INVENTORY_PET, null, dynamicScriptReturnToMap, null);
           
            DynamicScript dynamicScriptOpenInventoryCashRoot = new DynamicScript(
                TemplateMatchingAction.INVENTORY_CASH,
                FinishedScripts.OpenInventory,
                dynamicScriptOpenPet, 
                null);

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
    }

    public enum DynamicScriptType { 
        OPEN_PET
    }

    public static class DynamicScriptConfig {
        public static Dictionary<DynamicScriptType, DynamicScript> DynamicScripts = new Dictionary<DynamicScriptType, DynamicScript>()
        {
            { DynamicScriptType.OPEN_PET, DynamicScriptBuilder.BuildOpenPetDynamicScript() }
        };
    }
}
