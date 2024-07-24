using main;
using SFB;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

using Debug = UnityEngine.Debug;
using DS = data.DataSingleton;

namespace trial
{
    public class FieldTrial : AbstractTrial
    {
        private readonly InputField[] _fields;
        private EyeTrackingReciever _eyeTrackingReciever; // Reference to eye tracking receiver

        public FieldTrial(InputField[] fields) : base(-1, -1)
        {
            _fields = fields;

            var AutoRunConfigDir = Application.streamingAssetsPath + "/AutoRun_Config/";
            var files = new string[0];
            if (Directory.Exists(AutoRunConfigDir))
            {
                files = Directory.GetFiles(AutoRunConfigDir);
            }

            if (files.Length > 0)
            {
                var defaultConfig = AutoRunConfigDir + Path.GetFileName(files[0]);
                Loader.ExternalActivation(defaultConfig);
            }
            else
            {
                while (true)
                {
                    string[] paths = StandaloneFileBrowser.OpenFilePanel("Choose configuration file", "Configuration_Files", "", false);
                    string path = paths[0];
                    if (Loader.ExternalActivation(path)) break;
                }
            }

            TrialProgress = new TrialProgress();
            _fields = fields;

            // Initialize Eye Tracking Receiver
            _eyeTrackingReciever = GameObject.FindObjectOfType<EyeTrackingReciever>();
            if (_eyeTrackingReciever == null)
            {
                Debug.LogError("EyeTrackingReciever not found in the scene.");
            }
        }

        private void GenerateTrials()
        {
            Debug.Log("Generating Trials...");
            AbstractTrial currentTrial = this;
            foreach (var i in DS.GetData().BlockOrder)
            {
                var l = i - 1;
                var block = DS.GetData().Blocks[l];
                var newBlock = true;
                AbstractTrial currHead = null;

                var tCnt = 0;
                foreach (var j in block.TrialOrder)
                {
                    var k = j - 1;
                    AbstractTrial t;

                    if (k < 0)
                    {
                        t = new RandomTrial(l, k);
                    }
                    else
                    {
                        var trialData = DS.GetData().Trials[k];

                        if (trialData.FileLocation != null)
                        {
                            Debug.Log("Creating new Instructional Trial");
                            t = new InstructionalTrial(l, k);
                        }
                        else if (trialData.TwoDimensional == 1)
                        {
                            Debug.Log("Creating new 2D Screen Trial");
                            t = new TwoDTrial(l, k);
                        }
                        else
                        {
                            Debug.Log("Creating new 3D Screen Trial");
                            t = new ThreeDTrial(l, k);
                        }
                    }
                    if (newBlock) currHead = t;

                    t.isTail = tCnt == block.TrialOrder.Count - 1;
                    t.head = currHead;

                    currentTrial.next = t;
                    Debug.Log($"Linking trial {currentTrial} to {t}");

                    currentTrial = currentTrial.next;

                    newBlock = false;
                    tCnt++;
                }

                currentTrial.next = new CloseTrial(-1, -1);
                Debug.Log($"End of block {l}. Closing trial with {currentTrial.next}");
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Loader.Get().CurrTrial == null)
            {
                Debug.LogError("CurrTrial is null. Check the assignment in FieldTrial.");
                return;
            }

            if (_eyeTrackingReciever != null)
            {
                // Retrieve gaze data from the eye tracking receiver
                var gazeData = _eyeTrackingReciever.GetGazeData();

                // Process gaze data to determine player movement or actions
                ProcessGazeData(gazeData);
            }

            if (StartButton.clicked)
            {
                var Field1Text = _fields[0].transform.GetComponentsInChildren<Text>()[1];
                TrialProgress.Field1 = Field1Text.text;

                DS.GetData().OutputFile = "ExperimentNo_" + ExperimentNumberGenerator.launchCount + "_" +
                                          "Frequency_" + TrialProgress.Field1 + "Hz_" +
                                          DateTime.Now.ToString("yyyy-MM-dd-HH.mm.ss") + ".csv";

                GenerateTrials();

                Loader.LogHeaders();

                Progress();
            }
        }

        private void ProcessGazeData(GazeData gazeData)
        {
            if (gazeData == null)
            {
                Debug.LogWarning("Gaze data is null.");
                return;
            }

            // Example grid boundaries for a 3x3 grid
            int xIndex = Mathf.Clamp((int)(gazeData.docX * 3), 0, 2);
            int yIndex = Mathf.Clamp((int)(gazeData.docY * 3), 0, 2);

            switch (xIndex, yIndex)
            {
                case (0, 1):
                    MoveForward();
                    break;
                case (1, 0):
                    MoveLeft();
                    break;
                case (1, 2):
                    MoveRight();
                    break;
                case (2, 1):
                    MoveBackward();
                    break;
                case (1, 1):
                    PerformAction();
                    break;
                case (0, 2):
                    OpenSettingsMenu();
                    break;
                default:
                    break;
            }
        }

        private void MoveForward() { /* Implement move forward logic */ }
        private void MoveLeft() { /* Implement move left logic */ }
        private void MoveRight() { /* Implement move right logic */ }
        private void MoveBackward() { /* Implement move backward logic */ }
        private void PerformAction() { /* Implement action logic */ }
        private void OpenSettingsMenu() { /* Implement settings menu logic */ }

        public override void Progress()
        {
            Loader.Get().CurrTrial = next;
            if (next != null)
            {
                next.PreEntry(TrialProgress);
            }
            else
            {
                Debug.LogError("Next trial is null. Ensure that 'next' is properly assigned.");
            }
        }

    }
}
