namespace PocketRocket
{
    public class RocketComponentFactory
    {
        // Create Rocket Component From Rocket Component Meta
        public static RocketComponent CreateRocketComponent(RocketComponentMeta rocketComponentMeta)
        {
            // Check For Null
            if (rocketComponentMeta == null)
            {
                return null;
            }

            // Get Component Type
            RocketComponentType rocketComponentType = rocketComponentMeta.rocketComponentType;

            switch (rocketComponentType)
            {
                case RocketComponentType.SHIELD:
                    return new Shield(rocketComponentMeta);

                case RocketComponentType.CASING:
                    return new Casing(rocketComponentMeta);

                case RocketComponentType.ENGINE:
                    return new Engine(rocketComponentMeta);

                case RocketComponentType.CONTROLS:
                    return new Controls(rocketComponentMeta);
            }
            return null;
        }
    }
}
