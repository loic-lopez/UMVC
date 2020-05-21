using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
public static class UMVCPackageExport
{
    // method must be static
    [MenuItem("UMVC/Export to UnityPackage")]
    public static void Export()
    {
        // configure
        const string root = "UMVC";
        const string exportPath = "./UMVC.Editor.unitypackage";

        var path = Path.Combine(Application.dataPath, root);
        var assets = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
            .Where(x => Path.GetExtension(x) == ".cs")
            .Select(x => "Assets" + x.Replace(Application.dataPath, "").Replace(@"\", "/"))
            .ToArray();

        Debug.Log("Export below files" + Environment.NewLine + string.Join(Environment.NewLine, assets));

        AssetDatabase.ExportPackage(
            assets,
            exportPath,
            ExportPackageOptions.Default
        );

        Debug.Log("Export complete: " + Path.GetFullPath(exportPath));
    }
}