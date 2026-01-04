namespace ROConfigGUI
{
    public class DefaultConfigTemplate
    {
        public bool EnableCustomBoss { get; set; } = true;
        public bool EnableRequisitionOffice { get; set; } = true;
        public bool UseLegionGlobalSpawnChance { get; set; } = false;
        public int GlobalSpawnChance { get; set; } = 15;
        public bool EnableCustomItems { get; set; } = true;
        public bool BackupProfile { get; set; } = true;
        public bool ReduceFoodAndHydroDegradeEnabled { get; set; } = true;
        public float EnergyDecay { get; set; } = 0.6f;
        public float HydroDecay { get; set; } = 0.6f;
        public bool SaveQuestItems { get; set; } = true;
        public bool NoRunThrough { get; set; } = true;
        public bool LootableMelee { get; set; } = true;
        public bool LootableArmbands { get; set; } = false;
        public bool EnableExtendedRaids { get; set; } = true;
        public int TimeLimit { get; set; } = 999;
        public bool HolsterAnything { get; set; } = true;
        public bool LowerExamineTime { get; set; } = true;
        public bool SpecialSlotChanges { get; set; } = false;
        public bool ChangeBackpackSizes { get; set; } = false;
        public bool ModifyEnemyBotHealth { get; set; } = false;
        public bool ChangeAirdropValuesEnabled { get; set; } = true;
        public int Customs { get; set; } = 25;
        public int Woods { get; set; } = 25;
        public int Lighthouse { get; set; } = 25;
        public int Interchange { get; set; } = 25;
        public int Shoreline { get; set; } = 25;
        public int Reserve { get; set; } = 25;
        public int Streets { get; set; } = 25;
        public int GroundZero { get; set; } = 25;
        public bool PocketChangesEnabled { get; set; } = false;
        public int Pocket1Vertical { get; set; } = 1;
        public int Pocket1Horizontal { get; set; } = 1;
        public int Pocket2Vertical { get; set; } = 2;
        public int Pocket2Horizontal { get; set; } = 1;
        public int Pocket3Vertical { get; set; } = 2;
        public int Pocket3Horizontal { get; set; } = 1;
        public int Pocket4Vertical { get; set; } = 1;
        public int Pocket4Horizontal { get; set; } = 1;
        public bool WeightChangesEnabled { get; set; } = false;
        public float WeightMultiplier { get; set; } = 2f;
        public bool DisableFleaBlacklist { get; set; } = true;
        public bool LL1Items { get; set; } = false;
        public bool RemoveFirRequirementsForQuests { get; set; } = false;
        public bool InsuranceChangesEnabled { get; set; } = true;
        public int PraporMinReturn { get; set; } = 2;
        public int PraporMaxReturn { get; set; } = 4;
        public int TherapistMinReturn { get; set; } = 1;
        public int TherapistMaxReturn { get; set; } = 2;
        public bool BasicStackTuningEnabled { get; set; } = false;
        public int StackMultiplier { get; set; } = 5;
        public bool AdvancedStackTuningEnabled { get; set; } = false;
        public int ShotgunStack { get; set; } = 100;
        public int FlaresAndUBGL { get; set; } = 5;
        public int SniperStack { get; set; } = 100;
        public int SMGStack { get; set; } = 250;
        public int RifleStack { get; set; } = 150;
        public bool MoneyStackMultiplierEnabled { get; set; } = false;
        public int MoneyMultiplier { get; set; } = 10;
        public bool LootChangesEnabled { get; set; } = false;
        public float StaticLootMultiplier { get; set; } = 3f;
        public float LooseLootMultiplier { get; set; } = 2f;
        public float MarkedRoomLootMultiplier { get; set; } = 3f;
        public bool WeatherChangesEnabled { get; set; } = true;
        public bool AllSeasons { get; set; } = false;
        public bool NoWinter { get; set; } = false;
        public bool SeasonalProgression { get; set; } = true;
        public bool WinterWonderland { get; set; } = false;
    }

    public class DefaultWeightingTemplate
    {
        public int SwitchToggle { get; set; } = 2;
        public int DoorUnlock { get; set; } = 12;
        public int KeycardUnlock { get; set; } = 1;
        public int DoorEventRangeMinimum { get; set; } = 1;
        public int DoorEventRangeMaximum { get; set; } = 3;
        public int DamageEvent { get; set; } = 40;
        public int AirdropEvent { get; set; } = 110;
        public int BlackoutEvent { get; set; } = 80;
        public int JokeEvent { get; set; } = 40;
        public int HealEvent { get; set; } = 120;
        public int ArmorEvent { get; set; } = 140;
        public int SkillEvent { get; set; } = 60;
        public int MetabolismEvent { get; set; } = 60;
        public int MalfunctionEvent { get; set; } = 40;
        public int TraderEvent { get; set; } = 25;
        public int BerserkEvent { get; set; } = 40;
        public int WeightEvent { get; set; } = 40;
        public int MaxLLEvent { get; set; } = 5;
        public int LockdownEvent { get; set; } = 10;
        public int ArtilleryEvent { get; set; } = 10;
        public int RandomEventRangeMinimum { get; set; } = 5;
        public int RandomEventRangeMaximum { get; set; } = 25;
    }
}
