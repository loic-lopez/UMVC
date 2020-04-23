using System;
using UMVC.Core.MVC.Interfaces;

namespace UMVC.Core.MVC
{
    [Serializable]
    public abstract class BaseModel : IBaseModel
    {
        public virtual void Initialize()
        {
        }
    }
}