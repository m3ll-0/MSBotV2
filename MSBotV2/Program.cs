
using MSBotV2.Models.Key;
using MSBotV2;
using MSBotV2.PriorityQueue;
using System.Drawing.Imaging;
/// <summary>
/// My own question as reference: https://stackoverflow.com/questions/35138778/sending-keys-to-a-directx-game
/// http://www.gamespp.com/directx/directInputKeyboardScanCodes.html
/// </summary>
/// 
namespace MSBotV2
{

    public class Program
    {
        // TODO, activate - deactivate specter manually exit map 3, otherwise on mistake it can still go
        static void Main(string[] args)
        {

            Logger.Log(nameof(Program), $"Starting program in 3 seconds", Logger.LoggerPriority.MEDIUM);
            Thread.Sleep(3000);
            Logger.Log(nameof(Program), $"MSBot has started!", Logger.LoggerPriority.MEDIUM);

            Launch();
            //demo();
        }                                                                                                                                
                                                                                                                                         
        public static void Launch() {
            TemplateMatching.LoadNeedlesFromDiskToMemory();

            new Thread(new ThreadStart(() => { Orchestrator.Orchestrate(); })).Start();
            new Thread(new ThreadStart(() => { Orchestrator.PollTemplateMatchingThread(); })).Start();
        }

        public static void demo() {

            //List<ScriptItem> ChangeChannel = new List<ScriptItem>()
            //{
            //    new ScriptItem(AtomicParallelEvents.ChangeChannel),
            //    new ScriptItem(AtomicParallelEvents.PauseMedium),
            //    new ScriptItem(AtomicParallelEvents.ChangeChannel),
            //};

            TemplateMatching.LoadNeedlesFromDiskToMemory();
            DynamicScriptBuilder.BuildOpenPetDynamicScript().Invoke();

            //Orchestrator.GetRandomSpecterAttack();



            //Console.WriteLine("Starting in 3 seconds.");

            //TemplateMatching.TemplateMatch(TemplateMatching.TemplateMatchingAction.SPECTER_GAUGE_FULL);


            //List<ScriptItem> x = FinishedScripts.NavigateChuChuToFiveColorHillPath
            //    .Concat(FinishedScripts.NavigateFiveColorHillPathToMottledForest1)
            //    .Concat(FinishedScripts.NavigateMottledForest1ToMottledForest2)
            //    .Concat(FinishedScripts.NavigateMottledForest2ToMottledForest3).ToList();

            //List<ScriptItem> x = FinishedScripts.ScriptAttackSpecterLeftToRightVariation1;

            //new Core().RunScript(ScriptComposer.Compose(x));


            //new Core().RunScript(ScriptComposer.Compose(FinishedScripts.NavigateChuChuToFiveColorHillPath));

            //ScriptComposer.run(ScriptComposer.Compose(FinishedScripts.PreExitMottled.Concat(FinishedScripts.ChangeChannel).ToList()));
        }
    }

}