using System;

namespace UMVC.Core.Components
{
    [Serializable]
    public abstract class BaseComponent
    {
        protected string extends;
        
        public string Extends 
        { 
            get => extends;
            set => extends = value;
        }
        
        public string BaseNamespace;
    }
}