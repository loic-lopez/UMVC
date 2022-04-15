using System;
using UMVC.Core.Components;
using UMVC.Core.MVC;
using TypeReferences;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    // Inherit from BaseComponent in order to make the BaseComponent serialized by Unity ()
    public class BaseViewSettings : BaseComponent
    {
        [Inherits(typeof(BaseView<,>), AllowAbstract = true, IncludeBaseType = true, ShowNoneElement = false, ShowAllTypes = true)]
        public TypeReference ClassExtends;
    }
}