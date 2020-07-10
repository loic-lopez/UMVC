using System.ComponentModel;
using UMVC.Core.MVC.Interfaces;

namespace UMVC.Core.MVC
{

    public abstract class BaseController<TModel> where TModel : BaseModel
    {
        protected TModel Model { get; set; }
        protected IBaseView<TModel> View;
        // protected ModelProxy<TModel> ModelProxy;

        public virtual void Setup(IBaseView<TModel> view)
        {
            View = view;
            //ModelProxy<TModel> modelProxy = ModelProxy<TModel>.Bind(View.GetModel());
            //Model = modelProxy.GetTransparentProxy();
            Model = view.GetModel();
            Model.Initialize();
            SubscribeEvents();
        }

        public virtual void LateSetup()
        {
        }

        public virtual void Shutdown()
        {
            Model.OnFieldWillUpdate += RaiseOnFieldWillUpdate;
            Model.OnFieldDidUpdate += RaiseOnFieldDidUpdate;
        }

        protected virtual void SubscribeEvents()
        {
            Model.OnFieldWillUpdate += RaiseOnFieldWillUpdate;
            Model.OnFieldDidUpdate += RaiseOnFieldDidUpdate;
        }

        protected virtual void RaiseOnFieldWillUpdate(object model, object newValue, object oldValue, PropertyChangedEventArgs eventArgs)
        {
            View.OnFieldWillUpdate((TModel) model, newValue, oldValue, eventArgs);
        }
        
        protected virtual void RaiseOnFieldDidUpdate(object model, object newValue, PropertyChangedEventArgs eventArgs)
        {
            View.OnFieldDidUpdate((TModel) model, newValue, eventArgs);
        }
    }
}