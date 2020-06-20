using System;
using UMVC.Core.Components;
using UMVC.Core.MVC;
using UMVC.Editor.CustomPropertyDrawers.TypeReferences;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    public class UnitySerializableControllerComponent : Component
    {
        [ExtendsAttribute(typeof(BaseController<>), Grouping = ClassGrouping.ByAddComponentMenu, AllowAbstract = true)]
        public TypeReference ClassExtends;
    }
}