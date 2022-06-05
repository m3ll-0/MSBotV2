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
            map.Add(ScriptAttackLeftToRightVariation1, ScriptItemAttackType.LEFT_TO_RIGHT);
            map.Add(ScriptAttackLeftToRightVariation2, ScriptItemAttackType.LEFT_TO_RIGHT);
            
            map.Add(ScriptAttackRightToLeftVariation1, ScriptItemAttackType.RIGHT_TO_LEFT);
            map.Add(ScriptAttackRightToLeftVariation2, ScriptItemAttackType.RIGHT_TO_LEFT);

            return map;
        }

        public static Dictionary<List<ScriptItem>, ScriptItemAttackType> CreateAttackSpecterScriptsPool()
        {
            Dictionary<List<ScriptItem>, ScriptItemAttackType> map = new Dictionary<List<ScriptItem>, ScriptItemAttackType>();
            map.Add(ScriptAttackSpecterLeftToRightVariation1, ScriptItemAttackType.LEFT_TO_RIGHT);
            map.Add(ScriptAttackSpecterRightToLeftVariation1, ScriptItemAttackType.RIGHT_TO_LEFT);

            return map;
        }

        public enum ScriptItemAttackType
        {
            LEFT_TO_RIGHT,
            RIGHT_TO_LEFT,
        }

        // ATTACK SCRIPTS NORMAL MODE
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


        // ATTACK SPECTER SCRIPTS
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

        public static List<ScriptItem> ToggleSpecterMode = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.ToggleSpecterMode),
            };

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
                //new ScriptItem(AtomicParallelEvents.AttackSwipe),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackCube),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
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
                new ScriptItem(AtomicParallelEvents.BuffInfinity),
                new ScriptItem(AtomicParallelEvents.BuffFlora),
                new ScriptItem(AtomicParallelEvents.BuffWrath),
                new ScriptItem(AtomicParallelEvents.BuffContact),
                new ScriptItem(AtomicParallelEvents.BuffAmplifier),
                new ScriptItem(AtomicParallelEvents.BuffOverdrive),
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

        public static List<ScriptItem> NavigateChuChuToFiveColorHillPath = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.PauseLong),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.JumpDoubleRight),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.NavigateWalkLeftChuChuToFiveColorHillPath),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
                new ScriptItem(AtomicParallelEvents.EnterPortal),
                new ScriptItem(AtomicParallelEvents.PauseMedium),
            };

        public static List<ScriptItem> NavigateFiveColorHillPathToMottledForest1 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.WalkLeftShort),
                new ScriptItem(AtomicParallelEvents.TurnRight),
                // Prevent Birdshark
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),

                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),

                new ScriptItem(AtomicParallelEvents.DashComboRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                
                new ScriptItem(AtomicParallelEvents.ToggleSpecterMode),
                new ScriptItem(AtomicParallelEvents.TurnLeft),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),

                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.JumpDown),
                new ScriptItem(AtomicParallelEvents.PauseMedium),

                new ScriptItem(AtomicParallelEvents.NavigateWalkLeftFiveColorHillPathToMottledForest1 ),
                new ScriptItem(AtomicParallelEvents.EnterPortal),
                new ScriptItem(AtomicParallelEvents.PauseMediumLong),
                new ScriptItem(AtomicParallelEvents.ToggleSpecterMode)
            };

        public static List<ScriptItem> NavigateMottledForest1ToMottledForest2 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.WalkLeftShort),
                new ScriptItem(AtomicParallelEvents.TurnRight),
                // Prevent Birdshark
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),

                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),

                new ScriptItem(AtomicParallelEvents.DashComboRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),

                new ScriptItem(AtomicParallelEvents.ToggleSpecterMode),
                new ScriptItem(AtomicParallelEvents.TurnLeft),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),

                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.JumpDown),
                new ScriptItem(AtomicParallelEvents.PauseMedium),

                new ScriptItem(AtomicParallelEvents.NavigateWalkLeftMottledForest1ToMottledForest2 ),
                new ScriptItem(AtomicParallelEvents.EnterPortal),
                new ScriptItem(AtomicParallelEvents.PauseMediumLong),
                new ScriptItem(AtomicParallelEvents.ToggleSpecterMode)


                //new ScriptItem(AtomicParallelEvents.WalkLeftShort),
                //new ScriptItem(AtomicParallelEvents.TurnRight),
                //// Prevent Birdshark
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackSwipe),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackSwipe),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),
                //new ScriptItem(AtomicParallelEvents.AttackNormal),

                //new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                //new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                //new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                //new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                //new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                //new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                //new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                //new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                //new ScriptItem(AtomicParallelEvents.PauseMedium),

                //new ScriptItem(AtomicParallelEvents.ToggleSpecterMode),
                //new ScriptItem(AtomicParallelEvents.TurnLeft),
                //new ScriptItem(AtomicParallelEvents.AttackSwipe),
                //new ScriptItem(AtomicParallelEvents.AttackSwipe),
                //new ScriptItem(AtomicParallelEvents.AttackSwipe),

                //new ScriptItem(AtomicParallelEvents.NavigateWalkLeftMottledForest1ToMottledForest2 ),
                //new ScriptItem(AtomicParallelEvents.EnterPortal),
                //new ScriptItem(AtomicParallelEvents.PauseLong),
                //new ScriptItem(AtomicParallelEvents.ToggleSpecterMode)
            };

        public static List<ScriptItem> NavigateMottledForest2ToMottledForest3 = new List<ScriptItem>()
            {
                new ScriptItem(AtomicParallelEvents.WalkLeftShort),
                new ScriptItem(AtomicParallelEvents.TurnRight),
                // Prevent Birdshark
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),
                new ScriptItem(AtomicParallelEvents.AttackNormal),

                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),

                new ScriptItem(AtomicParallelEvents.DashComboRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),

                new ScriptItem(AtomicParallelEvents.ToggleSpecterMode),
                new ScriptItem(AtomicParallelEvents.TurnLeft),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),
                new ScriptItem(AtomicParallelEvents.AttackSwipe),

                new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight),
                new ScriptItem(AtomicParallelEvents.JumpDown),
                new ScriptItem(AtomicParallelEvents.PauseMedium),

                new ScriptItem(AtomicParallelEvents.NavigateWalkLeftMottledForest2ToMottledForest3 ),
                new ScriptItem(AtomicParallelEvents.EnterPortal),
                new ScriptItem(AtomicParallelEvents.PauseMediumLong),
                new ScriptItem(AtomicParallelEvents.ToggleSpecterMode)
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
                            //new ScriptItem(AtomicParallelEvents.PauseDeath),

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
            new ScriptItem(AtomicParallelEvents.CloseAllWindows),
            new ScriptItem(AtomicParallelEvents.PauseMedium),
            new ScriptItem(AtomicParallelEvents.OpenInventory)
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







        //List<ScriptItem> ScriptItemsMoveToCenterFromLeft = new List<ScriptItem>()
        //{
        //    new ScriptItem(AtomicParallelEvents.DashComboRight(), 0),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 3),
        //    new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight(), 5),
        //    new ScriptItem(AtomicParallelEvents.AttackSlash(), 7),
        //    new ScriptItem(AtomicParallelEvents.AttackJumpNormal(), 8),
        //};

        //List<ScriptItem> scriptItemsAttackLeftBottomVariation1 = new List<ScriptItem>()
        //{
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 0),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 1),
        //    new ScriptItem(AtomicParallelEvents.DashComboLeft(), 2),
        //    new ScriptItem(AtomicParallelEvents.PauseMedium(), 3),
        //    new ScriptItem(AtomicParallelEvents.TurnRight(), 4),
        //    new ScriptItem(AtomicParallelEvents.AttackCube(), 5),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipeJump(), 6),
        //    new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight(), 7),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 8),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 9),
        //    new ScriptItem(AtomicParallelEvents.TurnLeft(), 10),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 11),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 12),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 13),
        //    new ScriptItem(AtomicParallelEvents.AttackSlash(), 14),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 15),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 16),
        //};

        //List<ScriptItem> scriptItemsAttackRightBottomVariation1 = new List<ScriptItem>()
        //{
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 0),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 1),
        //    new ScriptItem(AtomicParallelEvents.DashComboRight(), 2),
        //    new ScriptItem(AtomicParallelEvents.PauseMedium(), 3),
        //    new ScriptItem(AtomicParallelEvents.TurnLeft(), 4),
        //    new ScriptItem(AtomicParallelEvents.AttackCube(), 5),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipeJump(), 6),
        //    new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalLeft(), 7),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 8),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 9),
        //    new ScriptItem(AtomicParallelEvents.TurnRight(), 10),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 11),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 12),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 13),
        //    new ScriptItem(AtomicParallelEvents.AttackSlash(), 14),
        //    new ScriptItem(AtomicParallelEvents.AttackNormal(), 15),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 16),
        //};

        //List<ScriptItem> scriptItemsAttackLeftBottomVariationSpecterMode1 = new List<ScriptItem>()
        //{
        //    new ScriptItem(AtomicParallelEvents.ToggleSpecterMode(), 0),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 1),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 2),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 3),
        //    new ScriptItem(AtomicParallelEvents.DashComboLeft(), 4),
        //    new ScriptItem(AtomicParallelEvents.PauseMedium(), 5),
        //    new ScriptItem(AtomicParallelEvents.TurnRight(), 6),
        //    new ScriptItem(AtomicParallelEvents.AttackCube(), 7),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipeJump(), 8),
        //    new ScriptItem(AtomicParallelEvents.AttackDoubleJumpNormalRight(), 9),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 10),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 11),
        //    new ScriptItem(AtomicParallelEvents.TurnLeft(), 12),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 13),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 14),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 15),
        //    new ScriptItem(AtomicParallelEvents.AttackSlash(), 16),
        //    new ScriptItem(AtomicParallelEvents.AttackSwipe(), 17),
        //    new ScriptItem(AtomicParallelEvents.AttackPulse(), 17),
        //    new ScriptItem(AtomicParallelEvents.ToggleSpecterMode(), 19),
        //};


    }
}
