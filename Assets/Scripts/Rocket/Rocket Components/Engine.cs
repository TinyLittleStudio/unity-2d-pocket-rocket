namespace PocketRocket
{
    public class Engine : RocketComponent
    {
        // Accuracy Key
        public static readonly string VELOCITY_KEY = "Velocity";

        // Velocity
        private float velocity;

        // Constructor
        public Engine(RocketComponentMeta rocketComponentMeta) : base(rocketComponentMeta)
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
            this.velocity = rocketComponentMeta.GetFloatValue(Engine.VELOCITY_KEY);
        }

        // Get Velocity
        public float GetVelocity()
        {
            return velocity;
        }
    }

    public static class EngineExtension
    {
        // Extending Method "Get Velocity"
        public static float GetVelocity(this Rocket rocket)
        {
            Engine engine = rocket.GetEngine();

            if (engine != null)
            {
                return engine.GetVelocity();
            }
            return 0.0f;
        }
    }
}
