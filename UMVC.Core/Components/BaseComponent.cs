using System;

namespace UMVC.Core.Components
{
    [Serializable]
    public abstract class BaseComponent
    {
        public string Extends;
        public string BaseNamespace;
    }
}