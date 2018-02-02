using System;
using UnityEngine;

namespace PocketRocket
{
    public abstract class RocketComponent
    {
        // Rocket Component Meta
        protected readonly RocketComponentMeta rocketComponentMeta;

        // Constructor
        public RocketComponent(RocketComponentMeta rocketComponentMeta)
        {
            if (rocketComponentMeta == null)
            {
                throw new ArgumentException("RocketComponentMeta can not be null");
            }
            this.rocketComponentMeta = rocketComponentMeta;
        }

        // Abstract Reset Method
        public abstract void Reset();

        // Returns The Name
        public string Name
        {
            get
            {
                return rocketComponentMeta.Name;
            }
        }

        // Get Tier
        public int GetTier()
        {
            return rocketComponentMeta.tier;
        }

        // Get Texture
        public Sprite GetTexture()
        {
            return rocketComponentMeta.GetTexture();
        }

        // Get The Rocket Component Type
        public RocketComponentType GetRocketComponentType()
        {
            return rocketComponentMeta.GetRocketComponentType();
        }

        // Get The Required Score to Unlock
        public int GetRequiredScore()
        {
            return rocketComponentMeta.GetRequiredScore();
        }

        // Get Integer Value From Meta By Key
        public float GetIntegerValue(string key)
        {
            if (key == null || rocketComponentMeta == null)
            {
                return 0.0f;
            }
            return rocketComponentMeta.GetRocketComponentMetaValue(key).ToFloat();
        }

        // Get Float Value From Meta By Key
        public float GetFloatValue(string key)
        {
            if (key == null || rocketComponentMeta == null)
            {
                return 0;
            }
            return rocketComponentMeta.GetRocketComponentMetaValue(key).ToFloat();
        }
    }
}