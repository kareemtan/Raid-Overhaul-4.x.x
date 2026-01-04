using System;
using System.Linq;
using EFT.InventoryLogic;
using EFT.UI;
using RaidOverhaul.Helpers;

namespace RaidOverhaul.Configs
{
    internal static class ConsoleCommands
    {
        public static void RegisterCC()
        {
            ConsoleScreen.Processor.RegisterCommand("DoHealEvent", new Action(Plugin._ecScript.DoHealPlayer));
            ConsoleScreen.Processor.RegisterCommand("DoDamageEvent", new Action(Plugin._ecScript.DoDamageEvent));
            ConsoleScreen.Processor.RegisterCommand("DoArmorEvent", new Action(Plugin._ecScript.DoArmorRepair));
            ConsoleScreen.Processor.RegisterCommand("DoAirdropEvent", new Action(Plugin._ecScript.DoAirdropEvent));
            ConsoleScreen.Processor.RegisterCommand("DoFunnyEvent", new Action(Plugin._ecScript.DoFunnyWrapper));
            ConsoleScreen.Processor.RegisterCommand("DoBlackoutEvent", new Action(Plugin._ecScript.DoBlackoutEventWrapper));
            ConsoleScreen.Processor.RegisterCommand("DoSkillEvent", new Action(Plugin._ecScript.DoSkillEvent));
            ConsoleScreen.Processor.RegisterCommand("DoMetabolismEvent", new Action(Plugin._ecScript.DoMetabolismEvent));
            ConsoleScreen.Processor.RegisterCommand("DoMalfEvent", new Action(Plugin._ecScript.DoMalfEventWrapper));
            ConsoleScreen.Processor.RegisterCommand("DoLLEvent", new Action(Plugin._ecScript.DoLLEvent));
            ConsoleScreen.Processor.RegisterCommand("DoBerserkEvent", new Action(Plugin._ecScript.DoBerserkEventWrapper));
            ConsoleScreen.Processor.RegisterCommand("DoWeightEvent", new Action(Plugin._ecScript.DoWeightEventWrapper));
            ConsoleScreen.Processor.RegisterCommand("DoMaxLLEvent", new Action(Plugin._ecScript.DoMaxLLEvent));
            ConsoleScreen.Processor.RegisterCommand("DoRepCorrect", new Action(Plugin._ecScript.CorrectRep));
            ConsoleScreen.Processor.RegisterCommand("DoLockdownEvent", new Action(Plugin._ecScript.DoLockDownEventWrapper));
            ConsoleScreen.Processor.RegisterCommand("DoArtilleryEvent", new Action(Plugin._ecScript.DoArtyEventWrapper));
            ConsoleScreen.Processor.RegisterCommand("RunTrain", new Action(Plugin._ecScript.RunTrainWrapper));
            ConsoleScreen.Processor.RegisterCommand("DoPmcExfil", new Action(Plugin._ecScript.DoPmcExfilEventWrapper));
            ConsoleScreen.Processor.RegisterCommand("ExfilNow", new Action(Plugin._ecScript.ExfilNow));
            ConsoleScreen.Processor.RegisterCommand("GetWeaponIds", new Action(GetAllWeaponIDs));
            ConsoleScreen.Processor.RegisterCommand("GetAllIds", new Action(GetAllItemIDs));
            //ConsoleScreen.Processor.RegisterCommand("DoGearExfil", new Action(Plugin.ECScript.DoGearExfilEvent));
        }

        private static void GetAllWeaponIDs()
        {
            var weapons = Plugin._session.Profile.Inventory?.AllRealPlayerItems;
            weapons = weapons.Where(x => x is Weapon);

            foreach (var weapon in weapons)
            {
                Plugin._log.LogInfo($"Template ID: {weapon.TemplateId}, locale name: {weapon.LocalizedName()}");
                Utils.LogToServerConsole($"Template ID: {weapon.TemplateId}, locale name: {weapon.LocalizedName()}");
            }
        }

        private static void GetAllItemIDs()
        {
            var items = Plugin._session.Profile.Inventory?.AllRealPlayerItems;
            items = items.Where(x => x is Item);

            foreach (var item in items)
            {
                Plugin._log.LogInfo($"Template ID: {item.TemplateId}, locale name: {item.LocalizedName()}");
                Utils.LogToServerConsole($"Template ID: {item.TemplateId}, locale name: {item.LocalizedName()}");
            }
        }
    }
}
