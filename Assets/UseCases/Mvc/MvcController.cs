using UMVC.Core.MVC;
using UnityEngine;

namespace UseCases.Mvc
{
    public class MvcController : BaseController<MvcModel>
    {
        public void UpdateValue()
        {
            Model.Value = Random.Range(0, 4242);
            Debug.Log("UpdateValue called with value: " + Model.Value);
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = new Vector3(0, 0.5f, 0);
        }
    }
}