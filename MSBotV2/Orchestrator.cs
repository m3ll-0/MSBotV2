using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSBotV2.FinishedScripts;

namespace MSBotV2
{
    public static class Orchestrator
    {
        private static OrchestratorMode orchestratorMode = OrchestratorMode.MODE_ATTACK;
        private static ScriptItemAttackType currentAttackTypeMode = ScriptItemAttackType.LEFT_TO_RIGHT;
        private static Dictionary<List<ScriptItem>, ScriptItemAttackType> attackPool = FinishedScripts.CreateAttackScriptsPool();

        public static void Orchestrate() {


            // Key - value 
            // (ENUM: ScriptItemAttackType: left to right / right to left) - FinishedScript

            // Main function
            // Makes sure scripts are properly called and rotated

            // Attack mode / CC mode 
            // Get random LeftToRight attacking function from pool

            Dictionary<List<ScriptItem>, ScriptItemAttackType> attackPool = new Dictionary<List<ScriptItem>, ScriptItemAttackType>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Core core = new Core();
            
            for (; ; ) {
                // Make non-blocking by running thread.
                long runTime = sw.ElapsedMilliseconds;

                if (orchestratorMode == OrchestratorMode.MODE_ATTACK) {
                    List<ScriptItem> randomAttack = GetRandomAttack();
                    Script attackScript = ScriptComposer.Compose(randomAttack);
                    core.RunCore(attackScript);

                    // Toggle attack type mode to change direction
                    currentAttackTypeMode = (currentAttackTypeMode == ScriptItemAttackType.RIGHT_TO_LEFT ? ScriptItemAttackType.LEFT_TO_RIGHT : ScriptItemAttackType.RIGHT_TO_LEFT);
                    Console.WriteLine($"Changed currentAttackTypeMode to [{currentAttackTypeMode}]");

                    // Change mode (Put in sep. function)
                    if (runTime > 1000 * 10)
                    {
                        orchestratorMode = OrchestratorMode.MODE_CC;
                    }
                }

                if (orchestratorMode == OrchestratorMode.MODE_CC) {

                    Console.WriteLine("Changing channel");

                    core.RunCore(ScriptComposer.Compose(ExitMapRight));
                    core.RunCore(ScriptComposer.Compose(ChangeChannel));
                    core.RunCore(ScriptComposer.Compose(EnterMapLeft));

                    orchestratorMode = OrchestratorMode.MODE_ATTACK;
                    currentAttackTypeMode = ScriptItemAttackType.LEFT_TO_RIGHT;
                    sw.Restart();
                }
            }

   
        }

        private static List<ScriptItem> GetRandomAttack()
        {
            // Attack scripts HAVE to be symmetric
            int relevantKeySpace = (int) (attackPool.Count * 0.5);

            var attackPoolEnumerator = attackPool.Where(x => x.Value == ScriptItemAttackType.LEFT_TO_RIGHT).GetEnumerator();

            if (currentAttackTypeMode == ScriptItemAttackType.RIGHT_TO_LEFT)
            {
                attackPoolEnumerator = attackPool.Where(x => x.Value == ScriptItemAttackType.LEFT_TO_RIGHT).GetEnumerator();
            }
            else {
                attackPoolEnumerator = attackPool.Where(x => x.Value == ScriptItemAttackType.RIGHT_TO_LEFT).GetEnumerator();
            }

            int attackMoveCounter = 0;
            int randomAttackMove = new Random().Next(0, relevantKeySpace);

            while (attackPoolEnumerator.MoveNext())
            {
                if (attackMoveCounter == randomAttackMove) {
                    return attackPoolEnumerator.Current.Key;
                }

                attackMoveCounter++;
            }

            return null;
        }
    }

    public enum OrchestratorMode
    { 
        MODE_ATTACK,
        MODE_CC
    }
}
