using System;
using System.Collections.Generic;
using System.Linq;
using ExileCore.Shared.Nodes;

namespace DeepwaterEngagementSuite;

public partial class DeepwaterEngagementSuite
{
    private void InitOnce()
    {
        if (!_initialized)
        {
            _initialized = true;
            var defaultMods = new List<VoyageBorderModifier>
            {
                new() { Id = new TextNode { Value = "DeepwaterBorderPackSize1" }, Abbreviation = new TextNode { Value = "PackSize1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderPackSize2" }, Abbreviation = new TextNode { Value = "PackSize2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderPackSize3" }, Abbreviation = new TextNode { Value = "PackSize3" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMonstersAtLeastMagic" }, Abbreviation = new TextNode { Value = "MonstersAtLeastMagic" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderIncreasedRareMonsters1" }, Abbreviation = new TextNode { Value = "IncreasedRareMonsters1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderIncreasedRareMonsters2" }, Abbreviation = new TextNode { Value = "IncreasedRareMonsters2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderIncreasedRareMonsters3" }, Abbreviation = new TextNode { Value = "IncreasedRareMonsters3" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderAdditionalSeaBeasts1" }, Abbreviation = new TextNode { Value = "AdditionalSeaBeasts1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderAdditionalSeaBeasts2" }, Abbreviation = new TextNode { Value = "AdditionalSeaBeasts2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderAdditionalSeaBeasts3" }, Abbreviation = new TextNode { Value = "AdditionalSeaBeasts3" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderAdditionalCrabs1" }, Abbreviation = new TextNode { Value = "AdditionalCrabs1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderAdditionalCrabs2" }, Abbreviation = new TextNode { Value = "AdditionalCrabs2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderAdditionalCrabs3" }, Abbreviation = new TextNode { Value = "AdditionalCrabs3" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderAdditionalDrowned1" }, Abbreviation = new TextNode { Value = "AdditionalDrowned1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderAdditionalDrowned2" }, Abbreviation = new TextNode { Value = "AdditionalDrowned2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderAdditionalDrowned3" }, Abbreviation = new TextNode { Value = "AdditionalDrowned3" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderChartEffect1" }, Abbreviation = new TextNode { Value = "ChartEffect1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderChartEffect2" }, Abbreviation = new TextNode { Value = "ChartEffect2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderChartEffect3" }, Abbreviation = new TextNode { Value = "ChartEffect3" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderChanceToNotConsumeChart1" }, Abbreviation = new TextNode { Value = "ChanceToNotConsumeChart1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderChanceToNotConsumeChart2" }, Abbreviation = new TextNode { Value = "ChanceToNotConsumeChart2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderGiantOctopus" }, Abbreviation = new TextNode { Value = "GiantOctopus" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderInfiniteLanterns" }, Abbreviation = new TextNode { Value = "InfiniteLanterns" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterAncient" }, Abbreviation = new TextNode { Value = "RareMonsterAncient" }, },
                new()
                {
                    Id = new TextNode { Value = "DeepwaterBorderRareMonsterDivine" }, Abbreviation = new TextNode { Value = "RareMonsterDivine" }, ValueMultiplier = { Value = 10 }
                },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterExalted" }, Abbreviation = new TextNode { Value = "RareMonsterExalted" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterAnnulment" }, Abbreviation = new TextNode { Value = "RareMonsterAnnulment" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterChaos" }, Abbreviation = new TextNode { Value = "RareMonsterChaos" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterVaal" }, Abbreviation = new TextNode { Value = "RareMonsterVaal" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterGemcutters" }, Abbreviation = new TextNode { Value = "RareMonsterGemcutters" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterChromatic" }, Abbreviation = new TextNode { Value = "RareMonsterChromatic" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterRegret" }, Abbreviation = new TextNode { Value = "RareMonsterRegret" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterBlessed" }, Abbreviation = new TextNode { Value = "RareMonsterBlessed" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterRegal" }, Abbreviation = new TextNode { Value = "RareMonsterRegal" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterSupport" }, Abbreviation = new TextNode { Value = "RareMonsterSupport" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRandomDucatChest" }, Abbreviation = new TextNode { Value = "RandomDucatChest" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderPiratePack" }, Abbreviation = new TextNode { Value = "PiratePack" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonstersPerConnection1" }, Abbreviation = new TextNode { Value = "RareMonstersPerConnection1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonstersPerConnection2" }, Abbreviation = new TextNode { Value = "RareMonstersPerConnection2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderQuantityPerConnection1" }, Abbreviation = new TextNode { Value = "QuantityPerConnection1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderQuantityPerConnection2" }, Abbreviation = new TextNode { Value = "QuantityPerConnection2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderEquipmentToGold1" }, Abbreviation = new TextNode { Value = "EquipmentToGold1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderEquipmentToGold2" }, Abbreviation = new TextNode { Value = "EquipmentToGold2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderCurrencyToStackedDecks" }, Abbreviation = new TextNode { Value = "CurrencyToStackedDecks" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderRareMonsterScarab" }, Abbreviation = new TextNode { Value = "RareMonsterScarab" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMoreCurrency1" }, Abbreviation = new TextNode { Value = "MoreCurrency1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMoreCurrency2" }, Abbreviation = new TextNode { Value = "MoreCurrency2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMoreCurrency3" }, Abbreviation = new TextNode { Value = "MoreCurrency3" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMoreScarabs1" }, Abbreviation = new TextNode { Value = "MoreScarabs1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMoreScarabs2" }, Abbreviation = new TextNode { Value = "MoreScarabs2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMoreScarabs3" }, Abbreviation = new TextNode { Value = "MoreScarabs3" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMoreRarity1" }, Abbreviation = new TextNode { Value = "MoreRarity1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMoreRarity2" }, Abbreviation = new TextNode { Value = "MoreRarity2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMoreRarity3" }, Abbreviation = new TextNode { Value = "MoreRarity3" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderCrabMiniboss" }, Abbreviation = new TextNode { Value = "CrabMiniboss" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderExpGain1" }, Abbreviation = new TextNode { Value = "ExpGain1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderExpGain2" }, Abbreviation = new TextNode { Value = "ExpGain2" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderExpGain3" }, Abbreviation = new TextNode { Value = "ExpGain3" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMagicMonsterMods1" }, Abbreviation = new TextNode { Value = "MagicMonsterMods1" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderMagicMonsterMods2" }, Abbreviation = new TextNode { Value = "MagicMonsterMods2" }, },
                new()
                {
                    Id = new TextNode { Value = "DeepwaterBorderTreasureAnchors1" }, Abbreviation = new TextNode { Value = "TreasureAnchors1" }, ValueMultiplier = { Value = 1.1f }
                },
                new()
                {
                    Id = new TextNode { Value = "DeepwaterBorderTreasureAnchors2" }, Abbreviation = new TextNode { Value = "TreasureAnchors2" }, ValueMultiplier = { Value = 1.2f }
                },
                new() { Id = new TextNode { Value = "DeepwaterBorderSulphurDrops" }, Abbreviation = new TextNode { Value = "SulphurDrops" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderGoldenLanterns" }, Abbreviation = new TextNode { Value = "GoldenLanterns" }, },
                new() { Id = new TextNode { Value = "DeepwaterBorderIzaroObject" }, Abbreviation = new TextNode { Value = "IzaroObject" }, },
                //new()
                //{
                //    Id = new TextNode { Value = "Rare Monsters in adjacent Areas drop an additional Divine Orb" },
                //    Abbreviation = new TextNode { Value = "Divine Orb (adjacent rares)" },
                //},
                //new() { Id = new TextNode { Value = "Players in adjacent Areas gain 200% increased Experience" }, Abbreviation = new TextNode { Value = "200% XP (adjacent)" },ValueMultiplier = {Value = 1}},
                //new() { Id = new TextNode { Value = "Adjacent Areas contain Captainsbane" }, Abbreviation = new TextNode { Value = "Captainsbane" } },
                //new()
                //{
                //    Id = new TextNode { Value = "Basic Currency items dropped by Monsters in adjacent Areas will instead drop as Stacked Decks" },
                //    Abbreviation = new TextNode { Value = "Stacked Decks from basic currency" },
                //},
                //new() { Id = new TextNode { Value = "Adjacent Areas contain 2 additional Treasure Anchors" }, Abbreviation = new TextNode { Value = "2 Treasure Anchors" }, },
                //new() { Id = new TextNode { Value = "Adjacent Areas contain 4 additional Treasure Anchors" }, Abbreviation = new TextNode { Value = "4 Treasure Anchors" }, },
                //new() { Id = new TextNode { Value = "100% more Scarabs found in adjacent Areas" }, Abbreviation = new TextNode { Value = "100% more Scarabs" }, },
            };

            foreach (var defaultBorderMod in defaultMods)
            {
                if (!Settings.VoyageSettings.BorderModifiers.Content.Any(x => x.Id.Value.Equals(defaultBorderMod.Id.Value, StringComparison.OrdinalIgnoreCase)))
                {
                    Settings.VoyageSettings.BorderModifiers.Content.Add(defaultBorderMod);
                }
            }

            var defaultChartMods = new List<VoyageChartModifier>
            {
                new() { Id = "MapDeepwaterChartImplicit", },
                new() { Id = "MapDeepwaterChartAdjacentEssence1", },
                new() { Id = "MapDeepwaterChartAdjacentEssence2", },
                new() { Id = "MapDeepwaterChartAdjacentEssence3", },
                new() { Id = "MapDeepwaterChartAdjacentStrongboxes1", },
                new() { Id = "MapDeepwaterChartAdjacentStrongboxes2", },
                new() { Id = "MapDeepwaterChartAdjacentStrongboxes3", },
                new() { Id = "MapDeepwaterChartAdjacentOctopusPacks1", },
                new() { Id = "MapDeepwaterChartAdjacentOctopusPacks2", },
                new() { Id = "MapDeepwaterChartAdjacentCrabPacks1", },
                new() { Id = "MapDeepwaterChartAdjacentCrabPacks2", },
                new() { Id = "MapDeepwaterChartAdjacentIncreasedMagicMonsters1", },
                new() { Id = "MapDeepwaterChartAdjacentIncreasedMagicMonsters2", },
                new() { Id = "MapDeepwaterChartAdjacentIncreasedRareMonsters1", },
                new() { Id = "MapDeepwaterChartAdjacentIncreasedRareMonsters2", },
                new() { Id = "MapDeepwaterChartAdjacentLostMessage1", },
                new() { Id = "MapDeepwaterChartAdjacentLostMessage2", },
                new() { Id = "MapDeepwaterChartAdjacentFish", },
                new() { Id = "MapDeepwaterChartAdjacentWisps1", },
                new() { Id = "MapDeepwaterChartAdjacentWisps2", },
                new() { Id = "MapDeepwaterChartAdjacentCorrupted", },
                new() { Id = "MapDeepwaterChartAdjacentGoldConvert1", },
                new() { Id = "MapDeepwaterChartAdjacentGoldConvert2", },
                new() { Id = "MapDeepwaterChartAdjacentTormentCages1", },
                new() { Id = "MapDeepwaterChartAdjacentTormentCages2", },
                new() { Id = "MapDeepwaterChartAdjacentDivinerBox1", },
                new() { Id = "MapDeepwaterChartAdjacentDivinerBox2", },
                new() { Id = "MapDeepwaterChartAdjacentArcanistBox1", },
                new() { Id = "MapDeepwaterChartAdjacentArcanistBox2", },
                new() { Id = "MapDeepwaterChartAdjacentOperativeBox1", },
                new() { Id = "MapDeepwaterChartAdjacentOperativeBox2", },
                new() { Id = "MapDeepwaterChartAdjacentBarrels1", },
                new() { Id = "MapDeepwaterChartAdjacentBarrels2", },
                new() { Id = "MapDeepwaterChartAdjacentStarfish1", },
                new() { Id = "MapDeepwaterChartAdjacentStarfish2", },
                new() { Id = "MapDeepwaterChartAdjacentFractured", },
                new() { Id = "MapDeepwaterChartAdjacentGoldenLanterns", },
                new() { Id = "MapDeepwaterChartAdjacentPantheon", },
                new() { Id = "MapDeepwaterChartAdjacentUniqueRing1", },
                new() { Id = "MapDeepwaterChartAdjacentUniqueRing2", },
                new() { Id = "MapDeepwaterChartAdjacentUniqueAmulet1", },
                new() { Id = "MapDeepwaterChartAdjacentUniqueAmulet2", },
                new() { Id = "MapDeepwaterChartAdjacentUniqueBelt1", },
                new() { Id = "MapDeepwaterChartAdjacentUniqueBelt2", },
                new() { Id = "MapDeepwaterChartVoyageSoulEater", },
                new() { Id = "MapDeepwaterChartVoyagePackSize1", },
                new() { Id = "MapDeepwaterChartVoyagePackSize2", },
                new() { Id = "MapDeepwaterChartVoyageQuantity1", },
                new() { Id = "MapDeepwaterChartVoyageQuantity2", },
                new() { Id = "MapDeepwaterChartVoyageRarity1", },
                new() { Id = "MapDeepwaterChartVoyageRarity2", },
                new() { Id = "MapDeepwaterChartVoyageFriendlyJelly", },
                new() { Id = "MapDeepwaterChartVoyageResourceFound1", },
                new() { Id = "MapDeepwaterChartVoyageResourceFound2", },
                new() { Id = "MapDeepwaterChartVoyageResourceFound3", },
                new() { Id = "MapDeepwaterChartVoyageIncreasedRareMonsters", },
                new() { Id = "MapDeepwaterChartVoyageIncreasedMagicMonsters", },
                new() { Id = "MapDeepwaterChartVoyageNoEquipmentDrops", },
                new() { Id = "MapDeepwaterChartVoyageMinimumMagicMonsters", },
                new() { Id = "MapDeepwaterChartVoyageMonstersPossessed", },
                new() { Id = "MapDeepwaterChartVoyageMonstersEssenced", },
                new() { Id = "MapDeepwaterChartVoyageRareFracture", },
                new() { Id = "MapDeepwaterChartVoyageFlaskQuality", },
            };

            foreach (var chartMod in defaultChartMods)
            {
                if (!Settings.VoyageSettings.ChartModifiers.Content.Any(x => x.Id.Value.Equals(chartMod.Id.Value, StringComparison.OrdinalIgnoreCase)))
                {
                    Settings.VoyageSettings.ChartModifiers.Content.Add(chartMod);
                }
            }
        }
    }
}