using System;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRocket
{
    [Serializable]
    public class RocketComponentMeta
    {
        [Serializable]
        public class RocketComponentMetaValue
        {
            // Value key
            public string key;

            // Value
            public string value;

            // Constructor
            public RocketComponentMetaValue(string key, string value)
            {
                this.key = key;
                this.value = value;
            }

            // Try To Cast Integer
            public int ToInteger()
            {
                return ToInteger(0);
            }

            // Try To Cast Integer Or Return Default Value
            public int ToInteger(int defaultValue)
            {
                int resultValue = defaultValue;

                int.TryParse(value, out resultValue);

                return resultValue;
            }

            // Try To Cast Float
            public float ToFloat()
            {
                return ToFloat(0.0f);
            }

            // Try To Cast Float Or Return Default Value
            public float ToFloat(float defaultValue)
            {
                float resultValue = defaultValue;

                float.TryParse(value, out resultValue);

                return resultValue;
            }
        }

        // Rocket Component Meta Name
        public string name;

        // Rocket Component Meta Texture Id
        public string texture;

        // Rocket Component Tier
        public int tier;

        // Rocket Component Type
        public RocketComponentType rocketComponentType;

        // Rocket Unlock Score
        public int score;

        // Is Default Component Meta ?
        public bool isDefault = false;

        // Rocket Component Meta Values As List
        public List<RocketComponentMetaValue> rocketComponentMetaValues = new List<RocketComponentMetaValue>();

        // Constructor
        public RocketComponentMeta(string name, string texture, int tier, RocketComponentType rocketComponentType, int score) : this(name, texture, tier, rocketComponentType, score, false)
        {

        }

        // Constructor
        public RocketComponentMeta(string name, string texture, int tier, RocketComponentType rocketComponentType, int score, bool isDefault)
        {
            // Set Values
            this.name = name;
            this.texture = texture;

            this.tier = tier;

            this.rocketComponentType = rocketComponentType;

            this.score = score;

            // Check If Exists
            if (Content.GetRocketComponentMeta(name) == null)
            {
                Content.AddRocketComponentMeta(this);
            }
            else
            {
                throw new ArgumentException(name + " already exists");
            }
            Validate();
        }

        // Returns Name (readonly)
        public string Name
        {
            get
            {
                return name;
            }
        }

        // Get Texture (From Texture Key)
        public Sprite GetTexture()
        {
            return Textures.GetSprite(texture);
        }

        // Get Tier
        public int GetTier()
        {
            return tier;
        }

        // Get Rocket Component Type
        public RocketComponentType GetRocketComponentType()
        {
            return rocketComponentType;
        }

        // Get Required Score To Unlock
        public int GetRequiredScore()
        {
            return score;
        }

        // Get Value As Integer
        public int GetIntegerValue(string key)
        {
            return GetRocketComponentMetaValue(key).ToInteger();
        }

        // Get Value As Float
        public float GetFloatValue(string key)
        {
            return GetRocketComponentMetaValue(key).ToFloat();

        }

        public void Validate()
        {
            // Validate Values
            List<RocketComponentMetaValue> rocketComponentMetaValues = new List<RocketComponentMetaValue>();

            // Iterate All Available Rocket Component Meta Values
            foreach (RocketComponentMetaValue rocketComponentMetaValue in this.rocketComponentMetaValues)
            {
                // Check If Key Exists
                bool exists = false;

                foreach (RocketComponentMetaValue temp in rocketComponentMetaValues)
                {
                    if (!temp.key.Equals(rocketComponentMetaValue.key))
                    {
                        continue;
                    }
                    exists = true;

                    break;
                }

                // Add If It Does Not Exist
                if (!exists)
                {
                    rocketComponentMetaValues.Add(rocketComponentMetaValue);
                }
            }
            // Set Filtered List
            this.rocketComponentMetaValues = rocketComponentMetaValues;
        }

        // Get Rocket Component Meta Value by Key
        public RocketComponentMetaValue GetRocketComponentMetaValue(string key)
        {
            // Check For Null
            if (key == null || key.Length == 0)
            {
                return null;
            }

            // Iterate Available Rocket Component Meta Values
            foreach (RocketComponentMetaValue rocketComponentMetaValue in rocketComponentMetaValues)
            {
                string temp = rocketComponentMetaValue.key;

                if (key.Equals(temp))
                {
                    return rocketComponentMetaValue;
                }
            }
            return null;
        }

        // Get Rocket Component Meta Values List
        public List<RocketComponentMetaValue> GetRocketComponentMetaValues()
        {
            return rocketComponentMetaValues;
        }
    }
}
