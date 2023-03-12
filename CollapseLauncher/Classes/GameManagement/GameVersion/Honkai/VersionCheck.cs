﻿using Hi3Helper.EncTool.Parser.AssetMetadata;
using Hi3Helper.Preset;
using Hi3Helper.Shared.ClassStruct;
using Microsoft.UI.Xaml;
using System.Collections;
using System.IO;

namespace CollapseLauncher.GameVersioning
{
    internal class GameTypeHonkaiVersion : GameVersionBase
    {
        #region Properties
        private string GameXMFPath { get => Path.Combine(GameDirPath, $"{Path.GetFileNameWithoutExtension(GamePreset.GameExecutableName)}_Data", "StreamingAssets\\Asb\\pc\\Blocks.xmf"); }
        private DeltaPatchProperty GameDeltaPatchProp { get; init; }
        #endregion

        public GameTypeHonkaiVersion(UIElement parentUIElement, RegionResourceProp gameRegionProp, PresetConfigV2 gamePreset)
            : base(parentUIElement, gameRegionProp, gamePreset)
        {
            // Try check for reinitializing game version from XMF file.
            TryReinitializeGameVersion();

            // Try check and assign for the Game Delta-Patch properties (if any).
            // If there's no Delta-Patch, then set it to null.
            GameDeltaPatchProp = CheckDeltaPatchUpdate(GameDirPath, GamePreset.ProfileName, GameVersionAPI);
        }

        public override bool IsGameHasDeltaPatch() => GameDeltaPatchProp != null;

        public override bool IsGameVersionMatch()
        {
            // In this override, the check will be done twice.
            // Check the version in INI file first, then check the version based on XMF file.
            bool IsBaseGameVersionMatch = base.IsGameVersionMatch();
            bool IsXMFVersionMatches = XMFUtility.CheckIfXMFVersionMatches(GameXMFPath, GameVersionAPI.VersionArrayXMF);

            // Choose either one of them in which one is matches.
            return IsBaseGameVersionMatch || IsXMFVersionMatches;
        }

        public override DeltaPatchProperty GetDeltaPatchInfo() => GameDeltaPatchProp == null ? null : GameDeltaPatchProp;

        private void TryReinitializeGameVersion()
        {
            // Check if the GameVersionInstalled == null (version config doesn't exist)
            // and if the XMF file version matches the version from GameVersionAPI, then reinitialize the version config
            // and save the version config by assigning GameVersionInstalled.
            bool IsXMFVersionMatches = XMFUtility.CheckIfXMFVersionMatches(GameXMFPath, GameVersionAPI.VersionArrayXMF);
            if (GameVersionInstalled == null && IsXMFVersionMatches)
            {
                GameVersionInstalled = GameVersionAPI;
            }
        }

        private DeltaPatchProperty CheckDeltaPatchUpdate(string gamePath, string profileName, GameVersion gameVersion)
        {
            // If GameVersionInstalled doesn't have a value (null). then return null.
            if (!GameVersionInstalled.HasValue) return null;

            // If the game version is matches with the API's version, then go to the next check.
            if (GameVersionInstalled.Value.IsMatch(gameVersion))
            {
                // Sanitation check if the directory doesn't exist, then return null.
                if (!Directory.Exists(gamePath)) return null;

                // Iterate the possible path
                IEnumerable PossiblePaths = Directory.EnumerateFiles(gamePath, $"{profileName}*.patch", SearchOption.TopDirectoryOnly);
                foreach (string path in PossiblePaths)
                {
                    // Initialize patchProperty for versioning check.
                    DeltaPatchProperty patchProperty = new DeltaPatchProperty(path);
                    // Convert TargetVer into GameVersion type.
                    GameVersion targetVer = new GameVersion(patchProperty.TargetVer);
                    // If the version of the game is valid and the profile name matches, then return the property.
                    if (GameVersionInstalled.Value.IsMatch(targetVer) && patchProperty.ProfileName == GamePreset.ProfileName) return patchProperty;
                }
            }

            // If all not passed, then return null.
            return null;
        }
    }
}
