using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MCustomCosmetics
{
    public class CommandOutfit : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "outfit";

        public string Help => "Change or manage outfits";

        public string Syntax => "/outfit <create/delete/select/list> (name)";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer p = caller as UnturnedPlayer;
            if (!MCustomCosmetics.Instance.pData.data.ContainsKey((ulong)p.CSteamID))
            {
                UnturnedChat.Say(caller, "You do not have any cosmetics set! Use /cosmetic first");
                return;
            }
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, Syntax);
                return;
            }
            List<string> fits = new List<string>();
            foreach (var f in MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Outfits)
            {
                fits.Add(f.Key);
            }
            if (command[0].ToLower() == "list")
            {
                UnturnedChat.Say(caller, $"Available outfits: None, {string.Join(", ", fits)}");
                return;
            }
            else if (command.Length < 2)
            {
                UnturnedChat.Say(caller, Syntax);
                return;
            }
            command[1] = Regex.Replace(command[1], "[^A-Za-z0-9 -]", "");
            if (command[1].Length < 1)
            {
                UnturnedChat.Say(caller, "Please reformat your outfit name. It must be alphanumeric and at least 1 character long");
                return;
            }
            switch (command[0].ToLower())
            {
                case "create":
                    if (MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Outfits.Count >= MCustomCosmetics.Instance.Configuration.Instance.OutfitLimit)
                    {
                        UnturnedChat.Say(caller, "You cannot create any more outfits! Please remove one first.");
                        return;
                    }
                    MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Outfits[command[1]] = new Outfit()
                    {
                        Hat = 0,
                        Mask = 0,
                        Glasses = 0,
                        Backpack = 0,
                        Shirt = 0,
                        Vest = 0,
                        skins = new Dictionary<int, string>()
                    };
                    MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].SelectedFit = command[1];
                    UnturnedChat.Say(caller, $"Created and equipped outfit: {command[1]}");
                    MCustomCosmetics.Instance.pData.CommitToFile();
                    break;
                case "delete":
                    if (MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Outfits.ContainsKey(command[1]))
                    {
                        MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Outfits.Remove(command[1]);
                        UnturnedChat.Say(caller, $"Deleted outfit {command[1]}");
                        if (MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].SelectedFit == command[1])
                        {
                            MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].SelectedFit = "none";
                        }
                        MCustomCosmetics.Instance.pData.CommitToFile();
                    }
                    else
                    {
                        UnturnedChat.Say(caller, "That is not a valid outfit you own!");
                        return;
                    }
                    break;
                case "select":
                    if (command[1].ToLower() == "none")
                    {
                        MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].SelectedFit = "none";
                        UnturnedChat.Say(caller, "Removed your outfit");
                        MCustomCosmetics.Instance.pData.CommitToFile();
                    }
                    else if (MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].Outfits.ContainsKey(command[1]))
                    {
                        MCustomCosmetics.Instance.pData.data[(ulong)p.CSteamID].SelectedFit = command[1];
                        UnturnedChat.Say(caller, $"Selecte outfit {command[1]}, Relog to see changes");
                        MCustomCosmetics.Instance.pData.CommitToFile();
                    }
                    else
                    {
                        UnturnedChat.Say(caller, "That is not a valid outfit you own!");
                        return;
                    }
                    break;
                default:
                    UnturnedChat.Say(caller, Syntax);
                    return;
            }
            MCustomCosmetics.Instance.pData.CommitToFile();
        }
    }
}
