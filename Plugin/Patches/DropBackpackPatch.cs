using System;
using System.Reflection;
using EFT;
using RaidOverhaul.Configs;
using SPT.Reflection.Patching;

namespace RaidOverhaul.Patches
{
    internal class OnDeadPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(Player).GetMethod("OnDead", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        private static void PatchPostFix(ref Player __instance)
        {
            if (DJConfig.DropBackPack.Value && DJConfig.DropBackPackChance.Value > new Random().NextDouble())
            {
                __instance.DropBackpack();
            }
        }
    }
}
