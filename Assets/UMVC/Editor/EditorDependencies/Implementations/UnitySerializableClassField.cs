using System;
using UMVC.Core.Components;
using UMVC.Editor.CustomPropertyDrawers.TypeReferences;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    public class UnitySerializableClassField : ClassField
    {
        [AllowEnumsClassesInterfaces(Grouping = ClassGrouping.ByAddComponentMenu)]
        public new TypeReference FieldType;
    }
}