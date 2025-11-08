using Cfg = PotatoMod.Config.Cfg;

namespace PotatoMod.Optimise {
    public class Optimiser : Loggable {
        private Cfg config;

        // Different optimisers
        private Misc misc;
        private PostProcess postProcess;

        public Optimiser(Cfg config) {
            this.config = config;
            misc = new Misc(config);
            postProcess = new PostProcess(config);
        }

        /**
         * <summary>
         * Updates optimisers to use a new configuration.
         * </summary>
         */
        public void Update() {
            misc.Update();
            postProcess.Update();
            LogDebug("Finished optimising level");
        }
    }
}
