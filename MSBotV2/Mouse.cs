using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    public static class Mouse
    {
        public static bool UseScreenScaling = true  ;

        public static void SetCursorPosition((int, int) coordinates_undetermined) {

            int x_coordinate_needle_undetermined = coordinates_undetermined.Item1;
            int y_coordinate_needle_undetermined = coordinates_undetermined.Item2;

            // x and y-coordinates may be physical or scaled depending on when VM is ran or not.
            (int, int) mapleStoryWindowCoordinates = GetGameWindowCoordinates();

            int x_coordinate_mswindow_undetermined = mapleStoryWindowCoordinates.Item1;
            int y_coordinate_mswindow_undetermined = mapleStoryWindowCoordinates.Item2;

            Logger.Log(nameof(Mouse), $"Found coordinates for Maplestory {mapleStoryWindowCoordinates.Item1}, {mapleStoryWindowCoordinates.Item2})", Logger.LoggerPriority.MEDIUM);

            // todo move to config
            double x_coordinate_scaling_factor = 0.793650794;
            double y_coordinate_scaling_factor = 0.8;

            int x_coordinate_result = 0;
            int y_coordinate_result = 0;

            switch (UseScreenScaling) {
                case true: // Coordinates are scaled
                    // Apply scaling factor to needle position
                    int x_coordinate_needle_scaled = (int)(x_coordinate_needle_undetermined * x_coordinate_scaling_factor);
                    int y_coordinate_needle_scaled = (int)(y_coordinate_needle_undetermined * y_coordinate_scaling_factor);

                    // Apply scaling factor to mswindow position
                    int x_coordinate_mswindow_scaled = (int)(x_coordinate_mswindow_undetermined * x_coordinate_scaling_factor);
                    int y_coordinate_mswindow_scaled = (int)(y_coordinate_mswindow_undetermined * y_coordinate_scaling_factor);

                    // Net result = scaled result + window position
                    x_coordinate_result = x_coordinate_needle_scaled + x_coordinate_mswindow_scaled;
                    y_coordinate_result = y_coordinate_needle_scaled + y_coordinate_mswindow_scaled;
                    break;

                case false: // Coordinates are absolute
                    x_coordinate_result = x_coordinate_needle_undetermined + x_coordinate_mswindow_undetermined;
                    y_coordinate_result = y_coordinate_needle_undetermined + y_coordinate_mswindow_undetermined;
                    break;
            }

            Logger.Log(nameof(Mouse), $"Found coordinate results to click {x_coordinate_result}, {y_coordinate_result})", Logger.LoggerPriority.MEDIUM);

            // Add 10px as padding because template matching selects the first pixel found
            // Another implementation is only selecting the middle of the image as a needle, coud be looked at
            x_coordinate_result += 10;
            y_coordinate_result += 10;

            // Set cursor position
            SetCursorPos(x_coordinate_result, y_coordinate_result);
        }

        private static (int,int) GetGameWindowCoordinates() {
            Process[] processes = Process.GetProcessesByName("MapleStory");
            //Process[] processes = Process.GetProcessesByName("Microsoft.Photos");
            Process maplestory = processes[0];
            IntPtr ptr = maplestory.MainWindowHandle;
            Rect MapleStoryRect = new Rect();
            GetWindowRect(ptr, ref MapleStoryRect);
            return (MapleStoryRect.Left, MapleStoryRect.Top);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public static void DoMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }


    }
}
