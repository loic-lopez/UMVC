namespace UMVC.Core.MVC.Interfaces
{
    public interface IBaseView<out TModel> where TModel : BaseModel
    {
        TModel GetModel();
    }
}