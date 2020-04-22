using UMVC.Core.MVC.Interfaces;

namespace UMVC.Core.MVC
{
    public abstract class BaseController<TModel> where TModel : IBaseModel
    {
        protected TModel Model { get; set; }
        
        /// <param name="model"></param>
        /// <param></param>
        public virtual void Setup(TModel model)
        {
            model.Initialize();
            Model = model;
        }
        
        public virtual void LateSetup()
        {
            
        }
    }
}