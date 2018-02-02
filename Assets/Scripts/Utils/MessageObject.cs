using UnityEngine;
using UnityEngine.UI;

namespace PocketRocket
{
    public class MessageObject : MonoBehaviour
    {
        [Header("Message Settings")]
        // CanvasGroup for Fading
        public CanvasGroup canvasGroup;

        // Message Text
        public Text text;

        // Lifetime
        public double time = 1.5d;

        // Start
        private void Start()
        {
            // Destroy If Settings Are Null
            if (text == null || canvasGroup == null)
            {
                Destroy(this.gameObject);
            }
        }

        // Update
        private void Update()
        {
            // Check For Null
            if (text == null)
            {
                return;
            }

            // Check For Lifetime
            time -= Time.deltaTime;

            if (time <= 0)
            {
                Destroy(this.gameObject);
            }

            // Fade
            canvasGroup.alpha = time < 1 ? (float)time : 1.0f;
        }

        // Set Message
        public void SetMessage(string message)
        {
            this.text.text = message;
        }

        // Get Message
        public string GetMessage()
        {
            return this.text.text;
        }
    }
}