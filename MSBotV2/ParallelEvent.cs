using MSBotV2.PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public class ParallelEvent : IIndexedObject
    {
        public int Index { get; set; }
            
        public int Priority { get; private set; }
        public PriorityQueue<ParallelCommand> ParallelCommands { get; private set; }

        public ParallelEvent(int priority, PriorityQueue<ParallelCommand> parallelCommands) {
            Priority = priority;
            ParallelCommands = parallelCommands;
        }
    }

    public class ParallelCommandComparer : IComparer<ParallelCommand>
    {
        public int Compare(ParallelCommand parallelCommandA, ParallelCommand parallelCommandB)
        {

            if (parallelCommandA.TimeExecuteInParallelEvent == parallelCommandB.TimeExecuteInParallelEvent) //If both are fancy (Or both are not fancy, return 0 as they are equal)
            {
                return 0;
            }
            else if (parallelCommandA.TimeExecuteInParallelEvent > parallelCommandB.TimeExecuteInParallelEvent) //Otherwise if A is fancy (And therefore B is not), then return -1
            {
                return 1;
            }
            else //Otherwise it must be that B is fancy (And A is not), so return 1
            {
                return -1;
            }
        }
    }
}
