using System.Linq;
using BepInEx;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using RaidOverhaul.Helpers;
using UnityEngine;

namespace RaidOverhaul.Controllers
{
    internal class KeyController : MonoBehaviour
    {
        private static Door[] _door;
        private static KeycardDoor[] _kdoor;

        public static void PatchLocks()
        {
            if (HasKey(Utils.SkeletonKey))
            {
                _door ??= FindObjectsOfType<Door>();

                foreach (var door in _door)
                {
                    if (!door.KeyId.IsNullOrWhiteSpace())
                    {
                        if (!HasKey(door.KeyId))
                        {
                            door.KeyId = Utils.SkeletonKey;
                        }
                    }
                }
            }

            if (HasKey(Utils.VipKeycard))
            {
                _kdoor ??= FindObjectsOfType<KeycardDoor>();

                foreach (var kDoor in _kdoor)
                {
                    if (!kDoor.KeyId.IsNullOrWhiteSpace())
                    {
                        if (!HasKey(kDoor.KeyId))
                        {
                            kDoor.KeyId = Utils.VipKeycard;
                        }
                    }
                }
            }
        }

        private static bool HasKey(string keyId)
        {
            return Singleton<GameWorld>.Instance.MainPlayer.Profile.Inventory.Equipment.GetAllItems().Any(x => x.TemplateId == keyId);
        }
    }
}
