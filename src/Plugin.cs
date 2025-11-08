using System;

using BepInEx;
using HarmonyLib;
using UnityEngine.SceneManagement;

using Cfg = PotatoMod.Config.Cfg;
using Optimiser = PotatoMod.Optimise.Optimiser;

namespace PotatoMod {
    [BepInPlugin("com.github.Kaden5480.poy-potato-mod", "Potato Mod", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        public static Plugin instance { get; private set; }

        private Cfg config;
        private Optimiser optimiser;
        private UI ui;

        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        private void Awake() {
            instance = this;

            SceneManager.sceneUnloaded += OnSceneUnloaded;

            Harmony.CreateAndPatchAll(typeof(PatchSceneLoads));

            config = new Cfg(this.Config);
            optimiser = new Optimiser(config);
            ui = new UI(config, optimiser);
        }

        /**
         * <summary>
         * Executes when the plugin is being destroyed.
         * </summary>
         */
        private void OnDestroy() {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        /**
         * <summary>
         * Executes each frame.
         * </summary>
         */
        private void Update() {
            ui.Update();
        }

        /**
         * <summary>
         * Executes to render the UI.
         * </summary>
         */
        private void OnGUI() {
            ui.Render();
        }

        /**
         * <summary>
         * Handles all scene loads.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         */
        private void DispatchSceneLoad(Scene scene) {
            Cache.FindObjects();
            LogDebug("Cached scene objects");

            optimiser.Update();
        }

        /**
         * <summary>
         * Handles all scene unloads.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         */
        private void DispatchSceneUnload(Scene scene) {
            Cache.Clear();
            LogDebug("Cleared cache");
        }

        /**
         * <summary>
         * Executes when a scene was unloaded.
         * </summary>
         * <param name="scene">The scene which unloaded</param>
         */
        private void OnSceneUnloaded(Scene scene) {
            LogDebug("Unity scene unload dispatched");
            DispatchSceneUnload(scene);
        }

        /**
         * <summary>
         * Dispatches scene load calls for non-custom levels.
         * </summary>
         */
        protected static class PatchSceneLoads {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(EnterPeakScene), "Start")]
            [HarmonyPatch(typeof(EnterRoomSegmentScene), "Start")]
            public static void NormalPlay() {
                Scene scene = SceneManager.GetActiveScene();
                if (scene.buildIndex == 69) {
                    return;
                }

                LogDebug("Normal scene loaded");
                instance.DispatchSceneLoad(scene);
            }

            /**
             * <summary>
             * Dispatches scene load calls when a custom level (in normal play mode)
             * has been fully loaded.
             * </summary>
             */
            [HarmonyPostfix]
            [HarmonyPatch(typeof(CustomLevel_DistanceActivator), "InitializeObjects")]
            public static void CustomNormalPlay() {
                LogDebug("Custom level (normal play) dispatched");
                instance.DispatchSceneLoad(SceneManager.GetActiveScene());
            }

            /**
             * <summary>
             * Dispatches scene load/unload calls when quick playtest mode
             * is activated/deactivated.
             * </summary>
             */
            [HarmonyPostfix]
            [HarmonyPatch(typeof(LevelEditorManager), "SetPlaymodeObjects")]
            public static void CustomQuickPlay(bool isPlaymode) {
                if (isPlaymode == true) {
                    LogDebug("Custom level (quick playtest) dispatched");
                    instance.DispatchSceneLoad(SceneManager.GetActiveScene());
                }
                else {
                    LogDebug("Custom level (exit quick playtest) dispatched");
                    instance.DispatchSceneUnload(SceneManager.GetActiveScene());
                }
            }
        }



        /**
         * <summary>
         * Logs a debug message.
         * </summary>
         * <param name="message">The message to log</param>
         */
        public static void LogDebug(string message) {
#if DEBUG
            if (instance == null) {
                Console.WriteLine($"[Debug] PotatoMod: {message}");
                return;
            }

            instance.Logger.LogInfo(message);
#else
            if (instance != null) {
                instance.Logger.LogDebug(message);
            }
#endif
        }

        /**
         * <summary>
         * Logs an informational message.
         * </summary>
         * <param name="message">The message to log</param>
         */
        public static void LogInfo(string message) {
            if (instance == null) {
                Console.WriteLine($"[Info] PotatoMod: {message}");
                return;
            }
            instance.Logger.LogInfo(message);
        }

        /**
         * <summary>
         * Logs an error message.
         * </summary>
         * <param name="message">The message to log</param>
         */
        public static void LogError(string message) {
            if (instance == null) {
                Console.WriteLine($"[Error] PotatoMod: {message}");
                return;
            }
            instance.Logger.LogError(message);
        }
    }
}
