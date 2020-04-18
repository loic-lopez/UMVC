using System;
using UnityEditor;
using UnityEngine;

namespace UMVC.Abstracts
{
    public abstract class Window : EditorWindow
    {
        protected static EditorWindow Instance;

        protected static void ShowWindow(Type typeToCreate)
        {
            Instance = (EditorWindow) CreateInstance(typeToCreate);
            Instance.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
            Instance.ShowUtility();
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }
    }
}