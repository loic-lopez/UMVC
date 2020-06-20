using System;
using System.Collections.Generic;
using UMVC.Core.Components;
using UMVC.Core.MVC;
using UMVC.Editor.CustomPropertyDrawers.TypeReferences;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    public class UnitySerializableModelComponent : ModelComponent
    {
        public List<UnitySerializableClassField> ClassFields;

        [ExtendsAttribute(typeof(BaseModel), Grouping = ClassGrouping.ByAddComponentMenu, AllowAbstract = true)]
        public TypeReference ClassExtends;

        public void CompileToSystemType()
        {
            foreach (var classField in ClassFields)
                Fields.Add(new ClassField
                {
                    FieldName = classField.FieldName,
                    FieldType = classField.FieldType.Type
                });
        }
    }
}