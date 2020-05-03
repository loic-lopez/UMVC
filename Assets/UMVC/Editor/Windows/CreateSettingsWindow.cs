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
        
        [SerializeField]
        private BaseComponentSettings model;
        
        [SerializeField]
        private BaseComponentSettings view;
        
        [SerializeField]
        private BaseComponentSettings controller;

        public override void SetupWindow()
        {
            base.SetupWindow();
            titleContent.text = "UMVC Settings";
        }


        private void Awake()
        {
            _outputNamespace = Singleton.UMVC.Instance.Settings.outputNamespace;
            model = Singleton.UMVC.Instance.Settings.model;
            controller = Singleton.UMVC.Instance.Settings.controller;
            view = Singleton.UMVC.Instance.Settings.view;
        }

        protected override void OnGUI()
        {
            if (GUI.changed) return; //Returns true if any controls changed the value of the input data.

            GUILayout.Label("Settings", Label.Header);
            DisplayOutputNamespace();

            GUILayout.Label("Base extends settings", Label.Header);
            DisplayBaseExtends();

            base.OnGUI();
        }

        private void DisplayBaseExtends()
        {
            SerializedObject so = new SerializedObject (this);
            SerializedProperty serializedModel = so.FindProperty("model");
            SerializedProperty serializedController = so.FindProperty("controller");
            SerializedProperty serializedView = so.FindProperty("view");
            EditorGUILayout.PropertyField(serializedModel);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedView);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedController);
            so.ApplyModifiedProperties();
        }

        private void DisplayOutputNamespace()
        {
            var outputNamespace = EditorGUILayout.TextField("Output Namespace", _outputNamespace);

            if (outputNamespace != _outputNamespace) _outputNamespace = outputNamespace;
        }

        protected override void DisplayEndButton()
        {
            if (GUILayout.Button("Save", Button.WithMargin))
            {
                Singleton.UMVC.Instance.Settings.outputNamespace = _outputNamespace;
                Singleton.UMVC.Instance.Settings.model = model;
                Singleton.UMVC.Instance.Settings.controller = controller;
                Singleton.UMVC.Instance.Settings.view = view;
                Singleton.UMVC.Instance.UpdateSettingsModel();
            }
        }
    }
}