using BepInEx.Configuration;
using UnityEngine;

using Cfg = PotatoMod.Config.Cfg;
using Optimiser = PotatoMod.Optimise.Optimiser;

namespace PotatoMod {
    public class UI {
        private const float width = 300f;
        private const float height = 400f;
        private const float padding = 20f;
        private const float elementWidth = 100f;

        private bool enabled = false;
        private Vector2 scrollPosition = Vector2.zero;

        private Cfg config;
        private Optimiser optimiser;


        public UI(Cfg config, Optimiser optimiser) {
            this.config = config;
            this.optimiser = optimiser;
        }

        public void Update() {
            if (Input.GetKeyDown(config.toggleUIKeybind.Value) == false) {
                return;
            }

            enabled = !enabled;
        }

        /**
         * <summary>
         * Special components which trigger updates on the optimiser
         * when a setting is changed.
         * </summary>
         */
        private void UpdateButton(ConfigEntry<bool> setting) {
            string text = (setting.Value == false) ? "Enable" : "Disable";
            if (GUILayout.Button(text) == true) {
                setting.Value = !setting.Value;
                optimiser.Update();
            }
        }
        private void UpdateSlider(ConfigEntry<int> setting, int min, int max) {
            int oldValue = setting.Value;
            setting.Value = (int) GUILayout.HorizontalSlider(setting.Value, min, max);
            if (setting.Value != oldValue) {
                optimiser.Update();
            }
        }

        private void RenderButton(string name, ConfigEntry<bool> setting) {
            GUILayout.BeginHorizontal();

            GUILayout.Label(name, GUILayout.Width(elementWidth));
            UpdateButton(setting);

            GUILayout.EndHorizontal();
        }
        private void RenderSlider(string name, ConfigEntry<int> setting, int min, int max) {
            GUILayout.BeginHorizontal();

            GUILayout.Label(name, GUILayout.Width(elementWidth));
            UpdateSlider(setting, min, max);

            GUILayout.EndHorizontal();
        }

        private void RenderMisc() {
            GUILayout.Label("===== Misc =====");
            RenderButton("Light Shadows", config.misc.lightShadows);
            RenderButton("Distance Activator", config.misc.distanceActivator);
            RenderSlider("Distance Activator Min", config.misc.distanceActivatorMin, 1, 10000);
            RenderSlider("Distance Activator Max", config.misc.distanceActivatorMax, 1, 10000);
        }

        private void RenderPostProcess() {
            GUILayout.Label("===== Post Processing =====");

            string text = (config.postProcess.disableAll.Value == true) ? "Enable" : "Disable";
            if (GUILayout.Button($"{text} Post Processing") == true) {
                config.postProcess.disableAll.Value = !config.postProcess.disableAll.Value;
                optimiser.Update();
            }

            RenderButton("Ambient Occlusion", config.postProcess.ambientOcclusion);
            RenderButton("Bloom", config.postProcess.bloom);
            RenderButton("Cloud Shadows", config.postProcess.cloudShadows);
            RenderButton("Color Grading", config.postProcess.colorGrading);
            RenderButton("Dithering", config.postProcess.dithering);
            RenderButton("Fog", config.postProcess.fog);
            RenderButton("LUT", config.postProcess.lut);
            RenderButton("Pixelize", config.postProcess.pixelize);
            RenderButton("Sharpen", config.postProcess.sharpen);
            RenderButton("Sunshafts", config.postProcess.sunshafts);
            RenderButton("Vignette", config.postProcess.vignette);
        }

        public void Render() {
            if (enabled == false) {
                return;
            }

            float x = 10f;
            float y = 10f;

            GUILayout.BeginArea(
                new Rect(x, y, width, height),
                GUI.skin.box
            );
            scrollPosition = GUILayout.BeginScrollView(scrollPosition,
                GUILayout.Width(width - padding),
                GUILayout.Height(height - padding - 15f)
            );

            GUILayout.Label("===== Global =====");
            UpdateButton(config.enabled);

            RenderPostProcess();
            RenderMisc();

            GUILayout.EndScrollView();

            if (GUILayout.Button("Close", GUILayout.Width(elementWidth)) == true) {
                enabled = false;
            }

            GUILayout.EndArea();
        }
    }
}
