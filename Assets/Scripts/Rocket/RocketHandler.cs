using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PocketRocket
{
    public class RocketHandler : MonoBehaviour
    {
        [Serializable]
        public class View
        {
            // View Key
            public string key;

            // View Object
            public GameObject gameObject;

            // Constructor
            public View(string key, GameObject gameObject)
            {
                this.key = key;
                this.gameObject = gameObject;
            }

            // Returns The View Key
            public string Key
            {
                get
                {
                    return key;
                }
            }

            // Get Game Object
            public GameObject GetGameObject()
            {
                return gameObject;
            }
        }

        // Base Score
        public static readonly int BASE_SCORE = 100;

        // Warning Time And Limit
        public static readonly double WARNING_TIME = 1.5d, WARNING_LIMIT = 10.0f;

        // View Keys
        public static readonly string MENU_VIEW = "Menu", GAME_VIEW = "Game", LOSE_VIEW = "Lose";

        // Rocket Handler Instance
        private static RocketHandler instance;

        [Header("Rocket UIs")]
        // Score Label
        public Text score;

        // Highscore Label
        public Text highscore;

        [Space(10)]
        // Views
        public List<View> views = new List<View>();

        [Header("Rocket Settings")]
        // Rocket Object / Prefab
        public GameObject rocket;

        // Warning Display Update Time
        private double displayTime;

        // Enemy Spawn Time
        private double enemyTime;

        // Current Distance / Score
        private float distance;

        // Is Game Running
        private bool isRunning = false, isPaused = false;

        // Is New Hightscore
        private bool isNewHighscore = false;

        // Start
        private void Start()
        {
            // Check If Exists
            if (RocketHandler.instance == null)
            {
                RocketHandler.instance = this;
            }
            else
            {
                Destroy(this);

                Debug.LogError("RocketHandler Initialized Twice");
            }

            // Check For Rocket / Player
            GameObject gameObject = GameObject.FindGameObjectWithTag(Settings.Tags.PLAYER);

            if (gameObject == null)
            {
                if (rocket != null)
                {
                    Vector3 position = new Vector3(0.5f, 0.325f, Settings.Zs.PLAYER);

                    position = Camera.main.ViewportToWorldPoint(position);

                    gameObject = Instantiate(rocket, position, Quaternion.identity);
                }
            }
            rocket = gameObject;

            // Enable Menu
            ToggleView(RocketHandler.MENU_VIEW);
        }

        // Update
        private void Update()
        {
            if (isRunning && !isPaused)
            {
                // Android Back Button Check
                if (Application.platform == RuntimePlatform.Android)
                {
                    if (Input.GetKey(KeyCode.Escape))
                    {
                        if (isRunning)
                        {
                            StopRocket();
                        }
                        else
                        {
                            Application.Quit();
                        }
                    }
                }

                // GamePlay Elements
                Rocket rocket = RocketHandler.GetRocket();

                if (rocket != null)
                {
                    // Add Rocket Velocity
                    distance += (RocketHandler.BASE_SCORE * rocket.GetVelocity()) * Time.deltaTime;

                    if (distance < 0)
                    {
                        distance = 0;
                    }

                    // Update Score Text
                    score.text = string.Format("{0:### ### ### }", Mathf.RoundToInt(distance));

                    // Drain Rocket Fuel
                    float fuel = rocket.DrainFuel(Time.deltaTime);

                    float shield = rocket.GetCurrentShield();

                    // Display Warnings
                    displayTime += Time.deltaTime;

                    if (displayTime >= RocketHandler.WARNING_TIME)
                    {
                        if (fuel <= RocketHandler.WARNING_LIMIT)
                        {
                            Message.Log("<color=#" + Settings.Colors.HEX_COLOR_RED + ">No Fuel</color>");
                        }
                        else if (shield <= RocketHandler.WARNING_LIMIT)
                        {
                            Message.Log("<color=#" + Settings.Colors.HEX_COLOR_RED + ">Shield Damaged</color>");
                        }
                        displayTime -= RocketHandler.WARNING_TIME;
                    }

                    // Shield Or Fuel Empty
                    if (shield <= 0 || fuel <= 0)
                    {
                        ToggleView(RocketHandler.LOSE_VIEW);

                        isPaused = true;
                    }

                    // Spawn Enemy 
                    enemyTime += Time.deltaTime;

                    if (enemyTime >= 0.05f)
                    {
                        SpawnEnemy();
                        enemyTime -= 0.05f;
                    }
                }
            }
            else
            {
                // Update Highscore
                UpdateHightscore();
            }
        }

        // Update Highscore Text
        public void UpdateHightscore()
        {
            // Check For Null
            if (highscore != null)
            {
                // Is Score Greater Zero
                int score = Manager.GetScore();

                if (score < 0)
                {
                    Manager.SetScore(0);
                }

                // Display Hightscore
                string color = (isNewHighscore ? Settings.Colors.HEX_COLOR_YELLOW : Settings.Colors.HEX_COLOR_WHITE);

                string text = "<color=#" + color + ">" + "HIGHSCORE\n" + string.Format("{0:### ### ### }", score) + "</color>";

                highscore.text = score > 0 ? text : null;
            }
        }

        // Stop Rocket
        public void StartRocket()
        {
            // Return If Running
            if (isRunning)
            {
                return;
            }

            // Disable Pause If Active
            isPaused = false;

            // Reset Rocket Values
            RocketHandler.GetRocket().Reset();

            // Calibrate Controls
            Controller controller = rocket.GetComponent<Controller>();

            if (controller != null)
            {
                controller.CalibrateAccelerometer();
            }

            // Reset Rocket Position
            Vector3 position = new Vector3(0.5f, 0.325f, Settings.Zs.PLAYER);

            position = Camera.main.ViewportToWorldPoint(position);

            rocket.transform.position = position;

            // Start
            isRunning = true;

            // Enable Game
            ToggleView(RocketHandler.GAME_VIEW);
        }

        // Start Rocket
        public void StopRocket()
        {
            // Return If Not Running
            if (!isRunning)
            {
                return;
            }

            // Disable Pause If Active
            isPaused = false;

            // Reset Rocket Values
            RocketHandler.GetRocket().Reset();

            // Calculate Hightscore
            int score = Manager.GetScore();

            int temp = Mathf.RoundToInt(distance);

            isNewHighscore = false;

            if (temp > score)
            {
                isNewHighscore = true;
                score = temp;
            }
            Manager.SetScore(score);

            distance = 0.0f;

            // Reset Rocket Position
            Vector3 position = new Vector3(0.5f, 0.325f, Settings.Zs.PLAYER);

            position = Camera.main.ViewportToWorldPoint(position);

            rocket.transform.position = position;

            // Reset Rocket Contents
            RocketObject rocketObject = rocket.GetComponent<RocketObject>();

            if (rocketObject != null)
            {
                GameObject content = rocketObject.GetContent();

                foreach (Transform transform in content.transform)
                {
                    Destroy(transform.gameObject);
                }
            }

            // Stop
            isRunning = false;

            // Enable Menu
            ToggleView(RocketHandler.MENU_VIEW);
        }

        // Toggle View
        public void ToggleView(string key)
        {
            View temp = null;

            foreach (View view in views)
            {
                if (temp == null && view.key != null && view.key.Equals(key))
                {
                    temp = view;
                }
                view.gameObject.SetActive(false);
            }
            temp.gameObject.SetActive(true);
        }

        // Spawn Enemy Randomly
        public void SpawnEnemy()
        {
            float isEnemySpawning = distance / 100000 * 100.0f;

            float percentage = UnityEngine.Random.Range(0.0f, 101.0f);

            if (isEnemySpawning >= percentage)
            {
                Enemy enemy = Content.GetRandomEnemy();

                Vector3 position = new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.0f, 101.0f);
                position = Camera.main.ViewportToWorldPoint(position);

                GameObject gameObject = Instantiate(enemy.gameObject, position, Quaternion.identity);
                gameObject.transform.SetParent(Manager.GetGameContentObject().transform);
            }
        }

        // Set Paused
        public void SetPaused(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        // Is Paused
        public bool IsPaused()
        {
            return isPaused;
        }

        // Increase Points 
        public static void Increase(int points)
        {
            if (RocketHandler.instance == null)
            {
                return;
            }
            RocketHandler.instance.distance += points;
        }

        // Decrease Points
        public static void Decrease(int points)
        {
            if (RocketHandler.instance == null)
            {
                return;
            }
            RocketHandler.instance.distance -= points;
        }

        // Is Running
        public static bool IsRunning()
        {
            return RocketHandler.instance != null && RocketHandler.instance.isRunning && !RocketHandler.instance.isPaused;
        }

        // Get Rocket
        public static Rocket GetRocket()
        {
            return Manager.GetRocket();
        }
    }
}