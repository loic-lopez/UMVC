using System;
using TypeReferences;
using UMVC.Core.Components;
using UMVC.Core.MVC;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    public class UnitySerializableViewComponent : Component
    {
        [Inherits(typeof(BaseView<,>), AllowAbstract = true, IncludeBaseType = true, ShowNoneElement = false,
            ShowAllTypes = true)]
        public TypeReference ClassExtends;
    }
}