using Microsoft.Build.Unity;
using UnityEditor;

namespace Editor.UMVC
{
    public static class BuildDependencies
    {
        [MenuItem("UMVC/Build UMVC.Core")]
        public static void Build()
        {
            MSBuildProjectBuilder.TryBuildAllProjects(MSBuildProjectBuilder.BuildProfileName);
        }
    }
}