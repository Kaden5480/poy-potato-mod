using SCPE;
using UnityEngine.Rendering.PostProcessing;

using Cfg = PotatoMod.Config.Cfg;

namespace PotatoMod.Optimise {
    public class PostProcess : Loggable {
        private Cfg config;

        /**
         * <summary>
         * Initializes a PostProcess optimiser.
         * </summary>
         * <param name="config">The config to use</param>
         */
        public PostProcess(Cfg config) {
            this.config = config;
        }

        /**
         * <summary>
         * Updates the state of post processing optimisation.
         * </summary>
         */
        public void Update() {
            if (Cache.postProcessGlobal == null) {
                LogDebug("Unable to configure post processing, the object doesn't exist");
                return;
            }

            // Check if defaults should be restored
            if (config.enabled.Value == false) {
                Cache.postProcessGlobal.enabled = true;
                for (int i = 0; i < Cache.postProcessProfile.settings.Count; i++) {
                    Cache.postProcessProfile.settings[i].active = Cache.postProcessDefaults[i];
                }
                LogDebug("Optimisations disabled, restored default settings");
                return;
            }

            // Check for disabling everything first
            if (config.postProcess.disableAll.Value == true) {
                Cache.postProcessGlobal.enabled = false;
                LogDebug("Disabled all post processing");
                return;
            }
            else {
                Cache.postProcessGlobal.enabled = true;
                LogDebug("Kept post processing object enabled");
            }

            foreach (PostProcessEffectSettings setting in Cache.postProcessProfile.settings) {
                switch (setting) {
                    case AmbientOcclusion _:
                        setting.active = config.postProcess.ambientOcclusion.Value;
                        break;
                    case Bloom _:
                        setting.active = config.postProcess.bloom.Value;
                        break;
                    case CloudShadows _:
                        setting.active = config.postProcess.cloudShadows.Value;
                        break;
                    case ColorGrading _:
                        setting.active = config.postProcess.colorGrading.Value;
                        break;
                    case Dithering _:
                        setting.active = config.postProcess.dithering.Value;
                        break;
                    case SCPE.Fog _:
                        setting.active = config.postProcess.fog.Value;
                        break;
                    case LUT _:
                        setting.active = config.postProcess.lut.Value;
                        break;
                    // Motion blur is unused
                    //case MotionBlur _:
                    //    break;
                    case Pixelize _:
                        setting.active = config.postProcess.pixelize.Value;
                        break;
                    case Sharpen _:
                        setting.active = config.postProcess.sharpen.Value;
                        break;
                    case Sunshafts _:
                        setting.active = config.postProcess.sunshafts.Value;
                        break;
                    case Vignette _:
                        setting.active = config.postProcess.vignette.Value;
                        break;
                    default:
                        break;
                }
            }

            LogDebug("Finished configuring individual post processing settings");
        }
    }
}
