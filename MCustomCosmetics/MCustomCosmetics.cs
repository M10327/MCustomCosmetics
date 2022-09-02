using Rocket.Core.Assets;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Permissions;
using SDG.Provider;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCustomCosmetics
{
    public class MCustomCosmetics : RocketPlugin<MCustomCosmeticsConfig>
    {
        public static MCustomCosmetics Instance { get; set; }
        public Dictionary<string, string> mythics;
        public PlayerData pData;
        public Dictionary<ulong, bool> globalCos;
        protected override void Load()
        {
            Instance = this;
            pData = new PlayerData();
            pData.Reload();
            Patches.PatchAll();

            // mythics dict for plugin
            mythics = new Dictionary<string, string>();
            mythics["burning"] = "particle_effect:1";
            mythics["glowing"] = "particle_effect:2";
            mythics["lovely"] = "particle_effect:3";
            mythics["musical"] = "particle_effect:4";
            mythics["shiny"] = "particle_effect:5";
            mythics["glitched"] = "particle_effect:6";
            mythics["wealthy"] = "particle_effect:7";
            //mythics["divine"] = "particle_effect:8";
            mythics["bubbling"] = "particle_effect:9";
            mythics["cosmic"] = "particle_effect:10";
            mythics["electric"] = "particle_effect:11";
            //mythics["rainbow"] = "particle_effect:12";
            mythics["party"] = "particle_effect:13";
            //mythics["haunted"] = "particle_effect:14";
            mythics["freezing"] = "particle_effect:15";
            mythics["energized"] = "particle_effect:16";
            mythics["holiday"] = "particle_effect:17";
            mythics["meta"] = "particle_effect:18";
            //mythics["pyrotechnic"] = "particle_effect:19";
            //mythics["atomic"] = "particle_effect:20";
            mythics["melting"] = "particle_effect:21";
            mythics["confetti"] = "particle_effect:22";
            mythics["radioactive"] = "particle_effect:23";
            mythics["steampunk"] = "particle_effect:24";
            mythics["bloodsucker"] = "particle_effect:25";
            mythics["luckycoins"] = "particle_effect:26";
            mythics["skylantern"] = "particle_effect:27";
            //mythics["firedragon"] = "particle_effect:28";
            //mythics["icedragon"] = "particle_effect:29";
            mythics["blossoming"] = "particle_effect:30";
            //mythics["bananza"] = "particle_effect:31";
            //mythics["hightide"] = "particle_effect:32";
            mythics["deckedout"] = "particle_effect:33";
            mythics["crystalshards"] = "particle_effect:34";
            mythics["soulshattered"] = "particle_effect:35";
            mythics["enchanted"] = "particle_effect:36";
            //mythics["crypticrunes"] = "particle_effect:37";
            //mythics["sacrificial"] = "particle_effect:38";
            mythics["frosty"] = "particle_effect:39";
            mythics["spectralgems"] = "particle_effect:40";
            //mythics["sunrise"] = "particle_effect:41";
            //mythics["sunset"] = "particle_effect:42";
            mythics["electrostatic"] = "particle_effect:43";
            //mythics["wicked"] = "particle_effect:44";
            //mythics["palmnights"] = "particle_effect:45";

            globalCos = new Dictionary<ulong, bool>();
        }

        private void Events_OnPlayerConnected(Rocket.Unturned.Player.UnturnedPlayer player)
        {
            if (!globalCos.ContainsKey((ulong)player.CSteamID))
            {
                globalCos[(ulong)player.CSteamID] = true;
            }
        }

        protected override void Unload()
        {
            Patches.UnpatchAll();
            pData.CommitToFile();
        }
    }
}
