using UMVC.Editor.Abstracts;
using UMVC.Editor.EditorDependencies.Implementations;
using UMVC.Editor.Styles;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Windows
{
    public class CreateSettingsWindow : Window
    {
        private string _outputNamespace;
        private SerializedProperty _serializedController;
        private SerializedObject _serializedGameObject;

        private SerializedProperty _serializedModel;
        private SerializedProperty _serializedView;

        [SerializeField] private BaseComponentSettings controller;

        [SerializeField] private BaseComponentSettings model;

        [SerializeField] private BaseComponentSettings view;

        public override void SetupWindow()
        {
            base.SetupWindow();
            titleContent.text = WindowName();
            _outputNamespace = Singleton.UMVC.Instance.Settings.outputNamespace;
            model = Singleton.UMVC.Instance.Settings.model;
            controller = Singleton.UMVC.Instance.Settings.controller;
            view = Singleton.UMVC.Instance.Settings.view;

            _serializedGameObject = new SerializedObject(this);
            _serializedModel = _serializedGameObject.FindProperty("model");
            _serializedController = _serializedGameObject.FindProperty("controller");
            _serializedView = _serializedGameObject.FindProperty("view");
        }

        protected override void OnGUI()
        {
            GUILayout.Label("Settings", Label.Header);
            DisplayOutputNamespace();

            GUILayout.Label("Base extends settings", Label.Header);
            DisplayBaseExtends();

            base.OnGUI();
        }

        private void DisplayBaseExtends()
        {
            EditorGUILayout.PropertyField(_serializedModel);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_serializedView);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_serializedController);
            _serializedGameObject.ApplyModifiedProperties();
        }

        private void DisplayOutputNamespace()
        {
            var outputNamespace = EditorGUILayout.TextField("Output Namespace", _outputNamespace);

            if (outputNamespace != _outputNamespace) _outputNamespace = outputNamespace;
        }

        protected override void DisplayEndButton()
        {
            EditorGUILayout.Space(2);
            if (GUILayout.Button("Save"))
            {
                Singleton.UMVC.Instance.Settings.outputNamespace = _outputNamespace;
                Singleton.UMVC.Instance.Settings.model = model;
                Singleton.UMVC.Instance.Settings.controller = controller;
                Singleton.UMVC.Instance.Settings.view = view;
                Singleton.UMVC.Instance.UpdateSettingsModel();
            }
        }

        public override string WindowName()
        {
            return "UMVC Settings";
        }
    }
}