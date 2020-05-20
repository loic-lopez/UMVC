using UMVC.Editor.Interfaces;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Abstracts
{
    public abstract class Window : EditorWindow, IWindow
    {
        public static Window Instance { get; private set; }

        bool IWindow.IsOpen => Instance != null;


        public virtual void SetupWindow()
        {
            Instance = this;
            titleContent.image = (Texture) EditorGUIUtility.Load(Singleton.UMVC.Instance.LogoPath);
        }


        private void OnInspectorUpdate()
        {
            Repaint();
        }

        protected virtual void OnGUI()
        {
            DisplayEndButton();
        }

        protected abstract void DisplayEndButton();

        public abstract string WindowName();
    }
}