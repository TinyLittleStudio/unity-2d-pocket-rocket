using UnityEngine;

namespace PocketRocket
{
    public class Comet : Enemy
    {
        [Header("Comet Settings")]
        // Audio Clip
        public AudioClip audioClip;

        // Damage
        public int damage;

        // Fragments
        public GameObject fragments;

        [Header("Rocket / Player")]
        // Rocket
        public GameObject rocket;

        // Start
        private void Start()
        {
            if (rocket == null)
            {
                this.rocket = GameObject.FindGameObjectWithTag(Settings.Tags.PLAYER);

                if (rocket == null)
                {
                    Debug.LogError("Could Not Find Rocket / Player");
                }
            }
        }

        // On Player Collision
        public override void OnCollideWithRocket(Rocket rocket)
        {
            // Check For Null
            if (rocket == null)
            {
                return;
            }

            // Instantiate Comet Fragments
            GameObject gameObject = Instantiate(fragments, transform.position, Quaternion.identity);
            gameObject.transform.SetParent(Manager.GetGameContentObject().transform);

            // Add Damage
            RocketObject rocketObject = RocketObject.GetRocketObject();

            if (rocketObject != null)
            {
                rocketObject.Damage(damage, transform.position);

                // Play Collision Sound
                AudioSource audioSource = rocketObject.GetAudioSource();

                if (audioSource != null)
                {
                    audioSource.PlayOneShot(audioClip);
                }
            }

            // Destroy Comet
            Destroy(this.gameObject);
        }
    }
}
