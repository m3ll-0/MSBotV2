
using MSBot.Models.Key;
using MSBotV2;
using MSBotV2.PriorityQueue;
using System.Drawing.Imaging;
/// <summary>
/// My own question as reference: https://stackoverflow.com/questions/35138778/sending-keys-to-a-directx-game
/// http://www.gamespp.com/directx/directInputKeyboardScanCodes.html
/// </summary>
/// 
namespace MSBot
{

    public class Program
    {
        // TODO, activate - deactivate specter manually exit map 3, otherwise on mistake it can still go
        static void Main(string[] args)
        {


            Console.WriteLine("Starting in 3 seconds.");
            Thread.Sleep(3000);

            var image = ScreenCapture.CaptureActiveWindow();
            image.Save(@"C:\temp\snippetsource.jpg", ImageFormat.Jpeg);

            //Orchestrator.Orchestrate();
            //demo();

            //img.x();
        }

        public static void demo() {

            //List<ScriptItem> ChangeChannel = new List<ScriptItem>()
            //{
            //    new ScriptItem(AtomicParallelEvents.ChangeChannel),
            //    new ScriptItem(AtomicParallelEvents.PauseMedium),
            //    new ScriptItem(AtomicParallelEvents.ChangeChannel),
            //};

            ScriptComposer.run(ScriptComposer.Compose(FinishedScripts.PreExitMottled.Concat(FinishedScripts.ChangeChannel).ToList()));
        }
    }

}