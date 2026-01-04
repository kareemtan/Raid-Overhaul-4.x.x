using System.Reflection;
using RaidOverhaulMain.Generators;
using RaidOverhaulMain.Helpers;
using RaidOverhaulMain.Models;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Logging;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;

namespace RaidOverhaulMain.Controllers;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 100)]
public class ROCustomItems(
    ISptLogger<ROCustomItems> logger,
    DatabaseService databaseService,
    ROItemGenerator roItemGenerator,
    ROHelpers roHelpers
)
{
    private static ConfigFile? _config;
    private static DebugFile? _debugConfig;

    public void PassCustomItemConfigs(ConfigFile config, DebugFile debugConfig)
    {
        _config = config;
        _debugConfig = debugConfig;
    }

    public void BuildCustomItems()
    {
        var assembly = Assembly.GetExecutingAssembly();
        LoadCustomItems(assembly, roHelpers, roItemGenerator);
        roItemGenerator.CreateRigLayouts(assembly);
        ROLogger.Log(logger, "Custom Items finished loading", LogTextColor.Magenta);
    }

    private void LoadCustomItems(Assembly assembly, ROHelpers helpers, ROItemGenerator itemGenerator)
    {
        var locations = databaseService.GetLocations();
        const string realismKey = "SPT-Realism";
        itemGenerator.CreateCustomItems(assembly, "Assets/db/itemGen/currency", _debugConfig);
        itemGenerator.CreateCustomItems(assembly, "Assets/db/itemGen/constItems", _debugConfig);
        itemGenerator.CreateCustomItems(assembly, "Assets/db/itemGen/customKeys", _debugConfig);
        itemGenerator.CreateCustomItems(assembly, "Assets/db/itemGen/cases", _debugConfig);
        if (_config.EnableCustomItems)
        {
            if (helpers.CheckForMod(realismKey))
            {
                itemGenerator.CreateCustomItems(assembly, "Assets/db/itemGen/ammoRealism", _debugConfig);
                ROLogger.Log(logger, "Realism detected, modifying custom ammunition.", LogTextColor.Magenta);
            }
            else if (!helpers.CheckForMod(realismKey))
            {
                itemGenerator.CreateCustomItems(assembly, "Assets/db/itemGen/ammo", _debugConfig);
            }
            itemGenerator.CreateCustomItems(assembly, "Assets/db/itemGen/weapons", _debugConfig);
            itemGenerator.CreateCustomItems(assembly, "Assets/db/itemGen/gear", _debugConfig);
            BuildSlots(helpers);
        }
        var labsKeys = locations.Laboratory.Base.AccessKeys?.ToList();
        labsKeys?.Add("66a2fc9886fbd5d38c5ca2a6");
        locations.Laboratory.Base.AccessKeys = labsKeys;
    }

    private void BuildSlots(ROHelpers helpers)
    {
        var tables = databaseService.GetTables();
        var items = tables.Templates.Items;
        var aug = helpers.FetchIdFromMap("Aug762", ClassMaps.CustomItemMap);
        var stm46 = helpers.FetchIdFromMap("Stm46", ClassMaps.CustomItemMap);
        var mcm4 = helpers.FetchIdFromMap("Mcm4", ClassMaps.CustomItemMap);
        var judge = helpers.FetchIdFromMap("Judge", ClassMaps.CustomItemMap);
        var jury = helpers.FetchIdFromMap("Jury", ClassMaps.CustomItemMap);
        var executioner = helpers.FetchIdFromMap("Exec", ClassMaps.CustomItemMap);
        var aug30 = helpers.FetchIdFromMap("Aug30Rd", ClassMaps.CustomItemMap);
        var aug42 = helpers.FetchIdFromMap("Aug42Rd", ClassMaps.CustomItemMap);
        var stm33 = helpers.FetchIdFromMap("Stm33Rd", ClassMaps.CustomItemMap);
        var stm50 = helpers.FetchIdFromMap("Stm50Rd", ClassMaps.CustomItemMap);
        var stmRec = helpers.FetchIdFromMap("StmRec", ClassMaps.CustomItemMap);
        var mag300 = helpers.FetchIdFromMap("Mag300", ClassMaps.CustomItemMap);
        var mag545 = helpers.FetchIdFromMap("Mag545", ClassMaps.CustomItemMap);
        var mag57 = helpers.FetchIdFromMap("Mag57", ClassMaps.CustomItemMap);
        var mag762 = helpers.FetchIdFromMap("Mag762", ClassMaps.CustomItemMap);
        var mag939 = helpers.FetchIdFromMap("Mag939", ClassMaps.CustomItemMap);
        var rec300 = helpers.FetchIdFromMap("Rec300", ClassMaps.CustomItemMap);
        var rec545 = helpers.FetchIdFromMap("Rec545", ClassMaps.CustomItemMap);
        var rec57 = helpers.FetchIdFromMap("Rec57", ClassMaps.CustomItemMap);
        var rec762 = helpers.FetchIdFromMap("Rec762", ClassMaps.CustomItemMap);
        var rec939 = helpers.FetchIdFromMap("Rec939", ClassMaps.CustomItemMap);
        var judge17 = helpers.FetchIdFromMap("Judge17Rd", ClassMaps.CustomItemMap);
        var judge33 = helpers.FetchIdFromMap("Judge33Rd", ClassMaps.CustomItemMap);
        var judge50 = helpers.FetchIdFromMap("Judge50Rd", ClassMaps.CustomItemMap);
        var judgeSlide = helpers.FetchIdFromMap("JudgeSlide", ClassMaps.CustomItemMap);
        var jury20 = helpers.FetchIdFromMap("Jury20Rd", ClassMaps.CustomItemMap);
        var jury25 = helpers.FetchIdFromMap("Jury25Rd", ClassMaps.CustomItemMap);
        var jury50 = helpers.FetchIdFromMap("Jury50Rd", ClassMaps.CustomItemMap);
        var juryRec = helpers.FetchIdFromMap("JuryRec", ClassMaps.CustomItemMap);
        var execAics = helpers.FetchIdFromMap("ExecAics", ClassMaps.CustomItemMap);
        var execPmag = helpers.FetchIdFromMap("ExecPmag", ClassMaps.CustomItemMap);
        var execWyatt = helpers.FetchIdFromMap("ExecWyatt", ClassMaps.CustomItemMap);

        items[aug].Properties.Slots.ElementAt(0).Properties.Filters.ElementAt(0).Filter = [aug30, aug42];
        items[stm46].Properties.Slots.ElementAt(1).Properties.Filters.ElementAt(0).Filter = [stm33, stm50];
        items[stm46].Properties.Slots.ElementAt(2).Properties.Filters.ElementAt(0).Filter = [stmRec];
        items[mcm4].Properties.Slots.ElementAt(1).Properties.Filters.ElementAt(0).Filter.Add(mag300);
        items[mcm4].Properties.Slots.ElementAt(1).Properties.Filters.ElementAt(0).Filter.Add(mag545);
        items[mcm4].Properties.Slots.ElementAt(1).Properties.Filters.ElementAt(0).Filter.Add(mag57);
        items[mcm4].Properties.Slots.ElementAt(1).Properties.Filters.ElementAt(0).Filter.Add(mag762);
        items[mcm4].Properties.Slots.ElementAt(1).Properties.Filters.ElementAt(0).Filter.Add(mag939);
        items[mcm4].Properties.Slots.ElementAt(2).Properties.Filters.ElementAt(0).Filter.Add(rec300);
        items[mcm4].Properties.Slots.ElementAt(2).Properties.Filters.ElementAt(0).Filter.Add(rec545);
        items[mcm4].Properties.Slots.ElementAt(2).Properties.Filters.ElementAt(0).Filter.Add(rec57);
        items[mcm4].Properties.Slots.ElementAt(2).Properties.Filters.ElementAt(0).Filter.Add(rec762);
        items[mcm4].Properties.Slots.ElementAt(2).Properties.Filters.ElementAt(0).Filter.Add(rec939);
        items[judge].Properties.Slots.ElementAt(3).Properties.Filters.ElementAt(0).Filter = [judge17, judge33, judge50];
        items[judge].Properties.Slots.ElementAt(2).Properties.Filters.ElementAt(0).Filter = [judgeSlide];
        items[jury].Properties.Slots.ElementAt(1).Properties.Filters.ElementAt(0).Filter = [jury20, jury25, jury50];
        items[jury].Properties.Slots.ElementAt(2).Properties.Filters.ElementAt(0).Filter = [juryRec];
        items[executioner].Properties.Slots.ElementAt(0).Properties.Filters.ElementAt(0).Filter = [execAics, execPmag, execWyatt];
    }
}
