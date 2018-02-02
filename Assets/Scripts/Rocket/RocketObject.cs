using System;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRocket
{
    public class RocketObject : MonoBehaviour
    {
        [Serializable]
        public class RocketObjectComponent
        {
            // Rocket Component Type
            public RocketComponentType rocketComponentType;

            // Sprite Renderer To Display Sprite
            public SpriteRenderer spriteRenderer;

            // Constructor
            public RocketObjectComponent(RocketComponentType rocketComponentType, SpriteRenderer spriteRenderer)
            {
                this.rocketComponentType = rocketComponentType;
                this.spriteRenderer = spriteRenderer;
            }

            // Get Rocket Component Type
            public RocketComponentType GetRocketComponentType()
            {
                return rocketComponentType;
            }

            // Get Sprite Renderer
            public SpriteRenderer GetSpriteRenderer()
            {
                return spriteRenderer;
            }
        }

        // Show Indicate From Value
        public static readonly int BASE_INDICATOR_VALUE = 10;

        // Rocket Object Instance
        public static RocketObject instance;

        [Header("Rocket Component")]
        // Rocket Component Sprite Renderers
        public List<RocketObjectComponent> rocketObjectComponents = new List<RocketObjectComponent>();

        [Space(10)]
        // Content GameObject
        public GameObject content;

        [Space(10)]
        // Audio Source
        public AudioSource audioSource;

        [Space(10)]
        // Damage Indicator
        public GameObject damageIndicator;

        // Start
        private void Start()
        {
            if (RocketObject.instance == null)
            {
                RocketObject.instance = this;
            }
            else
            {
                Destroy(this);
            }

            // Check For Null
            if (content == null || rocketObjectComponents == null || rocketObjectComponents.Count == 0)
            {
                Debug.LogError("Missing Components On Rocket Object");
            }
        }

        // Update
        private void Update()
        {
            Rocket rocket = RocketHandler.GetRocket();

            // Check For Null
            if (rocket != null)
            {
                // Iterate And Update Sprites
                foreach (RocketObjectComponent rocketObjectComponent in rocketObjectComponents)
                {

                    RocketComponent rocketComponent = rocket.GetRocketComponent(rocketObjectComponent.rocketComponentType);

                    if (rocketComponent != null)
                    {
                        rocketObjectComponent.spriteRenderer.sprite = rocketComponent.GetTexture();
                    }
                }
            }
        }

        // Damage Rocket
        public void Damage(float damage, Vector3 position)
        {
            // Show Indicator Only When Shield Is Down
            Rocket rocket = RocketHandler.GetRocket();

            rocket.Damage(damage);

            if (rocket.GetCurrentShield() > RocketObject.BASE_INDICATOR_VALUE)
            {
                return;
            }

            // Instantiate Damage Indicator
            if (damageIndicator != null)
            {
                Vector3 target = position;
                target.z = Settings.Zs.PARTICLES;

                GameObject smokeObject = Instantiate(damageIndicator, target, Quaternion.identity);

                smokeObject.transform.SetParent(content.transform);
                smokeObject.transform.rotation = damageIndicator.transform.rotation;
            }
        }

        // Get Audio Source
        public AudioSource GetAudioSource()
        {
            return audioSource;
        }

        // Get Content GameObject
        public GameObject GetContent()
        {
            return content;
        }

        // Get Rocket Object
        public static RocketObject GetRocketObject()
        {
            return RocketObject.instance;
        }
    }
}
