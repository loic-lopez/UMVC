namespace UMVC.Core.MVC
{
    public static class Delegates
    {
        public delegate void OnFieldUpdate(string field, object value);
        public delegate void OnFieldUpdated(string field, object value);
    }
}