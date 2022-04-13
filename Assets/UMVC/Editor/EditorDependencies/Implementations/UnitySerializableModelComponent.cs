using System;
using System.Collections.Generic;
using UMVC.Core.Components;
using UMVC.Core.MVC;
using TypeReferences;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    public class UnitySerializableModelComponent : ModelComponent
    {
        public List<UnitySerializableClassField> ClassFields;

        [Inherits(typeof(BaseModel), AllowAbstract = true, IncludeBaseType = true, ShowNoneElement = false, ShowAllTypes = true)]
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