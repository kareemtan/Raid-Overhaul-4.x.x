using System.Reflection;
using RaidOverhaulMain.Helpers;
using RaidOverhaulMain.Models;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Logging;
using SPTarkov.Server.Core.Models.Utils;
using Path = System.IO.Path;

namespace RaidOverhaulMain.Controllers;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 103)]
public class ROBoss(ISptLogger<ROBoss> logger, ROBossHelper roBossHelper)
{
    private static ConfigFile? _config;
    private static DebugFile? _debugConfig;
    private static LegionProgression? _legionConfig;

    public void PassBossConfigs(ConfigFile config, DebugFile debugConfig, LegionProgression legionConfig)
    {
        _config = config;
        _debugConfig = debugConfig;
        _legionConfig = legionConfig;
    }

    public void BuildBoss()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var botFilesPath = Path.Combine("db", "botFiles");

        if (_config.EnableCustomItems)
        {
            var botConfigPath = Path.Combine("Assets", "db", "botFiles", "botsCustomItems", "botConfigs");
            var botTypePath = Path.Combine("Assets", "db", "botFiles", "botsCustomItems", "botTypes");

            roBossHelper.SetBotConfig(assembly, botConfigPath);
            roBossHelper.CreateCustomBotTypes(assembly, botTypePath);
        }
        else if (!_config.EnableCustomItems)
        {
            var botConfigPath = Path.Combine("Assets", "db", "botFiles", "botsNoCustomItems", "botConfigs");
            var botTypePath = Path.Combine("Assets", "db", "botFiles", "botsNoCustomItems", "botTypes");

            roBossHelper.SetBotConfig(assembly, botConfigPath);
            roBossHelper.CreateCustomBotTypes(assembly, botTypePath);
        }
        if (_config.EnableCustomBoss)
        {
            if (_config.UseLegionGlobalSpawnChance)
            {
                roBossHelper.SetBossSpawns(assembly, botFilesPath, "botSpawns.json", _debugConfig, _config.GlobalSpawnChance);
            }
            else if (!_config.UseLegionGlobalSpawnChance)
            {
                roBossHelper.SetBossSpawns(assembly, botFilesPath, "botSpawns.json", _debugConfig, _legionConfig.LegionChance);
            }
        }

        ROLogger.Log(logger, "Custom bots finished loading", LogTextColor.Magenta);
    }
}
