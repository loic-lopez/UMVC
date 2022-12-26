using System;
using TypeReferences;
using UMVC.Core.Components;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    public class UnitySerializableClassField : ClassField
    {
        [TypeOptions(Grouping = Grouping.ByAddComponentMenu, ShowNoneElement = false, ShowAllTypes = true)]
        public new TypeReference FieldType;
    }
}