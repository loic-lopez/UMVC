using System;
using System.Reflection;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace UMVC.Builder
{
    public static class SonarCloudBuilder
    {
        [SuppressMessage("ReSharper", "PossibleNullReferenceException", Justification = "Having to use reflection because of internal types.")]
        private static void SyncSolution() {
            Type
                .GetType("Packages.Rider.Editor.RiderScriptEditor, Unity.Rider.Editor")
                .GetMethod("SyncSolution", BindingFlags.Static | BindingFlags.Public)
                .Invoke(null, Array.Empty<object>());

        }
    }
}