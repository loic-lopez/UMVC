using UMVC.Core.MVC.Interfaces;

namespace UMVC.Core.MVC
{

    public abstract class BaseController<TModel> where TModel : BaseModel
    {
        protected TModel Model { get; set; }
        protected IBaseView<TModel> View;
        protected ModelProxy<TModel> ModelProxy;

        public virtual void Setup(IBaseView<TModel> view)
        {
            View = view;
            ModelProxy<TModel> modelProxy = ModelProxy<TModel>.Bind(View.GetModel());
            Model = modelProxy.GetTransparentProxy();
            Model.Initialize();
            ModelProxy = modelProxy;
            SubscribeEvents();
        }

        public virtual void LateSetup()
        {
        }

        protected virtual void SubscribeEvents()
        {
            ModelProxy.OnFieldUpdate += View.OnFieldUpdate;
            ModelProxy.OnFieldUpdated += View.OnFieldUpdated;
        }


    }
}