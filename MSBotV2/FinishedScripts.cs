using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public static class FinishedScripts
    {
        /**
         * Creates a hashmap of attack scripts and their respective ScriptItemAttackType
         */
        public static Dictionary<List<ScriptItem>, ScriptItemAttackType> CreateAttackScriptsPool()
        {
            Dictionary<List<ScriptItem>, ScriptItemAttackType> map = new Dictionary<List<ScriptItem>, ScriptItemAttackType>();
            //map.Add(ScriptAttackLeftToRightVariation1, ScriptItemAttackType.LEFT_TO_RIGHT);
            //map.Add(ScriptAttackLeftToRightVariation2, ScriptItemAttackType.LEFT_TO_RIGHT);
            //map.Add(ScriptAttackLeftToRightVariation3, ScriptItemAttackType.LEFT_TO_RIGHT);

            //map.Add(ScriptAttackRightToLeftVariation1, ScriptItemAttackType.RIGHT_TO_LEFT);
            //map.Add(ScriptAttackRightToLeftVariation2, ScriptItemAttackType.RIGHT_TO_LEFT);
            //map.Add(ScriptAttackRightToLeftVariation3, ScriptItemAttackType.RIGHT_TO_LEFT);

            map.Add(ScriptAttackLeftToRightVariation1X, ScriptItemAttackType.LEFT_TO_RIGHT);
            map.Add(ScriptAttackRightToLeftVariation1X, ScriptItemAttackType.RIGHT_TO_LEFT);

            map.Add(ScriptAttackLeftToRightVariation1Y, ScriptItemAttackType.LEFT_TO_RIGHT);
            map.Add(ScriptAttackRightToLeftVariation1Y, ScriptItemAttackType.RIGHT_TO_LEFT);

            return map;
        }

        public static Dictionary<List<ScriptItem>, ScriptItemAttackType> CreateAttackSpecterScriptsPool()
        {
            Dictionary<List<ScriptItem>, ScriptItemAttackType> map = new Dictionary<List<ScriptItem>, ScriptItemAttackType>();
            map.Add(ScriptAttackSpecterLeftToRightVariation1, ScriptItemAttackType.LEFT_TO_RIGHT);
            map.Add(ScriptAttackSpecterLeftToRightVariation2, ScriptItemAttackType.LEFT_TO_RIGHT);

            map.Add(ScriptAttackSpecterRightToLeftVariation1, ScriptItemAttackType.RIGHT_TO_LEFT);
            map.Add(ScriptAttackSpecterRightToLeftVariation2, ScriptItemAttackType.RIGHT_TO_LEFT);


            return map;
        }

        public enum ScriptItemAttackType
        {
            LEFT_TO_RIGHT,
            RIGHT_TO_LEFT,
        }


        public static List<ScriptItem> ScriptAttackLeftToRightVariation1X = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackBowRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleUp),
                new ScriptItem(AtomicParallelEvents.PauseShort),
                new ScriptItem(AtomicParallelEvents.JumpDoubleUp),
                new ScriptItem(AtomicParallelEvents.TurnLeft),
                new ScriptItem(AtomicParallelEvents.PauseShort),
                new ScriptItem(AtomicParallelEvents.AttackLasso),
                new ScriptItem(AtomicParallelEvents.PauseShort),
            };

        public static List<ScriptItem> ScriptAttackRightToLeftVariation1Y = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackBowLeft),
            };


        public static List<ScriptItem> ScriptAttackLeftToRightVariation1Y = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackBowRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleUp),
                new ScriptItem(AtomicParallelEvents.TurnLeft),
                new ScriptItem(AtomicParallelEvents.PauseShort),
                new ScriptItem(AtomicParallelEvents.AttackLasso),
                new ScriptItem(AtomicParallelEvents.PauseShort),
            };

        public static List<ScriptItem> ScriptAttackRightToLeftVariation1X = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackBowLeft),
            };




        // Attack acripts normal mode
        public static List<ScriptItem> ScriptAttackLeftToRightVariation1 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRight),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackCube),
                new ScriptItem(AtomicParallelEvents.DashComboRight),
            };

        public static List<ScriptItem> ScriptAttackRightToLeftVariation1 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeft),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackCube),
                new ScriptItem(AtomicParallelEvents.DashComboLeft),
            };


        public static List<ScriptItem> ScriptAttackLeftToRightVariation2 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeUp),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRight),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackCube),
                new ScriptItem(AtomicParallelEvents.DashComboRight),
            };

        public static List<ScriptItem> ScriptAttackRightToLeftVariation2 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeUp),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackCube),
                new ScriptItem(AtomicParallelEvents.DashComboLeft),
            };

        public static List<ScriptItem> ScriptAttackLeftToRightVariation3 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeUp),
                new ScriptItem(AtomicParallelEvents.DashComboRightUp),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRight),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackCube),
                new ScriptItem(AtomicParallelEvents.DashComboRight),
            };


        public static List<ScriptItem> ScriptAttackRightToLeftVariation3 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeUp),
                new ScriptItem(AtomicParallelEvents.DashComboLeftUp),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackCube),
                new ScriptItem(AtomicParallelEvents.DashComboLeft),
            };


        // Attack scripts specter mode
        public static List<ScriptItem> ScriptAttackSpecterLeftToRightVariation1 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),
                new ScriptItem(AtomicParallelEvents.AttackCube),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeUp),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),
                new ScriptItem(AtomicParallelEvents.DashComboRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),

                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),
            };

        public static List<ScriptItem> ScriptAttackSpecterRightToLeftVariation1 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.AttackPulse),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeUp),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.DashComboLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
            };


        public static List<ScriptItem> ScriptAttackSpecterLeftToRightVariation2 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeUp),
                new ScriptItem(AtomicParallelEvents.DashComboRightUp),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),
                new ScriptItem(AtomicParallelEvents.AttackCube),
                new ScriptItem(AtomicParallelEvents.DashComboRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),

                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeRightSpecter),
            };

        public static List<ScriptItem> ScriptAttackSpecterRightToLeftVariation2 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeUp),
                new ScriptItem(AtomicParallelEvents.DashComboLeftUp),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.DashComboLeft),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
                new ScriptItem(AtomicParallelEvents.AttackPulse),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeLeftSpecter),
            };


        public static List<ScriptItem> ToggleSpecterMode = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.ToggleSpecterMode),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
            };

        // Misc

        public static List<ScriptItem> ExitMapRightCaveOfRepose = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.TurnLeft),
                new ScriptItem(AtomicParallelEvents.ToggleSpecterMode),
                new ScriptItem(AtomicParallelEvents.AttackPulse),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.WalkLeftShort),
                new ScriptItem(AtomicParallelEvents.EnterPortal),
                new ScriptItem(AtomicParallelEvents.PauseLong),
                new ScriptItem(AtomicParallelEvents.ToggleSpecterMode),
            };

        public static List<ScriptItem> EnterMapLeft = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.JumpDoubleLeft),
                new ScriptItem(AtomicParallelEvents.JumpDoubleLeft),
                new ScriptItem(AtomicParallelEvents.WalkRightShort),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.EnterPortal),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
            };

        public static List<ScriptItem> ChangeChannel = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.PauseLong),
                new ScriptItem(AtomicParallelEvents.ChangeChannel),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.ChangeChannel),
                new ScriptItem(AtomicParallelEvents.PauseLong),
            };

        public static List<ScriptItem> Buff = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.Buffs),
            };

        public static List<ScriptItem> PreExitMottled = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.AttackPulse),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
            };

        public static List<ScriptItem> NavigateToLastMap = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.AttackPulse),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
            };

        public static List<ScriptItem> OpenPet = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.PauseLong),
            };

        public static List<ScriptItem> PauseLong = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.PauseLong),
            };


        public static List<ScriptItem> OpenInventory = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.OpenInventory)
            };

        public static List<ScriptItem> CloseInventory = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.PauseLong),
                new ScriptItem(AtomicParallelEvents.CloseInventory)
            };

        public static List<ScriptItem> PauseAfterDeathAndOpenInventory = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.PauseDeath),
            new ScriptItem(AtomicParallelEvents.OpenInventory)
        };

        public static List<ScriptItem> PauseAfterDeath = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.PauseDeath),
        };

        public static List<ScriptItem> ExitProcess = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.PauseLong),
            new ScriptItem(AtomicParallelEvents.CloseInventory)
        };

        public static List<ScriptItem> CloseAllWindows = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.PauseLong),
            new ScriptItem(AtomicParallelEvents.CloseAllWindows)
        };

        public static List<ScriptItem> ConfirmQuest = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.ConfirmQuest),
        };

        public static List<ScriptItem> Return = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.Return),
        };

        public static List<ScriptItem> OpenMap = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.OpenMap),
        };

        public static List<ScriptItem> SearchMapColossalTail = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.SearchMapColossalTail),
        };

        public static List<ScriptItem> SearchMapMottledForest1 = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.SearchMapMottledForest1),
        };

        public static List<ScriptItem> SearchMapDealieBobberForest1 = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.SearchMapDealieBobberForest1),
        };

        public static List<ScriptItem> MoveToNearbyTown = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.MoveToNearbyTown),
        };

        public static List<ScriptItem> SearchMapCaveDepths = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.SearchMapCaveDepths),
        };

        public static List<ScriptItem> SearchMapFireRockZone = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.SearchMapFireRockZone),
        };

        public static List<ScriptItem> SearchMapHiddenLakeShore = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.SearchMapHiddenLakeShore),
        };

        public static List<ScriptItem> SearchMapLandOfSorrow = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.SearchMapLandOfSorrow),
        };

        public static List<ScriptItem> SearchMapHiddenFireZone = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.SearchMapHiddenFireZone),
        };

        public static List<ScriptItem> MoveLeftShort = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.SearchMapHiddenFireZone),
        };

        public static List<ScriptItem> MoveRightShort = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.SearchMapHiddenFireZone),
        };

        public static List<ScriptItem> MoveLeftLong = new List<ScriptItem>()
        {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft),
        };

        public static List<ScriptItem> MoveRightLong = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
        };

        public static List<ScriptItem> MoveLeftMedium = new List<ScriptItem>()
        {
                new ScriptItem(AtomicParallelEvents.MoveLeftMedium),
        };

        public static List<ScriptItem> MoveRightMedium = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.MoveRightMedium),
        };

        public static List<ScriptItem> MoveUp = new List<ScriptItem>()
        {
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpSwipeUp),
        };

        public static List<ScriptItem> MoveDown = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.JumpDown),
        };

        public static List<ScriptItem> RunePressLeft = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.RunePressLeft),
        };

        public static List<ScriptItem> RunePressRight = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.RunePressRight),
        };

        public static List<ScriptItem> RunePressUp = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.RunePressUp),
        };

        public static List<ScriptItem> RunePressDown = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.RunePressDown),
        };

        public static List<ScriptItem> RunePressPause = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.RunePressPause),
        };

        public static List<ScriptItem> RuneActivate = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.RuneAcivate),
        };

        public static List<ScriptItem> RuneActivatePause = new List<ScriptItem>()
        {
            new ScriptItem(AtomicParallelEvents.RuneActivatePause),
        };

    }
}
