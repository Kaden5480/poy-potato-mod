using BepInEx.Configuration;

namespace PotatoMod.Config {
    public class Misc {
        public ConfigEntry<bool> lightShadows        { get; private set; }
        public ConfigEntry<bool> distanceActivator   { get; private set; }
        public ConfigEntry<int> distanceActivatorMax { get; private set; }
        public ConfigEntry<int> distanceActivatorMin { get; private set; }

        public Misc(ConfigFile configFile) {
            lightShadows = configFile.Bind(
                "Misc", "lightShadows", true,
                "Whether to enable light shadows"
            );
            distanceActivator = configFile.Bind(
                "Misc", "distanceActivator", true,
                "Whether to enable the distance activator"
            );
            distanceActivatorMax = configFile.Bind(
                "Misc", "distanceActivatorMax", 400,
                "Maximum value for distance activator"
            );
            distanceActivatorMin = configFile.Bind(
                "Misc", "distanceActivatorMin", 100,
                "Minimum value for distance activator"
            );
        }
    }
}
