using UMVC.Editor.EditorDependencies.Implementations;
using UMVC.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Models
{
    public class SettingsModel : ScriptableObject
    {
        private const string ObjName = "Settings";
        public string logo = "logo.png";

        public BaseModelSettings model;
        public BaseViewSettings view;
        public BaseControllerSettings controller;

        public string outputNamespace;
        public string spritesDirectory = "Sprites";

        private string _settingsAssetPath;

        public static SettingsModel Initialize(string settingsAssetPath)
        {
            var instance = Instanciate(settingsAssetPath);

            instance._settingsAssetPath = settingsAssetPath;
            return instance;
        }

        private static SettingsModel Instanciate(string settingsAssetPath)
        {
            return Asset.CreateAssetIfNotExists<SettingsModel>(ObjName, settingsAssetPath);
        }

        public void Save(out SettingsModel updatedModel)
        {
            Delete();

            var obj = CreateInstance<SettingsModel>();
            obj.name = ObjName;
            obj.outputNamespace = outputNamespace;
            obj.logo = logo;
            obj.spritesDirectory = spritesDirectory;
            obj._settingsAssetPath = _settingsAssetPath;
            obj.model = model;
            obj.view = view;
            obj.controller = controller;

            updatedModel = obj;

            AssetDatabase.CreateAsset(obj, _settingsAssetPath);
        }

        public void Delete()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.DeleteAsset(_settingsAssetPath);
        }
    }
}