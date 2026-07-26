using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DeepwaterEngagementSuite;

[JsonConverter(typeof(StringEnumConverter))]
public enum IconPickerIndex
{
    OtherChests,
    BottledItemChest,
    GoldTreasureChest,
    ClamTreasureChest,
    CurrencyTreasureChest,
    UniqueWeaponChest,
    UniqueArmourChest,
    ScarabChest,
    StackedDecksChest,
    MapsChest,
    CursedDucatDrop,
    RandomDucatChest,
    IzaroObject,
    AltarCrab,
    AltarOctopus,
    TormentedSpiritEncounter,
    LanternReplenishEncounter,
}
