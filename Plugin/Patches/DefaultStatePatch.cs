using System.Reflection;
using EFT;
using RaidOverhaul.Controllers;
using SPT.Reflection.Patching;

namespace RaidOverhaul.Patches
{
    internal class RandomizeDefaultStatePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));
        }

        [PatchPrefix]
        private static void PatchPrefix()
        {
            DoorController.RandomizeDefaultDoors();
            DoorController.RandomizeLampState();
        }
    }
}
