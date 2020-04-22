using UnityEditor;
using UnityEngine;

namespace UMVC.Styles
{
    public static class Label
    {
        public static readonly GUIStyle Header;

        static Label()
        {
            Header = EditorStyles.boldLabel;
            Header.margin = new RectOffset(0,0,10, 0);
        }
    }
}