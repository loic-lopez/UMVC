using System;
using System.ComponentModel;
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
        
        protected override void OnFieldWillUpdate(MvcModel model, object newValue, object oldValue, PropertyChangedEventArgs eventArgs)
        {
            Debug.Log($"OnFieldWillUpdate called with previous value: {oldValue} and with new value: {newValue}" );
        }

        protected override void OnFieldDidUpdate(MvcModel model, object newValue, PropertyChangedEventArgs eventArgs)
        {
            Debug.Log($"OnFieldDidUpdate called with new value: {newValue}" );
        }
    }
}