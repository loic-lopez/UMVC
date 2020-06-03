using System;
using UMVC.Core.Components;
using UMVC.Core.MVC;
using UMVC.Editor.CustomPropertyDrawers.TypeReferences;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    // Inherit from BaseComponent in order to make the BaseComponent serialized by Unity ()
    public class BaseControllerSettings : BaseComponent
    {
        [Extends(typeof(BaseController<>), Grouping = ClassGrouping.ByAddComponentMenu, AllowAbstract = true)]
        public TypeReference ClassExtends;
    }
}