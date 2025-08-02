using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PotatoMod {
    public class Cache : Loggable {
        public PostProcessLayer camYPost           { get; private set; }
        public GameObject distanceRenderCamera     { get; private set; }
        public PostProcessLayer distanceRenderPost { get; private set; }

        /**
         * <summary>
         * Caches objects in the scene.
         * </summary>
         */
        public void FindObjects() {
            // Access the player's camera
            GameObject cameraHolderObj = GameObject.Find("PlayerCameraHolder");
            if (cameraHolderObj != null) {
                // The camera has two components, X and Y
                foreach (CameraLook cameraLook in cameraHolderObj.GetComponentsInChildren<CameraLook>()) {
                    // Get the Y component
                    if ("PlayerCameraHolder".Equals(cameraLook.gameObject.name) == false) {
                        LogDebug("Found camY PostProcessLayer");
                        camYPost = cameraLook.gameObject.GetComponent<PostProcessLayer>();
                    }
                }
            }

            distanceRenderCamera = GameObject.Find("DistanceRenderCam");
            if (distanceRenderCamera != null) {
                LogDebug("Found DistanceRenderCam");
                distanceRenderPost = distanceRenderCamera.GetComponent<PostProcessLayer>();
            }

            LogDebug("Finished finding objects");
        }

        /**
         * <summary>
         * Clears the cache.
         * </summary>
         */
        public void Clear() {
            camYPost = null;
            distanceRenderCamera = null;
            distanceRenderPost = null;

            LogDebug("Cleared cache");
        }
    }
}
