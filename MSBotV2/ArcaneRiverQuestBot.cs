using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSBotV2.TemplateMatching;

namespace MSBotV2
{
    public class ArcaneRiverQuestBot : QuestBot
    {
        // Should start attack orchestrator and seperate thread too somewhere. Orchestrator should be adjusted to specific events like quest completed and give callback 
        // in case a questpart has finished. In case of death, the questpart should be attempted again by returning to the designated map.

        private Thread ThreadOrchestrator = new Thread(new ThreadStart(() => { Orchestrator.Orchestrate(); }));
        private Thread ThreadTemplateMatching = new Thread(new ThreadStart(() => { Orchestrator.PollTemplateMatchingThread(); }));

        // List of quests that are activated and should be completed. If the quest is completed, it will be removed from the list.
        private Stack<QuestPart> ActivatedQuestParts = new Stack<QuestPart>();

        // Current quest part for reference
        private QuestPart CurrentQuestPart;

        // Map quest to string, so that it can be found 
        private Dictionary<QuestPart, List<ScriptItem>> QuestPartMapScripts = new Dictionary<QuestPart, List<ScriptItem>>() {
            {QuestPart.QUEST_200_TRANQUIL_ERDAS, FinishedScripts.SearchMapCaveDepths },
            {QuestPart.QUEST_50_TRANQUIL_ERDAS_SAMPLES, FinishedScripts.SearchMapCaveDepths },
            {QuestPart.QUEST_50_STONE_ERDAS_SAMPLES, FinishedScripts.SearchMapFireRockZone },
            {QuestPart.QUEST_50_JOYFUL_ERDAS_SAMPLES, FinishedScripts.SearchMapHiddenLakeShore },
            {QuestPart.QUEST_50_SOULFUL_ERDAS_SAMPLES, FinishedScripts.SearchMapHiddenFireZone },
            {QuestPart.QUEST_200_SAD_ERDAS, FinishedScripts.SearchMapLandOfSorrow }
        };

        private Dictionary<QuestPart, TemplateMatchingAction> QuestPartTemplateMatchingAction = new Dictionary<QuestPart, TemplateMatchingAction>() {
            {QuestPart.QUEST_200_TRANQUIL_ERDAS, TemplateMatchingAction.QUEST_200_TRANQUIL_ERDAS },
            {QuestPart.QUEST_50_TRANQUIL_ERDAS_SAMPLES, TemplateMatchingAction.QUEST_50_TRANQUIL_ERDAS_SAMPLES },
            {QuestPart.QUEST_50_STONE_ERDAS_SAMPLES, TemplateMatchingAction.QUEST_50_STONE_ERDAS_SAMPLES },
            {QuestPart.QUEST_50_JOYFUL_ERDAS_SAMPLES, TemplateMatchingAction.QUEST_50_JOYFUL_ERDAS_SAMPLES },
            {QuestPart.QUEST_50_SOULFUL_ERDAS_SAMPLES, TemplateMatchingAction.QUEST_50_SOULFUL_ERDAS_SAMPLES },
            {QuestPart.QUEST_200_SAD_ERDAS, TemplateMatchingAction.QUEST_200_SAD_ERDAS }
        };

        private QuestPartStatus QuestPartStatus;

        public static bool QuestPartCompleted = false;

        // The main method which will make sure the quests complete from beginning to end, so that some questbotmanager might start
        // different questbots. This function calls all other functions.
        public void ExecuteBot()
        {
            // Setup
            //StartQuestDialog();
            //DetectQuests();
            //FinishQuestDialog();

            ActivatedQuestParts.Push(QuestPart.QUEST_200_TRANQUIL_ERDAS);
            //ActivatedQuestParts.Push(QuestPart.QUEST_50_SOULFUL_ERDAS_SAMPLES);
            //ActivatedQuestParts.Push(QuestPart.QUEST_50_STONE_ERDAS_SAMPLES);

            // Execution
            QuestBotHandler();
        }

        // Main loop
        protected void QuestBotHandler()
        {

            // Pop initial questpart from stack
            CurrentQuestPart = ActivatedQuestParts.Pop();
            QuestPartStatus = QuestPartStatus.STARTING;


            for (; ; )
            {

                switch (QuestPartStatus)
                {

                    case QuestPartStatus.STARTING:
                        Logger.Log(nameof(ArcaneRiverQuestBot), $"New QuestPart [{CurrentQuestPart}] is starting."); ;

                        // Move to designated map
                        DynamicScriptBuilder.BuildNavigationDynamicScript(QuestPartMapScripts[CurrentQuestPart]).Invoke();

                        Logger.Log(nameof(ArcaneRiverQuestBot), $"Starting threads [{CurrentQuestPart}]."); ;

                        // Set ORCHESTRATOR_QUIT to false, otherwise Orchestrator will return immediately
                        Orchestrator.ORCHESTRATOR_QUIT = false;
                        QuestPartCompleted = false;

                        // Start threads
                        StartOrchestratorThreads();
                        QuestPartStatus = QuestPartStatus.RUNNING;

                        break;
                    case QuestPartStatus.RUNNING:

                        // Check if <prop> has been set, if so, remove Questpart from list and go to the next one
                        // Rotate questpart, remove from list and change questpartstatus. If all have finished, f12
                        if (QuestPartCompleted)
                        {
                            Logger.Log(nameof(ArcaneRiverQuestBot), $"Received callback. QuestPart [{CurrentQuestPart}] has been completed. {ActivatedQuestParts.Count} remaining."); ;
                            
                            // Stops thread by setting tread property to true, and then waits for the threads to finish
                            StopOrchestratorThreads();

                            Logger.Log(nameof(ArcaneRiverQuestBot), $"Moving to nearby town."); ;
                            new Core().RunDynamicScript(ScriptComposer.Compose(FinishedScripts.MoveToNearbyTown));

                            // Quest is completed
                            if (ActivatedQuestParts.Count == 0) {
                                QuestPartStatus = QuestPartStatus.FINISHED;
                                return;
                            }

                            QuestPartStatus = QuestPartStatus.STARTING;
                            CurrentQuestPart = ActivatedQuestParts.Pop();
                        }

                        break;
                    case QuestPartStatus.FINISHED:
                        Logger.Log(nameof(ArcaneRiverQuestBot), $"Arcane River daily quest is finished."); ;
                        return;
                }

                Logger.Log(nameof(ArcaneRiverQuestBot), $"Sleeping for 3 seconds."); ;
                Thread.Sleep(3000);
            }
        }

        // Detect the quests through template matching
        protected void DetectQuests()
        {

            foreach (KeyValuePair<QuestPart, TemplateMatchingAction> entry in QuestPartTemplateMatchingAction)
            {
                var resultTuple = TemplateMatch(entry.Value);

                // If TemplateMatching is true
                if (resultTuple.Item1)
                {
                    Logger.Log(nameof(ArcaneRiverQuestBot), $"Detected QuestPart [{entry.Key}]"); ;

                    // Add QuestPart to ActivatedQuestParts
                    ActivatedQuestParts.Push(entry.Key);
                }
            }

            if (ActivatedQuestParts.Count < 3)
            {
                // todo, lowimporta
            }
        }

        // Start the quest dialog until list of quests by means of DynamicScript
        protected void StartQuestDialog()
        {
            DynamicScriptBuilder.BuildStartArcaneRiverQuestStartingDialog().Invoke();
        }

        // Finish quest dialog by means of DynamicScript
        protected void FinishQuestDialog()
        {
            DynamicScriptBuilder.BuildFinishArcaneRiverQuestStartingDialog().Invoke();
        }

        protected void StartOrchestratorThreads()
        {
            Orchestrator.orchestratorType = OrchestratorType.QUEST;

            ThreadTemplateMatching = new Thread(new ThreadStart(() => { Orchestrator.PollTemplateMatchingThread(); }));
            ThreadOrchestrator = new Thread(new ThreadStart(() => { Orchestrator.Orchestrate(); }));

            ThreadTemplateMatching.Start();
            ThreadOrchestrator.Start();
        }

        protected void StopOrchestratorThreads()
        {
            Orchestrator.ORCHESTRATOR_QUIT = true;

            for (; ; )
            {

                if (!ThreadOrchestrator.IsAlive && !ThreadTemplateMatching.IsAlive)
                {
                    Logger.Log(nameof(ArcaneRiverQuestBot), $"Threads are dead, presuming");
                    break;
                }

                Logger.Log(nameof(ArcaneRiverQuestBot), $"Waiting for treads to be suspended, sleeping");
                Thread.Sleep(1000);
            }
        }

        public static void QuestArcaneRiverCompletedCallback()
        {
            QuestPartCompleted = true;
        }
    }
}
