using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSBotV2.FinishedScripts;

namespace MSBotV2
{
    public static class Helper
    {
        public static List<ScriptItem> GetRandomAttack(ScriptItemAttackType currentAttackTypeMode)
        {
            // Attack scripts HAVE to be symmetric
            int relevantKeySpace = (int)(CreateAttackScriptsPool().Count * 0.5);

            var attackPoolEnumerator = CreateAttackScriptsPool()
                .Where(x => x.Value == (currentAttackTypeMode == ScriptItemAttackType.RIGHT_TO_LEFT ? ScriptItemAttackType.LEFT_TO_RIGHT : ScriptItemAttackType.RIGHT_TO_LEFT))
                .GetEnumerator();

            int attackMoveCounter = 0;
            int randomAttackMove = new Random().Next(0, relevantKeySpace);

            while (attackPoolEnumerator.MoveNext())
            {
                if (attackMoveCounter++ == randomAttackMove)
                {
                    return attackPoolEnumerator.Current.Key;
                }
            }

            return null;
        }

        public static List<ScriptItem> GetRandomSpecterAttack(ScriptItemAttackType currentAttackTypeMode)
        {
            // Attack scripts HAVE to be symmetric
            int relevantKeySpace = (int)(CreateAttackSpecterScriptsPool().Count * 0.5);

            var attackPoolEnumerator = CreateAttackSpecterScriptsPool()
                .Where(x => x.Value == (currentAttackTypeMode == ScriptItemAttackType.RIGHT_TO_LEFT ? ScriptItemAttackType.LEFT_TO_RIGHT : ScriptItemAttackType.RIGHT_TO_LEFT))
                .GetEnumerator();

            int attackMoveCounter = 0;
            int randomAttackMove = new Random().Next(0, relevantKeySpace);

            while (attackPoolEnumerator.MoveNext())
            {
                if (attackMoveCounter++ == randomAttackMove)
                {
                    return attackPoolEnumerator.Current.Key;
                }
            }

            return null;
        }
    }
}
