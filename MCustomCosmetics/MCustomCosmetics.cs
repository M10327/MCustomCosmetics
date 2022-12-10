using HarmonyLib;
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
using UnityEngine;

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
            pData.CommitToFile();
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
            Rocket.Core.Logging.Logger.Log($"{Name} {Assembly.GetName().Version} has been loaded!");
            Rocket.Core.Logging.Logger.Log($"Permissions are the command names!");
            Rocket.Core.Logging.Logger.Log($"Bypass outfit permission is \'OutfitBypassLimit\'");
        }

        protected override void Unload()
        {
            pData.CommitToFile();
            Patches.UnpatchAll();
        }
    }

    internal class Patches
    {
        private static Harmony PatcherInstance;
        internal static void PatchAll()
        {
            PatcherInstance = new Harmony("MCustomCosmetics");
            PatcherInstance.PatchAll();
        }
        internal static void UnpatchAll()
        {
            PatcherInstance.UnpatchAll("MCustomCosmetics");
        }

        [HarmonyPatch]
        internal class ProviderAccept
        {
            [HarmonyPatch(typeof(Provider))]
            [HarmonyPatch("accept")]
            [HarmonyPatch(new Type[] { typeof(SteamPlayerID), typeof(bool), typeof(bool), typeof(byte), typeof(byte), typeof(byte), typeof(Color), typeof(Color), typeof(Color), typeof(bool), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int[]), typeof(string[]), typeof(string[]), typeof(EPlayerSkillset), typeof(string), typeof(CSteamID), typeof(EClientPlatform) })]
            [HarmonyPrefix]
            static void Accept(SteamPlayerID playerID, bool isPro, bool isAdmin, byte face, byte hair, byte beard, Color skin, ref Color color, Color markerColor, bool hand, ref int shirtItem, ref int pantsItem, ref int hatItem, ref int backpackItem, ref int vestItem, ref int maskItem, ref int glassesItem, ref int[] skinItems, ref string[] skinTags, ref string[] skinDynamicProps, EPlayerSkillset skillset, string language, CSteamID lobbyID)
            {
                if (MCustomCosmetics.Instance.pData.data.ContainsKey((ulong)playerID.steamID))
                {
                    var pData = MCustomCosmetics.Instance.pData.data[(ulong)playerID.steamID];
                    if (!pData.Outfits.ContainsKey(pData.SelectedFit))
                    {
                        pData.SelectedFit = "none";
                        MCustomCosmetics.Instance.pData.data[(ulong)playerID.steamID].SelectedFit = "none";
                        MCustomCosmetics.Instance.pData.CommitToFile();

                    }
                    if (pData.SelectedFit != "none")
                    {
                        if (pData.Outfits[pData.SelectedFit].Hat != 0) hatItem = pData.Outfits[pData.SelectedFit].Hat;
                        if (pData.Outfits[pData.SelectedFit].Mask != 0) maskItem = pData.Outfits[pData.SelectedFit].Mask;
                        if (pData.Outfits[pData.SelectedFit].Glasses != 0) glassesItem = pData.Outfits[pData.SelectedFit].Glasses;
                        if (pData.Outfits[pData.SelectedFit].Backpack != 0) backpackItem = pData.Outfits[pData.SelectedFit].Backpack;
                        if (pData.Outfits[pData.SelectedFit].Shirt != 0) shirtItem = pData.Outfits[pData.SelectedFit].Shirt;
                        if (pData.Outfits[pData.SelectedFit].Vest != 0) vestItem = pData.Outfits[pData.SelectedFit].Vest;
                        if (pData.Outfits[pData.SelectedFit].Pants != 0) pantsItem = pData.Outfits[pData.SelectedFit].Pants;
                        if (pData.Outfits[pData.SelectedFit].Hair != null)
                        {
                            color = new Color(pData.Outfits[pData.SelectedFit].Hair.R / 255, pData.Outfits[pData.SelectedFit].Hair.G / 255, pData.Outfits[pData.SelectedFit].Hair.B / 255);
                        }
                        List<int> newItems = skinItems.ToList();
                        List<string> newTags = skinTags.ToList();
                        List<string> newProps = skinDynamicProps.ToList();
                        foreach (var x in pData.Outfits[pData.SelectedFit].skins)
                        {
                            newItems.Add(x.Key);
                            newTags.Add(x.Value);
                            newProps.Add("");
                        }
                        newItems.Reverse();
                        newTags.Reverse();
                        newProps.Reverse();
                        skinItems = newItems.ToArray();
                        skinTags = newTags.ToArray();
                        skinDynamicProps = newProps.ToArray();
                    }
                }
                if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Enabled)
                {
                    if (!MCustomCosmetics.Instance.globalCos.ContainsKey((ulong)playerID.steamID))
                    {
                        MCustomCosmetics.Instance.globalCos[(ulong)playerID.steamID] = true;
                    }
                    if (!MCustomCosmetics.Instance.globalCos[(ulong)playerID.steamID])
                    {
                        return;
                    }
                    var gcos = MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings;
                    if (gcos.Hat > 0) 
                        if (gcos.OverridePersonalCosmetics || (!gcos.OverridePersonalCosmetics && hatItem == 0))
                            hatItem = gcos.Hat;
                    else if (gcos.Hat == -1) hatItem = 0;
                    if (gcos.Mask > 0)
                        if (gcos.OverridePersonalCosmetics || (!gcos.OverridePersonalCosmetics && maskItem == 0))
                            maskItem = gcos.Mask;
                    else if (gcos.Mask == -1) maskItem = 0;
                    if (gcos.Glasses > 0)
                        if (gcos.OverridePersonalCosmetics || (!gcos.OverridePersonalCosmetics && glassesItem == 0))
                            glassesItem = gcos.Glasses;
                    else if (gcos.Glasses == -1) glassesItem = 0;
                    if (gcos.Backpack > 0)
                        if (gcos.OverridePersonalCosmetics || (!gcos.OverridePersonalCosmetics && backpackItem == 0))
                            backpackItem = gcos.Backpack;
                    else if (gcos.Backpack == -1) backpackItem = 0;
                    if (gcos.Shirt > 0)
                        if (gcos.OverridePersonalCosmetics || (!gcos.OverridePersonalCosmetics && shirtItem == 0))
                            shirtItem = gcos.Shirt;
                    else if (gcos.Shirt == -1) shirtItem = 0;
                    if (gcos.Vest > 0)
                        if (gcos.OverridePersonalCosmetics || (!gcos.OverridePersonalCosmetics && vestItem == 0))
                            vestItem = gcos.Vest;
                    else if (gcos.Vest == -1) vestItem = 0;
                    if (gcos.Pants > 0)
                        if (gcos.OverridePersonalCosmetics || (!gcos.OverridePersonalCosmetics && pantsItem == 0))
                            pantsItem = gcos.Pants;
                    else if (gcos.Pants == -1) pantsItem = 0;
                }
            }
        }
    }
}
