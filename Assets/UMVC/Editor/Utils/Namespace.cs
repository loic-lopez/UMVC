using System.Text.RegularExpressions;
using UMVC.Editor.Extensions;
using UnityEngine;

namespace UMVC.Editor.Utils
{
    public static class Namespace
    {
        public static string GenerateOutputNamespace(bool wantCreateSubDir, string newSubdir, string outputDir)
        {
            string outputNamespace;
            if (Singleton.UMVC.Instance.Settings.outputNamespace.IsNotNullOrEmpty())
            {
                outputNamespace = Singleton.UMVC.Instance.Settings.outputNamespace;
            }
            else
            {
                var basePath = Application.dataPath + "/";
                outputNamespace = wantCreateSubDir ? newSubdir : outputDir;
                basePath = outputNamespace?.Replace(basePath, "");

                outputNamespace = basePath == Application.dataPath
                    ? Application.productName
                    : basePath?.Replace('/', '.');
            }

            outputNamespace = outputNamespace?.ToNamespacePascalCase();
            
            return outputNamespace;
        }
    }
}