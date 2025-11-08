using Cfg = PotatoMod.Config.Cfg;

namespace PotatoMod.Optimise {
    public class Misc : Loggable {
        private Cfg config;

        public Misc(Cfg config) {
            this.config = config;
        }

        public void Update() {
            if (config.enabled.Value == false
                || config.misc.lightShadows.Value == true
            ) {
                foreach (Light light in Cache.lights) {
                    light.Restore();
                }
                LogDebug("Restored light shadows");
                return;
            }

            foreach (Light light in Cache.lights) {
                light.Disable();
            }
            LogDebug("Disabled light shadows");
        }
    }
}
