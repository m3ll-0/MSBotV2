using MSBot.Models.Key;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public class Core
    {
        protected int numberOfAliveParallelCommandThreads = 0;

        public void RunCore(Script script) {

            for (; ; ) {
                // Peek to see if potentialParallelEvent is ready to be invoked
                ParallelEvent potentialParallelEvent = script.ParallelEvents.Peek();

                // Check if potentialParallelEvent is not null and ready to be invoked
                if (potentialParallelEvent != null) {
                    ParallelEvent parallelEvent = script.ParallelEvents.Pop();
                    InvokeParallelEvent(parallelEvent);
                }

                // Priority queue is empty, stop
                if (script.ParallelEvents.Count == 0)
                {
                    break;
                }
            }
        }

        protected void InvokeParallelEvent(ParallelEvent parallelEvent) {
            // Start stopwatch
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Set number of alive parallel command threads for callback
            numberOfAliveParallelCommandThreads = parallelEvent.ParallelCommands.Count;

            for (; ; )
            {
                // Peek to see if potentialParallelEvent is ready to be invoked
                ParallelCommand potentialParallelCommand = parallelEvent.ParallelCommands.Peek();

                // Check if potentialParallelEvent is not null and ready to be invoked
                if (potentialParallelCommand != null && sw.ElapsedMilliseconds > potentialParallelCommand.TimeExecuteInParallelEvent )
                {
                    ParallelCommand parallelCommand = parallelEvent.ParallelCommands.Pop();
                    new Thread(new ThreadStart(() => { InvokeParallelCommand(parallelCommand); ParallelCommandCallback(); })).Start();
                }

                // Threads are done running, parallelEvent is finished
                if (numberOfAliveParallelCommandThreads == 0) {
                    break;
                }
            }
        }
        protected void ParallelCommandCallback()
        {
            numberOfAliveParallelCommandThreads--;
        }

        protected void InvokeParallelCommand(ParallelCommand parallelCommand) {
            //Console.WriteLine(parallelCommand.Key);
            //Thread.Sleep(parallelCommand.TimeRunning);

            if (parallelCommand.Key == Keyboard.DirectXKeyStrokes.DIK_PAUSE) {
                Thread.Sleep(parallelCommand.TimeRunning);
            }

            Keyboard.SendKey(parallelCommand.Key, false, Keyboard.InputType.Keyboard);
            Thread.Sleep(parallelCommand.TimeRunning);
            Keyboard.SendKey(parallelCommand.Key, true, Keyboard.InputType.Keyboard);
        }
    }
}
