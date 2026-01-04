using System;
using System.Collections.Generic;
using BepInEx.Logging;
using EFT.UI.DragAndDrop;
using RaidOverhaul.Controllers;
using UnityEngine;

namespace RaidOverhaul.Helpers
{
    internal class BundleLoader
    {
        internal void LoadLayouts(ManualLogSource logger)
        {
            try
            {
                var layoutBundles = Utils.Get<Dictionary<string, string>>("/RaidOverhaul/GetLayoutBundles");
                if (layoutBundles == null)
                {
                    return;
                }

                foreach (var layoutBundle in layoutBundles)
                {
                    var layoutBundleName = layoutBundle.Key;
                    var base64Data = layoutBundle.Value;
                    if (string.IsNullOrEmpty(base64Data))
                    {
                        logger.LogWarning($"No data for rig layout: {layoutBundleName}");
                        continue;
                    }

                    byte[] bundleData;
                    try
                    {
                        bundleData = Convert.FromBase64String(base64Data);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"Base64 decode failed for rig layout {layoutBundleName}: {ex}");
                        continue;
                    }

                    if (bundleData.Length == 0)
                    {
                        continue;
                    }

                    LoadLayoutBundle(bundleData, layoutBundleName, logger);
                    if (ConfigController.DebugConfig.DebugMode)
                    {
                        Plugin._log.LogInfo("Successfully added new rig layouts to resources dictionary!");
                        Utils.LogToServerConsole("Successfully added new rig layouts to resources dictionary!");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error loading rig layouts: {ex}");
            }
        }

        private void LoadLayoutBundle(byte[] data, string bundleName, ManualLogSource logger)
        {
            try
            {
                if (data == null || data.Length == 0)
                {
                    return;
                }

                var bundle = AssetBundle.LoadFromMemory(data);
                if (bundle == null)
                {
                    return;
                }

                var gameObjects = bundle.LoadAllAssets<GameObject>();

                if (gameObjects != null)
                {
                    foreach (var prefab in gameObjects)
                    {
                        if (prefab == null)
                        {
                            continue;
                        }

                        var gridView = prefab.GetComponent<ContainedGridsView>();
                        if (gridView == null)
                        {
                            continue;
                        }

                        AddEntryToDictionary($"UI/Rig Layouts/{prefab.name}", gridView);
                    }
                }

                bundle.Unload(false);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error loading bundle {bundleName}: {ex}");
            }
        }

        private void AddEntryToDictionary(string key, object value)
        {
            if (CacheResourcesPopAbstractClass.Dictionary_0 is null)
            {
                return;
            }

            if (!CacheResourcesPopAbstractClass.Dictionary_0.ContainsKey(key))
            {
                CacheResourcesPopAbstractClass.Dictionary_0.Add(key, value);
            }
        }
    }
}
