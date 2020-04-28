using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Styles
{
    public static class Label
    {
        public static readonly GUIStyle Header;

        static Label()
        {
            Header = EditorStyles.boldLabel;
            Header.margin.top = 10;
        }
    }
}