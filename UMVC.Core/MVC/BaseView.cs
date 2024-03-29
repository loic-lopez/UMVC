using System.ComponentModel;
using UMVC.Core.MVC.Interfaces;
using UnityEngine;

namespace UMVC.Core.MVC
{
    public abstract class BaseView<TModel, TController> : MonoBehaviour, IBaseView<TModel>
        where TModel : BaseModel
        where TController : BaseController<TModel>, new()
    {
        // ReSharper disable once InconsistentNaming
        public TModel Model;
        protected readonly TController Controller = new();

        protected virtual void Awake()
        {
            Controller.Setup(this);
        }

        protected virtual void Start()
        {
            Controller.LateSetup();
        }

        protected virtual void OnEnable()
        {
            if (!Controller.IsAlreadySetup)
                Controller.Setup(this);
        }

        protected virtual void OnDisable()
        {
            Controller.Shutdown();
        }

        public TModel GetModel()
        {
            return Model;
        }

        protected virtual void OnFieldWillUpdate(TModel model, object newValue, object oldValue,
            PropertyChangedEventArgs eventArgs)
        {
        }

        protected virtual void OnFieldDidUpdate(TModel model, object newValue, PropertyChangedEventArgs eventArgs)
        {
        }
    }
}