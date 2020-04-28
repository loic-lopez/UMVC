using System.Collections.Generic;
using System.IO;
using UMVC.Editor.Models;
using UMVC.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Singleton
{
    public class UMVC : ScriptableObject
    {
        private const string SettingsFolder = "Settings";

        private UMVC()
        {
        }

        private string RootPath { get; set; }
        private string RelativePath { get; set; }

        public SettingsModel Settings { get; set; }


        public string LogoPath => $"{RelativePath}/{Settings.spritesDirectory}/{Settings.logo}";

        public void UpdateSettingsModel()
        {
            Settings.Save(out var updatedModel);
            Settings = updatedModel;
        }

        #region Static

        private static UMVC _instance;

        public static UMVC Instance
        {
            get
            {
                if (_instance == null) SetupInstance();
                return _instance;
            }
        }

        private static void SetupInstance()
        {
            _instance = CreateInstance<UMVC>();
            var script = MonoScript.FromScriptableObject(_instance);
            var currentScriptPath = AssetDatabase.GetAssetPath(script);

            var currentDirectory = Path.GetDirectoryName(currentScriptPath);
            _instance.RootPath = Path.GetFullPath(currentDirectory + "/../");
            _instance.RelativePath = "Assets" + $"{_instance.RootPath}".Substring(Application.dataPath.Length);

            var settingsAssetPath = $"{_instance.RelativePath}/{SettingsFolder}/SettingsAsset.asset";

            _instance.Settings = SettingsModel.Initialize(settingsAssetPath);
        }

        #endregion
    }
}