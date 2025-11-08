using BepInEx.Configuration;

namespace PotatoMod.Config {
    public class PostProcess {
        // Allow disabling everything
        public ConfigEntry<bool> disableAll       { get; private set; }

        // Specific options for post processing
        public ConfigEntry<bool> ambientOcclusion { get; private set; }
        public ConfigEntry<bool> bloom            { get; private set; }
        public ConfigEntry<bool> cloudShadows     { get; private set; }
        public ConfigEntry<bool> colorGrading     { get; private set; }
        public ConfigEntry<bool> dithering        { get; private set; }
        public ConfigEntry<bool> fog              { get; private set; }
        public ConfigEntry<bool> lut              { get; private set; }
        public ConfigEntry<bool> pixelize         { get; private set; }
        public ConfigEntry<bool> sharpen          { get; private set; }
        public ConfigEntry<bool> sunshafts        { get; private set; }
        public ConfigEntry<bool> vignette         { get; private set; }

        public PostProcess(ConfigFile configFile) {
            disableAll = configFile.Bind(
                "PostProcessing", "disableAll", false,
                "Whether to disable all post processing"
            );
            ambientOcclusion = configFile.Bind(
                "PostProcessing", "ambientOcclusion", true,
                "Whether to enable ambient occlusion"
            );
            bloom = configFile.Bind(
                "PostProcessing", "bloom", true,
                "Whether to enable bloom"
            );
            cloudShadows = configFile.Bind(
                "PostProcessing", "cloudShadows", true,
                "Whether to enable cloud shadows"
            );
            colorGrading = configFile.Bind(
                "PostProcessing", "colorGrading", true,
                "Whether to enable color grading"
            );
            dithering = configFile.Bind(
                "PostProcessing", "dithering", true,
                "Whether to enable dithering"
            );
            fog = configFile.Bind(
                "PostProcessing", "fog", true,
                "Whether to enable fog"
            );
            lut = configFile.Bind(
                "PostProcessing", "lut", true,
                "Whether to enable LUT"
            );
            pixelize = configFile.Bind(
                "PostProcessing", "pixelize", true,
                "Whether to enable pixelize"
            );
            sharpen = configFile.Bind(
                "PostProcessing", "sharpen", true,
                "Whether to enable sharpen"
            );
            sunshafts = configFile.Bind(
                "PostProcessing", "sunshafts", true,
                "Whether to enable sunshafts"
            );
            vignette = configFile.Bind(
                "PostProcessing", "vignette", true,
                "Whether to enable vignette"
            );
        }
    }
}
