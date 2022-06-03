using MSBotV2.Models.Key;
using MSBotV2.PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public class ScriptComposer
    {

        public static void run(Script script) {
            // Core
            Core core = new Core();
            core.RunScript(script);
        }

        /**
         * Composes list of scriptItems to Script
         */
        public static Script Compose(List<ScriptItem> scriptItems) {

            IComparer<ParallelEvent> parallelEventComparer = new ParallelEventComparer();
            PriorityQueue<ParallelEvent> priorityQueueEvents = new PriorityQueue<ParallelEvent>(parallelEventComparer);

            scriptItems.ForEach((scriptItem) =>
            {
                IComparer<ParallelCommand> parallelCommandComparer = new ParallelCommandComparer();
                PriorityQueue<ParallelCommand> priorityQueueCommands = new PriorityQueue<ParallelCommand>(parallelCommandComparer);
                
                // Invoke function and iterate
                scriptItem.ParallelCommandFunction.Invoke().ForEach((parallelCommand) =>
                {
                    priorityQueueCommands.Push(parallelCommand);
                });

                ParallelEvent parallelEvent = new ParallelEvent(scriptItems.IndexOf(scriptItem), priorityQueueCommands);
                priorityQueueEvents.Push(parallelEvent);
            });

            return new Script(priorityQueueEvents);
        }
    }
}
