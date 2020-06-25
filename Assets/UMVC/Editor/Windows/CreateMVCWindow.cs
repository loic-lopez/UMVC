using System.Collections.Generic;
using System.IO;
using UMVC.Core.Generation.Generator;
using UMVC.Core.Generation.GeneratorParameters;
using UMVC.Editor.Abstracts;
using UMVC.Editor.EditorDependencies.Implementations;
using UMVC.Editor.Extensions;
using UMVC.Editor.Styles;
using UMVC.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Windows
{
    public sealed class CreateMVCWindow : Window
    {
        private const string ModelPrefix = "Model";
        private const string ViewPrefix = "View";
        private const string ControllerPrefix = "Controller";

        [SerializeField] private UnitySerializableControllerComponent controller;
        [SerializeField] private UnitySerializableViewComponent view;
        [SerializeField] private UnitySerializableModelComponent model;

        private string _componentName;
        private string _newSubdir;
        private string _outputDir;

        private SerializedProperty _serializedController;

        private SerializedObject _serializedGameObject;
        private SerializedProperty _serializedModel;
        private SerializedProperty _serializedView;
        private bool _wantCreateSubDir;


        protected override void OnGUI()
        {
            if (GUI.changed) return; //Returns true if any controls changed the value of the input data.

            GUILayout.Label("Base Settings", Label.Header);
            var componentName = EditorGUILayout.TextField("New component name", _componentName);

            if (componentName != _componentName)
            {
                _componentName = componentName.ToNamespacePascalCase();

                UpdateNewSubdir();
            }

            DisplayGenerated();

            DisplayOutputSettings();

            base.OnGUI();
        }


        public override void SetupWindow()
        {
            base.SetupWindow();
            titleContent.text = WindowName();

            var baseModelSettings = Singleton.UMVC.Instance.Settings.model;
            var baseControllerSettings = Singleton.UMVC.Instance.Settings.controller;
            var baseViewSettings = Singleton.UMVC.Instance.Settings.view;

            model = new UnitySerializableModelComponent
            {
                BaseNamespace = baseModelSettings.BaseNamespace,
                ClassFields = new List<UnitySerializableClassField>(),
                ClassExtends = baseModelSettings.ClassExtends
            };

            controller = new UnitySerializableControllerComponent
            {
                BaseNamespace = baseControllerSettings.BaseNamespace,
                ClassExtends = baseControllerSettings.ClassExtends
            };
            view = new UnitySerializableViewComponent
            {
                BaseNamespace = baseViewSettings.BaseNamespace,
                ClassExtends = baseViewSettings.ClassExtends
            };
        }

        protected override void DisplayEndButton()
        {
            if (GUILayout.Button("Create", Button.WithMargin))
            {
                if (_componentName.IsNullOrEmpty())
                {
                    EditorUtility.DisplayDialog("UMVC", "New component name cannot be null or empty!", "Got it!");
                    return;
                }

                var outputDir = _wantCreateSubDir ? _newSubdir : _outputDir;

                if (_wantCreateSubDir && Directory.Exists(outputDir)
                    || File.Exists($"{outputDir}/{view.Name}.cs")
                    || File.Exists($"{outputDir}/{model.Name}.cs")
                    || File.Exists($"{outputDir}/{controller.Name}.cs")
                )
                {
                    if (!EditorUtility.DisplayDialog("UMVC", $"Are you sure to overwrite your files in directory: {outputDir}", "Overwrite", "Cancel"))
                    {
                        return;   
                    }
                }


                var outputNamespace = Namespace.GenerateOutputNamespace(_wantCreateSubDir, _newSubdir, _outputDir);

                if (outputNamespace.IsNullOrEmpty())
                {
                    EditorUtility.DisplayDialog("UMVC", "You need to choose an output directory!", "Got it!");
                    return;
                }

                if (_wantCreateSubDir) Directory.CreateDirectory(outputDir);

                model.CompileToSystemType();
                model.Extends = model.ClassExtends.ToString();
                controller.Extends = controller.ClassExtends.ToString();
                view.Extends = view.ClassExtends.ToString();


                Generator.GenerateMVC(
                    new GeneratorParameters.Builder()
                        .WithView(view)
                        .WithController(controller)
                        .WithModel(model)
                        .WithNamespaceName(outputNamespace)
                        .WithOutputDir(outputDir)
                        .Build()
                );

                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog("UMVC", $"Generated to {outputDir}", "Got it!");
            }
        }

        public override string WindowName()
        {
            return "UMVC Generator";
        }

        private void DisplayGenerated()
        {
            model.Name = $"{_componentName}{ModelPrefix}";
            view.Name = $"{_componentName}{ViewPrefix}";
            controller.Name = $"{_componentName}{ControllerPrefix}";

            model.BaseNamespace = model.ClassExtends.Type.Namespace;
            controller.BaseNamespace = controller.ClassExtends.Type.Namespace;
            view.BaseNamespace = view.ClassExtends.Type.Namespace;


            _serializedGameObject = new SerializedObject(this);
            _serializedModel = _serializedGameObject.FindProperty("model");
            _serializedController = _serializedGameObject.FindProperty("controller");
            _serializedView = _serializedGameObject.FindProperty("view");

            // Output
            GUILayout.Label("Generated", Label.Header);
            EditorGUILayout.PropertyField(_serializedModel);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_serializedView);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_serializedController);
            _serializedGameObject.ApplyModifiedProperties();
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

            var outputNamespace = "";

            if (_newSubdir.IsNotNullOrEmpty() || _outputDir.IsNotNullOrEmpty())
                outputNamespace = Namespace.GenerateOutputNamespace(_wantCreateSubDir, _newSubdir, _outputDir);
            GUILayout.Label($"Output namespace: {outputNamespace}");
        }

        private void UpdateNewSubdir()
        {
            if (_componentName.IsNotNullOrEmpty())
                _newSubdir = _outputDir + $"/{_componentName}";
        }
    }
}