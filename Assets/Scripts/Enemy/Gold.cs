using UnityEngine;

namespace PocketRocket
{
    public class Gold : Enemy
    {
        [Header("Gold Settings")]
        // Audio Clip
        public AudioClip audioClip;

        // Points
        public int points;

        // On Player Collision
        public override void OnCollideWithRocket(Rocket rocket)
        {
            // Check For Null
            if (rocket == null)
            {
                return;
            }

            RocketHandler.Increase(points);

            // Message Log
            Message.Log("<color=#" + Settings.Colors.HEX_COLOR_YELLOW + ">+" + points + " </color>");

            // Play Collision Sound
            RocketObject rocketObject = RocketObject.GetRocketObject();

            if (rocketObject != null)
            {
                // Play Collision Sound
                AudioSource audioSource = rocketObject.GetAudioSource();

                if (audioSource != null)
                {
                    audioSource.PlayOneShot(audioClip);
                }
            }

            Destroy(this.gameObject);
        }
    }
}
