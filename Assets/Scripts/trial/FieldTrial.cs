using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using data;
using main;

namespace trial
{
    public class FieldTrial : AbstractTrial
    {
        private readonly InputField[] _fields;

        public FieldTrial(InputField[] fields) : base(-1, -1)
        {
            _fields = fields;

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                // Create a new GameObject and add WebGlFileLoader component to it
                var loaderObject = new GameObject("WebGlFileLoader");
                var webGlFileLoader = loaderObject.AddComponent<WebGlFileLoader>();

                webGlFileLoader.StartCoroutine(WebGlFileLoader.LoadFile("Config_ArrowMovement.json",
                    content =>
                    {
                        Debug.Log("File loaded successfully: " + content);
                        // Handle the file content here
                        Loader.ExternalActivation(content); // Assuming ExternalActivation can handle the content
                    },
                    error =>
                    {
                        Debug.LogError("Failed to load file: " + error);
                    }));
            }
            else
            {
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
                        Debug.Log("Opening file browser...");
                        string[] paths = StandaloneFileBrowser.OpenFilePanel("Choose configuration file", "Configuration_Files", "", false);
                        if (paths.Length == 0)
                        {
                            Debug.LogError("No file selected.");
                            continue;
                        }

                        string path = paths[0];
                        Debug.Log("Selected file: " + path);

                        if (Loader.ExternalActivation(path))
                        {
                            break;
                        }
                        else
                        {
                            Debug.LogError("Failed to activate configuration file.");
                        }
                    }
                }
            }

            TrialProgress = new TrialProgress();
        }

        private void GenerateTrials()
        {
            AbstractTrial currentTrial = this;
            foreach (var i in DataSingleton.GetData().BlockOrder)
            {
                var l = i - 1;
                var block = DataSingleton.GetData().Blocks[l];
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
                        var trialData = DataSingleton.GetData().Trials[k];

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

                    currentTrial = currentTrial.next;

                    newBlock = false;
                    tCnt++;
                }

                currentTrial.next = new CloseTrial(-1, -1);
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (StartButton.clicked)
            {
                var Field1Text = _fields[0].transform.GetComponentsInChildren<Text>()[1];
                TrialProgress.Field1 = Field1Text.text;

                DataSingleton.GetData().OutputFile = "ExperimentNo_" + ExperimentNumberGenerator.launchCount + "_" +
                                          "Frequency_" + TrialProgress.Field1 + "Hz_" +
                                          DateTime.Now.ToString("yyyy-MM-dd-HH.mm.ss") + ".csv";

                GenerateTrials();

                Loader.LogHeaders(); // Call static method directly on the class

                Progress();
            }
        }

        public override void Progress()
        {
            Loader.Get().CurrTrial = next;
            next.PreEntry(TrialProgress);
        }
    }
}
