using System;
using System.IO;
using System.Reflection;
using UMVC.Abstracts;
using UMVC.Interfaces;
using UMVC.Styles;
using UnityEditor;
using UnityEngine;

namespace UMVC.Windows
{
    public sealed class CreateMVCWindow : Window
    {
        private string _componentName;

        private const string ModelPrefix = "Model";
        private const string ViewPrefix = "View";
        private const string ControllerPrefix = "Controller";

        private string _generatedModelName;
        private string _generatedViewName;
        private string _generatedControllerName;

        public override void SetupWindow()
        {
           base.SetupWindow();
           titleContent.text = "MVC Generator";
        }


        protected override void OnGUI()
        {
            GUILayout.Label("Base Settings", Label.Header);
            _componentName = EditorGUILayout.TextField("New component name", _componentName);
            
            _generatedModelName = $"{_componentName}{ModelPrefix}";
            _generatedViewName = $"{_componentName}{ViewPrefix}";
            _generatedControllerName = $"{_componentName}{ControllerPrefix}";

            // Output
            GUILayout.Label("Output", Label.Header);
            
            GUILayout.Label($"Generated Model: {_generatedModelName}");
            GUILayout.Label($"Generated View: {_generatedViewName}");
            GUILayout.Label($"Generated Model: {_generatedControllerName}");
            
            base.OnGUI();
        }
    }
}