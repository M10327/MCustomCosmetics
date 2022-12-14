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
    public class CommandVehicleSkin : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "VehicleSkin";

        public string Help => "Sets the skin/mythic of a vehicle";

        public string Syntax => "/vehicleskin (skin id or name)";

        public List<string> Aliases => new List<string>() { "vskin" };

        public List<string> Permissions => new List<string>() { "vehicleskin" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var color = MCustomCosmetics.Instance.MessageColor;
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, Syntax, color);
                return;
            }
            UnturnedPlayer p = caller as UnturnedPlayer;
            if (!p.IsInVehicle)
            {
                UnturnedChat.Say(caller, "You are not in a vehicle!", color);
                return;
            }
            var vehicle = p.CurrentVehicle;
            if (!vehicle.isLocked || vehicle.lockedOwner != p.CSteamID)
            {
                UnturnedChat.Say(caller, "This is not your vehicle! Please lock it first.", color);
                return;
            }
            ushort skinId = 0;
            ushort mythicId = 0;

            var cosmetic = Util.GetCosmetic(command[0]);
            if (cosmetic == null)
            {
                UnturnedChat.Say(caller, $"Cosmetic id {command[0]} not found!", color);
                return;
            }
            skinId = Provider.provider.economyService.getInventorySkinID(cosmetic.itemdefid);

            /*
            if (command.Length >= 2)
            {
                var mythCos = Util.GetCosmetic(command[1]);
                if (mythCos == null)
                {
                    UnturnedChat.Say(caller, $"Cosmetic id {command[1]} not found!", color);
                    return;
                }
                mythicId = Provider.provider.economyService.getInventoryMythicID(mythCos.itemdefid);
            }
            */

            vehicle.tellSkin(skinId, mythicId);
            VehicleManager.ReceiveVehicleSkin(vehicle.instanceID, skinId, mythicId);
            // i cannot believe this works
            var cl = ClientStaticMethod<uint, ushort, ushort>.Get(new ClientStaticMethod<uint, ushort, ushort>.ReceiveDelegate(VehicleManager.ReceiveVehicleSkin));
            object[] pars = new object[] { ENetReliability.Reliable, Provider.EnumerateClients_Remote(), vehicle.instanceID, skinId, mythicId };
            cl.GetType().GetTypeInfo().GetDeclaredMethod("InvokeAndLoopback").Invoke(cl, pars);
            //source for this: VehicleManager.SendVehicleSkin.InvokeAndLoopback(ENetReliability.Reliable, Provider.EnumerateClients_Remote(), vehicle.instanceID, skinId, mythicId);
            UnturnedChat.Say(caller, $"Set your vehicle skin to {cosmetic.name}", color);
        }
    }
}
