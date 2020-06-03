using UMVC.Core.MVC;

namespace MyNamespace 
{
    public class TestView : BaseView<Model, TestController>
    {
        public override void OnFieldWillUpdate(string field, object newObject, object oldObject)
        {
            
        }

        public override void OnFieldDidUpdate(string field, object value)
        {
            
        }
    }
}