
using MSBot.Models.Key;
using MSBotV2;
using MSBotV2.PriorityQueue;
/// <summary>
/// My own question as reference: https://stackoverflow.com/questions/35138778/sending-keys-to-a-directx-game
/// http://www.gamespp.com/directx/directInputKeyboardScanCodes.html
/// </summary>
/// 
namespace MSBot
{

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting in 3 seconds.");
            Thread.Sleep(3000);

            Orchestrator.Orchestrate();
        }

        public static void demo() {

            //List<ScriptItem> ChangeChannel = new List<ScriptItem>()
            //{
            //    new ScriptItem(AtomicParallelEvents.ChangeChannel),
            //    new ScriptItem(AtomicParallelEvents.PauseMedium),
            //    new ScriptItem(AtomicParallelEvents.ChangeChannel),
            //};

            //ScriptComposer.run(ScriptComposer.Compose(test2));
        }
    }

}