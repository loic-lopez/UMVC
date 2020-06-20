using System;
using UMVC.Editor.Extensions;
using UnityEngine;

namespace UMVC.Editor.CustomPropertyDrawers.TypeReferences
{
    /// <summary>
    ///     Reference to a class <see cref="System.Type" /> with support for Unity serialization.
    /// </summary>
    [Serializable]
    public sealed class TypeReference : ISerializationCallbackReceiver
    {
        [SerializeField] private string _classRef;

        private Type _type;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TypeReference" /> class.
        /// </summary>
        public TypeReference()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TypeReference" /> class.
        /// </summary>
        /// <param name="assemblyQualifiedClassName">Assembly qualified class name.</param>
        public TypeReference(string assemblyQualifiedClassName)
        {
            Type = !string.IsNullOrEmpty(assemblyQualifiedClassName)
                ? Type.GetType(assemblyQualifiedClassName)
                : null;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TypeReference" /> class.
        /// </summary>
        /// <param name="type">Class type.</param>
        /// <exception cref="System.ArgumentException">
        ///     If <paramref name="type" /> is not a class type.
        /// </exception>
        public TypeReference(Type type)
        {
            Type = type;
        }

        /// <summary>
        ///     Gets or sets type of class reference.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        ///     If <paramref name="value" /> is not a class type.
        /// </exception>
        public Type Type
        {
            get => _type;

            set
            {
                if (value == null)
                    throw new ArgumentException(
                        string.Format("'{0}' is null.", value.FullName),
                        "value");

                _type = value;
                _classRef = GetClassRef(value);
            }
        }

        public static string GetClassRef(Type type)
        {
            return type != null
                ? type.FullName + ", " + type.Assembly.GetName().Name
                : string.Empty;
        }

        public void UpdateInternalType(Type type)
        {
            Type = type;
        }

        public static implicit operator string(TypeReference typeReference)
        {
            return typeReference._classRef;
        }

        public static implicit operator Type(TypeReference typeReference)
        {
            return typeReference.Type;
        }

        public static implicit operator TypeReference(Type type)
        {
            return new TypeReference(type);
        }

        public override string ToString()
        {
            return Type.GetNameWithoutGenerics();
        }

        #region ISerializationCallbackReceiver Members

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (!string.IsNullOrEmpty(_classRef))
            {
                _type = Type.GetType(_classRef);

                if (_type == null)
                    Debug.LogWarningFormat("'{0}' was referenced but class type was not found.", _classRef);
            }
            else
            {
                _type = null;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        #endregion
    }
}