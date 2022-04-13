using System;
using UMVC.Core.Components;
using TypeReferences;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    public class UnitySerializableClassField : ClassField
    {
        [TypeOptions(Grouping = Grouping.ByAddComponentMenu, ShowNoneElement = false, ShowAllTypes = true)]
        public new TypeReference FieldType;
    }
}