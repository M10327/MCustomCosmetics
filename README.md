# MCustomCosmetics

Requires [Harmony Patcher](https://github.com/pardeike/Harmony/releases/tag/v2.2.2.0)

Unturned plugin for applying any possible cosmetic to yourself via command or globally via the config.

All permissions are just the command name. You can use the itemdefid (found in the game's econinfo.json) or cosmetic name to apply them. Requires relogging to update your cosmetics. To globally disable a cosmetic slot (ie: prevent anyone from wearing backpacks) turn on global cosmetics and set the id in that slot to -1.
Permission to bypass outfit limit is `OutfitBypassLimit`
Permission to save cosmetics (if config option to clear unsaved ones is turned on) is `CosmeticsAllowSaving`
### Commands:
- /cosmetic mythics : Shows available mythic effects
- /cosmetic < itemdefid or name > (mythical effect) : Add a cosmetic and optionally a mythic effect. Mythic effects only apply to weapons
- /removecosetic < all/id/hat/glasses/mask/backpack/vest/shirt/pants > : removes all or just one cosmetic you have applied via the plugin.
- /listcosmetics : lists all cosmetics you have applied via the plugin. 
- /togglecos : Toggles whether the global cosmetics applies to you. 
- /outfit < create/delete/select/list/clone > (name) : Manage outfits
- /hair < none > or < r > < g > < b > : Sets the hair color for your current outfit
- /mannequin < itemdefid or name > : Applies the cosmetic to the mannequin you are looking at
- /itemdisplay < item id or name > (skin id or name) (mythical) : Set the item display for the barricade you are looking at
- /vehicleskin < skin id or name> : Applies a skin to the vehicle you are in. WARNING: This uses unsafe methods and could potentially corrupt your vehicles.dat in your level folder. Use at your own risk.

### Default Aliases:
- /cosmetic : /cos
- /removecosmetic : /rcos
- /listcosmetics : /lcos
- /togglecos : /tcos
- /mannequin : /ma
- /vehicleskin : /vskin

### To Do (eventually):
- Add translations
- Ability to share outfits via a command that generates a code. 