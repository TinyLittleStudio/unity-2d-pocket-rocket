using UnityEngine;

namespace PocketRocket
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Enemy Settings")]
        // Spawn Probability
        public float probability = 100.0f;

        // Base Velocity
        public float velocity;

        // LifeTime
        public float time;

        // Awake
        private void Awake()
        {
            // Set Up Collider2D
            Collider2D collider2d = GetComponent<Collider2D>();

            if (collider2d != null)
            {
                collider2d.isTrigger = true;
            }
        }

        // Update
        private void Update()
        {
            // Apply Velocity
            Vector3 position = transform.position;

            Rocket rocket = RocketHandler.GetRocket();

            position.y += Time.deltaTime * -(velocity + rocket.GetVelocity());

            transform.position = position;

            // Check for LifeTime
            time -= Time.deltaTime;

            if (time <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        // On Trigger Enter 2D
        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            string tag = collider2d.gameObject.tag;

            if (tag != null)
            {
                if (!Settings.Tags.PLAYER.Equals(tag))
                {
                    return;
                }
            }

            // Call Only On Player Collision
            OnCollideWithRocket(RocketHandler.GetRocket());
        }

        // Custom Player Collision (Rocket)
        public abstract void OnCollideWithRocket(Rocket rocket);

        // Get Spawn Probability
        public float GetProbability()
        {
            return probability;
        }

        // Get Velocity
        public float GetVelocity()
        {
            return velocity;
        }
    }
}
