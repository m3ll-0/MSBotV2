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
        public static bool UseScreenScaling = false;

        public static void SetCursorPosition(int x_coordinate_needle_undetermined, int y_coordinate_needle_undetermined) {

            // x and y-coordinates may be physical or scaled depending on when VM is ran or not.
            (int, int) mapleStoryWindowCoordinates = GetGameWindowCoordinates();

            int x_coordinate_mswindow_undetermined = mapleStoryWindowCoordinates.Item1;
            int y_coordinate_mswindow_undetermined = mapleStoryWindowCoordinates.Item2;

            Console.Write($"XmapleStoryWindowCoordinates: {mapleStoryWindowCoordinates.Item1} || {mapleStoryWindowCoordinates.Item2}");

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

            Console.Write($"Result: {x_coordinate_result} || {y_coordinate_result}");


            // Add 10px as padding because template matching selects the first pixel found
            // Another implementation is only selecting the middle of the image as a needle, coud be looked at
            x_coordinate_result += 10;
            y_coordinate_result += 10;

            // Is accurate when placed at 0,0 without notepadleft!)
            SetCursorPos(x_coordinate_result, y_coordinate_result);
        }

        private static (int,int) GetGameWindowCoordinates() {
            Process[] processes = Process.GetProcessesByName("Microsoft.Photos");
            Process maplestory = processes[0];
            IntPtr ptr = maplestory.MainWindowHandle;
            Rect MapleStoryRect = new Rect();
            GetWindowRect(ptr, ref MapleStoryRect);
            return (MapleStoryRect.Left, MapleStoryRect.Top);
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
