using System;

using BepInEx;
using UnityEngine.SceneManagement;

using PotatoMod.Config;

namespace PotatoMod {
    [BepInPlugin("com.github.Kaden5480.poy-potato-mod", "Potato Mod", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        public static Plugin instance { get; private set; }

        public Cache cache { get; } = new Cache();
        public Cfg config { get; } = new Cfg();

        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        private void Awake() {
            instance = this;

            config.disablePostProcessing = Config.Bind(
                "General", "disablePostProcessing", true,
                "Whether to disable post processing"
            );

            config.disableDistanceRender = Config.Bind(
                "General", "disableDistanceRender", false,
                "Whether to disable the distance render camera"
            );

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        /**
         * <summary>
         * Executes when the plugin is being destroyed.
         * </summary>
         */
        private void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        /**
         * <summary>
         * Executes when a scene was loaded.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         * <param name="mode">The mode the scene was loaded with</param>
         */
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            cache.FindObjects();

            if (config.disableDistanceRender.Value == true) {
                if (cache.distanceRenderCamera != null) {
                    cache.distanceRenderCamera.SetActive(false);
                    LogDebug("Disabled distance render camera");
                }
            }

            if (config.disablePostProcessing.Value == true) {
                if (cache.camYPost != null) {
                    cache.camYPost.enabled = false;
                    LogDebug("Disabled post processing for player camera");
                }
                if (cache.distanceRenderPost != null) {
                    LogDebug("Disabled post processing for distance render camera");
                    cache.distanceRenderPost.enabled = false;
                }

            }
        }

        /**
         * <summary>
         * Executes when a scene was unloaded.
         * </summary>
         * <param name="scene">The scene which unloaded</param>
         */
        private void OnSceneUnloaded(Scene scene) {
            cache.Clear();
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
