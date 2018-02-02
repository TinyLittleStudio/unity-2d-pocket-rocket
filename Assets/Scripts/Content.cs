using System.Collections.Generic;
using UnityEngine;

namespace PocketRocket
{
    public class Content : MonoBehaviour
    {
        // Content Instance
        private static Content instance;

        [Header("Rocket Component Meta Settings")]
        // List Of All Available Rocket Component Metas
        public List<RocketComponentMeta> rocketComponentMetas = new List<RocketComponentMeta>();

        [Header("Enemy Settings")]
        // List Of All Available Enemies
        public List<Enemy> enemies = new List<Enemy>();

        // Awake
        private void Awake()
        {
            // Add Contents To Existing Instance
            if (Content.instance == null)
            {
                Content.instance = this;
            }
            else
            {
                Content.instance.rocketComponentMetas.AddRange(rocketComponentMetas);
            }

            // Validate All Rocket Component Metas
            foreach (RocketComponentMeta rocketComponentMeta in rocketComponentMetas)
            {
                rocketComponentMeta.Validate();
            }
        }

        // Get All Available Rocket Component Metas
        public static List<RocketComponentMeta> GetRocketComponentMetas()
        {
            if (Content.instance == null)
            {
                return null;
            }
            return Content.instance.rocketComponentMetas;
        }

        // Get Rocket Component Meta By Name
        public static RocketComponentMeta GetRocketComponentMeta(string name)
        {
            if (Content.instance == null)
            {
                return null;
            }

            if (name == null || name.Length == 0)
            {
                return null;
            }

            foreach (RocketComponentMeta rocketComponentMeta in Content.instance.rocketComponentMetas)
            {
                string temp = rocketComponentMeta.name;

                if (temp != null && temp.Equals(name))
                {
                    return rocketComponentMeta;
                }
            }
            return null;
        }

        // Get Rocket Component Meta List by Rocket Component Type
        public static List<RocketComponentMeta> GetRocketComponentMetas(RocketComponentType rocketComponentType)
        {
            if (Content.instance == null)
            {
                return null;
            }

            List<RocketComponentMeta> result = new List<RocketComponentMeta>();

            foreach (RocketComponentMeta rocketComponentMeta in Content.instance.rocketComponentMetas)
            {
                RocketComponentType temp = rocketComponentMeta.GetRocketComponentType();

                if (temp.Equals(rocketComponentType))
                {
                    result.Add(rocketComponentMeta);
                }
            }
            return result;
        }

        // Add Rocket Component Meta
        public static void AddRocketComponentMeta(RocketComponentMeta rocketComponentMeta)
        {
            if (Content.instance == null)
            {
                return;
            }
            Content.instance.rocketComponentMetas.Add(rocketComponentMeta);
        }

        // Get All Available Enemies
        public static List<Enemy> GetEnemies()
        {
            if (Content.instance == null)
            {
                return null;
            }
            return Content.instance.enemies;
        }

        // Get Random Enemy By Probability
        public static Enemy GetRandomEnemy()
        {
            float total = 0;

            foreach (Enemy enemy in GetEnemies())
            {
                total += enemy.probability;
            }

            float probability = Random.Range(0.0f, total);

            float last = 0.0f;

            foreach (Enemy enemy in GetEnemies())
            {
                if (probability >= last && probability < last + enemy.probability)
                {
                    return enemy;
                }
                last += enemy.probability;
            }
            return null;
        }
    }
}
