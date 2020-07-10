using System.ComponentModel;

namespace UMVC.Core.MVC.Interfaces
{
    public interface IBaseView<TModel> where TModel : BaseModel
    {
        TModel GetModel();
        void OnFieldWillUpdate(TModel model, object newValue, object oldValue, PropertyChangedEventArgs eventArgs);
        void OnFieldDidUpdate(TModel model, object newValue, PropertyChangedEventArgs eventArgs);
    }
}