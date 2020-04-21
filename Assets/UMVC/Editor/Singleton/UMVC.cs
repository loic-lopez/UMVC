using UnityEditor;
using UnityEngine;
using System;
using UMVC.Models;
using UMVC.Utils;

namespace UMVC.Singleton
{
    public class UMVC : ScriptableObject
    {
        private UMVC() { }

        private const string SettingsFolder = "Settings";

        private string RootPath { get; set; }
        private string RelativePath { get; set; }

        private SettingsModel SettingsModel { get; set; }


        public string LogoPath => $"{RelativePath}/{SettingsModel.spritesDirectory}/{SettingsModel.logo}";
        
        
        #region Static

        private static UMVC _instance;

        public static UMVC Instance
        {
            get
            {
                if (_instance == null)
                {
                    SetupInstance();
                }
                return _instance;
            }
        }

        private static void SetupInstance()
        {
            _instance = CreateInstance<UMVC>();
            var script = MonoScript.FromScriptableObject(_instance);
            var currentScriptPath = AssetDatabase.GetAssetPath(script);

            var currentDirectory = System.IO.Path.GetDirectoryName(currentScriptPath);
            _instance.RootPath = System.IO.Path.GetFullPath(currentDirectory + "/../");
            _instance.RelativePath = "Assets" + $"{_instance.RootPath}".Substring(Application.dataPath.Length);

            var settingsAssetPath = $"{_instance.RelativePath}/{SettingsFolder}/SettingsAsset.asset";
            
            _instance.SettingsModel = Asset.CreateAssetIfNotExists<SettingsModel>("Settings", settingsAssetPath);
        }
        
        #endregion
        
       
    }
}