using System;
using UMVC.Core.MVC;

namespace UseCases.Mvc
{
    public class MvcView : BaseView<MvcModel, MvcController>
    {
        public override void OnFieldWillUpdate(string field, object newObject, object oldObject)
        {
            throw new NotImplementedException();
        }

        public override void OnFieldDidUpdate(string field, object value)
        {
            throw new NotImplementedException();
        }
    }
}