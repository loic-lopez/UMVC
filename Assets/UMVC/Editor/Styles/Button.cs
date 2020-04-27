using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Styles
{
    public static class Button
    {
        public static readonly GUIStyle WithMargin;


        static Button()
        {
            WithMargin = EditorStyles.miniButton;
            WithMargin.margin.top = 10;
            WithMargin.margin.bottom = 10;
        }
    }
}