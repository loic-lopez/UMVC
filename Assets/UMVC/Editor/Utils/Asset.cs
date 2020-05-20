using System.IO;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Utils
{
    public static class Asset
    {
        public static T CreateAssetIfNotExists<T>(string objectName, string assetFile) where T : ScriptableObject
        {
            T obj;

            if (!File.Exists(assetFile))
            {
                obj = ScriptableObject.CreateInstance<T>();
                obj.name = objectName;
                AssetDatabase.CreateAsset(obj, assetFile);
            }
            else
            {
                obj = AssetDatabase.LoadAssetAtPath<T>(assetFile);
            }

            return obj;
        }
    }
}