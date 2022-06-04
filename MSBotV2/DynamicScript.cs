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

            if (TemplateMatchingAction == null)
            {
                return;
            }

            var templateMatchingResult = TemplateMatch((TemplateMatchingAction)TemplateMatchingAction);

            switch (templateMatchingResult.Item1) {
                case true:
                    if (DynamicScriptNodeTrue != null) {
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
