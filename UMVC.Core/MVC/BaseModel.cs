using System;
using UMVC.Core.MVC.Interfaces;

namespace UMVC.Core.MVC
{
    [Serializable]
    // MarshalByRefObject are necessary in order to RealProxy be able to work
    public abstract class BaseModel : MarshalByRefObject, IBaseModel
    {
        public virtual void Initialize()
        {
        }

       
    }
}