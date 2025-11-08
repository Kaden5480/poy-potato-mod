using Cfg = PotatoMod.Config.Cfg;

namespace PotatoMod.Optimise {
    public class Misc : Loggable {
        private Cfg config;

        public Misc(Cfg config) {
            this.config = config;
        }

        public void UpdateLights() {
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

        public void UpdateDistanceActivator() {
            if (Cache.customDistanceActivator == null) {
                return;
            }

            if (config.enabled.Value == false) {
                Cache.customDistanceActivator.enabled = Cache.customDistanceActivatorEnabled;
                Cache.customDistanceActivator.activateDistance = Cache.customDistanceActivatorMin;
                Cache.customDistanceActivator.deactivateDistance = Cache.customDistanceActivatorMax;
                LogDebug("Restoring default custom distance activator settings");
                return;
            }

            if (config.misc.distanceActivator.Value == false) {
                Cache.customDistanceActivator.enabled = false;
                LogDebug("Disabled distance activator");
                return;
            }

            Cache.customDistanceActivator.enabled = true;
            Cache.customDistanceActivator.activateDistance = config.misc.distanceActivatorMin.Value;
            Cache.customDistanceActivator.deactivateDistance = config.misc.distanceActivatorMax.Value;
            LogDebug("Modified distance activator");
        }

        public void Update() {
            UpdateLights();
            UpdateDistanceActivator();
        }
    }
}
