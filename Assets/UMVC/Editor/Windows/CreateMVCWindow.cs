using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UMVC.Editor.Abstracts;
using UMVC.Editor.Extensions;
using UMVC.Editor.Styles;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Windows
{
    public sealed class CreateMVCWindow : Window
    {
        private const string ModelPrefix = "Model";
        private const string ViewPrefix = "View";
        private const string ControllerPrefix = "Controller";
        private string _componentName;
        private string _generatedControllerName;

        private string _generatedModelName;
        private string _generatedViewName;
        private string _newSubdir;
        private string _outputPath;
        private bool _wantCreateSubDir;
        //private Trie<>


        public override void SetupWindow()
        {
            base.SetupWindow();
            titleContent.text = "UMVC Generator";
        }


        private string _previousComponentName;
        
        protected override void OnGUI()
        {
            if (GUI.changed) return;

            GUILayout.Label("Base Settings", Label.Header);
            _componentName = EditorGUILayout.TextField("New component name", _componentName);

            if (_componentName.IsNotNullOrEmpty() && _previousComponentName != _componentName)
            {
                var componentName = _componentName.Replace("[^A-Za-z0-9]", "");
                var wordList = componentName.Split(' ');
                _componentName = string.Empty;
                foreach (var word in wordList)
                {
                    _componentName += word.Capitalize();
                }
            }

            _previousComponentName = _componentName;

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
            var generatedControllerName =
                _generatedControllerName == ControllerPrefix ? null : _generatedControllerName;


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
            if (_wantCreateSubDir) GUILayout.Label($"New directory: {_newSubdir}");

            GUILayout.Label($"Output directory: {_outputPath}");
        }

        private void UpdateNewSubdir()
        {
            if (_newSubdir.IsNotNullOrEmpty() && _componentName.IsNotNullOrEmpty())
                _newSubdir = _outputPath + $"/{_componentName}";
        }
    }
}