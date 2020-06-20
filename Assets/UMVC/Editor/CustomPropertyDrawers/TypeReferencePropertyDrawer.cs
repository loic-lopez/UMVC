using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UMVC.Editor.CustomPropertyDrawers.TypeReferences;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.CustomPropertyDrawers
{
    /// <summary>
    ///     Custom property drawer for <see cref="TypeReference" /> properties.
    /// </summary>
    [CustomPropertyDrawer(typeof(TypeReference))]
    [CustomPropertyDrawer(typeof(TypeConstraintAttribute), true)]
    public sealed class TypeReferencePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorStyles.popup.CalcHeight(GUIContent.none, 0);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var constraintAttribute = attribute as TypeConstraintAttribute ?? new TypeConstraintAttribute();
            DrawTypeSelectionControl(position, property.FindPropertyRelative("_classRef"), label, constraintAttribute);
        }

        #region Type Filtering

        /// <summary>
        ///     Gets or sets a function that returns a collection of types that are
        ///     to be excluded from drop-down. A value of <c>null</c> specifies that
        ///     no types are to be excluded.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This property must be set immediately before presenting a class
        ///         type reference property field using <see cref="EditorGUI.PropertyField" />
        ///         or <see cref="EditorGUILayout.PropertyField" /> since the value of this
        ///         property is reset to <c>null</c> each time the control is drawn.
        ///     </para>
        ///     <para>
        ///         Since filtering makes extensive use of <see cref="ICollection{T}.Contains" />
        ///         it is recommended to use a collection that is optimized for fast
        ///         lookups such as <see cref="HashSet{Type}" /> for better performance.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <para>Exclude a specific type from being selected:</para>
        ///     <code language="csharp"><![CDATA[
        /// private SerializedProperty _someTypeReferenceProperty;
        /// 
        /// public override void OnInspectorGUI()
        /// {
        ///     serializedObject.Update();
        /// 
        ///     TypeReferencePropertyDrawer.ExcludedTypeCollectionGetter = GetExcludedTypeCollection;
        ///     EditorGUILayout.PropertyField(_someTypeReferenceProperty);
        /// 
        ///     serializedObject.ApplyModifiedProperties();
        /// }
        /// 
        /// private ICollection<Type> GetExcludedTypeCollection()
        /// {
        ///     var set = new HashSet<Type>();
        ///     set.Add(typeof(SpecialClassToHideInDropdown));
        ///     return set;
        /// }
        /// ]]></code>
        /// </example>
        public Func<ICollection<Type>> ExcludedTypeCollectionGetter { get; set; }

        private static void FilterTypes(Assembly assembly, TypeConstraintAttribute filter,
            ICollection<Type> excludedTypes, List<Type> output)
        {
            output.AddRange(
                from type in assembly.GetTypes()
                where type.IsVisible
                where filter == null || filter.IsConstraintSatisfied(type)
                where excludedTypes == null || !excludedTypes.Contains(type)
                select type
            );
        }

        private List<Type> GetFilteredTypes(TypeConstraintAttribute filter)
        {
            var types = new List<Type>();

            var excludedTypes = ExcludedTypeCollectionGetter != null ? ExcludedTypeCollectionGetter() : null;

            foreach (var referencedAssembly in AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.FullName.Contains("Tests")))
                FilterTypes(referencedAssembly, filter, excludedTypes, types);

            types.Sort((a, b) => a.FullName.CompareTo(b.FullName));

            return types;
        }

        #endregion

        #region Type Utility

        private static readonly Dictionary<string, Type> s_TypeMap = new Dictionary<string, Type>();

        private static Type ResolveType(string classRef)
        {
            if (!s_TypeMap.TryGetValue(classRef, out var type))
            {
                type = !string.IsNullOrEmpty(classRef) ? Type.GetType(classRef) : null;
                s_TypeMap[classRef] = type;
            }

            return type;
        }

        #endregion

        #region Control Drawing / Event Handling

        private static readonly int s_ControlHint = typeof(TypeReferencePropertyDrawer).GetHashCode();
        private static readonly GUIContent s_TempContent = new GUIContent();
        private static readonly GenericMenu.MenuFunction2 s_OnSelectedTypeName = OnSelectedTypeName;
        private static int s_SelectionControlID;
        private static string s_SelectedClassRef;

        private static void DisplayDropDown(Rect position, List<Type> types, Type selectedType, ClassGrouping grouping)
        {
            var menu = new GenericMenu();

            menu.AddItem(new GUIContent("(None)"), selectedType == null, s_OnSelectedTypeName, null);
            menu.AddSeparator(string.Empty);

            for (var i = 0; i < types.Count; ++i)
            {
                var type = types[i];

                var menuLabel = FormatGroupedTypeName(type, grouping);
                if (string.IsNullOrEmpty(menuLabel)) continue;

                var content = new GUIContent(menuLabel);
                menu.AddItem(content, type == selectedType, s_OnSelectedTypeName, type);
            }

            menu.DropDown(position);
        }

        private static string FormatGroupedTypeName(Type type, ClassGrouping grouping)
        {
            var name = type.FullName;

            switch (grouping)
            {
                default:
                case ClassGrouping.None:
                    return name;

                case ClassGrouping.ByNamespace:
                    return name.Replace('.', '/');

                case ClassGrouping.ByNamespaceFlat:
                    var lastPeriodIndex = name.LastIndexOf('.');
                    if (lastPeriodIndex != -1)
                        name = name.Substring(0, lastPeriodIndex) + "/" + name.Substring(lastPeriodIndex + 1);

                    return name;

                case ClassGrouping.ByAddComponentMenu:
                    var addComponentMenuAttributes = type.GetCustomAttributes(typeof(AddComponentMenu), false);
                    if (addComponentMenuAttributes.Length == 1)
                        return ((AddComponentMenu) addComponentMenuAttributes[0]).componentMenu;

                    return type.FullName.Replace('.', '/');
            }
        }

        private static void OnSelectedTypeName(object userData)
        {
            var selectedType = userData as Type;

            s_SelectedClassRef = TypeReference.GetClassRef(selectedType);
            var typeReferenceUpdatedEvent = EditorGUIUtility.CommandEvent("TypeReferenceUpdated");
            EditorWindow.focusedWindow.SendEvent(typeReferenceUpdatedEvent);
        }

        private string DrawTypeSelectionControl(Rect position, GUIContent label, string classRef,
            TypeConstraintAttribute filter)
        {
            if (label != null && label != GUIContent.none) position = EditorGUI.PrefixLabel(position, label);

            var controlID = GUIUtility.GetControlID(s_ControlHint, FocusType.Keyboard, position);

            var triggerDropDown = false;

            switch (Event.current.GetTypeForControl(controlID))
            {
                case EventType.ExecuteCommand:
                    if (Event.current.commandName == "TypeReferenceUpdated")
                        if (s_SelectionControlID == controlID)
                        {
                            if (classRef != s_SelectedClassRef)
                            {
                                classRef = s_SelectedClassRef;
                                GUI.changed = true;
                            }

                            s_SelectionControlID = 0;
                            s_SelectedClassRef = null;
                        }

                    break;

                case EventType.MouseDown:
                    if (GUI.enabled && position.Contains(Event.current.mousePosition))
                    {
                        GUIUtility.keyboardControl = controlID;
                        triggerDropDown = true;
                        Event.current.Use();
                    }

                    break;

                case EventType.KeyDown:
                    if (GUI.enabled && GUIUtility.keyboardControl == controlID)
                        if (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.Space)
                        {
                            triggerDropDown = true;
                            Event.current.Use();
                        }

                    break;

                case EventType.Repaint:
                    // Remove assembly name from content of popup control.
                    var classRefParts = classRef.Split(',');

                    s_TempContent.text = classRefParts[0].Trim();
                    if (s_TempContent.text == string.Empty)
                        s_TempContent.text = "(None)";
                    else if (ResolveType(classRef) == null) s_TempContent.text += " {Missing}";

                    EditorStyles.popup.Draw(position, s_TempContent, controlID);
                    break;
            }

            if (triggerDropDown)
            {
                s_SelectionControlID = controlID;
                s_SelectedClassRef = classRef;

                var filteredTypes = GetFilteredTypes(filter);
                DisplayDropDown(position, filteredTypes, ResolveType(classRef), filter.Grouping);
            }

            return classRef;
        }

        private void DrawTypeSelectionControl(Rect position, SerializedProperty property, GUIContent label,
            TypeConstraintAttribute filter)
        {
            try
            {
                var restoreShowMixedValue = EditorGUI.showMixedValue;
                EditorGUI.showMixedValue = property.hasMultipleDifferentValues;

                property.stringValue = DrawTypeSelectionControl(position, label, property.stringValue, filter);

                EditorGUI.showMixedValue = restoreShowMixedValue;
            }
            finally
            {
                ExcludedTypeCollectionGetter = null;
            }
        }

        #endregion
    }
}