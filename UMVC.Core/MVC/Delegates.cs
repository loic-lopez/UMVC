namespace UMVC.Core.MVC
{
    public static class Delegates
    {
        public delegate void OnFieldUpdate(string field, object newObject, object oldObject);
        public delegate void OnFieldUpdated(string field, object value);
    }
}