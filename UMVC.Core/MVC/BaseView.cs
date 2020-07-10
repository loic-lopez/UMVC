using System.ComponentModel;
using UMVC.Core.MVC.Interfaces;
using UnityEngine;

namespace UMVC.Core.MVC
{
    public abstract class BaseView<TModel, TController> : MonoBehaviour, IBaseView<TModel>
        where TModel : BaseModel
        where TController : BaseController<TModel>, new()
    {
        protected readonly TController Controller = new TController();

        // ReSharper disable once InconsistentNaming
        public TModel Model;
        
        public TModel GetModel() => Model;

        protected virtual void Awake()
        {
            Controller.Setup(this);
        }

        protected virtual void Start()
        {
            Controller.LateSetup();
        }

        protected virtual void OnDisable()
        {
            Controller.Shutdown();
        }

        protected virtual void OnEnable()
        {
            if (!Controller.IsAlreadySetup)
                Controller.Setup(this);
        }

        public virtual void OnFieldWillUpdate(TModel model, object newValue, object oldValue, PropertyChangedEventArgs eventArgs)
        {
            
        }

        public virtual void OnFieldDidUpdate(TModel model, object newValue, PropertyChangedEventArgs eventArgs)
        {
            
        }
    }
}