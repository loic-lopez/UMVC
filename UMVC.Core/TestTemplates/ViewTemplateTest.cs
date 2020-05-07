using UMVC.Core.MVC;

namespace MyNamespace 
{
    public class TestView : BaseView<Model, TestController>
    {
        public override void OnFieldUpdate(string field, object newObject, object oldObject)
        {
            
        }

        public override void OnFieldUpdated(string field, object value)
        {
            
        }
    }
}