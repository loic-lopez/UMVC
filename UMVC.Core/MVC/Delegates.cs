using System.ComponentModel;

namespace UMVC.Core.MVC
{
    public static class Delegates
    {
        public delegate void OnFieldDidUpdate(object model, object newValue, PropertyChangedEventArgs eventArgs);

        public delegate void OnFieldWillUpdate(object model, object newValue, object oldValue,
            PropertyChangedEventArgs eventArgs);
    }
}