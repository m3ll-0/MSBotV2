using System.Diagnostics;
using static MSBotV2.TemplateMatching;

// todo: jump higher, if tracking not solved after x amount of seconds, absolve
// If rune not found, 99% will be due to verticality. Maybe implemented different strategies. dashcombo left, dashcombo right etc.
namespace MSBotV2
{
    public class RuneSolverBot
    {

        private (int, int) targetCoordinates;

        public RuneSolverResult Run() {

            Logger.Log(nameof(RuneSolverBot), $"Starting RuneSolverBot.");

            // Track until certain result
            RuneSolverResult runeSolverResult = Track(TemplateMatchingAction.MINIMAP_RUNE);

            if (runeSolverResult == RuneSolverResult.SUCCESS)
            {

                Logger.Log(nameof(RuneSolverBot), $"Attempting to activate rune dialog.");
                ActivateRuneDialog();


                // Detect rune
                Logger.Log(nameof(RuneSolverBot), $"Attempting to detect rune using AI.");
                string runeArrowStringResult = DetectRune();

                Logger.Log(nameof(RuneSolverBot), $"Handling rune result.");

                // Handle rune arrow string result
                runeSolverResult = HandleRuneResult(runeArrowStringResult);

                // If AI arrow detection error was thrown, return immediately
                if (runeSolverResult == RuneSolverResult.ERROR_AI_ARROW_DETECTION) return runeSolverResult;

                Logger.Log(nameof(RuneSolverBot), $"Checking if AI passed. Attempting to detect rune activation status after sleeping 1 second.");
                Thread.Sleep(1000);

                // Attempt to detect activation result
                runeSolverResult = DetectRuneActivationResult();

                //return runeActivationResult;
                return runeSolverResult;
            }
            else {
                Logger.Log(nameof(RuneSolverBot), $"Tracking rune failed with error [{runeSolverResult}]. Returning.");
                return runeSolverResult;
            }
        }

        public RuneSolverResult Track(TemplateMatchingAction targetTemplateMatchingAction) {

            // Get target results once
            var targetResult = TemplateMatch(targetTemplateMatchingAction);
            targetCoordinates = targetResult.Item2;
            targetCoordinates.Item1 -= 2; // Margin of -2
            targetCoordinates.Item2 -= 2; // Margin of -2

            if (!targetResult.Item1)
            {
                Logger.Log(nameof(RuneSolverBot), $"Error: Failed to find target coordinates.");
                return RuneSolverResult.ERROR_PLAYER_COORDS;
            }

            // Keep track of results
            RuneSolverResult trackingResultHorizontally;
            RuneSolverResult trackingResultVertically;

            // Keep track of time
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Main loop
            while (true) {

                if (sw.Elapsed.TotalMilliseconds > 20000) {
                    Logger.Log(nameof(RuneSolverBot), $"Error: Tracking timeout reached.");
                    return RuneSolverResult.ERROR_TRACKING_TIMEOUT;
                }

                trackingResultHorizontally = TrackHorizontally();

                if (trackingResultHorizontally == RuneSolverResult.SUCCESS)
                {
                    Logger.Log(nameof(RuneSolverBot), $"Finished horizontal tracking.");

                    // track vertically
                    trackingResultVertically = TrackVertically();

                    if (trackingResultVertically == RuneSolverResult.SUCCESS)
                    {
                        Logger.Log(nameof(RuneSolverBot), $"Finished vertical tracking.");
                        Logger.Log(nameof(RuneSolverBot), $"Arrived at destination.");
                        return trackingResultVertically;
                    }
                }
                else if(trackingResultHorizontally != RuneSolverResult.SUCCESS && trackingResultHorizontally != RuneSolverResult.TRACKING) {
                    Logger.Log(nameof(RuneSolverBot), $"Some error has occurred, see log.");
                    return trackingResultHorizontally;
                }
            }
        }

        private RuneSolverResult TrackHorizontally() {
            while (true)
            {
                // Get player results every cycle
                var playerResult = TemplateMatch(TemplateMatchingAction.MINIMAP_PLAYER);

                if (!playerResult.Item1)
                {
                    Logger.Log(nameof(RuneSolverBot), $"Error: Failed to find player coordinates.");
                    return RuneSolverResult.ERROR_PLAYER_COORDS;
                }

                // Get coordinates
                var playerCoordinates = playerResult.Item2;

                return ParseCoordinatesToHorizontalAction(playerCoordinates);
            }
        }

        private RuneSolverResult ParseCoordinatesToHorizontalAction((int, int) playerCoordinates) {
            // If negative, player is to the left of target
            // If positive, player is to the right of target
            int x_difference = playerCoordinates.Item1 - targetCoordinates.Item1;
            int x_difference_absolute = Math.Abs(x_difference);

            Console.WriteLine("Hordiff: " + x_difference);

            MovementDirection movementDirection = x_difference > 0 ? MovementDirection.LEFT : MovementDirection.RIGHT;

            var rangeEnumerator = HorizontalRangeMovementActions.GetEnumerator();
            HorizontalMovementType horizontalMovementType = HorizontalMovementType.NONE;

            while (rangeEnumerator.MoveNext())
            {
                if (x_difference_absolute >= rangeEnumerator.Current.Key.Item1 && x_difference_absolute <= rangeEnumerator.Current.Key.Item2) {
                    horizontalMovementType = rangeEnumerator.Current.Value;
                }
            }

            if (horizontalMovementType == HorizontalMovementType.NONE)
            {
                Logger.Log(nameof(RuneSolverBot), $"Error: Failed to find horizontal range.");
                return RuneSolverResult.ERROR_HORIZONTAL_MOVEMENT_RANGE;
            }
            else if (horizontalMovementType == HorizontalMovementType.SUCCESS) {
                return RuneSolverResult.SUCCESS;
            }

            Logger.Log(nameof(RuneSolverBot), $"Running horizontal movement script.");

            List<ScriptItem> horizontalMovementScriptToExecute = HorizontalMovementTypeScript[(horizontalMovementType, movementDirection)];
            Core core = new Core();
            core.RunScript(ScriptComposer.Compose(horizontalMovementScriptToExecute));

            return RuneSolverResult.TRACKING;
        }

        private RuneSolverResult TrackVertically()
        {
            while (true)
            {
                // Get player results every cycle
                var playerResult = TemplateMatch(TemplateMatchingAction.MINIMAP_PLAYER);

                if (!playerResult.Item1)
                {
                    Logger.Log(nameof(RuneSolverBot), $"Error: Failed to find player coordinates.");
                    return RuneSolverResult.ERROR_PLAYER_COORDS;
                }

                // Get coordinates
                var playerCoordinates = playerResult.Item2;

                return ParseCoordinatesToVerticalAction(playerCoordinates);
            }
        }

        private RuneSolverResult ParseCoordinatesToVerticalAction((int, int) playerCoordinates)
        {
            // If negative, player is to the bottom of target
            // If positive, player is to the top of target
            int y_difference = playerCoordinates.Item2 - targetCoordinates.Item2;
            int y_difference_absolute = Math.Abs(y_difference);

            Console.WriteLine("Vert diff: " + y_difference);

            MovementDirection movementDirection = y_difference > 0 ? MovementDirection.UP : MovementDirection.DOWN;

            var rangeEnumerator = VerticalRangeMovementActions.GetEnumerator();
            VerticalMovementType verticalMovementType = VerticalMovementType.NONE;

            while (rangeEnumerator.MoveNext())
            {
                if (y_difference_absolute >= rangeEnumerator.Current.Key.Item1 && y_difference_absolute <= rangeEnumerator.Current.Key.Item2)
                {
                    verticalMovementType = rangeEnumerator.Current.Value;
                }
            }

            if (verticalMovementType == VerticalMovementType.NONE)
            {
                Logger.Log(nameof(RuneSolverBot), $"Error: Failed to find vertical range.");
                return RuneSolverResult.ERROR_VERTICAL_MOVEMENT_RANGE;
            }
            else if (verticalMovementType == VerticalMovementType.SUCCESS)
            {
                return RuneSolverResult.SUCCESS;
            }

            Logger.Log(nameof(RuneSolverBot), $"Running vertical movement script.");

            List<ScriptItem> verticalMovementScriptToExecute = VerticalMovementTypeScript[(verticalMovementType, movementDirection)];
            Core core = new Core();
            core.RunScript(ScriptComposer.Compose(verticalMovementScriptToExecute));

            return RuneSolverResult.TRACKING;
        }

        private void ActivateRuneDialog() {

            // Activate rune
            Thread.Sleep(500);
            Logger.Log(nameof(RuneSolverBot), $"Activating rune dialog.");
            Core core = new Core();
            core.RunScript(ScriptComposer.Compose(FinishedScripts.RuneActivate.Concat(FinishedScripts.RuneActivatePause).ToList()));
        }

        private string DetectRune() {
            return RuneSolverServerCommunicator.ConsultRuneService();
        }

        private RuneSolverResult HandleRuneResult(string runeResult)
        {
            if (runeResult.Length != 4)
            {
                // Error path
                Logger.Log(nameof(RuneSolverBot), $"Detected that rune result length is not equal to 4. Rune result instead is: [{runeResult}]");
                return RuneSolverResult.ERROR_AI_ARROW_DETECTION;
            }
            else {
                // Succes path
                Logger.Log(nameof(RuneSolverBot), $"Translate rune result [{runeResult}] to keyPresses.");

                // Translates the rune result and executes the script
                TranslateRuneResultToScript(runeResult);
                return RuneSolverResult.SUCCESS;
            }
        }

        private void TranslateRuneResultToScript(string runeResult) {
            Core core = new Core();

            foreach (char runeResultChar in runeResult) {

                List<ScriptItem> scriptItems = new List<ScriptItem>();

                switch (runeResultChar) {
                    case 'l':
                        // Press left
                        scriptItems = FinishedScripts.RunePressLeft;
                        break;

                    case 'r':
                        // Press right
                        scriptItems = FinishedScripts.RunePressRight;
                        break;

                    case 'u':
                        scriptItems = FinishedScripts.RunePressUp;
                        break;

                    case 'd':
                        scriptItems = FinishedScripts.RunePressDown;
                        break;
                }

                // Attempt to activate rune
                core.RunScript(ScriptComposer.Compose(scriptItems));
                Thread.Sleep(120);
            }
        }

        public RuneSolverResult DetectRuneActivationResult() {
            var templateMatchingResult = TemplateMatch(TemplateMatchingAction.RUNE_ACTIVATED);

            if (templateMatchingResult.Item1)
            {
                Logger.Log(nameof(RuneSolverBot), $"Rune activation detected.");
                return RuneSolverResult.SUCCESS;
            }
            else {
                Logger.Log(nameof(RuneSolverBot), $"Error: Rune activation not detected.");
                return RuneSolverResult.ERROR_RUNE_ACTIVATION_DETECTION;
            }
        }

        public static Dictionary<(int,int), HorizontalMovementType> HorizontalRangeMovementActions { get; set; } = new Dictionary<(int,int), HorizontalMovementType>()
        {
            { (0,2) , HorizontalMovementType.SUCCESS},
            { (3,50) , HorizontalMovementType.MOVE_MEDIUM},
            { (51,1000) , HorizontalMovementType.MOVE_LONG},
        };

        public static Dictionary<(int, int), VerticalMovementType> VerticalRangeMovementActions { get; set; } = new Dictionary<(int, int), VerticalMovementType>()
        {
            { (0,1) , VerticalMovementType.SUCCESS},
            { (2,1000) , VerticalMovementType.MOVE_LONG},
        };

        public static Dictionary<(HorizontalMovementType, MovementDirection), List<ScriptItem>> HorizontalMovementTypeScript { get; set; } = new Dictionary<(HorizontalMovementType, MovementDirection), List<ScriptItem>>()
        {
            {(HorizontalMovementType.MOVE_MEDIUM, MovementDirection.LEFT), FinishedScripts.MoveLeftMedium},
            {(HorizontalMovementType.MOVE_MEDIUM, MovementDirection.RIGHT), FinishedScripts.MoveRightMedium},

            {(HorizontalMovementType.MOVE_LONG, MovementDirection.LEFT), FinishedScripts.MoveLeftLong},
            {(HorizontalMovementType.MOVE_LONG, MovementDirection.RIGHT), FinishedScripts.MoveRightLong},
        };

        public static Dictionary<(VerticalMovementType, MovementDirection), List<ScriptItem>> VerticalMovementTypeScript { get; set; } = new Dictionary<(VerticalMovementType, MovementDirection), List<ScriptItem>>()
        {
            {(VerticalMovementType.MOVE_LONG, MovementDirection.UP), FinishedScripts.MoveUp},
            {(VerticalMovementType.MOVE_LONG, MovementDirection.DOWN), FinishedScripts.MoveDown},
        };

    }

    public enum HorizontalMovementType {
        NONE,
        SUCCESS,
        MOVE_SHORT,
        MOVE_MEDIUM,
        MOVE_LONG,
    }

    public enum VerticalMovementType
    {
        NONE,
        SUCCESS,
        MOVE_SHORT,
        MOVE_MEDIUM,
        MOVE_LONG,
    }


    public enum MovementDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public enum RuneSolverResult
    {
        SUCCESS,
        TRACKING,
        ERROR_PLAYER_COORDS,
        ERROR_VERTICAL_MOVEMENT_RANGE,
        ERROR_HORIZONTAL_MOVEMENT_RANGE,
        ERROR_AI_ARROW_DETECTION,
        ERROR_RUNE_ACTIVATION_DETECTION,
        ERROR_TRACKING_TIMEOUT
    }
}
