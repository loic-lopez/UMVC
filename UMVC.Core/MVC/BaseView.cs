using UMVC.Core.MVC.Interfaces;
using UnityEngine;

namespace UMVC.Core.MVC
{
    public abstract class BaseView<TModel, TController> : MonoBehaviour, IBaseView<TModel>
        where TModel : BaseModel
        where TController : BaseController<TModel>, new()
    {
        protected readonly TController Controller = new TController();

        public TModel model;

        protected virtual void Awake()
        {
            Controller.Setup(this);
        }

        protected virtual void Start()
        {
            Controller.LateSetup();
        }
        
        public TModel Model() => model;

        public abstract void OnFieldUpdate(string field, object value);
        public abstract void OnFieldUpdated(string field, object value);
    }
}