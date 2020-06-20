using System;

namespace UMVC.Editor.Extensions
{
    public static class TypeExtensions
    {
        public static string GetNameWithoutGenerics(this Type t)
        {
            var name = t.Name;
            var index = name.IndexOf('`');
            return index == -1 ? name : name.Substring(0, index);
        }
    }
}