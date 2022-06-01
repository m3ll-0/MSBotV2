using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MSBotV2
{
    static class TemplateMatchingResult {
        private static List<(TemplateMatchingAction, List<ScriptItem>, List<ScriptItem>)> TemplateMatchingResults = new List<(TemplateMatchingAction, List<ScriptItem>, List<ScriptItem>)>()
        {
            new (TemplateMatchingAction.DEATH_SCREEN, FinishedScripts.Buff, FinishedScripts.Buff)
        };
    }

    public enum TemplateMatchingAction { 
        DEATH_SCREEN
    }

    static class TemplateMatchingActionFile
    {
        public static Dictionary<TemplateMatchingAction, string> templateMatchingActionFiles { get; set; } = new Dictionary<TemplateMatchingAction, string>()
        {
            { TemplateMatchingAction.DEATH_SCREEN, "needle_deathscreen.png" },
        };
    }

    public static class TemplateMatching
    {
        public static bool TemplateMatch(TemplateMatchingAction templateMatchingAction) {

            string imageDirectory = "C:/test/";
            string needleFileName = TemplateMatchingActionFile.templateMatchingActionFiles[templateMatchingAction];

            // HayStack
            var haystackSourceImagePath = Path.Combine(imageDirectory, "screenshot.png");
            Image<Bgr, byte> haystackSourceImage = new Image<Bgr, byte>(haystackSourceImagePath);
            var haystackSourceGrayscaleImage = haystackSourceImage.Convert<Gray, byte>();

            // Needle
            var needleTemplateImagePath = Path.Combine(imageDirectory, needleFileName); // static
            Image<Bgr, byte> needleTemplateImage = new Image<Bgr, byte>(needleTemplateImagePath);
            Image<Gray, byte> needleTemplateGrayscaleImage = needleTemplateImage.Convert<Gray, byte>();


            double Threshold = 0.8; //set it to a decimal value between 0 and 1.00, 1.00 meaning that the images must be identical

            Image<Gray, float> Matches = haystackSourceGrayscaleImage.MatchTemplate(needleTemplateGrayscaleImage, TemplateMatchingType.CcoeffNormed);   

            for (int y = 0; y < Matches.Data.GetLength(0); y++)
            {
                for (int x = 0; x < Matches.Data.GetLength(1); x++)
                {
                    if (Matches.Data[y, x, 0] >= Threshold) //Check if its a valid match
                    {
                        return true;
                    }
                }
            }

            return false;
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
