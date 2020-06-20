using UMVC.Editor.Interfaces;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Abstracts
{
    public abstract class Window : EditorWindow, IWindow
    {
        public static Window Instance { get; private set; }

        protected virtual void OnGUI()
        {
            DisplayEndButton();
        }


        private void OnInspectorUpdate()
        {
            Repaint();
        }

        bool IWindow.IsOpen => Instance != null;


        public virtual void SetupWindow()
        {
            Instance = this;
            titleContent.image = (Texture) EditorGUIUtility.Load(Singleton.UMVC.Instance.LogoPath);
        }

        public abstract string WindowName();

        protected abstract void DisplayEndButton();
    }
}