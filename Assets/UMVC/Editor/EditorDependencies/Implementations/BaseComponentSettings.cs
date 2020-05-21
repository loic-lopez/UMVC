using System;
using UMVC.Core.Components;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    // Inherit from BaseComponent in order to make the BaseComponent serialized by Unity ()
    public sealed class BaseComponentSettings : BaseComponent
    {
    }
}