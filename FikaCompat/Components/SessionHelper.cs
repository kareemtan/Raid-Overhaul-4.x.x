using System;
using System.Collections.Generic;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using UnityEngine;

namespace RaidOverhaul.FikaModule.Components
{
    internal class ROSession : MonoBehaviour
    {
        private ROSession() { }

        private static ROSession _instance;
        private static bool _isInitialized;

        private Player Player { get; set; }
        private GameWorld _gameWorld;
        private GameWorld GameWorld
        {
            get { return _gameWorld; }
            set { _gameWorld = value; }
        }

        public GamePlayerOwner GamePlayerOwner { get; private set; }

        private static ROSession Instance
        {
            get
            {
                if (!_isInitialized)
                {
                    if (!Singleton<GameWorld>.Instantiated)
                    {
                        throw new Exception("Can't get Instance, GameWorld is not instantiated");
                    }

                    _instance = Singleton<GameWorld>.Instance.MainPlayer.gameObject.GetOrAddComponent<ROSession>();
                    _isInitialized = true;
                }
                return _instance;
            }
        }

        private Dictionary<string, Door> AllDoors { get; set; }
        private Dictionary<string, Switch> AllSwitches { get; set; }
        private Dictionary<string, KeycardDoor> AllKDoors { get; set; }
        private Dictionary<string, LampController> AllLamps { get; set; }

        private void Awake()
        {
            var gameWorldInstance = Singleton<GameWorld>.Instance;
            GameWorld = gameWorldInstance;
            Player = gameWorldInstance.MainPlayer;
            GamePlayerOwner = Player.gameObject.GetComponent<GamePlayerOwner>();

            InitializeInteractiveObjects();
        }

        private void OnDestroy()
        {
            _isInitialized = false;
            _instance = null;
        }

        private void InitializeInteractiveObjects()
        {
            var doors = FindObjectsOfType<Door>();
            AllDoors = new Dictionary<string, Door>(doors.Length);
            foreach (var door in doors)
            {
                if (!string.IsNullOrEmpty(door.Id))
                {
                    AllDoors[door.Id] = door;
                }
            }

            var lamps = FindObjectsOfType<LampController>();
            AllLamps = new Dictionary<string, LampController>(lamps.Length);
            foreach (var lamp in lamps)
            {
                if (!string.IsNullOrEmpty(lamp.Id))
                {
                    AllLamps[lamp.Id] = lamp;
                }
            }

            var kDoors = FindObjectsOfType<KeycardDoor>();
            AllKDoors = new Dictionary<string, KeycardDoor>(kDoors.Length);
            foreach (var kDoor in kDoors)
            {
                if (!string.IsNullOrEmpty(kDoor.Id))
                {
                    AllKDoors[kDoor.Id] = kDoor;
                }
            }

            var switches = FindObjectsOfType<Switch>();
            AllSwitches = new Dictionary<string, Switch>(switches.Length);
            foreach (var pSwitch in switches)
            {
                if (!string.IsNullOrEmpty(pSwitch.Id))
                {
                    AllSwitches[pSwitch.Id] = pSwitch;
                }
            }
        }

        public static Door GetDoorById(string id)
        {
            if (Instance.AllDoors.TryGetValue(id, out var door))
            {
                return door;
            }
            return null;
        }

        public static bool TryGetDoorById(string id, out Door door)
        {
            return Instance.AllDoors.TryGetValue(id, out door);
        }

        public static KeycardDoor GetKeycardDoorById(string id)
        {
            if (Instance.AllKDoors.TryGetValue(id, out var kDoor))
            {
                return kDoor;
            }
            return null;
        }

        public static bool TryGetKeycardDoorById(string id, out KeycardDoor kDoor)
        {
            return Instance.AllKDoors.TryGetValue(id, out kDoor);
        }

        public static Switch GetSwitchById(string id)
        {
            if (Instance.AllSwitches.TryGetValue(id, out var pSwitch))
            {
                return pSwitch;
            }
            return null;
        }

        public static bool TryGetSwitchById(string id, out Switch pSwitch)
        {
            return Instance.AllSwitches.TryGetValue(id, out pSwitch);
        }

        public static LampController GetLampById(string id)
        {
            if (Instance.AllLamps.TryGetValue(id, out var lamp))
            {
                return lamp;
            }
            return null;
        }

        public static bool TryGetLampById(string id, out LampController lamp)
        {
            return Instance.AllLamps.TryGetValue(id, out lamp);
        }
    }
}
