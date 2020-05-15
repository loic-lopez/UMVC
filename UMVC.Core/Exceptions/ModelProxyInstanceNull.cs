using System;

namespace UMVC.Core.Exceptions
{
    [Serializable]
    public class ModelProxyInstanceNull : Exception
    {
        public ModelProxyInstanceNull(Type model) : base($"{model} in ModelProxy cannot be null!")
        {
        }
    }
}