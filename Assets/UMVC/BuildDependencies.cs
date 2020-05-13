using Microsoft.Build.Unity;
using UnityEditor;

namespace UMVC
{
    public static class BuildDependencies
    {
        [MenuItem("UMVC/Build UMVC.Core")]
        private static void Build()
        {
            MSBuildProjectBuilder.TryBuildAllProjects(MSBuildProjectBuilder.BuildProfileName);
        }

        public static void ExternalCall()
        {
            Build();
        }
    }
}