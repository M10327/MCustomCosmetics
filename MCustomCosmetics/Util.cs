using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.NetTransport;
using SDG.Provider;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCustomCosmetics
{
    public static class Util
    {
        public static UnturnedEconInfo GetCosmetic (string search)
        {
            var econInfos = TempSteamworksEconomy.econInfo;
            UnturnedEconInfo cosmetic;
            cosmetic = int.TryParse(search, out int searchId) ? econInfos.FirstOrDefault(x => x.itemdefid == searchId) : econInfos.FirstOrDefault(x => x.name.ToLower().Contains(search.ToLower()));
            return cosmetic;
        }
    }
}
