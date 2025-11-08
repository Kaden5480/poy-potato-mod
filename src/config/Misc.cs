using BepInEx.Configuration;

namespace PotatoMod.Config {
    public class Misc {
        public ConfigEntry<bool> lightShadows { get; private set; }

        public Misc(ConfigFile configFile) {
            lightShadows = configFile.Bind(
                "Misc", "lightShadows", true,
                "Whether to enable light shadows"
            );
        }
    }
}
