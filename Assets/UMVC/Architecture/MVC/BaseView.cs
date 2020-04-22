using UMVC.Architecture.MVC.Interfaces;
using UnityEngine;

namespace UMVC.Architecture.MVC
{
    public abstract class BaseView<TModel, TController> : MonoBehaviour 
        where TModel : IBaseModel, new()
        where TController : BaseController<TModel>, new()
    {
        protected readonly TController Controller = new TController();

        public TModel Model = new TModel();
        
        protected virtual void Awake()
        {
            Controller.Setup(Model);
        }

        protected virtual void Start()
        {
            Controller.LateSetup();
        }
    }
}