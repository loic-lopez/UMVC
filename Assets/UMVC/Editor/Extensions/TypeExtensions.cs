using System;

namespace UMVC.Editor.Extensions
{
    public static class TypeExtensions
    {
        public static string GetNameWithoutGenerics(this Type t)
        {
            string name = t.Name;
            int index = name.IndexOf('`');
            return index == -1 ? name : name.Substring(0, index);
        }
    }
}