using System.Reflection;
using EFT;
using EFT.Communications;
using EFT.Interactive;
using SPT.Reflection.Patching;

namespace RaidOverhaul.Patches
{
    internal class EventExfilPatch : ModulePatch
    {
        internal static bool _isLockdown = false;

        internal static bool _awaitDrop = false;

        protected override MethodBase GetTargetMethod()
        {
            return typeof(ExfiltrationRequirement).GetMethod("Met", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        private static void Postfix(Player player, ref bool __result)
        {
            if (player.IsYourPlayer)
            {
                if (_isLockdown)
                {
                    NotificationManagerClass.DisplayMessageNotification(
                        "Cannot extract during a lockdown",
                        ENotificationDurationType.Long,
                        ENotificationIconType.Alert
                    );
                }
            }
            __result = true;
        }
    }
}
