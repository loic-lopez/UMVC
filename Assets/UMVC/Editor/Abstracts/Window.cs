using UMVC.Editor.Interfaces;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Abstracts
{
    public abstract class Window : EditorWindow, IWindow
    {
        public virtual void SetupWindow()
        {
            titleContent.image = (Texture) EditorGUIUtility.Load(Singleton.UMVC.Instance.LogoPath);
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        protected virtual void OnGUI()
        {
            if (GUILayout.Button("Close"))
                Close();
        }
    }
}