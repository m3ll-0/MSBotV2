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

            // Load the needles from disk initially to save IO reading
            foreach(TemplateMatchingAction templateMatchingAction in Enum.GetValues(typeof(TemplateMatchingAction)))
            {
                // For each existing TemplateMatchingAction, load image in memory
                string needleFilename = Config.TemplateMatchingConfig.TemplateMatchingActionFiles[templateMatchingAction];

                var needleTemplateImagePath = Path.Combine(Config.TemplateMatchingConfig.ImageDirectory, needleFilename);
                Image<Bgr, byte> needleTemplateImage = new Image<Bgr, byte>(needleTemplateImagePath);

                templateMatchingMemoryImage.Add(templateMatchingAction, needleTemplateImage);
            };
        }

        public static (bool, (int, int)) TemplateMatch(TemplateMatchingAction templateMatchingAction) {

            string needleFileName = Config.TemplateMatchingConfig.TemplateMatchingActionFiles[templateMatchingAction];

            // Create haystack (screenshot, but keep it in memory as optimization)
            Bitmap haystack = ScreenCapture.CaptureActiveWindow();
            //haystack.Save("C:/msbot/test.jpg", ImageFormat.Jpeg);

            // HayStack
            Image<Bgr, byte> haystackSourceImage = haystack.ToImage<Bgr, byte>();
            var haystackSourceGrayscaleImage = haystackSourceImage.Convert<Gray, byte>();

            // Needle
            Image<Gray, byte> needleTemplateGrayscaleImage = templateMatchingMemoryImage[templateMatchingAction].Convert<Gray, byte>();

            //set threshold, 1.00 meaning that the images must be identical
            double Threshold = Config.TemplateMatchingConfig.TemplateMatchingActionThreshold.GetValueOrDefault(templateMatchingAction) == 0 ? 0.8 : Config.TemplateMatchingConfig.TemplateMatchingActionThreshold.GetValueOrDefault(templateMatchingAction);
            //double Threshold = Config.TemplateMatchingConfig.TemplateMatchingActionThreshold.GetValueOrDefault(templateMatchingAction);
            //Console.WriteLine("XXXX");
            //Console.WriteLine(Threshold);

            Image<Gray, float>? Matches = null;

            try {
                Matches = haystackSourceGrayscaleImage.MatchTemplate(needleTemplateGrayscaleImage, TemplateMatchingType.CcoeffNormed);
            } catch (Exception e)
            { // Catch Emgu.CV.Util.CVException
                Logger.Log(nameof(TemplateMatching), e.Message);
                return (false, (0, 0));
            }

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

                        Logger.Log(nameof(TemplateMatching), $"A match was found for templateMatchingAction [{templateMatchingAction}] on coordinates: ({x},{y})");

                        // Break nested loop
                        goto LoopEnd;
                    }
                }
            }

            LoopEnd:

            if(!foundMatch) Logger.Log(nameof(TemplateMatching), $"No match was found for TemplateMatchingAction [{templateMatchingAction}])");

            if (foundMatch) {
                // Calculate relavitve offset if exists, if it doesn't exist, it defaults to (0,0) so it can be used regardless
                var relativeTemplateMatchingActionOffset = Config.TemplateMatchingConfig.TemplateMatchingActionRelativeOffset.GetValueOrDefault(templateMatchingAction);
                x_coordinate += relativeTemplateMatchingActionOffset.Item1;
                y_coordinate += relativeTemplateMatchingActionOffset.Item2;

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
            RUNE_ACTIVATED,
            RUNE_TIMEOUT,
            RUNE_TIMEOUT2,

            // Minimap
            MINIMAP_BORDER_LEFT,
            MINIMAP_BORDER_RIGHT,
            MINIMAP_PLAYER,
            MINIMAP_RUNE,
            MINIMAP_PORTAL,

            // General (Orchestrator)
            DEATH_SCREEN,
            PENALTY,
            INVENTORY_CASH,
            INVENTORY_PET,
            SPECTER_GAUGE_FULL,
            BOSS_CURSE,
            INVENTORY_HYPER_ROCK,
            HYPER_ROCK_MAP,
            HYPER_ROCK_MOVE_BUTTON,
            HYPER_ROCK_CONFIRM_BUTTON,

            // Quests
            QUEST_50_LYCKS_RECIPES,
            QUEST_200_BIGHORN_PINEDEERS,
            QUEST_200_GREEN_CATFISH,
            QUEST_MASTER_LYCK,
            QUEST_COMPLETED,

            QUEST_50_TRANQUIL_ERDAS_SAMPLES,
            QUEST_50_STONE_ERDAS_SAMPLES,
            QUEST_200_TRANQUIL_ERDAS,
            QUEST_50_JOYFUL_ERDAS_SAMPLES,
            QUEST_50_SOULFUL_ERDAS_SAMPLES,
            QUEST_200_SAD_ERDAS,
            QUEST_RONA,
            QUEST_RONA_DIALOG,

            // Map
            MAP_SEARCH_BAR,
            MAP_SEARCH_BUTTON,
            MAP_SELECTED,
            MAP_SEARCH_FOUND_ICON,
            MAP_CONFIRM

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

            if (Config.MouseConfig.UseScreenScaling) {
                rect.Left = (int)(rect.Left * Config.MouseConfig.draw_screen_factor);
                rect.Top = (int)(rect.Top * Config.MouseConfig.draw_screen_factor);
                rect.Right = (int)(rect.Right * Config.MouseConfig.draw_screen_factor);
                rect.Bottom = (int)(rect.Bottom * Config.MouseConfig.draw_screen_factor);
            }

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
