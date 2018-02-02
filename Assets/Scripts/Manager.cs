using System;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRocket
{
    public class Manager : MonoBehaviour
    {
        // Required Delete Calls To Delete Data
        public static readonly int DELETE_COUNT = 10;

        // Score PlayerPrefs Key
        public static readonly string SCORE_KEY = "SCORE";

        // Manager instance
        private static Manager instance;

        [Header("Game Content Object")]
        // Game Content Container Object
        public GameObject content;

        // Rocket Data Object
        private Rocket rocket;

        // Delete Count
        private int delete;

        // Awake
        private void Awake()
        {
            if (Manager.instance == null)
            {
                Manager.instance = this;
            }
            else
            {
                Destroy(this);
            }
            this.rocket = new Rocket();

            Load();

            // Disable Screen Sleep
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        // On Application Quit
        private void OnApplicationQuit()
        {
            Save();
        }

        // on Application Pause
        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {
                Save();
            }
            else
            {
                Load();
            }
        }

        // Save 
        private void Save()
        {
            // Get Rocket
            Rocket rocket = RocketHandler.GetRocket();

            // Save Rocket Components
            List<RocketComponent> rocketComponents = rocket.GetRocketComponents();

            Manager.SetRocketComponents(rocketComponents);

            PlayerPrefs.Save();

            // Unload Rocket Data
            rocket.Unload();
        }

        // Load
        private void Load()
        {
            // Get Rocket
            Rocket rocket = RocketHandler.GetRocket();

            // Load Rocket Components
            List<RocketComponent> rocketComponents = Manager.GetRocketComponents();

            rocket.SetRocketComponents(rocketComponents);

            // Load Rocket Data
            rocket.Load();
        }

        // Delte Player Data
        public void Delete()
        {
            if (Manager.GetScore() > 0)
            {
                delete++;

                if (delete >= Manager.DELETE_COUNT)
                {
                    PlayerPrefs.DeleteAll();

                    this.rocket = new Rocket();
                    this.rocket.Load();

                    Message.Log("DELTED PLAYER DATA");

                    Save();

                    delete = 0;
                }
                else
                {
                    Message.Log("TAP " + (Manager.DELETE_COUNT - delete) + " TIMES TO DELETE");
                }
            }
        }

        // Set Global Score
        public static void SetScore(int score)
        {
            PlayerPrefs.SetInt(Manager.SCORE_KEY, score);
        }

        // Get Global Score
        public static int GetScore()
        {
            return PlayerPrefs.GetInt(Manager.SCORE_KEY);
        }

        // Set RocketComponents
        public static void SetRocketComponents(List<RocketComponent> rocketComponents)
        {
            // Iterate Components And Save Each
            foreach (RocketComponent rocketComponent in rocketComponents)
            {
                RocketComponentType rocketComponentType = rocketComponent.GetRocketComponentType();

                string key = rocketComponentType.ToString(), name = rocketComponent.Name;

                if (key != null && name != null)
                {
                    PlayerPrefs.SetString(key, name);
                }
            }
        }

        // Get RocketComponents
        public static List<RocketComponent> GetRocketComponents()
        {
            List<RocketComponent> result = new List<RocketComponent>();

            Array rocketComponentTypes = Enum.GetValues(typeof(RocketComponentType));

            // Check For All Component Types And Load Each
            foreach (RocketComponentType rocketComponentType in rocketComponentTypes)
            {
                string key = rocketComponentType.ToString();

                string temp = PlayerPrefs.GetString(key);

                RocketComponentMeta rocketComponentMeta = Content.GetRocketComponentMeta(temp);

                if (rocketComponentMeta != null)
                {
                    result.Add(RocketComponentFactory.CreateRocketComponent(rocketComponentMeta));
                }
            }
            return result;
        }

        // Get Game Content Container
        public static GameObject GetGameContentObject()
        {
            if (Manager.instance == null)
            {
                return null;
            }
            return Manager.instance.content;
        }

        // Get Rocket
        public static Rocket GetRocket()
        {
            if (Manager.instance == null)
            {
                return null;
            }
            return Manager.instance.rocket;
        }
    }
}