using System.Collections.Generic;

using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PotatoMod {
    public static class Cache {
        public static PostProcessVolume postProcessGlobal;
        public static PostProcessProfile postProcessProfile;

        public static List<bool> postProcessDefaults;
        public static List<PotatoMod.Light> lights;

        public static CustomLevel_DistanceActivator customDistanceActivator;
        public static bool customDistanceActivatorEnabled;
        public static float customDistanceActivatorMin;
        public static float customDistanceActivatorMax;

        public static void FindObjects() {
            GameObject postProcessGlobalObj = GameObject.Find("_PostProcessingGlobal");
            if (postProcessGlobalObj != null) {
                postProcessGlobal = postProcessGlobalObj.GetComponent<PostProcessVolume>();
            }

            if (postProcessGlobal != null) {
                postProcessProfile = (PostProcessProfile) AccessTools.Field(
                    typeof(PostProcessVolume), "m_InternalProfile"
                ).GetValue(postProcessGlobal);

                if (postProcessProfile == null) {
                    Plugin.LogDebug("Profile is missing");
                }

                postProcessDefaults = new List<bool>();
                foreach (PostProcessEffectSettings setting in postProcessProfile.settings) {
                    Plugin.LogDebug($"{setting}: {setting.active}");
                    postProcessDefaults.Add(setting.active);
                }
            }

            lights = new List<PotatoMod.Light>();
            foreach (UnityEngine.Light light in GameObject.FindObjectsOfType<UnityEngine.Light>()) {
                lights.Add(new PotatoMod.Light(light));
            }

            customDistanceActivator = GameObject.FindObjectOfType<CustomLevel_DistanceActivator>();
            if (customDistanceActivator != null) {
                customDistanceActivatorEnabled = customDistanceActivator.enabled;
                customDistanceActivatorMin = customDistanceActivator.activateDistance;
                customDistanceActivatorMax = customDistanceActivator.deactivateDistance;
            }
        }

        public static void Clear() {
            postProcessGlobal = null;
            postProcessProfile = null;
            postProcessDefaults = null;
            lights = null;
            customDistanceActivator = null;
        }
    }
}
