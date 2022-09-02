using HarmonyLib;
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
    internal class Patches
    {
        private static Harmony PatcherInstance;
        // run in Load()
        internal static void PatchAll()
        {
            PatcherInstance = new Harmony("MCustomCosmetics");
            PatcherInstance.PatchAll();
            Rocket.Core.Logging.Logger.Log("Skins editor loaded");
        }
        // run in Unload()
        internal static void UnpatchAll()
        {
            PatcherInstance.UnpatchAll("MCustomCosmetics");
            Rocket.Core.Logging.Logger.Log("Skins editor unloaded");
        }

        [HarmonyPatch]
        internal class ProviderAccept
        {
            [HarmonyPatch(typeof(Provider))]
            [HarmonyPatch("accept")]
            [HarmonyPatch(new Type[] { typeof(SteamPlayerID), typeof(bool), typeof(bool), typeof(byte), typeof(byte), typeof(byte), typeof(Color), typeof(Color), typeof(Color), typeof(bool), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int[]), typeof(string[]), typeof(string[]), typeof(EPlayerSkillset), typeof(string), typeof(CSteamID), typeof(EClientPlatform) })]
            [HarmonyPrefix]
            static void Accept(SteamPlayerID playerID, bool isPro, bool isAdmin, byte face, byte hair, byte beard, Color skin, Color color, Color markerColor, bool hand, ref int shirtItem, ref int pantsItem, ref int hatItem, ref int backpackItem, ref int vestItem, ref int maskItem, ref int glassesItem, ref int[] skinItems, ref string[] skinTags, ref string[] skinDynamicProps, EPlayerSkillset skillset, string language, CSteamID lobbyID)
            {
                if (MCustomCosmetics.Instance.pData.data.ContainsKey((ulong)playerID.steamID))
                {
                    var pData = MCustomCosmetics.Instance.pData.data[(ulong)playerID.steamID];
                    if (pData.Hat != 0) hatItem = pData.Hat;
                    if (pData.Mask != 0) maskItem = pData.Mask;
                    if (pData.Glasses != 0) glassesItem = pData.Glasses;
                    if (pData.Backpack != 0) backpackItem = pData.Backpack;
                    if (pData.Shirt != 0) shirtItem = pData.Shirt;
                    if (pData.Vest != 0) vestItem = pData.Vest;
                    if (pData.Pants != 0) pantsItem = pData.Pants;
                    List<int> newItems = skinItems.ToList();
                    List<string> newTags = skinTags.ToList();
                    List<string> newProps = skinDynamicProps.ToList();
                    foreach (var x in pData.skins)
                    {
                        newItems.Add(x.Key);
                        newTags.Add(x.Value);
                        newProps.Add("");
                    }
                    skinItems = newItems.ToArray();
                    skinTags = newTags.ToArray();
                    skinDynamicProps = newProps.ToArray();
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
                    if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Hat > 0) hatItem = MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Hat;
                    else if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Hat == -1) hatItem = 0;
                    if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Mask > 0) maskItem = MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Mask;
                    else if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Mask == -1) maskItem = 0;
                    if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Glasses > 0) glassesItem = MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Glasses;
                    else if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Glasses == -1) glassesItem = 0;
                    if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Backpack > 0) backpackItem = MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Backpack;
                    else if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Backpack == -1) backpackItem = 0;
                    if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Shirt > 0) shirtItem = MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Shirt;
                    else if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Shirt == -1) shirtItem = 0;
                    if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Vest > 0) vestItem = MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Vest;
                    else if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Vest == -1) vestItem = 0;
                    if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Pants > 0) pantsItem = MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Pants;
                    else if (MCustomCosmetics.Instance.Configuration.Instance.globalCosmeticSettings.Pants == -1) pantsItem = 0;
                }
            }

            public static async void PrintSkins(int[] skinItems, string[] skinTags, string[] skinDynamicProps)
            {
                await Task.Delay(1);
                Rocket.Core.Logging.Logger.Log("Dynamic Props");
                foreach (var x in skinDynamicProps)
                {
                    Rocket.Core.Logging.Logger.Log(x);
                }
                Rocket.Core.Logging.Logger.Log("Skin Tags");
                foreach (var x in skinTags)
                {
                    Rocket.Core.Logging.Logger.Log(x);
                }
                Rocket.Core.Logging.Logger.Log("Skin Items");
                foreach (var x in skinItems)
                {
                    Rocket.Core.Logging.Logger.Log(x.ToString());
                }
            }
        }
    }
}
