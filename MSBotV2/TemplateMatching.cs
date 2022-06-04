using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{

    public static class TemplateMatching
    {
        public static Dictionary<TemplateMatchingAction, Image<Bgr, byte>> templateMatchingMemoryImage { get; set; } = new Dictionary<TemplateMatchingAction, Image<Bgr, byte>>();

        public static void LoadNeedlesFromDiskToMemory() {
            string imageDirectory = "../../../img/";

            // Load the needles from disk initially to save IO reading
            foreach(TemplateMatchingAction templateMatchingAction in Enum.GetValues(typeof(TemplateMatchingAction)))
            {
                // For each existing TemplateMatchingAction, load image in memory
                string needleFilename = Config.TemplateMatchingConfig.templateMatchingActionFiles[templateMatchingAction];

                var needleTemplateImagePath = Path.Combine(imageDirectory, needleFilename);
                Image<Bgr, byte> needleTemplateImage = new Image<Bgr, byte>(needleTemplateImagePath);

                templateMatchingMemoryImage.Add(templateMatchingAction, needleTemplateImage);
            };
        }

        public static (bool, (int, int)) TemplateMatch(TemplateMatchingAction templateMatchingAction) {

            string needleFileName = Config.TemplateMatchingConfig.templateMatchingActionFiles[templateMatchingAction];

            // Create haystack (screenshot, but keep it in memory as optimization)
            var haystack = ScreenCapture.CaptureActiveWindow();

            // HayStack
            Image<Bgr, byte> haystackSourceImage = haystack.ToImage<Bgr, byte>();
            var haystackSourceGrayscaleImage = haystackSourceImage.Convert<Gray, byte>();

            // Needle
            Image<Gray, byte> needleTemplateGrayscaleImage = templateMatchingMemoryImage[templateMatchingAction].Convert<Gray, byte>();

            //set threshold, 1.00 meaning that the images must be identical
            double Threshold = 0.8; 

            Image<Gray, float> Matches = haystackSourceGrayscaleImage.MatchTemplate(needleTemplateGrayscaleImage, TemplateMatchingType.CcoeffNormed);
            bool foundMatch = false;
            
            // Get the physical coordinates
            int x_coordinate = 0;
            int y_coordinate = 0;

            for (int y = 0; y < Matches.Data.GetLength(0); y++)
            {
                for (int x = 0; x < Matches.Data.GetLength(1); x++)
                {
                    if (Matches.Data[y, x, 0] >= Threshold) //Check if its a valid match
                    {
                        foundMatch = true;
                        x_coordinate = x;
                        y_coordinate = y;

                        Logger.Log(nameof(TemplateMatching), $"A match was found for templateMatchingAction [{templateMatchingAction}] on coordinates: ({x},{y})", Logger.LoggerPriority.HIGH);

                        // Break nested loop
                        goto LoopEnd;
                    }
                }
            }

            LoopEnd:

            if(!foundMatch) Logger.Log(nameof(TemplateMatching), $"No match was found for TemplateMatchingAction [{templateMatchingAction}])", Logger.LoggerPriority.HIGH);

            if (foundMatch) {
                // Check if mouse click has to be performed
                switch (Config.TemplateMatchingConfig.TemplateMatchingMouseClicks[templateMatchingAction])
                {
                    case TemplateMatchingMouseClickType.MOUSE_CLICK_SINGLE:
                        Mouse.SetCursorPosition((x_coordinate, y_coordinate));
                        Thread.Sleep(300);
                        Mouse.DoMouseClick();
                        break;
                    case TemplateMatchingMouseClickType.MOUSE_CLICK_DOUBLE:
                        Mouse.SetCursorPosition((x_coordinate, y_coordinate));
                        Thread.Sleep(300);
                        Mouse.DoMouseClick();
                        Mouse.DoMouseClick();
                        break;
                    case TemplateMatchingMouseClickType.NONE:
                        break;
                }
            }

            return (foundMatch, (x_coordinate, y_coordinate));
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public enum TemplateMatchingMouseClickType
        {
            MOUSE_CLICK_SINGLE,
            MOUSE_CLICK_DOUBLE,
            NONE
        }

        public enum TemplateMatchingAction
        {
            DEATH_SCREEN,
            PENALTY,
            INVENTORY_CASH,
            INVENTORY_PET,
            SPECTER_GAUGE_FULL,
            BOSS_CURSE
        }
    }

    public class ScreenCapture
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static Image CaptureDesktop()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        public static Bitmap CaptureActiveWindow()
        {
            return CaptureWindow(GetForegroundWindow());
        }

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);

            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }
    }

}
