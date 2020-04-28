using System.IO;
using UMVC.Core.Generation;
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
        private string _outputDir;
        private bool _wantCreateSubDir;


        public override void SetupWindow()
        {
            base.SetupWindow();
            titleContent.text = "UMVC Generator";
        }


        protected override void OnGUI()
        {
            if (GUI.changed) return; //Returns true if any controls changed the value of the input data.

            GUILayout.Label("Base Settings", Label.Header);
            var componentName = EditorGUILayout.TextField("New component name", _componentName);

            if (componentName != _componentName)
            {
                var wordList = componentName.Replace("[^A-Za-z0-9]", "").Split(' ');
                _componentName = string.Empty;
                foreach (var word in wordList) _componentName += word.Capitalize();

                UpdateNewSubdir();
            }

            DisplayGenerated();

            DisplayOutputSettings();

            base.OnGUI();
        }

        protected override void DisplayEndButton()
        {
            if (GUILayout.Button("Create", Button.WithMargin) && _componentName.IsNotNullOrEmpty())
            {
                var outputNamespace = GenerateOutputNamespace();
                var outputDir = _wantCreateSubDir ? _newSubdir : _outputDir;
                if (_wantCreateSubDir) Directory.CreateDirectory(outputDir);

                var baseModel = Singleton.UMVC.Instance.Settings.baseModelExtends;
                var baseController = Singleton.UMVC.Instance.Settings.baseControllerExtends;
                var baseView = Singleton.UMVC.Instance.Settings.baseViewExtends;
                Generator.GenerateModel(_generatedModelName, outputNamespace, baseModel, outputDir);
                Generator.GenerateController(
                    _generatedControllerName,
                    _generatedModelName,
                    outputNamespace,
                    baseController,
                    outputDir
                );
                Generator.GenerateView(
                    _generatedViewName,
                    _generatedControllerName,
                    _generatedModelName,
                    outputNamespace,
                    baseView,
                    outputDir
                );

                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog("MVC Generated!", $"Generated to {outputDir}", "Got it!");
            }
        }

        private string GenerateOutputNamespace()
        {
            string outputNamespace;
            if (Singleton.UMVC.Instance.Settings.outputNamespace.IsNotNullOrEmpty())
            {
                outputNamespace = Singleton.UMVC.Instance.Settings.outputNamespace;
            }
            else
            {
                var basePath = Application.dataPath + "/";
                outputNamespace = _wantCreateSubDir ? _newSubdir : _outputDir;
                outputNamespace = outputNamespace.Replace(basePath, "").Replace('/', '.');
            }

            return outputNamespace;
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
                _outputDir = EditorUtility.OpenFolderPanel("Choose an Output directory", Application.dataPath, null);
                UpdateNewSubdir();
            }

            _wantCreateSubDir = GUILayout.Toggle(_wantCreateSubDir, "Create subdirectory");
            if (_wantCreateSubDir) GUILayout.Label($"New directory: {_newSubdir}");

            var outputDir = _wantCreateSubDir ? _newSubdir : _outputDir;
            GUILayout.Label($"Output directory: {outputDir}");
        }

        private void UpdateNewSubdir()
        {
            if (_componentName.IsNotNullOrEmpty())
                _newSubdir = _outputDir + $"/{_componentName}";
        }
    }
}