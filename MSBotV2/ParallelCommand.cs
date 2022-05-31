using MSBot.Models.Key;
using MSBotV2.PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public class ParallelCommand : IIndexedObject
    {
        public Keyboard.DirectXKeyStrokes Key { get; private set; }
        public int TimeRunning { get; private set; }
        public int TimeExecuteInParallelEvent { get; private set; }
        public int Index { get; set; }

        public ParallelCommand(Keyboard.DirectXKeyStrokes key, int timeRunning, int timeExecuteInParallelEvent)
        {
            Key = key;
            TimeRunning = timeRunning;
            TimeExecuteInParallelEvent = timeExecuteInParallelEvent;
        }
    }
}
