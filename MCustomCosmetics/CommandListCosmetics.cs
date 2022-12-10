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
            ulong playerId = (ulong)((UnturnedPlayer)caller).CSteamID;
            string skins = "Equipped weapon skins: ";
            if (MCustomCosmetics.Instance.pData.data.ContainsKey(playerId))
            {
                if (!MCustomCosmetics.Instance.pData.data[playerId].Outfits.ContainsKey(MCustomCosmetics.Instance.pData.data[playerId].SelectedFit))
                {
                    UnturnedChat.Say(caller, "You do not have a selected outfit! Select one with /outfit");
                    return;
                }
                var pData = MCustomCosmetics.Instance.pData.data[playerId];
                string clothingSkins = $"" +
                    $"Hat: {FindCosmetic(pData.Outfits[pData.SelectedFit].Hat)} " +
                    $"Mask: {FindCosmetic(pData.Outfits[pData.SelectedFit].Mask)} " +
                    $"Glasses: {FindCosmetic(pData.Outfits[pData.SelectedFit].Glasses)} " +
                    $"Backpack: {FindCosmetic(pData.Outfits[pData.SelectedFit].Backpack)} " +
                    $"Shirt: {FindCosmetic(pData.Outfits[pData.SelectedFit].Shirt)} " +
                    $"Vest: {FindCosmetic(pData.Outfits[pData.SelectedFit].Vest)} " +
                    $"Pants: {FindCosmetic(pData.Outfits[pData.SelectedFit].Pants)}";
                UnturnedChat.Say(caller, clothingSkins);
                if (pData.Outfits[pData.SelectedFit].skins.Count >= 1)
                {
                    foreach (var x in pData.Outfits[pData.SelectedFit].skins)
                    {
                        skins += FindCosmetic(x.Key) + ", ";
                    }
                    UnturnedChat.Say(caller, skins);
                }

            }
            else
            {
                UnturnedChat.Say(caller, "You do not have any cosmetics equipped!");
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
