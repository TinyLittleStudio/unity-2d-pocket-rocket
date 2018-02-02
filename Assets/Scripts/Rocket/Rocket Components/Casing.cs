namespace PocketRocket
{
    public class Casing : RocketComponent
    {
        // Fuel Key
        public static readonly string FUEL_KEY = "Fuel";

        // Fuel
        private float fuel, current;

        // Constructor
        public Casing(RocketComponentMeta rocketComponentMeta) : base(rocketComponentMeta)
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
            this.fuel = rocketComponentMeta.GetFloatValue(Casing.FUEL_KEY);
            this.current = fuel;
        }

        // Get Fuel
        public float GetFuel()
        {
            return fuel;
        }

        // Set Current Fuel
        public void SetCurrentFuel(float fuel)
        {
            this.current = fuel;
        }

        // Get Current Fuel
        public float GetCurrentFuel()
        {
            return current;
        }
    }

    public static class CasingExtension
    {
        // Extending Method "Get Fuel"
        public static float GetFuel(this Rocket rocket)
        {
            Casing casing = rocket.GetCasing();

            if (casing != null)
            {
                return casing.GetFuel();
            }
            return 0.0f;
        }

        // Extending Method "Set Current Fuel"
        public static void SetCurrentFuel(this Rocket rocket, float value)
        {
            Casing casing = rocket.GetCasing();

            if (casing != null)
            {
                casing.SetCurrentFuel(value);
            }
        }

        // Extending Method "Get Current Fuel"
        public static float GetCurrentFuel(this Rocket rocket)
        {
            Casing casing = rocket.GetCasing();

            if (casing != null)
            {
                return casing.GetCurrentFuel();
            }
            return 0.0f;
        }

        // Extending Method "Drain Fuel"
        public static float DrainFuel(this Rocket rocket, float value)
        {
            // Subtract Fuel And Return Remaining Fuel
            Casing casing = rocket.GetCasing();

            if (casing != null)
            {
                float fuel = casing.GetCurrentFuel();

                fuel -= value;

                casing.SetCurrentFuel(fuel);

                return fuel;
            }
            return 0.0f;
        }
    }
}
