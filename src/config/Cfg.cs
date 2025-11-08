using BepInEx.Configuration;
using UnityEngine;

namespace PotatoMod.Config {
    public class Cfg {
        public Misc misc               { get; private set; }
        public PostProcess postProcess { get; private set; }

        public ConfigEntry<bool> enabled            { get; private set; }
        public ConfigEntry<KeyCode> toggleUIKeybind { get; private set; }

        public Cfg(ConfigFile configFile) {
            misc = new Misc(configFile);
            postProcess = new PostProcess(configFile);

            enabled = configFile.Bind(
                "General", "enabled", false,
                "Whether optimisations are enabled"
            );
            toggleUIKeybind = configFile.Bind(
                "General", "toggleUIKeybind", KeyCode.Home,
                "The keybind to toggle the UI"
            );
        }
    }
}
