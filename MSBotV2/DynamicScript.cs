using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSBotV2.TemplateMatching;

namespace MSBotV2
{
    public class DynamicScript
    {
        public DynamicScript(TemplateMatching.TemplateMatchingAction? templateMatchingAction, List<ScriptItem>? scriptItems, DynamicScript? dynamicScriptNodeTrue, DynamicScript? dynamicScriptNodeFalse)
        {
            ScriptItems = scriptItems;
            TemplateMatchingAction = templateMatchingAction;
            DynamicScriptNodeTrue = dynamicScriptNodeTrue;
            DynamicScriptNodeFalse = dynamicScriptNodeFalse;
        }

        public TemplateMatchingAction? TemplateMatchingAction { get; private set; }

        public List<ScriptItem>? ScriptItems { get; private set; }

        public DynamicScript? DynamicScriptNodeTrue { get; private set; }

        public DynamicScript? DynamicScriptNodeFalse { get; private set; }

        public void Invoke()
        {
            if (ScriptItems != null) {
                new Core().RunDynamicScript(ScriptComposer.Compose(ScriptItems));
            }

            // If TemplateMatchingAction is null and if next node is not set, return.
            // But if next node is set, invoke true node as there is no validation being done.
            // This allows for flexible Dynamic scripts
            if (TemplateMatchingAction == null && DynamicScriptNodeTrue == null)
            {
                return;
            }
            else if (TemplateMatchingAction == null && DynamicScriptNodeTrue != null) // Invoke true regardless of result
            {
                DynamicScriptNodeTrue.Invoke();
            }
            else if (TemplateMatchingAction != null) // Invoke next based on result
            {
                var templateMatchingResult = TemplateMatch((TemplateMatchingAction)TemplateMatchingAction);

                switch (templateMatchingResult.Item1)
                {
                    case true:
                        if (DynamicScriptNodeTrue != null)
                        {
                            DynamicScriptNodeTrue.Invoke();
                        }
                        break;

                    case false:
                        if (DynamicScriptNodeFalse != null)
                        {
                            DynamicScriptNodeFalse.Invoke();
                        }
                        break;
                }
            }
        }

    }
}
