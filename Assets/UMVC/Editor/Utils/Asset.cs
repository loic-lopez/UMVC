using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UMVC.Editor.Utils
{
    public class Asset
    {
        /// <summary> will create the asset file if it does not exist and add the object and save the file</summary>
        public static void AddObjectToAssetFile(Object obj, string assetFile)
        {
            if (!File.Exists(assetFile))
            {
                AssetDatabase.CreateAsset(obj, assetFile);
            }
            else
            {
                AssetDatabase.AddObjectToAsset(obj, assetFile);
                AssetDatabase.ImportAsset(assetFile); // force a save
            }
        }
 
        /// <summary>
        /// will create a new asset of the type, save it and return a reference to it. 
        /// assetFile should be a relative path to the file to store the new asset in
        /// if createNewIfExist then a unique name will be generated if the file allready
        /// exist and the asset will be saved there
        /// </summary>
        public static T CreateAssetIfNotExists<T>(string objectName, string assetFile, bool createNewIfExist = false) where T : ScriptableObject
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
                if (createNewIfExist)
                {
                    obj = ScriptableObject.CreateInstance<T>();
                    obj.name = objectName;
                    string fn = AssetDatabase.GenerateUniqueAssetPath(assetFile);
                    AssetDatabase.CreateAsset(obj, fn);
                }
                else
                {
                    obj = AssetDatabase.LoadAssetAtPath<T>(assetFile);
                }
            }
 
            return obj;
        }
    }
}