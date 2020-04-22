using System;
using UMVC.Architecture.MVC.Interfaces;

namespace UMVC.Architecture.MVC
{
    [Serializable]
    public abstract class BaseModel : IBaseModel
    {
        public virtual void Initialize()
        {
        }
    }
}