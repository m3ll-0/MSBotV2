using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public class ScriptItem
    {
        public ScriptItem(Func<List<ParallelCommand>> parallelCommandFunction)
        {
            ParallelCommandFunction = parallelCommandFunction;
        }

        public Func<List<ParallelCommand>> ParallelCommandFunction { get; private set; }
    }
}
