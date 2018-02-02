using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PocketRocket
{
    public class RocketUpgradeHandler : MonoBehaviour
    {
        [Serializable]
        public class FactoryDisplayObject
        {
            // Rocket Component Type Key
            public RocketComponentType rocketComponentType;

            // Image
            public Image image;

            // Constructor
            public FactoryDisplayObject(RocketComponentType rocketComponentType, Image image)
            {
                this.rocketComponentType = rocketComponentType;
                this.image = image;
            }

            // Get Rocket Component Type
            public RocketComponentType GetRocketComponentType()
            {
                return rocketComponentType;
            }

            // Get Image
            public Image GetImage()
            {
                return image;
            }
        }

        // Factory Handler Instance
        private static RocketUpgradeHandler instance;

        [Header("Display Components")]
        // Display Objects By Rocket Component Type
        public List<FactoryDisplayObject> factoryDisplayObjects = new List<FactoryDisplayObject>();

        // Dictionary Of Rocket Component Metas By Rocket Component Type
        private Dictionary<RocketComponentType, List<RocketComponentMeta>> rocketComponentMetas = new Dictionary<RocketComponentType, List<RocketComponentMeta>>();

        // Start
        private void Start()
        {
            // Create Instance
            if (RocketUpgradeHandler.instance == null)
            {
                RocketUpgradeHandler.instance = this;
            }
            else
            {
                Destroy(this);
            }

            // Initialize Values
            Array values = Enum.GetValues(typeof(RocketComponentType));

            foreach (RocketComponentType rocketComponentType in values)
            {
                List<RocketComponentMeta> result = Content.GetRocketComponentMetas(rocketComponentType);

                rocketComponentMetas.Add(rocketComponentType, result);
            }
        }

        // Update
        private void Update()
        {
            // Update Display Objects
            Rocket rocket = RocketHandler.GetRocket();

            foreach (FactoryDisplayObject factoryDisplayObject in factoryDisplayObjects)
            {
                if (factoryDisplayObject.image == null)
                {
                    continue;
                }
                RocketComponent rocketComponent = rocket.GetRocketComponent(factoryDisplayObject.rocketComponentType);

                if (rocketComponent != null)
                {
                    string name = rocketComponent.Name;

                    RocketComponentMeta rocketComponentMeta = Content.GetRocketComponentMeta(name);

                    factoryDisplayObject.image.sprite = rocketComponentMeta.GetTexture();
                }
            }
        }

        // Upgrade Rocket
        public void Upgrade()
        {
            Rocket rocket = RocketHandler.GetRocket();

            List<RocketComponent> rocketComponents = rocket.GetRocketComponents();

            // Search Next Upgradable
            RocketComponent result = null;

            int last = -1;

            foreach (RocketComponent rocketComponent in rocketComponents)
            {
                int tier = rocketComponent.GetTier();

                if (last == -1)
                {
                    last = tier;
                }
                else
                {
                    if (tier < last)
                    {
                        result = rocketComponent;
                        break;
                    }
                }
            }

            if (result == null)
            {
                result = rocketComponents[0];
            }

            if (result != null)
            {
                // Search For Next Upgrade
                List<RocketComponentMeta> rocketComponentMetas = Content.GetRocketComponentMetas();

                RocketComponentMeta upgrade = null;

                foreach (RocketComponentMeta rocketComponentMeta in rocketComponentMetas)
                {
                    RocketComponentType rocketComponentType = rocketComponentMeta.rocketComponentType;

                    if (!rocketComponentType.Equals(result.GetRocketComponentType()))
                    {
                        continue;
                    }
                    int tier = rocketComponentMeta.GetTier();

                    if ((upgrade == null && tier > result.GetTier()) || (upgrade != null && tier > result.GetTier() && tier < upgrade.GetTier()))
                    {
                        upgrade = rocketComponentMeta;
                    }
                }

                // Upgrade
                if (upgrade != null)
                {
                    if (Manager.GetScore() >= upgrade.score)
                    {
                        rocket.SetRocketComponent(RocketComponentFactory.CreateRocketComponent(upgrade));

                        Message.Log("<color=#" + Settings.Colors.HEX_COLOR_GREEN + ">UPGRADED TO " + upgrade.name.ToUpper() + " TIER " + upgrade.tier + "</color>");
                    }
                    else
                    {
                        Message.Log("<color=#" + Settings.Colors.HEX_COLOR_RED + ">" + upgrade.score + " POINTS REQUIRED</color>");
                    }
                }
                else
                {
                    Message.Log("<color=#" + Settings.Colors.HEX_COLOR_RED + ">NO UPGRADES AVAILABLE</color>");
                }
            }
        }
    }
}