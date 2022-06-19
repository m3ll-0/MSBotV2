
using MSBotV2.Models.Key;
using MSBotV2;
using MSBotV2.PriorityQueue;
using System.Drawing.Imaging;
using System.IO.Pipes;
using System.Diagnostics;
/// <summary>
/// My own question as reference: https://stackoverflow.com/questions/35138778/sending-keys-to-a-directx-game
/// http://www.gamespp.com/directx/directInputKeyboardScanCodes.html
/// </summary>
/// 
namespace MSBotV2
{

    public class Program
    {
        static void Main(string[] args)
        {   
            Launch();
            //demo();
            //ConsultRuneService();

        }
                                                                                                                                         
        public static void Launch() {

            Process runCmd = new Process();
            runCmd.StartInfo.FileName = "cmd.exe";
            runCmd.StartInfo.UseShellExecute = true;

            runCmd.StartInfo.RedirectStandardOutput = false;
            runCmd.StartInfo.WorkingDirectory = "C:\\Users\\mihae\\Desktop\\auto-maple";

            runCmd.StartInfo.Arguments = "/K python main.py";

            runCmd.StartInfo.CreateNoWindow = true;
            runCmd.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            runCmd.Start();

            Logger.Log(nameof(Program), $"Starting program in 3 seconds");
            Thread.Sleep(9000);
            Logger.Log(nameof(Program), $"MSBot has started!");


            // Load all needles which are mandatory for template matching
            TemplateMatching.LoadNeedlesFromDiskToMemory();


            //runCmd.StandardInput.WriteLine(@"cd C:\\Users\\mihae\\Desktop\\auto-maple");
            //runCmd.StandardInput.WriteLine(@"python main.py");

            //// Start RuneSolver AI
            //var startInfo = new ProcessStartInfo
            //{
            //    FileName = "cmd.exe",
            //    RedirectStandardInput = true,
            //    RedirectStandardOutput = true,
            //    UseShellExecute = false,
            //    CreateNoWindow = false
            //};

            //var process = new Process { StartInfo = startInfo };

            //process.Start();
            //process.StandardInput.WriteLine(@"cd C:\\Users\\mihae\\Desktop\\auto-maple");
            //process.StandardInput.WriteLine(@"python main.py");

            // Run main threads
            new Thread(new ThreadStart(() => { Orchestrator.PollTemplateMatchingThread(); })).Start();
            new Thread(new ThreadStart(() => { Orchestrator.Orchestrate(); })).Start();

        }

        public static void demo() {

            //List<ScriptItem> ChangeChannel = new List<ScriptItem>()
            //{
            //    new ScriptItem(AtomicParallelEvents.ChangeChannel),
            //    new ScriptItem(AtomicParallelEvents.PauseMedium),
            //    new ScriptItem(AtomicParallelEvents.ChangeChannel),
            //};




            //TemplateMatching.LoadNeedlesFromDiskToMemory();
            //DynamicScriptBuilder.BuildSpecterGaugeFullDynamicScript().Invoke();

            //ChuChuQuestBot chuChuQuestBot = new ChuChuQuestBot();
            //chuChuQuestBot.ExecuteBot();


            //TemplateMatching.TemplateMatch(TemplateMatching.TemplateMatchingAction.QUEST_COMPLETED);




            //ArcaneRiverQuestBot arcaneRiverQuestBot = new ArcaneRiverQuestBot();
            //arcaneRiverQuestBot.ExecuteBot();


            //new Core().RunScript(ScriptComposer.Compose(FinishedScripts.ScriptAttackSpecterLeftToRightVariation2));



            //TemplateMatching.TemplateMatch(TemplateMatching.TemplateMatchingAction.MINIMAP_PLAYER);




            //Orchestrator.GetRandomSpecterAttack();



            //Console.WriteLine("Starting in 3 seconds.");




            //List<ScriptItem> x = FinishedScripts.NavigateChuChuToFiveColorHillPath
            //    .Concat(FinishedScripts.NavigateFiveColorHillPathToMottledForest1)
            //    .Concat(FinishedScripts.NavigateMottledForest1ToMottledForest2)
            //    .Concat(FinishedScripts.NavigateMottledForest2ToMottledForest3).ToList();

            //List<ScriptItem> x = FinishedScripts.ScriptAttackSpecterLeftToRightVariation1;

            //new Core().RunScript(ScriptComposer.Compose(x));


            //new Core().RunScript(ScriptComposer.Compose(FinishedScripts.NavigateChuChuToFiveColorHillPath));

            //ScriptComposer.run(ScriptComposer.Compose(FinishedScripts.PreExitMottled.Concat(FinishedScripts.ChangeChannel).ToList()));





            Process runCmd = new Process();
            runCmd.StartInfo.FileName = "cmd.exe";
            runCmd.StartInfo.UseShellExecute = true;

            runCmd.StartInfo.RedirectStandardOutput = false;
            runCmd.StartInfo.WorkingDirectory = "C:\\Users\\mihae\\Desktop\\auto-maple";

            runCmd.StartInfo.Arguments = "/K python main.py";

            runCmd.StartInfo.CreateNoWindow = true;
            runCmd.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            runCmd.Start();

            Logger.Log(nameof(Program), $"Starting program in 3 seconds");
            Thread.Sleep(3000);



            TemplateMatching.LoadNeedlesFromDiskToMemory();

            RuneSolverBot trackerBot = new RuneSolverBot();
            trackerBot.Run();


        }
    }

}