using System;
using UMVC.Core.Components;
using UMVC.Editor.CustomPropertyDrawers.TypeReferences;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    public class UnitySerializableClassField : ClassField
    {
        [AllowPrimitivesEnumsClassesInterfaces(Grouping = ClassGrouping.ByAddComponentMenu)]
        public new TypeReference FieldType;
    }
}