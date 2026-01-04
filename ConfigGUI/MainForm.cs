using Newtonsoft.Json;

namespace ROConfigGUI
{
    public partial class MainForm : Form
    {
        private ConfigTemplate? configTemplate;
        private string? configFilePath;
        private WeightingTemplate? weightingTemplate;
        private string? weightingsFilePath;
        private DefaultConfigTemplate? defaultConfigTemplate;
        private DefaultWeightingTemplate? defaultWeightingTemplate;

        public MainForm()
        {
            try
            {
                InitializeComponent();
                LoadConfigs();
                SetVersion();
                SetDisplayValues();
                WarningBox.Hide();
            }
            catch (Exception exception)
            {
                WelcomeTextBox.Hide();
                VersionLabel.Hide();
                WarningBox.Show();
                WarningBox.Text =
                    $"{exception.Message}  \n\nMake sure the config app is located in the root of the RaidOverhaul server folder.";
            }
        }

        private void LoadConfigs()
        {
            configFilePath = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), @"Assets\config\config.json");
            string configFile = File.ReadAllText(configFilePath);
            configTemplate = JsonConvert.DeserializeObject<ConfigTemplate>(configFile);
            defaultConfigTemplate = JsonConvert.DeserializeObject<DefaultConfigTemplate>(configFile);

            weightingsFilePath = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), @"Assets\config\eventWeightings.json");
            string weightingsFile = File.ReadAllText(weightingsFilePath);
            weightingTemplate = JsonConvert.DeserializeObject<WeightingTemplate>(weightingsFile);
            defaultWeightingTemplate = JsonConvert.DeserializeObject<DefaultWeightingTemplate>(weightingsFile);
        }

        private void SetVersion()
        {
            string modVer = "v" + "2.8.0";
            string sptVer = "v" + "~4.0.x";

            VersionLabel.Text = "Raid Overhaul Config for RO " + modVer + " and Spt " + sptVer;
        }

        private void SetDisplayValues()
        {
            customBossCheckBox.Checked = defaultConfigTemplate.EnableCustomBoss;
            TraderCheckBox.Checked = defaultConfigTemplate.EnableRequisitionOffice;
            GlobalChanceCheckBox.Checked = defaultConfigTemplate.UseLegionGlobalSpawnChance;
            GlobalSpawnChanceNumeric.Value = (decimal)defaultConfigTemplate.GlobalSpawnChance;
            CustomItemsCheckBox.Checked = defaultConfigTemplate.EnableCustomItems;
            BackupProfileCheckBox.Checked = defaultConfigTemplate.BackupProfile;
            QuestItemsCheckBox.Checked = defaultConfigTemplate.SaveQuestItems;
            RunThhroughCheckBox.Checked = defaultConfigTemplate.NoRunThrough;
            MeleeLootCheckBox.Checked = defaultConfigTemplate.LootableMelee;
            ArmbandLootCheckBox.Checked = defaultConfigTemplate.LootableArmbands;
            ExtendedRaidsCheckBox.Checked = defaultConfigTemplate.EnableExtendedRaids;
            RaidTimeLimitNumeric.Value = (decimal)defaultConfigTemplate.TimeLimit;
            HolsterCheckBox.Checked = defaultConfigTemplate.HolsterAnything;
            ExamineTimeCheckBox.Checked = defaultConfigTemplate.LowerExamineTime;
            SpecialSlotCheckBox.Checked = defaultConfigTemplate.SpecialSlotChanges;
            BackpackSizeCheckBox.Checked = defaultConfigTemplate.ChangeBackpackSizes;
            EnemyHealthCheckBox.Checked = defaultConfigTemplate.ModifyEnemyBotHealth;
            HydrationEnergyCheckBox.Checked = defaultConfigTemplate.ReduceFoodAndHydroDegradeEnabled;
            EnergyDecayNumeric.Value = (decimal)defaultConfigTemplate.EnergyDecay;
            HydrationDecayNumeric.Value = (decimal)defaultConfigTemplate.HydroDecay;
            EnableWeatherCheckBox.Checked = defaultConfigTemplate.WeatherChangesEnabled;
            RandomizeWeatherCheckBox.Checked = defaultConfigTemplate.AllSeasons;
            NoWinterCheckBox.Checked = defaultConfigTemplate.NoWinter;
            WinterWonderlandCheckBox.Checked = defaultConfigTemplate.WinterWonderland;
            SeasonalProgressionCheckBox.Checked = defaultConfigTemplate.SeasonalProgression;
            LootChangesCheckBox.Checked = defaultConfigTemplate.LootChangesEnabled;
            StaticLootNumeric.Value = (decimal)defaultConfigTemplate.StaticLootMultiplier;
            LooseLootNumeric.Value = (decimal)defaultConfigTemplate.LooseLootMultiplier;
            MarkedLootNumeric.Value = (decimal)defaultConfigTemplate.MarkedRoomLootMultiplier;
            AirdropChangesCheckBox.Checked = defaultConfigTemplate.ChangeAirdropValuesEnabled;
            CustomsNumeric.Value = (decimal)defaultConfigTemplate.Customs;
            WoodsNumeric.Value = (decimal)defaultConfigTemplate.Woods;
            LighthouseNumeric.Value = (decimal)defaultConfigTemplate.Lighthouse;
            InterchangeNumeric.Value = (decimal)defaultConfigTemplate.Interchange;
            ShorelineNumeric.Value = (decimal)defaultConfigTemplate.Shoreline;
            ReserveNumeric.Value = (decimal)defaultConfigTemplate.Reserve;
            StreetsNumeric.Value = (decimal)defaultConfigTemplate.Streets;
            GroundZeroNumeric.Value = (decimal)defaultConfigTemplate.GroundZero;
            DisableFleaBlacklistCheckBox.Checked = defaultConfigTemplate.DisableFleaBlacklist;
            LL1ItemsCheckBox.Checked = defaultConfigTemplate.LL1Items;
            FIRForQuestsCheckBox.Checked = defaultConfigTemplate.RemoveFirRequirementsForQuests;
            InsuranceTimeCheckBox.Checked = defaultConfigTemplate.InsuranceChangesEnabled;
            PraporMinNumeric.Value = (decimal)defaultConfigTemplate.PraporMinReturn;
            PraporMaxNumeric.Value = (decimal)defaultConfigTemplate.PraporMaxReturn;
            TherapistMinNumeric.Value = (decimal)defaultConfigTemplate.TherapistMinReturn;
            TherapistMaxNumeric.Value = (decimal)defaultConfigTemplate.TherapistMaxReturn;
            MoneyStackCheckBox.Checked = defaultConfigTemplate.MoneyStackMultiplierEnabled;
            MoneyMultiNumeric.Value = (decimal)defaultConfigTemplate.MoneyMultiplier;
            BasicStackCheckBox.Checked = defaultConfigTemplate.BasicStackTuningEnabled;
            StackMultiNumeric.Value = (decimal)defaultConfigTemplate.StackMultiplier;
            AdvancedStackCheckBox.Checked = defaultConfigTemplate.AdvancedStackTuningEnabled;
            ShotgunNumeric.Value = (decimal)defaultConfigTemplate.ShotgunStack;
            FlareUBGLNumeric.Value = (decimal)defaultConfigTemplate.FlaresAndUBGL;
            SniperNumeric.Value = (decimal)defaultConfigTemplate.SniperStack;
            SMGNumeric.Value = (decimal)defaultConfigTemplate.SMGStack;
            RifleNumeric.Value = (decimal)defaultConfigTemplate.RifleStack;
            WeightChangesCheckBox.Checked = defaultConfigTemplate.WeightChangesEnabled;
            WeightMultiNumeric.Value = (decimal)defaultConfigTemplate.WeightMultiplier;
            PocketSizeChangesCheckBox.Checked = defaultConfigTemplate.PocketChangesEnabled;
            P1VNumeric.Value = (decimal)defaultConfigTemplate.Pocket1Vertical;
            P1HNumeric.Value = (decimal)defaultConfigTemplate.Pocket1Horizontal;
            P2VNumeric.Value = (decimal)defaultConfigTemplate.Pocket2Vertical;
            P2HNumeric.Value = (decimal)defaultConfigTemplate.Pocket2Horizontal;
            P3VNumeric.Value = (decimal)defaultConfigTemplate.Pocket3Vertical;
            P3HNumeric.Value = (decimal)defaultConfigTemplate.Pocket3Horizontal;
            P4V.Value = (decimal)defaultConfigTemplate.Pocket4Vertical;
            P4HNumeric.Value = (decimal)defaultConfigTemplate.Pocket4Horizontal;

            HANumeric.Value = (decimal)defaultWeightingTemplate.DamageEvent;
            ADNumeric.Value = (decimal)defaultWeightingTemplate.AirdropEvent;
            BONumeric.Value = (decimal)defaultWeightingTemplate.BlackoutEvent;
            JokeNumeric.Value = (decimal)defaultWeightingTemplate.JokeEvent;
            HealNumeric.Value = (decimal)defaultWeightingTemplate.HealEvent;
            ARNumeric.Value = (decimal)defaultWeightingTemplate.ArmorEvent;
            SPNumeric.Value = (decimal)defaultWeightingTemplate.SkillEvent;
            MetaNumeric.Value = (decimal)defaultWeightingTemplate.MetabolismEvent;
            MalfNumeric.Value = (decimal)defaultWeightingTemplate.MalfunctionEvent;
            TRNumeric.Value = (decimal)defaultWeightingTemplate.TraderEvent;
            BERSERKNumeric.Value = (decimal)defaultWeightingTemplate.BerserkEvent;
            PWNumeric.Value = (decimal)defaultWeightingTemplate.WeightEvent;
            MLLNumeric.Value = (decimal)defaultWeightingTemplate.MaxLLEvent;
            LDNumeric.Value = (decimal)defaultWeightingTemplate.LockdownEvent;
            ArtyNumeric.Value = (decimal)defaultWeightingTemplate.ArtilleryEvent;
            IREventStartTimeMinNumeric.Value = (decimal)defaultWeightingTemplate.RandomEventRangeMinimum;
            IREventStartTimeMaxNumeric.Value = (decimal)defaultWeightingTemplate.RandomEventRangeMaximum;

            DoorNumeric.Value = (decimal)defaultWeightingTemplate.DoorUnlock;
            KeycardNumeric.Value = (decimal)defaultWeightingTemplate.KeycardUnlock;
            PowerSwitchNumeric.Value = (decimal)defaultWeightingTemplate.SwitchToggle;
            DoorEventStartTimeMinNumeric.Value = (decimal)defaultWeightingTemplate.DoorEventRangeMinimum;
            DoorEventStartTimeMaxNumeric.Value = (decimal)defaultWeightingTemplate.DoorEventRangeMaximum;
        }

        public void LabelTimerRun(Label label)
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) =>
            {
                label.ForeColor = label.BackColor;
                timer.Stop();
            };
            timer.Start();
        }

        private void Save1_Click(object sender, EventArgs e)
        {
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            SavedChangesLabel1.ForeColor = Color.GreenYellow;
            LabelTimerRun(SavedChangesLabel1);
        }

        private void Save2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            SavedChangesLabel2.ForeColor = Color.GreenYellow;
            LabelTimerRun(SavedChangesLabel2);
        }

        private void Save3_Click(object sender, EventArgs e)
        {
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            SavedChangesLabel3.ForeColor = Color.GreenYellow;
            LabelTimerRun(SavedChangesLabel3);
        }

        private void Save4_Click(object sender, EventArgs e)
        {
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            SavedChangesLabel4.ForeColor = Color.GreenYellow;
            LabelTimerRun(SavedChangesLabel4);
        }

        private void Save5_Click(object sender, EventArgs e)
        {
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            SavedChangesLabel5.ForeColor = Color.GreenYellow;
            LabelTimerRun(SavedChangesLabel5);
        }

        private void Save6_Click(object sender, EventArgs e)
        {
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            SavedChangesLabel6.ForeColor = Color.GreenYellow;
            LabelTimerRun(SavedChangesLabel6);
        }

        private void Revert1_Click(object sender, EventArgs e)
        {
            SetDisplayValues();
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            RevertedChangesLabel1.ForeColor = Color.Maroon;
            LabelTimerRun(RevertedChangesLabel1);
        }

        private void Revert2_Click(object sender, EventArgs e)
        {
            SetDisplayValues();
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            RevertedChangesLabel2.ForeColor = Color.Maroon;
            LabelTimerRun(RevertedChangesLabel2);
        }

        private void Revert3_Click(object sender, EventArgs e)
        {
            SetDisplayValues();
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            RevertedChangesLabel3.ForeColor = Color.Maroon;
            LabelTimerRun(RevertedChangesLabel3);
        }

        private void Revert4_Click(object sender, EventArgs e)
        {
            SetDisplayValues();
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            RevertedChangesLabel4.ForeColor = Color.Maroon;
            LabelTimerRun(RevertedChangesLabel4);
        }

        private void Revert5_Click(object sender, EventArgs e)
        {
            SetDisplayValues();
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            RevertedChangesLabel5.ForeColor = Color.Maroon;
            LabelTimerRun(RevertedChangesLabel5);
        }

        private void Revert6_Click(object sender, EventArgs e)
        {
            SetDisplayValues();
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(configTemplate));
            File.WriteAllText(weightingsFilePath, JsonConvert.SerializeObject(weightingTemplate));
            RevertedChangesLabel6.ForeColor = Color.Maroon;
            LabelTimerRun(RevertedChangesLabel6);
        }

        private void customBossCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.EnableCustomBoss = customBossCheckBox.Checked == true ? true : false;
        }

        private void TraderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.EnableRequisitionOffice = TraderCheckBox.Checked == true ? true : false;
        }

        private void GlobalChanceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.UseLegionGlobalSpawnChance = GlobalChanceCheckBox.Checked == true ? true : false;
        }

        private void GlobalSpawnChanceNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.GlobalSpawnChance = (int)GlobalSpawnChanceNumeric.Value;
        }

        private void CustomItemsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.EnableCustomItems = CustomItemsCheckBox.Checked == true ? true : false;
        }

        private void BackupProfileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.BackupProfile = BackupProfileCheckBox.Checked == true ? true : false;
        }

        private void QuestItemsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.SaveQuestItems = QuestItemsCheckBox.Checked == true ? true : false;
        }

        private void RunThhroughCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.NoRunThrough = RunThhroughCheckBox.Checked == true ? true : false;
        }

        private void MeleeLootCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.LootableMelee = MeleeLootCheckBox.Checked == true ? true : false;
        }

        private void ArmbandLootCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.LootableArmbands = ArmbandLootCheckBox.Checked == true ? true : false;
        }

        private void ExtendedRaidsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.EnableExtendedRaids = ExtendedRaidsCheckBox.Checked == true ? true : false;
        }

        private void RaidTimeLimitNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.TimeLimit = (int)RaidTimeLimitNumeric.Value;
        }

        private void HolsterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.HolsterAnything = HolsterCheckBox.Checked == true ? true : false;
        }

        private void ExamineTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.LowerExamineTime = ExamineTimeCheckBox.Checked == true ? true : false;
        }

        private void SpecialSlotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.SpecialSlotChanges = SpecialSlotCheckBox.Checked == true ? true : false;
        }

        private void BackpackSizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.ChangeBackpackSizes = BackpackSizeCheckBox.Checked == true ? true : false;
        }

        private void EnemyHealthCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.ModifyEnemyBotHealth = EnemyHealthCheckBox.Checked == true ? true : false;
        }

        private void HydrationEnergyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.ReduceFoodAndHydroDegradeEnabled = HydrationEnergyCheckBox.Checked == true ? true : false;
        }

        private void EnergyDecayNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.EnergyDecay = (float)EnergyDecayNumeric.Value;
        }

        private void HydrationDecayNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.HydroDecay = (float)HydrationDecayNumeric.Value;
        }

        private void EnableWeatherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.WeatherChangesEnabled = EnableWeatherCheckBox.Checked == true ? true : false;
        }

        private void RandomizeWeatherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.AllSeasons = RandomizeWeatherCheckBox.Checked == true ? true : false;
        }

        private void NoWinterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.NoWinter = NoWinterCheckBox.Checked == true ? true : false;
        }

        private void WinterWonderlandCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.WinterWonderland = WinterWonderlandCheckBox.Checked == true ? true : false;
        }

        private void SeasonalProgressionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.SeasonalProgression = SeasonalProgressionCheckBox.Checked == true ? true : false;
        }

        private void LootChangesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.LootChangesEnabled = LootChangesCheckBox.Checked == true ? true : false;
        }

        private void StaticLootNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.StaticLootMultiplier = (int)StaticLootNumeric.Value;
        }

        private void LooseLootNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.LooseLootMultiplier = (int)LooseLootNumeric.Value;
        }

        private void MarkedLootNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.MarkedRoomLootMultiplier = (int)MarkedLootNumeric.Value;
        }

        private void AirdropChangesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.ChangeAirdropValuesEnabled = AirdropChangesCheckBox.Checked == true ? true : false;
        }

        private void CustomsNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Customs = (int)CustomsNumeric.Value;
        }

        private void WoodsNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Woods = (int)WoodsNumeric.Value;
        }

        private void LighthouseNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Lighthouse = (int)LighthouseNumeric.Value;
        }

        private void InterchangeNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Interchange = (int)InterchangeNumeric.Value;
        }

        private void ShorelineNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Shoreline = (int)ShorelineNumeric.Value;
        }

        private void ReserveNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Reserve = (int)ReserveNumeric.Value;
        }

        private void StreetsNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Streets = (int)StreetsNumeric.Value;
        }

        private void GroundZeroNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.GroundZero = (int)GroundZeroNumeric.Value;
        }

        private void DisableFleaBlacklistCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.DisableFleaBlacklist = DisableFleaBlacklistCheckBox.Checked == true ? true : false;
        }

        private void LL1ItemsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.Ll1Items = LL1ItemsCheckBox.Checked == true ? true : false;
        }

        private void FIRForQuestsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.RemoveFirRequirementsForQuests = FIRForQuestsCheckBox.Checked == true ? true : false;
        }

        private void InsuranceTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.InsuranceChangesEnabled = InsuranceTimeCheckBox.Checked == true ? true : false;
        }

        private void PraporMinNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.PraporMinReturn = (int)PraporMinNumeric.Value;
        }

        private void PraporMaxNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.PraporMaxReturn = (int)PraporMaxNumeric.Value;
        }

        private void TherapistMinNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.TherapistMinReturn = (int)TherapistMinNumeric.Value;
        }

        private void TherapistMaxNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.TherapistMaxReturn = (int)TherapistMaxNumeric.Value;
        }

        private void MoneyStackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.MoneyStackMultiplierEnabled = MoneyStackCheckBox.Checked == true ? true : false;
        }

        private void MoneyMultiNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.MoneyMultiplier = (int)MoneyMultiNumeric.Value;
        }

        private void BasicStackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.BasicStackTuningEnabled = BasicStackCheckBox.Checked == true ? true : false;
        }

        private void StackMultiNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.StackMultiplier = (int)StackMultiNumeric.Value;
        }

        private void AdvancedStackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.AdvancedStackTuningEnabled = AdvancedStackCheckBox.Checked == true ? true : false;
        }

        private void ShotgunNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.ShotgunStack = (int)ShotgunNumeric.Value;
        }

        private void FlareUBGLNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.FlaresAndUbgl = (int)FlareUBGLNumeric.Value;
        }

        private void SniperNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.SniperStack = (int)SniperNumeric.Value;
        }

        private void SMGNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.SmgStack = (int)SMGNumeric.Value;
        }

        private void RifleNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.RifleStack = (int)RifleNumeric.Value;
        }

        private void WeightChangesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.WeightChangesEnabled = WeightChangesCheckBox.Checked == true ? true : false;
        }

        private void WeightMultiNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.WeightMultiplier = (float)WeightMultiNumeric.Value;
        }

        private void PocketSizeChangesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configTemplate.PocketChangesEnabled = PocketSizeChangesCheckBox.Checked == true ? true : false;
        }

        private void P1VNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Pocket1Vertical = (int)P1VNumeric.Value;
        }

        private void P1HNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Pocket1Horizontal = (int)P1HNumeric.Value;
        }

        private void P2VNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Pocket2Vertical = (int)P2VNumeric.Value;
        }

        private void P2HNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Pocket2Horizontal = (int)P2HNumeric.Value;
        }

        private void P3VNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Pocket3Vertical = (int)P3VNumeric.Value;
        }

        private void P3HNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Pocket3Horizontal = (int)P3HNumeric.Value;
        }

        private void P4V_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Pocket4Vertical = (int)P4V.Value;
        }

        private void P4HNumeric_ValueChanged(object sender, EventArgs e)
        {
            configTemplate.Pocket4Horizontal = (int)P4HNumeric.Value;
        }

        private void HANumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.DamageEvent = (int)HANumeric.Value;
        }

        private void ADNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.AirdropEvent = (int)ADNumeric.Value;
        }

        private void BONumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.BlackoutEvent = (int)BONumeric.Value;
        }

        private void JokeNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.JokeEvent = (int)JokeNumeric.Value;
        }

        private void HealNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.HealEvent = (int)HealNumeric.Value;
        }

        private void ARNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.ArmorEvent = (int)ARNumeric.Value;
        }

        private void SPNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.SkillEvent = (int)SPNumeric.Value;
        }

        private void MetaNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.MetabolismEvent = (int)MetaNumeric.Value;
        }

        private void MalfNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.MalfunctionEvent = (int)MalfNumeric.Value;
        }

        private void TRNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.TraderEvent = (int)TRNumeric.Value;
        }

        private void BERSERKNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.BerserkEvent = (int)BERSERKNumeric.Value;
        }

        private void PWNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.WeightEvent = (int)PWNumeric.Value;
        }

        private void MLLNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.MaxLLEvent = (int)MLLNumeric.Value;
        }

        private void LDNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.LockdownEvent = (int)LDNumeric.Value;
        }

        private void ArtyNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.ArtilleryEvent = (int)ArtyNumeric.Value;
        }

        private void IREventStartTimeMinNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.RandomEventRangeMinimum = (int)IREventStartTimeMinNumeric.Value;
        }

        private void IREventStartTimeMaxNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.RandomEventRangeMaximum = (int)IREventStartTimeMaxNumeric.Value;
        }

        private void DoorNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.DoorUnlock = (int)DoorNumeric.Value;
        }

        private void KeycardNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.KeycardUnlock = (int)KeycardNumeric.Value;
        }

        private void PowerSwitchNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.SwitchToggle = (int)PowerSwitchNumeric.Value;
        }

        private void DoorEventStartTimeMinNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.DoorEventRangeMinimum = (int)DoorEventStartTimeMinNumeric.Value;
        }

        private void DoorEventStartTimeMaxNumeric_ValueChanged(object sender, EventArgs e)
        {
            weightingTemplate.DoorEventRangeMaximum = (int)DoorEventStartTimeMaxNumeric.Value;
        }
    }
}
