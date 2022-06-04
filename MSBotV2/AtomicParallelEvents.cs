﻿using MSBotV2.Models.Key;
using MSBotV2.PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public static class AtomicParallelEvents
    {

        public static List<ParallelCommand> JumpDoubleUp() {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_UP, 1000, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 200, 500),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 200, 900)
            };
        }

        public static List<ParallelCommand> JumpDoubleLeft()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFT, 800, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700),
            };
        }

        public static List<ParallelCommand> JumpDoubleRight()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHT, 800, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700),
                //new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_Z, 200, 720)

                //new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHT, 100, 0),
                //new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                //new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700)
            };
        }

        public static List<ParallelCommand> WalkRight()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHT, 2000, 0),
            };
        }

        public static List<ParallelCommand> JumpDown()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_DOWNARROW, 500, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 100, 0),

            };
        }

        public static List<ParallelCommand> WalkLeftShort()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFTARROW, 750, 0),
            };
        }

        public static List<ParallelCommand> NavigateWalkLeftChuChuToFiveColorHillPath()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFTARROW, 575, 0),
            };
        }

        public static List<ParallelCommand> NavigateWalkLeftFiveColorHillPathToMottledForest1()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFTARROW, 305, 0),
            };
        }

        public static List<ParallelCommand> NavigateWalkLeftMottledForest1ToMottledForest2()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFTARROW, 200, 0),
            };
        }

        public static List<ParallelCommand> NavigateWalkLeftMottledForest2ToMottledForest3()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFTARROW, 375, 0),
            };
        }

        public static List<ParallelCommand> WalkRightShort()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHTARROW, 820, 0),
            };
        }

        public static List<ParallelCommand> EnterPortal()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_UPARROW, 200, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_UPARROW, 400, 200),
            };
        }

        public static List<ParallelCommand> ChangeChannel()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_F10, 200, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHTARROW, 200, 200),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHTARROW, 200, 400),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHTARROW, 200, 600),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RETURN, 200, 1000),
            };
        }

        public static List<ParallelCommand> PauseMedium()
        {
            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_PAUSE, 600, 0),
            };
        }

        public static List<ParallelCommand> PauseMediumLong()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_PAUSE, 1300, 0),
            };
        }

        public static List<ParallelCommand> PauseLong()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_PAUSE, 2000, 0),
            };
        }

        public static List<ParallelCommand> PauseDeath()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_PAUSE, 5000, 0),
            };
        }

        public static List<ParallelCommand> DashComboRight()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHT, 400, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_UP, 400, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_S, 200, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_DOWN, 400, 600),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_S, 500, 800),
            };
        }

        public static List<ParallelCommand> DashComboLeft()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFT, 400, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_UP, 400, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_S, 200, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_DOWN, 400, 600),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_S, 500, 800),
            };
        }

        public static List<ParallelCommand> AttackNormal()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_Z, 600, 0),
            };
        }

        public static List<ParallelCommand> AttackJumpNormal()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 100, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_Z, 100, 300),
            };
        }

        public static List<ParallelCommand> AttackDoubleJumpNormalRight()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHT, 1200, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_Z, 200, 720)
            };
        }

        public static List<ParallelCommand> AttackDoubleJumpNormalUp()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_UP, 800, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_Z, 200, 720)
            };
        }

        public static List<ParallelCommand> AttackDoubleJumpNormalLeft()
        {


            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFT, 800, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_Z, 200, 720)
            };
        }

        public static List<ParallelCommand> AttackSwipe()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LSHIFT, 400, 0),
            };
        }

        public static List<ParallelCommand> AttackSwipeJump()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHT, 100, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 100, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LSHIFT, 100, 500)            
            };
        }

        public static List<ParallelCommand> AttackDoubleJumpSwipeLeft()
        {


            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFT, 800, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LSHIFT, 200, 720)
            };
        }

        public static List<ParallelCommand> AttackDoubleJumpSwipeRight()
        {


            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHT, 800, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LSHIFT, 200, 720)
            };
        }

        public static List<ParallelCommand> AttackDoubleJumpSwipeRightSpecter()
        {


            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHT, 800, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LSHIFT, 600, 720)
            };
        }

        public static List<ParallelCommand> AttackDoubleJumpSwipeLeftSpecter()
        {


            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFT, 800, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LSHIFT, 600, 720)
            };
        }

        public static List<ParallelCommand> AttackDoubleJumpSwipeUp()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_UP, 800, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 300),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 300, 700),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LSHIFT, 200, 720)
            };
        }

        public static List<ParallelCommand> AttackSlash()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LALT, 100, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_X, 700, 300),
            };
        }

        public static List<ParallelCommand> AttackCube()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_A, 700, 0),
            };
        }

        public static List<ParallelCommand> AttackCubeSpecter()
        {

            return new List<ParallelCommand>()
            {
                //new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_A, 700, 0),
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_A, 700, 0),
            };
        }

        public static List<ParallelCommand> AttackPulse()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_W, 400, 0),
            };
        }

        public static List<ParallelCommand> AttackDemon()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_Q, 400, 0),
            };
        }

        public static List<ParallelCommand> TurnRight()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_RIGHTARROW, 100, 0),
            };
        }

        public static List<ParallelCommand> TurnLeft()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_LEFTARROW, 100, 0),
            };
        }

        public static List<ParallelCommand> ToggleSpecterMode()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_F9, 1000, 0),
            };
        }

        public static List<ParallelCommand> BuffContact()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_5, 400, 0),
            };
        }

        public static List<ParallelCommand> BuffFlora()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_6, 400, 0),
            };
        }

        public static List<ParallelCommand> BuffWrath()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_7, 400, 0),
            };
        }

        public static List<ParallelCommand> BuffAmplifier()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_8, 400, 0),
            };
        }

        public static List<ParallelCommand> BuffOverdrive()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_9, 400, 0),
            };
        }

        public static List<ParallelCommand> BuffInfinity()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_0, 400, 0),
            };
        }

        public static List<ParallelCommand> OpenInventory()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_I, 200, 0),
            };
        }

        public static List<ParallelCommand> CloseInventory()
        {

            return new List<ParallelCommand>()
            {
                new ParallelCommand(Keyboard.DirectXKeyStrokes.DIK_I, 200, 0),
            };
        }
    }
}
