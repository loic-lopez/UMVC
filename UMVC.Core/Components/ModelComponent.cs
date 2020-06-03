using System;
using System.Collections.Generic;

namespace UMVC.Core.Components
{
    [Serializable]
    public class ModelComponent : Component
    {
        private List<ClassField> _fields;

        public ModelComponent()
        {
            _fields = new List<ClassField>();
        }

        public List<ClassField> Fields
        {
            get => _fields;
            set => _fields = value;
        }
    }
}