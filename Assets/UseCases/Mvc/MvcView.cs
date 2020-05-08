using UMVC.Core.MVC;

namespace UseCases.Mvc 
{
    public class MvcView : BaseView<MvcModel, MvcController>
    {
        public override void OnFieldUpdate(string field, object newObject, object oldObject)
        {
            
        }

        public override void OnFieldUpdated(string field, object value)
        {

        }
    }
}