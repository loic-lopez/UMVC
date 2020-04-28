using System;

namespace UMVC.Editor.Extensions
{
    public static class Extensions
    {
        /// <summary>
        ///     Gets the declared type of the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     A <see cref="Type" /> object representing type
        ///     <typeparamref name="T" />; i.e., the type of <paramref name="obj" />
        ///     as it was declared. Note that the contents of
        ///     <paramref name="obj" /> are irrelevant; if <paramref name="obj" />
        ///     contains an object whose class is derived from
        ///     <typeparamref name="T" />, then <typeparamref name="T" /> is
        ///     returned, not the derived type.
        /// </returns>
        public static Type GetDeclaredType<T>(this T obj)
        {
            return typeof(T);
        }
    }
}