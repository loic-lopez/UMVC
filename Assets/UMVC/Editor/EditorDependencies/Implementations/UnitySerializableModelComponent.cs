using System;
using System.Collections.Generic;
using UMVC.Core.Components;
using UMVC.Editor.CustomPropertyDrawers.TypeReferences;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    public class UnitySerializableModelComponent : ModelComponent
    {
        public List<UnitySerializableClassField> ClassFields;
    }
}