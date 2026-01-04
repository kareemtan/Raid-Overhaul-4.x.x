using System;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;

namespace RaidOverhaul.Checkers
{
    internal class DependencyChecker
    {
        /// <summary>
        /// Check that all of the BepInDependency entries for the given pluginType are available and instantiated. This allows a
        /// plugin to validate that its dependent plugins weren't disabled post-dependency check (Such as for the wrong EFT version)
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="info"></param>
        /// <param name="pluginType"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static bool ValidateDependencies(ManualLogSource logger, PluginInfo info, Type pluginType, ConfigFile config = null)
        {
            var noVersion = new Version("0.0.0");
            var dependencies = pluginType.GetCustomAttributes(typeof(BepInDependency), true) as BepInDependency[];

            foreach (var dependency in dependencies)
            {
                PluginInfo pluginInfo;
                if (!Chainloader.PluginInfos.TryGetValue(dependency.DependencyGUID, out pluginInfo))
                {
                    pluginInfo = null;
                }

                foreach (var softDependancy in Plugin._softDependancies)
                {
                    if (dependency.DependencyGUID == softDependancy)
                    {
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }

                // If the plugin isn't found, or the instance isn't enabled, it means the required plugin failed to load
                if (pluginInfo == null || pluginInfo.Instance?.enabled == false)
                {
                    string dependencyName = pluginInfo?.Metadata.Name ?? dependency.DependencyGUID;
                    string dependencyVersion = "";
                    if (dependency.MinimumVersion > noVersion)
                    {
                        dependencyVersion = $" v{dependency.MinimumVersion}";
                    }

                    var errorMessage =
                        $"ERROR: This version of {info.Metadata.Name} v{info.Metadata.Version} depends on {dependencyName}{dependencyVersion}, but it was not loaded.";
                    logger.LogError(errorMessage);
                    Chainloader.DependencyErrors.Add(errorMessage);

                    if (config != null)
                    {
                        // This results in a bogus config entry in the BepInEx config file for the plugin, but it shouldn't hurt anything
                        // We leave the "section" parameter empty so there's no section header drawn
                        config.Bind(
                            "",
                            "MissingDeps",
                            "",
                            new ConfigDescription(
                                errorMessage,
                                null,
                                new ConfigurationManagerAttributes
                                {
                                    CustomDrawer = ErrorLabelDrawer,
                                    ReadOnly = true,
                                    HideDefaultButton = true,
                                    HideSettingName = true,
                                    Category = null,
                                }
                            )
                        );
                    }

                    return false;
                }
            }

            return true;
        }

        private static void ErrorLabelDrawer(ConfigEntryBase entry)
        {
            var styleNormal = new GUIStyle(GUI.skin.label);
            styleNormal.wordWrap = true;
            styleNormal.stretchWidth = true;

            var styleError = new GUIStyle(GUI.skin.label);
            styleError.stretchWidth = true;
            styleError.alignment = TextAnchor.MiddleCenter;
            styleError.normal.textColor = Color.red;
            styleError.fontStyle = FontStyle.Bold;

            // General notice that we're the wrong version
            GUILayout.BeginVertical();
            GUILayout.Label(entry.Description.Description, styleNormal, new GUILayoutOption[] { GUILayout.ExpandWidth(true) });

            // Centered red disabled text
            GUILayout.Label("Plugin has been disabled!", styleError, new GUILayoutOption[] { GUILayout.ExpandWidth(true) });
            GUILayout.EndVertical();
        }

#pragma warning disable 0169, 0414, 0649
        internal sealed class ConfigurationManagerAttributes
        {
            public bool? ShowRangeAsPercent;
            public Action<ConfigEntryBase> CustomDrawer;
            public CustomHotkeyDrawerFunc CustomHotkeyDrawer;
            public delegate void CustomHotkeyDrawerFunc(ConfigEntryBase setting, ref bool isCurrentlyAcceptingInput);
            public bool? Browsable;
            public string Category;
            public object DefaultValue;
            public bool? HideDefaultButton;
            public bool? HideSettingName;
            public string Description;
            public string DispName;
            public int? Order;
            public bool? ReadOnly;
            public bool? IsAdvanced;
            public Func<object, string> ObjToStr;
            public Func<string, object> StrToObj;
        }
    }
}
