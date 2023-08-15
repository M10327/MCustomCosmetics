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
        public bool ClearUnsavedOnReboot;
        public string TextColor;
        public List<string> BlockedCosmetics;
        public CosmeticTypeAllow AllowedCosmeticTypes;
        public void LoadDefaults()
        {
            OutfitLimit = 5;
            ClearUnsavedOnReboot = false;
            TextColor = "cd87ff";
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
            BlockedCosmetics = new List<string>() { "1360", "69200-69201" };
            AllowedCosmeticTypes = new CosmeticTypeAllow()
            {
                Hat = true,
                Mask = true,
                Glasses = true,
                Backpack = true,
                Shirt = true,
                Vest = true,
                Pants = true
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

    public class CosmeticTypeAllow
    {
        public bool Hat;
        public bool Mask;
        public bool Glasses;
        public bool Backpack;
        public bool Shirt;
        public bool Vest;
        public bool Pants;
    }
}
