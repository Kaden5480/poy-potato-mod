using UELight = UnityEngine.Light;
using LightShadows = UnityEngine.LightShadows;

namespace PotatoMod {
    public class Light {
        private UELight light;
        private LightShadows defaultShadows;

        public Light(UELight light) {
            this.light = light;
            defaultShadows = light.shadows;
        }

        public void Disable() {
            light.shadows = LightShadows.None;
        }

        public void Restore() {
            light.shadows = defaultShadows;
        }
    }
}
