namespace PocketRocket
{
    public class Shield : RocketComponent
    {
        // Shield Key
        public static readonly string SHIELD_KEY = "Shield";

        // Fuel
        private float shield, current;

        // Constructor
        public Shield(RocketComponentMeta rocketComponentMeta) : base(rocketComponentMeta)
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
            this.shield = rocketComponentMeta.GetFloatValue(Shield.SHIELD_KEY);
            this.current = shield;
        }

        // Get Shield
        public float GetShield()
        {
            return shield;
        }

        // Set Current Shield
        public void SetCurrentShield(float shield)
        {
            this.current = shield;
        }

        // Get Current Shield
        public float GetCurrentShield()
        {
            return current;
        }
    }

    public static class ShieldExtension
    {
        // Extending Method "Get Shield"
        public static float GetShield(this Rocket rocket)
        {
            Shield shield = rocket.GetShield();

            if (shield != null)
            {
                return shield.GetShield();
            }
            return 0.0f;
        }

        // Extending Method "Set Current Shield"
        public static void SetCurrentShield(this Rocket rocket, float value)
        {
            Shield shield = rocket.GetShield();

            if (shield != null)
            {
                shield.SetCurrentShield(value);
            }
        }

        // Extending Method "Get Current Shield"
        public static float GetCurrentShield(this Rocket rocket)
        {
            Shield shield = rocket.GetShield();

            if (shield != null)
            {
                return shield.GetCurrentShield();
            }
            return 0.0f;
        }

        // Extending Method "Damage"
        public static void Damage(this Rocket rocket, float damage)
        {
            Shield shield = rocket.GetShield();

            if (shield != null)
            {
                float temp = shield.GetCurrentShield() - damage;

                if (temp < 0)
                {
                    temp = 0;
                }
                shield.SetCurrentShield(temp);
            }
        }
    }
}
