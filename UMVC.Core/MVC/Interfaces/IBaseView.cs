namespace UMVC.Core.MVC.Interfaces
{
    public interface IBaseView<out TModel> where TModel : BaseModel
    {
        TModel GetModel();
        void OnFieldWillUpdate(string field, object newObject, object oldObject);
        void OnFieldDidUpdate(string field, object value);
    }
}