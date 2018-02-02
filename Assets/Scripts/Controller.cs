using UnityEngine;

namespace PocketRocket
{
    public class Controller : MonoBehaviour
    {
        // Calibration Quaternion
        private Quaternion calibrationQuaternion;

        // Start
        private void Start()
        {
            // Calibrate Aceelerometer
            CalibrateAccelerometer();
        }

        // Update
        private void Update()
        {
            // Check If Is Running
            if (!RocketHandler.IsRunning())
            {
                return;
            }

            // Move Rocket Accordint To Acceleration
            Rocket rocket = RocketHandler.GetRocket();

            float accuracy = rocket.GetAccuracy();

            transform.Translate(Input.acceleration.x * accuracy, AdjustAcceleration(Input.acceleration).y * accuracy, 0);
        }

        // Late Update
        private void LateUpdate()
        {
            Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
            position.x = Mathf.Clamp01(position.x);
            position.y = Mathf.Clamp01(position.y);

            transform.position = Camera.main.ViewportToWorldPoint(position);
        }

        // Calibrate 
        public void CalibrateAccelerometer()
        {
            Vector3 accelerationSnapshot = Input.acceleration;

            Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);

            calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
        }

        // Adjust Acceleration
        public Vector3 AdjustAcceleration(Vector3 acceleration)
        {
            return calibrationQuaternion * acceleration;
        }
    }
}