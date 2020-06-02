using System;
using System.Collections.Generic;

namespace UMVC.Core.Components
{
    [Serializable]
    public class ModelComponent : Component
    {
        public List<ClassField> Fields;

        public ModelComponent()
        {
            Fields = new List<ClassField>();
        }
    }
}