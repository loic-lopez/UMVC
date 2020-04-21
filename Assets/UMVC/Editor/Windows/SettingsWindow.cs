using System.IO;
using System.Reflection;
using UMVC.Abstracts;
using UnityEditor;
using UnityEngine;

namespace UMVC.Windows
{
    public class SettingsWindow : Window
    {

        public override void SetupWindow()
        {
            
            titleContent = new GUIContent("TITLE");
        }

        protected override void OnGUI()
        {
            base.OnGUI();
            
            //EditorGUILayout.PropertyField()
        }
    }
}