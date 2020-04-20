using System;
using UMVC.Extensions;
using UnityEditor;
using UnityEngine;

namespace UMVC.Abstracts
{
    public abstract class Window : EditorWindow
    {

        public virtual void ShowWindow()
        {
            position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
            ShowUtility();
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