using UnityEngine;

namespace PocketRocket
{
    public class Message : MonoBehaviour
    {
        // Message Instance
        private static Message instance;

        [Header("Message System Settings")]
        // Container Instantiate Environment
        public GameObject container;

        // Object To Instantiate
        public GameObject prefab;

        // Last Message Object
        private MessageObject previous;

        // Start
        private void Start()
        {
            // Allow Only Once Instance
            if (Message.instance == null)
            {
                Message.instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Log
        public void Log(string message, bool debug)
        {
            // Check For Null
            if (message == null || prefab == null || container == null)
            {
                return;
            }

            // Destroy Last Message If Exists
            if (previous != null)
            {
                Destroy(previous.gameObject);
            }

            // Create Message Object And Assign Values
            GameObject gameObject = Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            gameObject.transform.SetParent(container.transform, false);

            MessageObject MessageObject = gameObject.GetComponent<MessageObject>();

            if (MessageObject == null)
            {
                MessageObject = gameObject.AddComponent(typeof(MessageObject)) as MessageObject;
            }
            MessageObject.SetMessage(message);

            // Debug Print
            if (debug)
            {
                Debug.Log("Message: " + message);
            }

            // Set Last Message Object
            previous = MessageObject;
        }

        // Log Static
        public static void Log(string message)
        {
            if (Message.instance == null || message == null)
            {
                return;
            }
            Message.instance.Log(message, false);
        }
    }
}