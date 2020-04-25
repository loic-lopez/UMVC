using System.Globalization;
using System.Text.RegularExpressions;
using UMVC.Editor.Abstracts;
using UMVC.Editor.Styles;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Windows
{
    public sealed class CreateMVCWindow : Window
    {
        private string _componentName;
        private string _outputPath;
        private bool _wantCreateSubDir;
        private string _newSubdir;

        private const string ModelPrefix = "Model";
        private const string ViewPrefix = "View";
        private const string ControllerPrefix = "Controller";
        
        private readonly TextInfo _textInfo = new CultureInfo("en-US",false).TextInfo;

        private string _generatedModelName;
        private string _generatedViewName;
        private string _generatedControllerName;


        public override void SetupWindow()
        {
           base.SetupWindow();
           titleContent.text = "UMVC Generator";
        }


        protected override void OnGUI()
        {
            GUILayout.Label("Base Settings", Label.Header);
            _componentName = EditorGUILayout.TextField("New component name", _componentName);
            _componentName = Regex.Replace(_componentName, @"[^a-zA-Z ]", "");
            _componentName = _textInfo.ToTitleCase(_componentName.Replace(" ", ""));
            UpdateNewSubdir();
            
            DisplayGenerated(); 
           
            DisplayOutputSettings();

            base.OnGUI();
        }

        private void DisplayGenerated()
        {
            _generatedModelName = $"{_componentName}{ModelPrefix}";
            _generatedViewName = $"{_componentName}{ViewPrefix}";
            _generatedControllerName = $"{_componentName}{ControllerPrefix}";

            // Output
            GUILayout.Label("Generated", Label.Header);

            var generatedModelName = _generatedModelName == ModelPrefix ? null : _generatedModelName;
            var generatedViewName = _generatedViewName == ViewPrefix ? null : _generatedViewName;
            var generatedControllerName = _generatedControllerName == ControllerPrefix ? null : _generatedControllerName;
            
            
            GUILayout.Label($"Generated Model: {generatedModelName}");
            GUILayout.Label($"Generated View: {generatedViewName}");
            GUILayout.Label($"Generated Model: {generatedControllerName}");

        }

        private void DisplayOutputSettings()
        {
            GUILayout.Label("Output Settings", Label.Header);

            if (GUILayout.Button("Choose an Output directory", Button.WithMargin))
            {
                _outputPath = EditorUtility.OpenFolderPanel("Choose an Output directory", Application.dataPath, null);
                UpdateNewSubdir();
            }
            
            _wantCreateSubDir = GUILayout.Toggle(_wantCreateSubDir, "Create subdirectory");
            if (_wantCreateSubDir)
            {
                GUILayout.Label($"New directory: {_newSubdir}");
            }
            
            GUILayout.Label($"Output directory: {_outputPath}");
        }

        private void UpdateNewSubdir()
        {
            _newSubdir = _outputPath + $"/{_componentName}";
        }
    }
}