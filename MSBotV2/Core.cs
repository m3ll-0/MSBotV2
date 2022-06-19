using MSBotV2.Models.Key;
using System.Diagnostics;

namespace MSBotV2
{
    public class Core
    {
        public static bool CORE_INTERRUPTED = false;

        protected int numberOfAliveParallelCommandThreads = 0;
        private ExecutionContext ExecutionContext;

        public Core(ExecutionContext executionContext = ExecutionContext.DYNAMICSCRIPT) {
            this.ExecutionContext = executionContext;
        }

        public void RunScript(Script script) {

            Logger.Log(nameof(Script), $"Invoking script");

            for (; ; ) {
                //Check if CORE_INTERRUPTED is set to through, meaning that an DynamicScript is being invoked and should be waited upon.
                 //If so, cancel current instance.
                if (CORE_INTERRUPTED && this.ExecutionContext == ExecutionContext.ORCHESTRATOR)
                {
                    Logger.Log(nameof(Script), $"Core interrupted to call DynamicScript");

                    CORE_INTERRUPTED = false;
                    return;
                }

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

        public void RunDynamicScript(Script script)
        {

            Logger.Log(nameof(Script), $"Invoking script");


            for (; ; )
            {
                // Peek to see if potentialParallelEvent is ready to be invoked
                ParallelEvent potentialParallelEvent = script.ParallelEvents.Peek();

                // Check if potentialParallelEvent is not null and ready to be invoked
                if (potentialParallelEvent != null)
                {
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
                if (CORE_INTERRUPTED && this.ExecutionContext == ExecutionContext.ORCHESTRATOR)
                {
                    Logger.Log(nameof(Script), $"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

                    CORE_INTERRUPTED = false;
                    return;
                }

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

            Console.WriteLine($"Running command: {parallelCommand.Key}");

            if (parallelCommand.Key == Keyboard.DirectXKeyStrokes.DIK_PAUSE)
            {
                Thread.Sleep(parallelCommand.TimeRunning);
            }

            Keyboard.SendKey(parallelCommand.Key, false, Keyboard.InputType.Keyboard);
            Thread.Sleep(parallelCommand.TimeRunning);
            Keyboard.SendKey(parallelCommand.Key, true, Keyboard.InputType.Keyboard);
        }
    }

    public enum ExecutionContext { 
        ORCHESTRATOR,
        DYNAMICSCRIPT
    }
}
