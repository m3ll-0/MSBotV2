using MSBotV2.PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public class Script
    {
        public PriorityQueue<ParallelEvent> ParallelEvents { get; private set; }

        public Script(PriorityQueue<ParallelEvent> parallelEvents)
        {
            ParallelEvents = parallelEvents;
        }
    }

    public class ParallelEventComparer : IComparer<ParallelEvent>
    {
        public int Compare(ParallelEvent parallelEventA, ParallelEvent parallelEventB)
        {

            if (parallelEventA.Priority == parallelEventB.Priority) //If both are fancy (Or both are not fancy, return 0 as they are equal)
            {
                return 0;
            }
            else if (parallelEventA.Priority > parallelEventB.Priority) //Otherwise if A is fancy (And therefore B is not), then return -1
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
