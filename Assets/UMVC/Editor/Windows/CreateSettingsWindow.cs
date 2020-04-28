using UMVC.Editor.Abstracts;
using UMVC.Editor.Styles;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Windows
{
    public class CreateSettingsWindow : Window
    {
        private string _baseControllerExtends;
        private string _baseModelExtends;
        private string _baseViewExtends;
        private string _outputNamespace;

        public override void SetupWindow()
        {
            base.SetupWindow();
            titleContent.text = "UMVC Settings";
            _outputNamespace = Singleton.UMVC.Instance.Settings.outputNamespace;
            _baseModelExtends = Singleton.UMVC.Instance.Settings.baseModelExtends;
            _baseControllerExtends = Singleton.UMVC.Instance.Settings.baseControllerExtends;
            _baseViewExtends = Singleton.UMVC.Instance.Settings.baseViewExtends;
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
            var baseModelExtends = EditorGUILayout.TextField("Base model extends", _baseModelExtends);
            if (baseModelExtends != _baseModelExtends) _baseModelExtends = baseModelExtends;

            var baseControllerExtends = EditorGUILayout.TextField("Base controller extends", _baseControllerExtends);
            if (baseControllerExtends != _baseControllerExtends) _baseControllerExtends = baseControllerExtends;

            var baseViewExtends = EditorGUILayout.TextField("Base view extends", _baseViewExtends);
            if (baseViewExtends != _baseViewExtends) _baseViewExtends = baseViewExtends;
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
                Singleton.UMVC.Instance.Settings.baseModelExtends = _baseModelExtends;
                Singleton.UMVC.Instance.Settings.baseControllerExtends = _baseControllerExtends;
                Singleton.UMVC.Instance.Settings.baseViewExtends = _baseViewExtends;
                Singleton.UMVC.Instance.UpdateSettingsModel();
            }
        }
    }
}