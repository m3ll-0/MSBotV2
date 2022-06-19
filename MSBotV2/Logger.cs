using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public static class Logger
    {
        public static void Log(string className, string message, bool newLine = true) {

            Console.ForegroundColor = LoggerClassColors[className];

            string currentTime = DateTime.Now.ToString("h:mm:ss");
            string line = $"{currentTime} | {className} | {message}";

            if (Config.LogConfig.LogToConsole)
            {
                if (newLine)
                {
                    Console.WriteLine(line);
                }
                else
                {
                    Console.Write(line);
                }
            }

            if (Config.LogConfig.LogToFile) {
                LogToFile(line);
            }
        }

        public static Dictionary<string, ConsoleColor> LoggerClassColors = new Dictionary<string, ConsoleColor>(){
            { "Orchestrator", ConsoleColor.DarkYellow },
            { "TemplateMatching", ConsoleColor.Magenta },
            { "ArcaneRiverQuestBot", ConsoleColor.Green },
            { "ChuChuQuestBot", ConsoleColor.Green },
            { "RuneSolverBot", ConsoleColor.Green },
            { "Script", ConsoleColor.DarkGray },
            { "Program", ConsoleColor.White },
            { "Mouse", ConsoleColor.DarkGray },
        };

        private static void LogToFile(string message)
        {
            try
            {
                string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
                string logfile = $"log_{currentDate}.txt";

                TextWriter tw = new StreamWriter($"C:/msbot/{logfile}", true);
                tw.WriteLine(message);
                tw.Close();
            }
            catch (Exception ex) {
                Console.WriteLine("Error logging to file: " + ex.Message);
            }
        }
    }
}
