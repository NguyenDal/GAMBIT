using System;
using System.Collections.Generic;
using System.Threading;
using trial;
using UnityEngine;
using UnityEngine.UI;
using data;
using DS = data.DataSingleton;
using E = main.Loader;
using Random = UnityEngine.Random;
using System.Collections;
using UnityEngine.Analytics;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace wallSystem
{
    public class PlayerController : MonoBehaviour
    {
        public Camera Cam;
        private GenerateGenerateWall _gen;
        public CharacterController _controller;
        private Vector3 _moveDirection = Vector3.zero;
        private float _currDelay;
        private float _iniRotation;
        private float _waitTime;
        private bool _playingSound;
        private bool _isStarted = false;
        private bool _reset;
        private int localQuota;
        public bool firstperson;
        private GameObject participant; // Corrected variable name
        public respawn respawn;
        public Animator animator;
        private PlayerMovementWithKeyboard movementScript;

        private void Start()
        {
            
            firstperson = PlayerPrefs.GetInt("FirstPersonEnabled", 0) == 1;
            //For the 3 star system, start by assumming the player will complete level successfully within time limit. TimeouttableTrial will change the LevelCompleted
            //PlayerPref to 0 if the opposite case
            if (PlayerPrefs.GetFloat("BestTime" + SceneManager.GetActiveScene().name) != 0)
            {
                //Print Current Level's best time
                Debug.Log("Current Level Best Time: " + PlayerPrefs.GetFloat("BestTime" + SceneManager.GetActiveScene().name));
            }
            else if(PlayerPrefs.GetFloat("BestTime" + SceneManager.GetActiveScene().name) == 0)
            {
                //Create best time and set it as current level's best time
                Debug.Log("The Current level does not have a best time! creating one .....");
                PlayerPrefs.SetFloat("BestTime" + SceneManager.GetActiveScene().name, 400000);
                PlayerPrefs.SetFloat(
                    "CurrentBestTime" + SceneManager.GetActiveScene().name,
                    PlayerPrefs.GetFloat("BestTime" + SceneManager.GetActiveScene().name, 200000)
                    );
                Debug.Log("Current Level Best Time Created: " + PlayerPrefs.GetFloat("BestTime" + SceneManager.GetActiveScene().name));
            }
            if (PlayerPrefs.GetInt("StarsAwardedForPreviousLevel") != 0) {
                Debug.Log("Stars awarded for previous level: " + PlayerPrefs.GetInt("StarsAwardedForPreviousLevel"));
            }

            //storing level name to make use of it in LevelCompleteDisplayScene
            PlayerPrefs.SetString("previousLevelName", SceneManager.GetActiveScene().name);

            PlayerPrefs.SetInt("LevelCompleted", 1);
            PlayerPrefs.SetInt("PlayerCollecdtedAllPickUps", 0);
            PlayerPrefs.Save();
            string log = "Started with level completed: " + PlayerPrefs.GetInt("LevelCompleted");
            Debug.Log(log);

            participant = this.gameObject;

            if (firstperson)
            {
                Cam = this.transform.Find("FirstPerson Camera").gameObject.GetComponent<Camera>();
                transform.Find("FirstPerson Camera").gameObject.SetActive(true);
            }

            try
            {
                var trialText = GameObject.Find("TrialText").GetComponent<Text>();
                var blockText = GameObject.Find("BlockText").GetComponent<Text>();
                var currBlockId = E.Get().CurrTrial.BlockID;
                trialText.text = E.Get().CurrTrial.trialData.DisplayText;
                blockText.text = DS.GetData().Blocks[currBlockId].DisplayText;

                if (!string.IsNullOrEmpty(E.Get().CurrTrial.trialData.DisplayImage))
                {
                    var filePath = DS.GetData().SpritesPath + E.Get().CurrTrial.trialData.DisplayImage;
                    var displayImage = GameObject.Find("DisplayImage").GetComponent<RawImage>();
                    displayImage.enabled = true;
                    displayImage.texture = Img2Sprite.LoadTexture(filePath);
                }

            }
            catch (NullReferenceException e)
            {
                UnityEngine.Debug.LogWarning("Goal object not set: running an instructional trial");
            }

            Random.InitState(DateTime.Now.Millisecond);
            _currDelay = 0;

            if (E.Get().CurrTrial.trialData.StartFacing == -1)
            {
                _iniRotation = Random.Range(0, 360);
            }
            else
            {
                _iniRotation = E.Get().CurrTrial.trialData.StartFacing;
            }

            transform.Rotate(0, _iniRotation, 0);

            try
            {
                _controller = GetComponent<CharacterController>();
                _gen = GameObject.Find("WallCreator").GetComponent<GenerateGenerateWall>();
                Cam.transform.Rotate(0, 0, 0);
            }
            catch (NullReferenceException e)
            {
                UnityEngine.Debug.LogWarning("Can't set controller object: running an instructional trial");
            }
            _waitTime = E.Get().CurrTrial.trialData.Rotate;
            _reset = false;
            localQuota = E.Get().CurrTrial.trialData.Quota;
            Debug.Log("localQuota: " + localQuota);

            TrialProgress.GetCurrTrial().TrialProgress.TrialNumber++;
            TrialProgress.GetCurrTrial().TrialProgress.Instructional = TrialProgress.GetCurrTrial().trialData.Instructional;
            TrialProgress.GetCurrTrial().TrialProgress.EnvironmentType = TrialProgress.GetCurrTrial().trialData.Scene;
            TrialProgress.GetCurrTrial().TrialProgress.CurrentEnclosureIndex = TrialProgress.GetCurrTrial().trialData.Enclosure - 1;
            TrialProgress.GetCurrTrial().TrialProgress.BlockID = TrialProgress.GetCurrTrial().BlockID;
            TrialProgress.GetCurrTrial().TrialProgress.TrialID = TrialProgress.GetCurrTrial().TrialID;
            TrialProgress.GetCurrTrial().TrialProgress.TwoDim = TrialProgress.GetCurrTrial().trialData.TwoDimensional;
            TrialProgress.GetCurrTrial().TrialProgress.LastX = TrialProgress.GetCurrTrial().TrialProgress.TargetX;
            TrialProgress.GetCurrTrial().TrialProgress.LastY = TrialProgress.GetCurrTrial().TrialProgress.TargetY;
            TrialProgress.GetCurrTrial().TrialProgress.TargetX = 0;
            TrialProgress.GetCurrTrial().TrialProgress.TargetY = 0;

            _isStarted = true;
        }

        public void ExternalStart(float pickX, float pickY, bool useEnclosure = false)
        {
            while (!_isStarted)
            {
                Thread.Sleep(20);
            }

            UnityEngine.Debug.Log($"ExternalStart called with pickX={pickX}, pickY={pickY}, useEnclosure={useEnclosure}");
            UnityEngine.Debug.Log($"StartPosition count: {E.Get().CurrTrial.trialData.StartPosition.Count}");

            TrialProgress.GetCurrTrial().TrialProgress.TargetX = pickX;
            TrialProgress.GetCurrTrial().TrialProgress.TargetY = pickY;

            if (E.Get().CurrTrial.trialData.StartPosition.Count == 0)
            {
                var i = 0;
                while (i++ < 100)
                {
                    var CurrentTrialRadius = DS.GetData().Enclosures[E.Get().CurrTrial.TrialProgress.CurrentEnclosureIndex].Radius;
                    var v = Random.insideUnitCircle * CurrentTrialRadius * 0.9f;
                    var mag = Vector3.Distance(v, new Vector2(pickX, pickY));
                    if (mag > DS.GetData().CharacterData.DistancePickup)
                    {
                        if (!firstperson)
                        {
                            transform.position = new Vector3(v.x, 0.5f, v.y);
                            var camPos = Cam.transform.position;
                            camPos.y = DS.GetData().CharacterData.Height;
                            Cam.transform.position = camPos;
                            return;
                        }
                    }
                }
                UnityEngine.Debug.LogError("Could not randomly place player. Probably due to" +
                               " a pick up location setting");
            }
            else
            {
                var p = E.Get().CurrTrial.trialData.StartPosition;

                if (useEnclosure)
                {
                    p = new List<float>() { pickX, pickY };
                }

                UnityEngine.Debug.Log($"StartPosition values: {string.Join(", ", p)}");

                if (p.Count >= 2)
                {
                    if (!firstperson)
                    {
                        transform.position = new Vector3(p[0], 0.5f, p[1]);
                        var camPos = Cam.transform.position;
                        camPos.y = DS.GetData().CharacterData.Height;
                        Cam.transform.position = camPos;
                    }
                }
                else
                {
                    Debug.LogError("StartPosition does not contain enough elements.");
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Pickup")) return;
            
            if(other.name == "FlagGoal(Clone)"){
                StartCoroutine(WaitForVictoryAnimation());
            }

            GetComponent<AudioSource>().PlayOneShot(other.gameObject.GetComponent<AudioSource>().clip, 1);
            Destroy(other.gameObject);

            // Set the checkpoint at the coin's position
            respawn.SetCheckpoint(other.transform.position);

            int BlockID = TrialProgress.GetCurrTrial().BlockID;

            if (BlockID < TrialProgress.GetCurrTrial().TrialProgress.NumCollectedPerBlock.Length)
            {
                TrialProgress.GetCurrTrial().TrialProgress.NumCollectedPerBlock[BlockID]++;
            }
            else
            {
                UnityEngine.Debug.LogError("BlockID is out of range of NumCollectedPerBlock.");
            }

            TrialProgress.GetCurrTrial().NumCollected++;
            E.LogData(
                TrialProgress.GetCurrTrial().TrialProgress,
                TrialProgress.GetCurrTrial().TrialStartTime,
                transform,
                1
            );

            if (--localQuota > 0)
            {

                return;
            }

            E.Get().CurrTrial.Notify();
            PlayerPrefs.SetInt("PlayerCollecdtedAllPickUps", 1);
            Debug.Log("Collected everything for the level!");
            
            PlayerPrefs.Save();
            _playingSound = true;
        }

        public void ComputeMovement(float rotationInput, float movementInput, String keyImput)
        {
            if (localQuota <= 0 && E.Get().CurrTrial.trialData.Quota != 0)
            {
                return;
            }

            var rotation = rotationInput * DS.GetData().CharacterData.RotationSpeed * Time.deltaTime;
            _moveDirection = transform.forward * movementInput * DS.GetData().CharacterData.MovementSpeed;
            const double tolerance = 0.0001;

            if (Math.Abs(Mathf.Abs(rotation)) < tolerance)
                _controller.Move(_moveDirection * Time.deltaTime);

            transform.Rotate(0, rotation, 0);
        }

        private void Update()
        {
            if (participant.transform.position.y < -1) // Corrected variable name
            {
                respawn.Respawn();
            }
            E.LogData(TrialProgress.GetCurrTrial().TrialProgress, TrialProgress.GetCurrTrial().TrialStartTime, transform);

            if (_playingSound)
            {
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    StartCoroutine(WaitForVictoryAnimation());
                    _playingSound = false;
                }
            }

            if (_currDelay < _waitTime)
            {
                var angle = 360f * _currDelay / _waitTime + _iniRotation - transform.rotation.eulerAngles.y;
                transform.Rotate(new Vector3(0, angle, 0));
            }
            else
            {
                if (!_reset)
                {
                    Cam.transform.Rotate(0, 0, 0);
                    _reset = true;
                    TrialProgress.GetCurrTrial().ResetTime();
                }

                try
                {
                    // ComputeMovement is now called from inputHandler
                    // Original code below
                    // ComputeMovement();
                    // There is no call from inside inputHandler. Need to figure out the correct combination of inputs to make this work, below is not it.
                    // left in so that code can be run to view the impact.
                    // ComputeMovement(0, -1, "down");
                }
                catch (MissingComponentException e)
                {
                    UnityEngine.Debug.LogWarning("Skipping movement calc: instructional trial");
                }
            }
            //keeps track of current level time
            _currDelay += Time.deltaTime;
            PlayerPrefs.SetFloat("CurrentBestTime" + SceneManager.GetActiveScene().name, _currDelay);
            PlayerPrefs.Save();
        }

        public IEnumerator WaitForVictoryAnimation(){
            animator.SetBool("IsAtGoal",true);
            
            // Wait for the death animation to finish
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            while (!(stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 1.0f)){
                yield return null;
                stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            }
            animator.SetBool("IsAtGoal",false);
            TrialProgress.GetCurrTrial().Progress();
            
            yield break;
        }
    }
}
