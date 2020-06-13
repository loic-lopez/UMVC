using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UMVC.Export
{
    public static class UMVCPackageExport
    {
        private const string Root = "UMVC/";
        private const string ExportWithDllsPath = "./UMVC.Editor.PreBuiltDlls.unitypackage";
        private const string ExportMsBuildForUnityPath = "./UMVC.Editor.MsBuildForUnity.unitypackage";

        [MenuItem("UMVC Tools/ExportWithPreBuiltDlls")]
        public static void ExportWithPreBuiltDlls()
        {
            File.Delete(
                Path.Combine(Application.dataPath, $"{Root}Editor/EditorDependencies/UMVC.Dependencies.msb4u.csproj")
            );
            File.Delete(Path.Combine(Application.dataPath, $"{Root}PlayerDependencies/UMVC.Dependencies.msb4u.csproj"));
            AssetDatabase.Refresh();
        
            Export(ExportWithDllsPath);
        }

        [MenuItem("UMVC Tools/ExportWithMsBuildForUnity")]
        public static void ExportWithMsBuildForUnity()
        {
            File.Copy(
                $"{Application.dataPath}/../MSBuildForUnity/EditorDependencies/UMVC.Dependencies.msb4u.csproj", 
                $"{Application.dataPath}/UMVC/Editor/EditorDependencies/UMVC.Dependencies.msb4u.csproj"
                ,true
            );
        
            File.Copy(
                $"{Application.dataPath}/../MSBuildForUnity/PlayerDependencies/UMVC.Dependencies.msb4u.csproj", 
                $"{Application.dataPath}/UMVC/PlayerDependencies/UMVC.Dependencies.msb4u.csproj"
                ,true
            );
            AssetDatabase.Refresh();
        
            Export(ExportMsBuildForUnityPath, true);
        }

        private static void Export(string destFile, bool disableDlls = false)
        {
            var path = Path.Combine(Application.dataPath, Root);
            var assets = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
                .Where(x => !x.Contains("Export"));;

            if (disableDlls)
            {
                assets = assets.Where(x => Path.GetExtension(x) != ".dll" && !x.Contains("net46"));
            }

            assets = assets.Select(x => "Assets" + x.Replace(Application.dataPath, "").Replace(@"\", "/"));

            var assetList = assets.ToList();
            Debug.Log("Export bellow files" + Environment.NewLine + string.Join(Environment.NewLine, assetList));

            AssetDatabase.ExportPackage(
                assetList.ToArray(),
                destFile,
                ExportPackageOptions.Default
            );

            Debug.Log("Export complete: " + Path.GetFullPath(destFile));
        }
    }
}