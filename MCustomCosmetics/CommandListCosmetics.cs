using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCustomCosmetics
{
    public class CommandListCosmetics : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "listcosmetics";

        public string Help => "lists all equipped cosmetics";

        public string Syntax => "lcos";

        public List<string> Aliases => new List<string>() { "lcos" };

        public List<string> Permissions => new List<string>() { "listcosmetics" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var color = MCustomCosmetics.Instance.MessageColor;
            ulong playerId = (ulong)((UnturnedPlayer)caller).CSteamID;
            if (MCustomCosmetics.Instance.pData.data.ContainsKey(playerId))
            {
                if (!MCustomCosmetics.Instance.pData.data[playerId].Outfits.ContainsKey(MCustomCosmetics.Instance.pData.data[playerId].SelectedFit))
                {
                    UnturnedChat.Say(caller, "You do not have a selected outfit! Select one with /outfit", color);
                    return;
                }
                var pData = MCustomCosmetics.Instance.pData.data[playerId];
                UnturnedChat.Say(caller, $"Hat: {FindCosmetic(pData.Outfits[pData.SelectedFit].Hat)}\nMask: {FindCosmetic(pData.Outfits[pData.SelectedFit].Mask)}", color);
                UnturnedChat.Say(caller, $"Glasses: {FindCosmetic(pData.Outfits[pData.SelectedFit].Glasses)}\nBackpack: {FindCosmetic(pData.Outfits[pData.SelectedFit].Backpack)}", color);
                UnturnedChat.Say(caller, $"Shirt: {FindCosmetic(pData.Outfits[pData.SelectedFit].Shirt)}\nVest: {FindCosmetic(pData.Outfits[pData.SelectedFit].Vest)}", color);
                UnturnedChat.Say(caller, $"Pants: {FindCosmetic(pData.Outfits[pData.SelectedFit].Pants)}", color);
                if (pData.Outfits[pData.SelectedFit].skins.Count >= 1)
                {
                    List<string> skins = new List<string>();
                    foreach (var s in pData.Outfits[pData.SelectedFit].skins)
                    {
                        skins.Add(FindCosmetic(s.Key));
                    }
                    skins.Sort();
                    List<string> skins2 = new List<string>();
                    for (int i = 0; i < skins.Count; i += 2)
                    {
                        string add = "";
                        if (skins.Count > i)
                        {
                            add += skins[i];
                        }
                        if (skins.Count > i + 1)
                        {
                            add += "\n" + skins[i + 1];
                        }
                        skins2.Add(add);
                    }
                    foreach (var tell in skins2)
                    {
                        UnturnedChat.Say(caller, tell, color);
                    }
                }
            }
            else
            {
                UnturnedChat.Say(caller, "You do not have any cosmetics equipped!", color);
            }
        }

        public string FindCosmetic(int input)
        {
            var search = input.ToString();
            var econInfos = TempSteamworksEconomy.econInfo;
            UnturnedEconInfo cosmetic;
            cosmetic = int.TryParse(search, out int searchId) ? econInfos.FirstOrDefault(x => x.itemdefid == searchId) : econInfos.FirstOrDefault(x => x.name.ToLower().Contains(search.ToLower()));
            if (cosmetic != null)
            {
                return $"{cosmetic.name} ({cosmetic.itemdefid})";
            }
            else
            {
                return "none";
            }

        }
    }
}
