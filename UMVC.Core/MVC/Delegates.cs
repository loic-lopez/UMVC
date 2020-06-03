namespace UMVC.Core.MVC
{
    public static class Delegates
    {
        public delegate void OnFieldWillUpdate(string field, object newObject, object oldObject);
        public delegate void OnFieldDidUpdate(string field, object value);
    }
}