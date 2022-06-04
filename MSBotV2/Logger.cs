using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public static class Logger
    {
        public static void Log(string className, string message, LoggerPriority loggerPriority, bool newLine = true) {

            Console.ForegroundColor = LoggerPriorities[loggerPriority];

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

        public static Dictionary<LoggerPriority, ConsoleColor> LoggerPriorities = new Dictionary<LoggerPriority, ConsoleColor>(){
            { LoggerPriority.CRITICAL, ConsoleColor.Red },
            { LoggerPriority.HIGH, ConsoleColor.Magenta },
            { LoggerPriority.MEDIUM, ConsoleColor.White },
            { LoggerPriority.LOW, ConsoleColor.DarkYellow },
            { LoggerPriority.INFO, ConsoleColor.DarkGray },
        };

        public enum LoggerPriority
        { 
            CRITICAL,
            HIGH,
            MEDIUM,
            LOW,
            INFO
        }
        private static void LogToFile(string message)
        {
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            string logfile = $"log_{currentDate}.txt"; 

            TextWriter tw = new StreamWriter($"C:/msbot/{logfile}", true);
            tw.WriteLine(message);
            tw.Close();
        }
    }
}
