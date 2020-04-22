using UMVC.Editor.Abstracts;
using UMVC.Editor.Styles;
using UnityEditor;
using UnityEngine;
using Microsoft.VisualStudio.TextTemplating;

namespace UMVC.Editor.Windows
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

            var generatedModelName = _generatedModelName == ModelPrefix ? null : _generatedModelName;
            var generatedViewName = _generatedViewName == ViewPrefix ? null : _generatedViewName;
            var generatedControllerName = _generatedControllerName == ControllerPrefix ? null : _generatedControllerName;
            
            GUILayout.Label($"Generated Model: {generatedModelName}");
            GUILayout.Label($"Generated View: {generatedViewName}");
            GUILayout.Label($"Generated Model: {generatedControllerName}");

            base.OnGUI();
        }
    }
}