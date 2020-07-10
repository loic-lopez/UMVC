using System;
using UMVC.Core.MVC;
using UnityEngine;

namespace UseCases.Mvc
{
    public class MvcView : BaseView<MvcModel, MvcController>
    {
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                Controller.UpdateValue();
            }
        }
    }
}