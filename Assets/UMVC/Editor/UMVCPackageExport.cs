using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
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
        File.Delete(
            Path.Combine(Application.dataPath, $"{Root}Editor/EditorDependencies/UMVC.Dependencies.msb4u.csproj.meta")
        );
        File.Delete(Path.Combine(Application.dataPath, $"{Root}PlayerDependencies/UMVC.Dependencies.msb4u.csproj"));
        File.Delete(Path.Combine(Application.dataPath, $"{Root}PlayerDependencies/UMVC.Dependencies.msb4u.csproj.meta"));
        AssetDatabase.Refresh();
        
        Export(ExportWithDllsPath);
    }

    [MenuItem("UMVC Tools/ExportWithMsBuildForUnity")]
    public static void ExportWithMsBuildForUnity()
    {
        Directory.Delete(Path.Combine(Application.dataPath, $"{Root}Editor/EditorDependencies"), true);
        Directory.Delete(Path.Combine(Application.dataPath, $"{Root}PlayerDependencies/"), true);
        Copy($"{Application.dataPath}/../MSBuildForUnity/PlayerDependencies", $"{Application.dataPath}/UMVC/PlayerDependencies");
        Copy($"{Application.dataPath}/../MSBuildForUnity/EditorDependencies", $"{Application.dataPath}/UMVC/Editor/EditorDependencies");
        AssetDatabase.Refresh();

        Export(ExportMsBuildForUnityPath);
    }

    private static void Export(string destFile)
    {
        var path = Path.Combine(Application.dataPath, Root);
        var assets = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
            .Select(x => "Assets" + x.Replace(Application.dataPath, "").Replace(@"\", "/"))
            .ToArray();

        Debug.Log("Export bellow files" + Environment.NewLine + string.Join(Environment.NewLine, assets));

        AssetDatabase.ExportPackage(
            assets,
            destFile,
            ExportPackageOptions.Default
        );

        Debug.Log("Export complete: " + Path.GetFullPath(destFile));
    }
    
    private static void Copy(string sourceDirectory, string targetDirectory)
    {
        var diSource = new DirectoryInfo(sourceDirectory);
        var diTarget = new DirectoryInfo(targetDirectory);
 
        CopyAll(diSource, diTarget);
    }
 
    private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        Directory.CreateDirectory(target.FullName);
 
        // Copy each file into the new directory.
        foreach (FileInfo fi in source.GetFiles())
        {
            Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }
 
        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }
    }
}