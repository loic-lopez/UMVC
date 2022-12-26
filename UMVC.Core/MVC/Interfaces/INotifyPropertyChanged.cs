namespace UMVC.Core.MVC.Interfaces
{
    public interface INotifyPropertyChanged
    {
        event Delegates.OnFieldWillUpdate OnFieldWillUpdate;
        event Delegates.OnFieldDidUpdate OnFieldDidUpdate;
    }
}