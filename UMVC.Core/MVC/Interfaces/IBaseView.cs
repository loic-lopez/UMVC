using System.ComponentModel;

namespace UMVC.Core.MVC.Interfaces
{
    public interface IBaseView<TModel> where TModel : BaseModel
    {
        TModel GetModel();
    }
}