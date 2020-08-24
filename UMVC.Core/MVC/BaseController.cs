using System;
using System.ComponentModel;
using System.Reflection;
using UMVC.Core.MVC.Interfaces;

namespace UMVC.Core.MVC
{

    public abstract class BaseController<TModel> where TModel : BaseModel
    {
        protected TModel Model { get; set; }
        public bool IsAlreadySetup { get; protected set; } = false;

        protected IBaseView<TModel> View;
        protected MethodInfo OnFieldWillUpdate;
        protected MethodInfo OnFieldDidUpdate;

        protected const BindingFlags ViewEventsBindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
        protected const string OnFieldWillUpdateViewMemberName = "OnFieldWillUpdate";
        protected const string OnFieldDidUpdateViewMemberName = "OnFieldDidUpdate";

        public virtual void Setup(IBaseView<TModel> view)
        {
            View = view;
            Model = view.GetModel();
            Model.Initialize();
            SubscribeEvents();
            IsAlreadySetup = true;
            var viewType = View.GetType();
            OnFieldWillUpdate = viewType.GetMethod(OnFieldWillUpdateViewMemberName, ViewEventsBindingFlags);
            OnFieldDidUpdate = viewType.GetMethod(OnFieldDidUpdateViewMemberName, ViewEventsBindingFlags);
        }

        public virtual void LateSetup()
        {
        }

        public virtual void Shutdown()
        {
            Model.OnFieldWillUpdate -= RaiseOnFieldWillUpdate;
            Model.OnFieldDidUpdate -= RaiseOnFieldDidUpdate;
            IsAlreadySetup = false;
        }

        protected virtual void SubscribeEvents()
        {
            Model.OnFieldWillUpdate += RaiseOnFieldWillUpdate;
            Model.OnFieldDidUpdate += RaiseOnFieldDidUpdate;
        }

        protected virtual void RaiseOnFieldWillUpdate(object model, object newValue, object oldValue, PropertyChangedEventArgs eventArgs)
        {
            OnFieldWillUpdate?.Invoke(View, new[] { model, newValue, oldValue, eventArgs });
        }
        
        protected virtual void RaiseOnFieldDidUpdate(object model, object newValue, PropertyChangedEventArgs eventArgs)
        {
            OnFieldDidUpdate?.Invoke(View, new[] { model, newValue, eventArgs });
        }
    }
}