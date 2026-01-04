using System.Reflection;
using RaidOverhaulMain.Models;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Logging;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Utils;
using Path = System.IO.Path;

namespace RaidOverhaulMain.Helpers;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class ROBossHelper(
    ISptLogger<ROBossHelper> logger,
    ConfigServer configServer,
    ModHelper modHelper,
    JsonUtil jsonUtil,
    RandomUtil randomUtil,
    DatabaseService databaseService
)
{
    private List<string> _botTypesToAdd { get; } = new();
    private BotConfig? _botConfig;

    public void SetBotConfig(Assembly assembly, string dataPath)
    {
        _botConfig ??= configServer.GetConfig<BotConfig>();

        var modPath = modHelper.GetAbsolutePathToModFolder(assembly);
        var finalPath = Path.Combine(modPath, dataPath);

        if (!Directory.Exists(finalPath))
        {
            return;
        }

        var botFiles = Directory.GetFiles(finalPath, "*.json*");

        foreach (var file in botFiles)
        {
            var botConfigData = jsonUtil.DeserializeFromFile<BotConfigData>(file);
            var botTypeToAdd = Path.GetFileNameWithoutExtension(file);
            var botTypeToAddLower = botTypeToAdd.ToLowerInvariant();

            if (botConfigData is null)
            {
                return;
            }

            _botConfig.PresetBatch[botTypeToAdd] = botConfigData.PresetBatch ?? 1;

            if (botConfigData.IsBoss == true)
            {
                _botConfig.Bosses.Add(botTypeToAdd);
            }

            if (botConfigData.Durability != null)
            {
                _botConfig.Durability.BotDurabilities[botTypeToAddLower] = botConfigData.Durability;
            }

            if (botConfigData.ItemSpawnLimits != null)
            {
                _botConfig.ItemSpawnLimits[botTypeToAddLower] = botConfigData.ItemSpawnLimits;
            }

            if (botConfigData.EquipmentFilters != null)
            {
                _botConfig.Equipment[botTypeToAddLower] = botConfigData.EquipmentFilters;
            }

            if (botConfigData.CurrencyStackSize != null)
            {
                _botConfig.CurrencyStackSize[botTypeToAddLower] = botConfigData.CurrencyStackSize;
            }

            if (botConfigData.MustHaveUniqueName == true)
            {
                _botConfig.BotRolesThatMustHaveUniqueName.Add(botTypeToAddLower);
            }
        }
    }

    public void CreateCustomBotTypes(Assembly assembly, string dataPath)
    {
        var botTypes = databaseService.GetBots().Types;
        var modPath = modHelper.GetAbsolutePathToModFolder(assembly);
        var finalPath = Path.Combine(modPath, dataPath);

        if (!Directory.Exists(finalPath))
        {
            return;
        }

        var botFiles = Directory.GetFiles(finalPath, "*.json*");

        foreach (var file in botFiles)
        {
            var botTypeData = jsonUtil.DeserializeFromFile<BotType>(file);
            var botTypeName = Path.GetFileNameWithoutExtension(file);

            botTypeName = botTypeName.ToLowerInvariant();

            if (botTypeData == null)
            {
                continue;
            }

            botTypes[botTypeName] = botTypeData;
            _botTypesToAdd.Add(botTypeName);
        }
    }

    public Dictionary<string, Dictionary<string, DifficultyCategories>>? GetBotDifficulties(
        string url,
        EmptyRequestData info,
        string sessionID,
        string output
    )
    {
        var botTypes = databaseService.GetBots().Types;

        Dictionary<string, Dictionary<string, DifficultyCategories>> result = new();

        if (output != null && output != string.Empty)
        {
            result = jsonUtil.Deserialize<Dictionary<string, Dictionary<string, DifficultyCategories>>>(output);
        }

        if (botTypes is null || botTypes.Count == 0)
        {
            return null;
        }

        foreach (var botType in _botTypesToAdd)
        {
            var botData = botTypes.ContainsKey(botType) ? botTypes[botType] : null;

            if (result is not null)
            {
                if (!result.TryGetValue(botType, out Dictionary<string, DifficultyCategories>? value))
                {
                    value = new Dictionary<string, DifficultyCategories>();
                    result[botType] = value;
                }

                if (botData is not null)
                {
                    value["easy"] = botData.BotDifficulty["easy"];
                    value["normal"] = botData.BotDifficulty["normal"];
                    value["hard"] = botData.BotDifficulty["hard"];
                    value["impossible"] = botData.BotDifficulty["impossible"];
                }
            }
        }
        return result;
    }

    public void SetBossSpawns(
        Assembly assembly,
        string pathFromAssets,
        string dataFileName,
        DebugFile debugConfig,
        double? legionSpawnChance
    )
    {
        var pathToMod = modHelper.GetAbsolutePathToModFolder(assembly);
        var finalPath = Path.Combine(pathToMod, "Assets", pathFromAssets);
        var spawnConfig = modHelper.GetJsonDataFromFile<BotSpawnData>(finalPath, dataFileName);
        var locations = databaseService.GetLocations();

        if (spawnConfig is null)
        {
            return;
        }

        foreach (var (bot, data) in spawnConfig.SpawnData)
        {
            foreach (var (map, mapData) in data)
            {
                var finalMap = databaseService.GetLocations().GetMappedKey(map);
                var bossSpawns = locations.GetDictionary()[finalMap].Base.BossLocationSpawn;

                bossSpawns.RemoveAll(x => x.BossName.Contains(bot));

                var bossInfo = new BossLocationSpawn
                {
                    BossChance = legionSpawnChance,
                    BossDifficulty = mapData.BossDifficulty,
                    BossEscortAmount = mapData.BossEscortAmount,
                    BossEscortDifficulty = mapData.BossEscortDifficulty,
                    BossEscortType = mapData.BossEscortType,
                    BossName = mapData.BossName,
                    IsBossPlayer = false,
                    BossZone = mapData.BossZone,
                    ForceSpawn = mapData.ForceSpawn,
                    IgnoreMaxBots = mapData.IgnoreMaxBots,
                    IsRandomTimeSpawn = mapData.IsRandomTimeSpawn,
                    SpawnMode = mapData.SpawnMode,
                    Supports = mapData.Supports,
                    Time = -1,
                    TriggerId = mapData.TriggerId,
                    TriggerName = mapData.TriggerName,
                };

                bossSpawns.Add(bossInfo);

                if (debugConfig.DebugMode)
                {
                    ROLogger.Log(logger, $"Added {bot} to {map}.", LogTextColor.Magenta);
                }
            }
        }
    }
}
