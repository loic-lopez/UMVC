using System.IO;
using UMVC.Editor.Extensions;
using UMVC.Editor.Models;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Singleton
{
    public class UMVC : ScriptableObject
    {
        private const string SettingsFolder = "Settings";

        protected UMVC()
        {
        }

        protected string RootPath { get; set; }
        protected string RelativePath { get; set; }

        public SettingsModel Settings { get; set; }


        public string LogoPath => $"{RelativePath}/{Settings.spritesDirectory}/{Settings.logo}";

        public void UpdateSettingsModel()
        {
            Settings.Save(out var updatedModel);
            Settings = updatedModel;
        }

        #region Static

        protected static UMVC _instance;

        public static UMVC Instance
        {
            get
            {
                if (_instance == null) SetupInstance<UMVC>();
                return _instance;
            }
        }

        protected static void SetupInstance<T>() where T : UMVC
        {
            _instance = CreateInstance<T>();
            var script = MonoScript.FromScriptableObject(_instance);
            var currentScriptPath = AssetDatabase.GetAssetPath(script);
            if (currentScriptPath.IsNullOrEmpty())
            {
                _instance.RootPath = Path.GetFullPath(Application.dataPath + "/UMVC/Editor");
            }
            else
            {
                var currentDirectory = Path.GetDirectoryName(currentScriptPath);
                _instance.RootPath = Path.GetFullPath(currentDirectory + "/../");
            }

            _instance.RelativePath = "Assets" + $"{_instance.RootPath}".Substring(Application.dataPath.Length);

            var settingsAssetPath = $"{_instance.RelativePath}/{SettingsFolder}/SettingsAsset.asset";

            _instance.Settings = SettingsModel.Initialize(settingsAssetPath);
        }

        #endregion
    }
}