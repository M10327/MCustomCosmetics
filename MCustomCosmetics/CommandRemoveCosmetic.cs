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
    internal class CommandRemoveCosmetic : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "removecosmetic";

        public string Help => "remove a cosmetic or all of them";

        public string Syntax => "/rcos <all/id/hat shirt pants etc>";

        public List<string> Aliases => new List<string>() { "rcos" };

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer p = caller as UnturnedPlayer;
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, "No arguments given! /rcos <all/id/hat shirt pants etc>");
                return;
            }
            if (!MCustomCosmetics.Instance.pData.data.ContainsKey((ulong)p.CSteamID))
            {
                UnturnedChat.Say(caller, "You don't have any cosmetics set!");
                return;

            }
            if (int.TryParse(command[0], out _))
            {
                var search = command[0];
                var econInfos = TempSteamworksEconomy.econInfo;
                UnturnedEconInfo cosmetic;
                cosmetic = int.TryParse(search, out int searchId) ? econInfos.FirstOrDefault(x => x.itemdefid == searchId) : econInfos.FirstOrDefault(x => x.name.ToLower().Contains(search.ToLower()));

                if (cosmetic == null)
                {
                    UnturnedChat.Say(caller, "Cosmetic id " + search + " not found!");
                    return;
                }
                if (MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].skins.ContainsKey(searchId))
                {
                    UnturnedChat.Say(caller, "Removed " + cosmetic.name);
                    MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].skins.Remove(searchId);
                }
                else
                {
                    UnturnedChat.Say(caller, "You do not have " + cosmetic.name + " equipped");
                }
            }
            else
            {
                switch (command[0].ToLower())
                {
                    case "all":
                        MCustomCosmetics.Instance.pData.data.Remove((ulong)p.CSteamID);
                        UnturnedChat.Say(caller, "Removed all custom cosmetics");
                        break;
                    case "hat":
                        MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Hat = 0;
                        UnturnedChat.Say(caller, "Removed your custom hat");
                        break;
                    case "mask":
                        MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Mask = 0;
                        UnturnedChat.Say(caller, "Removed your custom mask");
                        break;
                    case "glasses":
                        MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Glasses = 0;
                        UnturnedChat.Say(caller, "Removed your custom glasses");
                        break;
                    case "backpack":
                        MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Backpack = 0;
                        UnturnedChat.Say(caller, "Removed your custom backpack");
                        break;
                    case "shirt":
                        MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Shirt = 0;
                        UnturnedChat.Say(caller, "Removed your custom shirt");
                        break;
                    case "vest":
                        MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Vest = 0;
                        UnturnedChat.Say(caller, "Removed your custom vest");
                        break;
                    case "pants":
                        MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Pants = 0;
                        UnturnedChat.Say(caller, "Removed your custom pants");
                        break;
                    default:
                        UnturnedChat.Say(caller, "bad arguments given! /rcos <all/id/hat shirt pants etc>");
                        break;
                }
            }

            MCustomCosmetics.Instance.pData.CommitToFile();
        }
    }
}
