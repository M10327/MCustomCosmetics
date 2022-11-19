using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCustomCosmetics
{
    public class MCustomCosmeticsConfig : IRocketPluginConfiguration
    {
        public GlobalCosmeticSettings globalCosmeticSettings;
        public int OutfitLimit;
        public void LoadDefaults()
        {
            OutfitLimit = 5;
            globalCosmeticSettings = new GlobalCosmeticSettings()
            {
                Enabled = false,
                OverridePersonalCosmetics = true,
                Hat = 0,
                Mask = 0,
                Glasses = 0,
                Backpack = 0,
                Shirt = 0,
                Vest = 0,
                Pants = 0
            };
        }
    }

    public class GlobalCosmeticSettings
    {
        public bool Enabled;
        public bool OverridePersonalCosmetics;
        public int Hat;
        public int Mask;
        public int Glasses;
        public int Backpack;
        public int Shirt;
        public int Vest;
        public int Pants;
    }
}
