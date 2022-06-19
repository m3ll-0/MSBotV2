using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public static class RuneSolverServerCommunicator
    {
        public static string ConsultRuneService()
        {
            try { 

                using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "RuneSolverServer", PipeDirection.In))
                {
                    // Connect to the pipe or wait until the pipe is available.
                    Console.Write("Attempting to connect to pipe...");
                    pipeClient.Connect(5000);

                    Console.WriteLine("Connected to pipe.");
                    Console.WriteLine($"There are currently {pipeClient.NumberOfServerInstances} pipe server instances open.");

                    using (StreamReader sr = new StreamReader(pipeClient))
                    {
                        // Display the read text to the console
                        string runeResult;
                        while ((runeResult = sr.ReadLine()) != null)
                        {
                            return runeResult;
                        }
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine("Error: Detected timeout.");
                Console.WriteLine(e.Message);
            }

            return "";
        }
    }
}
