using System;
using TypeReferences;
using UMVC.Core.Components;
using UMVC.Core.MVC;

namespace UMVC.Editor.EditorDependencies.Implementations
{
    [Serializable]
    // Inherit from BaseComponent in order to make the BaseComponent serialized by Unity ()
    public class BaseModelSettings : BaseComponent
    {
        [Inherits(typeof(BaseModel), AllowAbstract = true, IncludeBaseType = true, ShowNoneElement = false, ShowAllTypes = true)]
        public TypeReference ClassExtends;
    }
}