using System.Text.Json.Serialization;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Bot.GlobalSettings;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Spt.Mod;

namespace RaidOverhaulMain.Models;

public class CustomItemFormat
{
    [JsonPropertyName("ItemToClone")]
    public required string ItemToClone { get; set; }

    [JsonPropertyName("OverrideProperties")]
    public required TemplateItemProperties OverrideProperties { get; set; }

    [JsonPropertyName("LocalePush")]
    public required Dictionary<string, LocaleDetails> LocaleData { get; set; }

    [JsonPropertyName("Handbook")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public HandbookDataFormat? HandbookData { get; set; }

    [JsonPropertyName("SlotPush")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SlotPushDataFormat? SlotPushData { get; set; }

    [JsonPropertyName("BotPush")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BotPushDataFormat? BotPushData { get; set; }

    [JsonPropertyName("CasePush")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CasePushDataFormat? CasePushData { get; set; }

    [JsonPropertyName("LootPush")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LootPushDataFormat? LootPushData { get; set; }

    [JsonPropertyName("PresetPush")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PresetPushDataFormat? PresetPushData { get; set; }

    [JsonPropertyName("QuestPush")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public QuestPushDataFormat? QuestPushData { get; set; }

    [JsonPropertyName("PushToFleaBlacklist")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? PushToFleaBlacklist { get; set; }

    [JsonPropertyName("CloneToFilters")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CloneToFilters { get; set; }

    [JsonPropertyName("PushMastery")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? PushMastery { get; set; }
}

public class HandbookDataFormat
{
    [JsonPropertyName("HandbookParent")]
    public string? HbParent { get; set; }

    [JsonPropertyName("HandbookPrice")]
    public double? HbPrice { get; set; }
}

public class SlotPushDataFormat
{
    [JsonPropertyName("Slot")]
    public string? Slot { get; set; }
}

public class BotPushDataFormat
{
    [JsonPropertyName("AddToBots")]
    public bool? AddToBots { get; set; }
}

public class CasePushDataFormat
{
    [JsonPropertyName("CaseFiltersToAdd")]
    public string[]? CaseFiltersToAdd { get; set; }
}

public class LootPushDataFormat
{
    [JsonPropertyName("LootContainersToAdd")]
    public string[]? LootContainersToAdd { get; set; }

    [JsonPropertyName("StaticLootProbability")]
    public int? StaticLootProbability { get; set; }
}

public class PresetPushDataFormat
{
    [JsonPropertyName("PresetToAdd")]
    public PresetFormat[]? PresetToAdd { get; set; }
}

public class QuestPushDataFormat
{
    [JsonPropertyName("QuestConditionType")]
    public string? QuestConditionType { get; set; }

    [JsonPropertyName("QuestTargetConditionToClone")]
    public string? QuestTargetConditionToClone { get; set; }
}

public class PresetFormat
{
    [JsonPropertyName("_changeWeaponName")]
    public bool ChangeWeaponName { get; set; }

    [JsonPropertyName("_encyclopedia")]
    public string Encyclopedia { get; set; }

    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("_items")]
    public ItemFormat[] Items { get; set; }

    [JsonPropertyName("_name")]
    public string Name { get; set; }

    [JsonPropertyName("_parent")]
    public string Parent { get; set; }

    [JsonPropertyName("_type")]
    public string PresetType { get; set; }
}

public class ItemFormat
{
    [JsonPropertyName("_id")]
    public string? Id { get; set; }

    [JsonPropertyName("_tpl")]
    public string? Tpl { get; set; }

    [JsonPropertyName("parentId")]
    public string? ParentId { get; set; }

    [JsonPropertyName("slotId")]
    public string? SlotId { get; set; }
}

public class AmmoStackList
{
    public required string[] RifleList { get; set; }
    public required string[] ShotgunList { get; set; }
    public required string[] SmgList { get; set; }
    public required string[] SniperList { get; set; }
    public required string[] UbglList { get; set; }
}

public record BotConfigData
{
    [JsonPropertyName("type")]
    public string? BotType { get; set; }

    [JsonPropertyName("presetBatch")]
    public int? PresetBatch { get; set; }

    [JsonPropertyName("isBoss")]
    public bool? IsBoss { get; set; }

    [JsonPropertyName("durability")]
    public DefaultDurability? Durability { get; set; }

    [JsonPropertyName("itemSpawnLimits")]
    public Dictionary<MongoId, double>? ItemSpawnLimits { get; set; }

    [JsonPropertyName("equipment")]
    public EquipmentFilters? EquipmentFilters { get; set; }

    [JsonPropertyName("currencyStackSize")]
    public Dictionary<string, Dictionary<string, double>>? CurrencyStackSize { get; set; }

    [JsonPropertyName("mustHaveUniqueName")]
    public bool? MustHaveUniqueName { get; set; }
}

public record BotTypeData
{
    [JsonPropertyName("appearance")]
    public Appearance? BotAppearance { get; set; }

    [JsonPropertyName("chances")]
    public Chances? BotChances { get; set; }

    [JsonPropertyName("difficulty")]
    public Dictionary<string, BotDifficultyData>? BotDifficulty { get; set; }

    [JsonPropertyName("experience")]
    public Experience? BotExperience { get; set; }

    [JsonPropertyName("firstName")]
    public List<string>? FirstNames { get; set; }

    [JsonPropertyName("generation")]
    public Generation? BotGeneration { get; set; }

    [JsonPropertyName("health")]
    public BotTypeHealth? BotHealth { get; set; }

    [JsonPropertyName("inventory")]
    public BotTypeInventory? BotInventory { get; set; }

    [JsonPropertyName("lastName")]
    public IEnumerable<string>? LastNames { get; set; }

    [JsonPropertyName("skills")]
    public BotDbSkills? BotSkills { get; set; }
}

public record BotDifficultyData
{
    public BotGlobalAimingSettings? Aiming { get; set; }

    public BotGlobalsBossSettings? Boss { get; set; }

    public BotGlobalsChangeSettings? Change { get; set; }

    public BotGlobalCoreSettings? Core { get; set; }

    public BotGlobalsCoverSettings? Cover { get; set; }

    public BotGlobalsGrenadeSettings? Grenade { get; set; }

    public BotGlobalsHearingSettings? Hearing { get; set; }

    public BotGlobalLayData? Lay { get; set; }

    public BotGlobalLookData? Look { get; set; }

    public BotGlobalsMindSettings? Mind { get; set; }

    public BotGlobalsMoveSettings? Move { get; set; }

    public BotGlobalPatrolSettings? Patrol { get; set; }

    public BotGlobalsScatteringSettings? Scattering { get; set; }

    public BotGlobalShootData? Shoot { get; set; }
}

public class BotLoadoutItemData
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("chance")]
    public int? Chance { get; set; }

    [JsonPropertyName("children")]
    public Dictionary<string, BotLoadoutItemData>? Children { get; set; }

    [JsonPropertyName("slots")]
    public Dictionary<string, List<string>>? Slots { get; set; }
}

public record BotSpawnData
{
    [JsonPropertyName("spawnData")]
    public Dictionary<string, Dictionary<string, BossLocationSpawn>> SpawnData { get; set; }
}

public class ShopInfoFile
{
    [JsonPropertyName("blacklist")]
    public MongoId[]? ShopBlacklist { get; set; }

    [JsonPropertyName("specialItems")]
    public MongoId[]? SpecialShopItems { get; set; }
}
