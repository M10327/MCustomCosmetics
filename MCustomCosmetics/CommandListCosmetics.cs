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

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            ulong playerId = (ulong)((UnturnedPlayer)caller).CSteamID;
            string skins = "Equipped weapon skins: ";
            if (MCustomCosmetics.Instance.pData.data.ContainsKey(playerId))
            {
                var pData = MCustomCosmetics.Instance.pData.data[playerId];
                string clothingSkins = $"" +
                    $"Hat: {FindCosmetic(pData.Hat)} " +
                    $"Mask: {FindCosmetic(pData.Mask)} " +
                    $"Glasses: {FindCosmetic(pData.Glasses)} " +
                    $"Backpack: {FindCosmetic(pData.Backpack)} " +
                    $"Shirt: {FindCosmetic(pData.Shirt)} " +
                    $"Vest: {FindCosmetic(pData.Vest)} " +
                    $"Pants: {FindCosmetic(pData.Pants)}";
                UnturnedChat.Say(caller, clothingSkins);
                if (pData.skins.Count >= 1)
                {
                    foreach (var x in pData.skins)
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
