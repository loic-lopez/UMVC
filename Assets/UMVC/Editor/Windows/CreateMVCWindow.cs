using System;
using System.Collections.Generic;
using System.IO;
using UMVC.Core.Components;
using UMVC.Core.Generation.Generator;
using UMVC.Core.Generation.GeneratorParameters;
using UMVC.Editor.Abstracts;
using UMVC.Editor.CustomPropertyDrawers.TypeReferences;
using UMVC.Editor.EditorDependencies.Implementations;
using UMVC.Editor.Extensions;
using UMVC.Editor.Styles;
using UMVC.Editor.Utils;
using UnityEditor;
using UnityEngine;
using Component = UMVC.Core.Components.Component;

namespace UMVC.Editor.Windows
{
    public sealed class CreateMVCWindow : Window
    {
        private const string ModelPrefix = "Model";
        private const string ViewPrefix = "View";
        private const string ControllerPrefix = "Controller";

        private string _componentName;
        private string _newSubdir;
        private string _outputDir;
        private bool _wantCreateSubDir;
        
        private SerializedObject _serializedGameObject;

        private SerializedProperty _serializedController;
        private SerializedProperty _serializedModel;
        private SerializedProperty _serializedView;

        [SerializeField] private Component controller;
        [SerializeField] private Component view;
        [SerializeField] private UnitySerializableModelComponent model;

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
                Extends = baseModelSettings.Extends,
                ClassFields = new List<UnitySerializableClassField>()
            };
            controller = new Component
            {
                BaseNamespace = baseControllerSettings.BaseNamespace,
                Extends = baseControllerSettings.Extends, 
            };
            view = new Component
            {
                BaseNamespace = baseViewSettings.BaseNamespace,
                Extends = baseViewSettings.Extends,
            };
        }


        protected override void OnGUI()
        {
            if (GUI.changed) return; //Returns true if any controls changed the value of the input data.

            GUILayout.Label("Base Settings", Label.Header);
            var componentName = EditorGUILayout.TextField("New component name", _componentName);

            if (componentName != _componentName)
            {
                _componentName = componentName.ToPascalCase();

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
                var outputNamespace = Namespace.GenerateOutputNamespace(_wantCreateSubDir, _newSubdir, _outputDir);
                var outputDir = _wantCreateSubDir ? _newSubdir : _outputDir;
                if (_wantCreateSubDir) Directory.CreateDirectory(outputDir);

               

                Generator.GenerateMVC(
                    new GeneratorParameters.Builder()
                        .WithView(view)
                        .WithController(controller)
                        //.WithModel(model)
                        .WithNamespaceName(outputNamespace)
                        .WithOutputDir(outputDir)
                        .Build()
                );

                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog("MVC Generated!", $"Generated to {outputDir}", "Got it!");
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
        }

        private void UpdateNewSubdir()
        {
            if (_componentName.IsNotNullOrEmpty())
                _newSubdir = _outputDir + $"/{_componentName}";
        }
    }
}