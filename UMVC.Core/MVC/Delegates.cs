using System.ComponentModel;
using UMVC.Core.MVC.Interfaces;

namespace UMVC.Core.MVC
{
    public static class Delegates
    {
        public delegate void OnFieldWillUpdate(object model, object newValue, object oldValue, PropertyChangedEventArgs eventArgs);
        public delegate void OnFieldDidUpdate(object model, object newValue, PropertyChangedEventArgs eventArgs);
    }
}