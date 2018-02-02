namespace PocketRocket
{
    public class Controls : RocketComponent
    {
        // Accuracy Key
        public static readonly string ACCURACY_KEY = "Accuracy";

        // Accuracy
        private float accuracy;

        // Constructor
        public Controls(RocketComponentMeta rocketComponentMeta) : base(rocketComponentMeta)
        {
            Reset();
        }

        // Reset
        public override void Reset()
        {
            if (rocketComponentMeta == null)
            {
                return;
            }
            this.accuracy = rocketComponentMeta.GetFloatValue(Controls.ACCURACY_KEY);
        }

        // Get Accuracy
        public float GetAccuracy()
        {
            return accuracy;
        }
    }

    public static class ControlsExtension
    {
        // Extending Method "Get Accuracy"
        public static float GetAccuracy(this Rocket rocket)
        {
            Controls controls = rocket.GetControls();

            if (controls != null)
            {
                return controls.GetAccuracy();
            }
            return 0.0f;
        }
    }
}
