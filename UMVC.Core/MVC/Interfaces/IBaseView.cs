namespace UMVC.Core.MVC.Interfaces
{
    public interface IBaseView<out TModel> where TModel : BaseModel
    {
        TModel Model();
        void OnFieldUpdate(string field, object newObject, object oldObject);
        void OnFieldUpdated(string field, object value);
    }
}