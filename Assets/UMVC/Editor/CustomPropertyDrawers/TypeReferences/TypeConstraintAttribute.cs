using System;
using UnityEngine;

namespace UMVC.Editor.CustomPropertyDrawers.TypeReferences
{
    /// <summary>
    ///     Indicates how selectable classes should be collated in drop-down menu.
    /// </summary>
    public enum ClassGrouping
    {
        /// <summary>
        ///     No grouping, just show type names in a list; for instance, "Some.Nested.Namespace.SpecialClass".
        /// </summary>
        None,

        /// <summary>
        ///     Group classes by namespace and show foldout menus for nested namespaces; for
        ///     instance, "Some > Nested > Namespace > SpecialClass".
        /// </summary>
        ByNamespace,

        /// <summary>
        ///     Group classes by namespace; for instance, "Some.Nested.Namespace > SpecialClass".
        /// </summary>
        ByNamespaceFlat,

        /// <summary>
        ///     Group classes in the same way as Unity does for its component menu. This
        ///     grouping method must only be used for <see cref="MonoBehaviour" /> types.
        /// </summary>
        ByAddComponentMenu
    }

    /// <summary>
    ///     Base class for class selection constraints that can be applied when selecting
    ///     a <see cref="TypeReference" /> with the Unity inspector.
    /// </summary>
    public class TypeConstraintAttribute : PropertyAttribute
    {
        /// <summary>
        ///     Gets or sets grouping of selectable classes. Defaults to <see cref="ClassGrouping.ByNamespaceFlat" />
        ///     unless explicitly specified.
        /// </summary>
        public ClassGrouping Grouping { get; set; } = ClassGrouping.ByNamespaceFlat;

        /// <summary>
        ///     Gets or sets whether abstract classes can be selected from drop-down.
        ///     Defaults to a value of <c>false</c> unless explicitly specified.
        /// </summary>
        public bool AllowAbstract { get; set; } = false;

        /// <summary>
        ///     Determines whether the specified <see cref="Type" /> satisfies filter constraint.
        /// </summary>
        /// <param name="type">Type to test.</param>
        /// <returns>
        ///     A <see cref="bool" /> value indicating if the type specified by <paramref name="type" />
        ///     satisfies this constraint and should thus be selectable.
        /// </returns>
        public virtual bool IsConstraintSatisfied(Type type)
        {
            return AllowAbstract || !type.IsAbstract;
        }
    }

    /// <summary>
    ///     Constraint that allows selection of classes that extend a specific class when
    ///     selecting a <see cref="TypeReference" /> with the Unity inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AllowPrimitivesEnumsClassesInterfaces : TypeConstraintAttribute
    {
        /// <summary>
        ///     Determines whether the specified <see cref="Type" /> satisfies filter constraint.
        /// </summary>
        /// <param name="type">Type to test.</param>
        /// <returns>
        ///     A <see cref="bool" /> value indicating if the type specified by <paramref name="type" />
        ///     satisfies this constraint and should thus be selectable.
        /// </returns>
        public override bool IsConstraintSatisfied(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) == null && type.IsAbstract && type.IsSealed) // is a static class
                return false;

            return type.IsAbstract || type.IsClass || type.IsInterface || type.IsEnum || type.IsPrimitive;
        }
    }

    /// <summary>
    ///     Constraint that allows selection of classes that extend a specific class when
    ///     selecting a <see cref="TypeReference" /> with the Unity inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ExtendsAttribute : TypeConstraintAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExtendsAttribute" /> class.
        /// </summary>
        public ExtendsAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExtendsAttribute" /> class.
        /// </summary>
        /// <param name="baseType">Type of class that selectable classes must derive from.</param>
        public ExtendsAttribute(Type baseType)
        {
            BaseType = baseType;
        }

        /// <summary>
        ///     Gets the type of class that selectable classes must derive from.
        /// </summary>
        public Type BaseType { get; }

        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur) return true;
                toCheck = toCheck.BaseType;
            }

            return false;
        }

        /// <inheritdoc />
        public override bool IsConstraintSatisfied(Type type)
        {
            return base.IsConstraintSatisfied(type) && BaseType.IsAssignableFrom(type)
                   || base.IsConstraintSatisfied(type) && IsSubclassOfRawGeneric(BaseType, type);
        }
    }

    /// <summary>
    ///     Constraint that allows selection of classes that implement a specific interface
    ///     when selecting a <see cref="TypeReference" /> with the Unity inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ImplementsAttribute : TypeConstraintAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ImplementsAttribute" /> class.
        /// </summary>
        public ImplementsAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImplementsAttribute" /> class.
        /// </summary>
        /// <param name="interfaceType">Type of interface that selectable classes must implement.</param>
        public ImplementsAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }

        /// <summary>
        ///     Gets the type of interface that selectable classes must implement.
        /// </summary>
        public Type InterfaceType { get; }

        /// <inheritdoc />
        public override bool IsConstraintSatisfied(Type type)
        {
            if (base.IsConstraintSatisfied(type))
                foreach (var interfaceType in type.GetInterfaces())
                    if (interfaceType == InterfaceType)
                        return true;

            return false;
        }
    }
}