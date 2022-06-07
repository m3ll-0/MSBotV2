using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSBotV2.TemplateMatching;

namespace MSBotV2
{
    public class ChuChuQuestBot : QuestBot
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
            {QuestPart.QUEST_200_BIGHORN_PINEDEERS, FinishedScripts.SearchMapMottledForest1 },
            {QuestPart.QUEST_50_LYCKS_RECIPES, FinishedScripts.SearchMapDealieBobberForest1 },
            {QuestPart.QUEST_200_GREEN_CATFISH, FinishedScripts.SearchMapMottledForest1 } // TODO: REMOVE FROM LIST
        };

        private Dictionary<QuestPart, TemplateMatchingAction> QuestPartTemplateMatchingAction = new Dictionary<QuestPart, TemplateMatchingAction>() {
                {QuestPart.QUEST_50_LYCKS_RECIPES, TemplateMatchingAction.QUEST_50_LYCKS_RECIPES },
                {QuestPart.QUEST_200_BIGHORN_PINEDEERS, TemplateMatchingAction.QUEST_200_BIGHORN_PINEDEERS },
                {QuestPart.QUEST_200_GREEN_CATFISH, TemplateMatchingAction.QUEST_200_GREEN_CATFISH }
            };

        private QuestPartStatus QuestPartStatus;

        public static bool QuestPartCompleted = false;

        // The main method which will make sure the quests complete from beginning to end, so that some questbotmanager might start
        // different questbots. This function calls all other functions.
        public void ExecuteBot() {
            // Setup
            //StartQuestDialog();
            //DetectQuests();
            //FinishQuestDialog();

            ActivatedQuestParts.Push(QuestPart.QUEST_200_BIGHORN_PINEDEERS);
            ActivatedQuestParts.Push(QuestPart.QUEST_50_LYCKS_RECIPES);
            ActivatedQuestParts.Push(QuestPart.QUEST_200_GREEN_CATFISH);

            // Execution
            QuestBotHandler();
        }

        // Main loop
        protected void QuestBotHandler() {

            // Pop initial questpart from stack
            CurrentQuestPart = ActivatedQuestParts.Pop();
            QuestPartStatus = QuestPartStatus.STARTING;
            

            for (; ; ) {

                switch (QuestPartStatus) {

                    case QuestPartStatus.STARTING:
                        Logger.Log(nameof(ChuChuQuestBot), $"New QuestPart [{CurrentQuestPart}] is starting."); ;

                        // Move to designated map
                        DynamicScriptBuilder.BuildNavigationDynamicScript(QuestPartMapScripts[CurrentQuestPart]).Invoke();

                        Logger.Log(nameof(ChuChuQuestBot), $"Starting threads [{CurrentQuestPart}]."); ;
                        
                        // Start threads
                        StartOrchestratorThreads();
                        QuestPartStatus = QuestPartStatus.RUNNING;

                        break;
                    case QuestPartStatus.RUNNING:

                        // Check if <prop> has been set, if so, remove Questpart from list and go to the next one
                        // Rotate questpart, remove from list and change questpartstatus. If all have finished, f12
                        if (QuestPartCompleted) {
                            Logger.Log(nameof(ChuChuQuestBot), $"Received callback. QuestPart [{CurrentQuestPart}] has been completed. {ActivatedQuestParts.Count} remaining."); ;
                            StopOrchestratorThreads();
                            
                            Logger.Log(nameof(ChuChuQuestBot), $"Moving to nearby town."); ;
                            new Core().RunDynamicScript(ScriptComposer.Compose(FinishedScripts.MoveToNearbyTown));

                            QuestPartStatus = QuestPartStatus.STARTING;
                            CurrentQuestPart = ActivatedQuestParts.Pop();
                        }

                        break;
                    case QuestPartStatus.FINISHED:
                        // Some ending scripts
                        break;
                }

                Logger.Log(nameof(ChuChuQuestBot), $"Sleeping for 3 seconds."); ;
                Thread.Sleep(3000);
            }
        }

        // Detect the quests through template matching
        protected void DetectQuests() {

            foreach (KeyValuePair<QuestPart, TemplateMatchingAction> entry in QuestPartTemplateMatchingAction)
            {
                var resultTuple = TemplateMatch(entry.Value);
                
                // If TemplateMatching is true
                if (resultTuple.Item1) {
                    Logger.Log(nameof(ChuChuQuestBot), $"Detected QuestPart [{entry.Key}]"); ;

                    // Add QuestPart to ActivatedQuestParts
                    ActivatedQuestParts.Push(entry.Key);
                }
            }

            if (ActivatedQuestParts.Count < 3) {
                // todo, lowimporta
            }
        }

        // Start the quest dialog until list of quests by means of DynamicScript
        protected void StartQuestDialog() {
            DynamicScriptBuilder.BuildStartChuChuQuestStartingDialog().Invoke();
        }

        // Finish quest dialog by means of DynamicScript
        protected void FinishQuestDialog() {
            DynamicScriptBuilder.BuildFinishChuChuQuestStartingDialog().Invoke();
        }

        protected void StartOrchestratorThreads() {
            Orchestrator.orchestratorType = OrchestratorType.QUEST;
            ThreadTemplateMatching.Start();
            ThreadOrchestrator.Start();
        }

        protected void StopOrchestratorThreads() {
            try
            {
                ThreadTemplateMatching.Suspend();
                ThreadOrchestrator.Suspend();
            }
            catch (Exception ex) { 
                // log ex
            }
        }

        public static void QuestChuChuCompletedCallback() {
            QuestPartCompleted = true;
        }
    }

    public enum QuestPart { 

        // Chu Chu daily quest
        QUEST_50_LYCKS_RECIPES,
        QUEST_200_BIGHORN_PINEDEERS,
        QUEST_200_GREEN_CATFISH,

        // Arcane River daily quest
        QUEST_50_TRANQUIL_ERDAS_SAMPLES,
        QUEST_50_STONE_ERDAS_SAMPLES,
        QUEST_200_TRANQUIL_ERDAS,
        QUEST_50_JOYFUL_ERDAS_SAMPLES,
        QUEST_50_SOULFUL_ERDAS_SAMPLES,
        QUEST_200_SAD_ERDAS

    }

    public enum QuestPartStatus { 
        STARTING,
        RUNNING,
        FINISHED
    }

    public interface QuestBot {
        public void ExecuteBot();
    }
}
