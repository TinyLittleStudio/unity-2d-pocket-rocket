using System.Collections.Generic;
using UnityEngine;

namespace PocketRocket
{
    public class Rocket
    {
        // Rocket Components
        private List<RocketComponent> rocketComponents = new List<RocketComponent>();

        // Constructor
        public Rocket()
        {

        }

        // Load
        public void Load()
        {
            // Load Defaults
            if (rocketComponents.Count > 0)
            {
                return;
            }

            foreach (RocketComponentMeta rocketComponentMeta in Content.GetRocketComponentMetas())
            {
                if (!rocketComponentMeta.isDefault)
                {
                    continue;
                }
                RocketComponentType rocketComponentType = rocketComponentMeta.GetRocketComponentType();

                if (GetRocketComponent(rocketComponentType) == null)
                {
                    SetRocketComponent(RocketComponentFactory.CreateRocketComponent(rocketComponentMeta));
                }
            }
        }

        // Unload
        public void Unload()
        {

        }

        // Reset Rocket Components
        public void Reset()
        {
            foreach (RocketComponent rocketComponent in rocketComponents)
            {
                rocketComponent.Reset();
            }
        }

        // Get Shield
        public Shield GetShield()
        {
            RocketComponent rocketComponent = GetRocketComponent(RocketComponentType.SHIELD);

            if (rocketComponent != null)
            {
                return (Shield)rocketComponent;
            }
            return null;
        }

        // Get Casing
        public Casing GetCasing()
        {
            RocketComponent rocketComponent = GetRocketComponent(RocketComponentType.CASING);

            if (rocketComponent != null)
            {
                return (Casing)rocketComponent;
            }
            return null;
        }

        // Get Engine
        public Engine GetEngine()
        {
            RocketComponent rocketComponent = GetRocketComponent(RocketComponentType.ENGINE);

            if (rocketComponent != null)
            {
                return (Engine)rocketComponent;
            }
            return null;
        }

        // Get Controls
        public Controls GetControls()
        {
            RocketComponent rocketComponent = GetRocketComponent(RocketComponentType.CONTROLS);

            if (rocketComponent != null)
            {
                return (Controls)rocketComponent;
            }
            return null;
        }

        // Set Specific Rocket Component
        public void SetRocketComponent(RocketComponent rocketComponent)
        {
            if (rocketComponent == null)
            {
                return;
            }

            for (int i = 0; i < rocketComponents.Count; i++)
            {
                RocketComponent temp = rocketComponents[i];

                if (rocketComponent.GetRocketComponentType().Equals(temp.GetRocketComponentType()))
                {
                    rocketComponents[i] = rocketComponent;
                    return;
                }
            }
            rocketComponents.Add(rocketComponent);
        }

        // Get Specific Rocket Component
        public RocketComponent GetRocketComponent(RocketComponentType rocketComponentType)
        {
            foreach (RocketComponent rocketComponent in rocketComponents)
            {
                if (rocketComponent.GetRocketComponentType().Equals(rocketComponentType))
                {
                    return rocketComponent;
                }
            }
            return null;
        }

        // Set Rocket Components
        public void SetRocketComponents(List<RocketComponent> rocketComponents)
        {
            this.rocketComponents = rocketComponents;
        }

        // Get Rocket Components
        public List<RocketComponent> GetRocketComponents()
        {
            return rocketComponents;
        }
    }
}
