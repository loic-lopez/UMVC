using UMVC.Core.MVC;

namespace MyNamespace 
{
    public class TestView : BaseView<Model, TestController>
    {
        public override void OnFieldUpdate(string field, object value)
        {
            
        }

        public override void OnFieldUpdated(string field, object value)
        {
            
        }
    }
}