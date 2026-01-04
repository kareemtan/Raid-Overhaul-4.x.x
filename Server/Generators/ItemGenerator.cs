using System.Reflection;
using RaidOverhaulMain.Helpers;
using RaidOverhaulMain.Models;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Logging;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Services.Mod;
using Path = System.IO.Path;

namespace RaidOverhaulMain.Generators;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class ROItemGenerator(
    ISptLogger<ROItemGenerator> logger,
    DatabaseService databaseService,
    ConfigServer configServer,
    CustomItemService customItemService,
    ModHelper modHelper,
    ROJsonHelper jsonHelper,
    ROHelpers helpers
)
{
    private readonly RagfairConfig _ragfairConfig = configServer.GetConfig<RagfairConfig>();
    private readonly Dictionary<string, Dictionary<string, string>> _layoutBundles = [];

    public void CreateCustomItems(Assembly assembly, string dataPath, DebugFile debugConfig)
    {
        var modPath = modHelper.GetAbsolutePathToModFolder(assembly);
        var itemDirectory = Path.Combine(modPath, dataPath);

        if (!Directory.Exists(itemDirectory))
        {
            return;
        }

        var itemConfigs = jsonHelper.LoadCombinedJsons<Dictionary<string, CustomItemFormat>>(itemDirectory);

        if (itemConfigs.Count == 0)
        {
            return;
        }

        foreach (var itemConfig in itemConfigs)
        {
            foreach (var (itemId, configData) in itemConfig)
            {
                CreateItemFromConfig(itemId, configData, debugConfig);
            }
        }
    }

    private void CreateItemFromConfig(string itemId, CustomItemFormat config, DebugFile debugConfig)
    {
        var itemToClone = helpers.FetchIdFromMap(config.ItemToClone, ClassMaps.AllItemList);
        var itemDetails = new NewItemFromCloneDetails
        {
            ItemTplToClone = itemToClone,
            ParentId = helpers.GetItemInTables(itemToClone).Parent,
            NewId = itemId,
            FleaPriceRoubles = helpers.GetFleaPrice(itemToClone),
            HandbookPriceRoubles = config.HandbookData?.HbPrice ?? helpers.GetItemInHandbook(itemToClone)?.Price,
            HandbookParentId = BuildHandbook(itemToClone, config),
            Locales = config.LocaleData,
            OverrideProperties = config.OverrideProperties,
        };
        customItemService.CreateItemFromClone(itemDetails);
        ProcessAdditionalProperties(itemId, config);

        if (debugConfig.DebugMode)
        {
            ROLogger.Log(logger, $"Finished adding custom item {config.LocaleData["en"].Name}", LogTextColor.Magenta);
        }
    }

    private void ProcessAdditionalProperties(string itemId, CustomItemFormat config)
    {
        if (config.SlotPushData?.Slot is not null)
        {
            PushToSlot(itemId, config);
        }

        if (config.CloneToFilters is not null)
        {
            CloneToFilters(itemId, config);
        }

        if (config.PushMastery is not null)
        {
            PushMastery(itemId, config);
        }

        if (config.BotPushData?.AddToBots is not null)
        {
            AddToBots(itemId, config);
        }

        if (config.LootPushData?.LootContainersToAdd is not null)
        {
            AddToStaticLoot(itemId, config);
        }

        if (config.CasePushData?.CaseFiltersToAdd is not null)
        {
            AddToCases(itemId, config);
        }

        if (config.PushToFleaBlacklist is not null)
        {
            AddToFleaBlacklist(itemId);
        }

        if (config.PresetPushData is not null)
        {
            AddCustomPresets(config);
        }
        /*
                if (config.QuestPushData != null) {
                    this.addToQuests(
                        tables.templates.quests,
                        itemConfig.QuestPush.QuestConditionType,
                        itemConfig.QuestPush.QuestTargetConditionToClone,
                        newId,
                    );
                }
        */
    }

    private string BuildHandbook(string itemToClone, CustomItemFormat config)
    {
        if (config.HandbookData?.HbParent is not null)
        {
            var handbookParentMap = helpers.FetchIdFromMap(config.HandbookData?.HbParent, ClassMaps.HandbookIds);

            return handbookParentMap;
        }

        var handbookParent = helpers.GetItemInHandbook(itemToClone)?.ParentId;

        return handbookParent;
    }

    private void PushToSlot(string itemId, CustomItemFormat config)
    {
        var items = databaseService.GetItems();
        var defaultInventory = items["55d7217a4bdc2d86028b456d"].Properties?.Slots;
        var slotToPush = helpers.FetchIdFromMap(config.SlotPushData?.Slot, ClassMaps.SlotIds);

        if (defaultInventory is null)
        {
            return;
        }

        foreach (var slot in defaultInventory)
        {
            var filters = slot.Properties?.Filters?.ToList();

            if (filters is null || filters.Count == 0)
            {
                continue;
            }

            var slotId = slot.Id;

            if (slotId is null)
            {
                continue;
            }

            if (slotId != slotToPush)
            {
                continue;
            }
            filters.FirstOrDefault()?.Filter?.Add(itemId);
        }
    }

    private void CloneToFilters(string itemId, CustomItemFormat config)
    {
        var items = databaseService.GetItems();
        var itemToClone = helpers.FetchIdFromMap(config.ItemToClone, ClassMaps.AllItemList);

        if (items is not null)
        {
            foreach (var (item, _) in items)
            {
                if (items[item].Properties?.ConflictingItems is not null)
                {
                    var conflictingItems = items[item].Properties?.ConflictingItems?.ToList();

                    foreach (var itemInConflict in conflictingItems)
                    {
                        var originalConflictIds = items[item].Properties?.ConflictingItems;

                        if (itemInConflict == itemToClone && !conflictingItems.Contains(itemId))
                        {
                            originalConflictIds.Add(itemId);
                        }
                    }
                }

                if (items[item].Properties?.Slots is not null)
                {
                    var slotList = items[item].Properties?.Slots?.ToList();

                    foreach (var slots in slotList)
                    {
                        var slotsId = slots.Properties?.Filters?.FirstOrDefault()?.Filter?.ToList();

                        foreach (var itemInFilters in slotsId)
                        {
                            var orginalSlots = slots.Properties?.Filters?.FirstOrDefault()?.Filter;

                            if (itemInFilters == itemToClone && !slotsId.Contains(itemId))
                            {
                                orginalSlots.Add(itemId);
                            }
                        }
                    }
                }

                if (items[item].Properties?.Cartridges is not null)
                {
                    var cartridges = items[item].Properties?.Cartridges?.ToList();

                    foreach (var cartridge in cartridges)
                    {
                        var cartridgeId = cartridge.Properties?.Filters?.FirstOrDefault()?.Filter?.ToList();

                        foreach (var itemInFilters in cartridgeId)
                        {
                            var originalCartridges = cartridge.Properties?.Filters?.FirstOrDefault()?.Filter;

                            if (itemInFilters == itemToClone && !cartridgeId.Contains(itemId))
                            {
                                originalCartridges.Add(itemId);
                            }
                        }
                    }
                }

                if (items[item].Properties?.Chambers is not null)
                {
                    var chambers = items[item].Properties?.Chambers?.ToList();

                    foreach (var chamber in chambers)
                    {
                        var chamberId = chamber.Properties?.Filters?.FirstOrDefault()?.Filter?.ToList();

                        foreach (var itemInFilters in chamberId)
                        {
                            var originalChambers = chamber.Properties?.Filters?.FirstOrDefault()?.Filter;

                            if (itemInFilters == itemToClone && !chamberId.Contains(itemId))
                            {
                                originalChambers.Add(itemId);
                            }
                        }
                    }
                }
            }
        }
    }

    private void PushMastery(string itemId, CustomItemFormat config)
    {
        var globals = databaseService.GetGlobals();
        var locales = config.LocaleData;
        var newMasteryDjCore = new Mastering
        {
            Name = locales.FirstOrDefault().Value.Name,
            Templates = [itemId],
            Level2 = 450,
            Level3 = 900,
        };
        var newMastering = globals.Configuration.Mastering.ToList();
        newMastering.Add(newMasteryDjCore);
        globals.Configuration.Mastering = newMastering.ToArray();
    }

    private void AddCustomPresets(CustomItemFormat config)
    {
        var globals = databaseService.GetGlobals();
        var presets = globals.ItemPresets;

        foreach (var presetToAdd in config.PresetPushData?.PresetToAdd)
        {
            var preset = new Preset
            {
                Id = presetToAdd.Id,
                Name = presetToAdd.Name,
                Parent = presetToAdd.Parent,
                ChangeWeaponName = presetToAdd.ChangeWeaponName,
                Encyclopedia = presetToAdd.Encyclopedia,
                Type = "Preset",
                Items = new List<Item>(),
            };

            foreach (var itemData in presetToAdd.Items)
            {
                var item = new Item
                {
                    Id = itemData.Id,
                    Template = itemData.Tpl,
                    ParentId = itemData.ParentId,
                    SlotId = itemData.SlotId,
                };
                preset.Items.Add(item);
            }
            presets[preset.Id] = preset;
        }
    }

    private void AddToBots(string itemId, CustomItemFormat config)
    {
        var itemToClone = helpers.FetchIdFromMap(config.ItemToClone, ClassMaps.AllItemList);
        var bots = databaseService.GetBots();

        foreach (var (_, bot) in bots.Types)
        {
            var items = bot?.BotInventory.Items;
            var botItems = new[] { items.Backpack, items.Pockets, items.SecuredContainer, items.SpecialLoot, items.TacticalVest };

            foreach (var botItem in botItems)
            {
                foreach (var (existingItem, chance) in botItem)
                {
                    if (existingItem.ToString() == itemToClone)
                    {
                        botItem[new MongoId(itemId)] = chance;
                        break;
                    }
                }
            }
        }
    }

    private void AddToFleaBlacklist(string itemId)
    {
        _ragfairConfig.Dynamic.Blacklist.Custom.Add(itemId);
    }

    private void AddToStaticLoot(string itemId, CustomItemFormat config)
    {
        if (config.LootPushData?.LootContainersToAdd is not null)
        {
            foreach (var container in config.LootPushData?.LootContainersToAdd)
            {
                var locations = databaseService.GetLocations().GetDictionary();

                foreach (var (_, location) in locations)
                {
                    location.StaticLoot?.AddTransformer(lazyloadedStaticLootData =>
                    {
                        if (lazyloadedStaticLootData is null)
                        {
                            return lazyloadedStaticLootData;
                        }

                        var containerId = helpers.FetchIdFromMap(container, ClassMaps.AllItemList);

                        if (!lazyloadedStaticLootData.TryGetValue(containerId, out var containerData))
                        {
                            return lazyloadedStaticLootData;
                        }

                        var newItemDistribution = containerData.ItemDistribution.ToList();

                        newItemDistribution.Add(
                            new ItemDistribution { Tpl = itemId, RelativeProbability = config.LootPushData?.StaticLootProbability }
                        );

                        containerData.ItemDistribution = newItemDistribution.ToArray();

                        return lazyloadedStaticLootData;
                    });
                }
            }
        }
    }

    private void AddToCases(string itemId, CustomItemFormat config)
    {
        var items = databaseService.GetItems();

        foreach (var caseToAdd in config.CasePushData?.CaseFiltersToAdd)
        {
            var finalCaseToAdd = helpers.FetchIdFromMap(caseToAdd, ClassMaps.AllItemList);

            foreach (var (item, _) in items)
            {
                if (items[item].Id == finalCaseToAdd)
                {
                    if (items[item].Properties?.Grids?.FirstOrDefault()?.Properties?.Filters?.FirstOrDefault()?.Filter is not null)
                    {
                        items[item].Properties?.Grids?.FirstOrDefault()?.Properties?.Filters?.FirstOrDefault()?.Filter?.Add(itemId);
                    }
                }
            }
        }
    }

    public void CreateRigLayouts(Assembly assembly)
    {
        var modKey = assembly.GetName().Name ?? string.Empty;
        var pathToMod = modHelper.GetAbsolutePathToModFolder(assembly);
        var finalPath = Path.Combine(pathToMod, "Assets", "db", "itemGen", "customLayouts");

        if (!_layoutBundles.ContainsKey(modKey))
        {
            _layoutBundles[modKey] = new Dictionary<string, string>();
        }

        foreach (var bundlePath in Directory.GetFiles(finalPath, "*.bundle"))
        {
            var bundleName = Path.GetFileNameWithoutExtension(bundlePath);
            _layoutBundles[modKey][bundleName] = bundlePath;
        }
    }

    public List<string> GetRigLayouts()
    {
        var allBundles = new List<string>();
        foreach (var modBundles in _layoutBundles.Values)
        {
            allBundles.AddRange(modBundles.Keys);
        }
        return allBundles;
    }

    public async Task<byte[]?> GetRigLayoutData(string bundleName)
    {
        foreach (var bundles in _layoutBundles.Values)
        {
            if (!bundles.TryGetValue(bundleName, out var path))
            {
                continue;
            }

            if (!File.Exists(path))
            {
                continue;
            }

            return await File.ReadAllBytesAsync(path);
        }

        return null;
    }
}
