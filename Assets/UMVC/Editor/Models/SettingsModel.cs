using UMVC.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Models
{
    public class SettingsModel : ScriptableObject
    {
        private const string ObjName = "Settings";

        private string _settingsAssetPath;
        public string baseControllerExtends = "BaseController";
        public string baseModelExtends = "BaseModel";
        public string baseViewExtends = "BaseView";
        public string logo = "logo.jpg";

        public string outputNamespace;
        public string spritesDirectory = "Sprites";

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
            AssetDatabase.SaveAssets();
            AssetDatabase.DeleteAsset(_settingsAssetPath);

            var obj = CreateInstance<SettingsModel>();
            obj.name = ObjName;
            obj.outputNamespace = outputNamespace;
            obj.logo = logo;
            obj.spritesDirectory = spritesDirectory;
            obj._settingsAssetPath = _settingsAssetPath;
            obj.baseModelExtends = baseModelExtends;
            obj.baseControllerExtends = baseControllerExtends;
            obj.baseViewExtends = baseViewExtends;

            updatedModel = obj;

            AssetDatabase.CreateAsset(obj, _settingsAssetPath);
        }
    }
}