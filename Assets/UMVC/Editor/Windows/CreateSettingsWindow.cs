using UMVC.Editor.Abstracts;
using UMVC.Editor.Extensions;
using UMVC.Editor.Styles;
using UnityEditor;
using UnityEngine;

namespace UMVC.Editor.Windows
{
    public class CreateSettingsWindow : Window
    {
        private string _outputNamespace;
        
        public override void SetupWindow()
        {
            base.SetupWindow();
            titleContent.text = "UMVC Settings";
            _outputNamespace = Singleton.UMVC.Instance.Settings.outputNamespace;
        }

        protected override void OnGUI()
        {
            if (GUI.changed) return; //Returns true if any controls changed the value of the input data.
            
            GUILayout.Label("Settings", Label.Header);
            DisplayOutputNamespace();

            base.OnGUI();
        }

        private void DisplayOutputNamespace()
        {
            var outputNamespace = EditorGUILayout.TextField("Output Namespace", _outputNamespace);

            if (outputNamespace.IsNotNullOrEmpty() && outputNamespace != _outputNamespace)
            {
                _outputNamespace = outputNamespace;
            }
        }

        protected override void DisplayEndButton()
        {
            if (GUILayout.Button("Save", Button.WithMargin))
            {
                Singleton.UMVC.Instance.Settings.outputNamespace = _outputNamespace;
                Singleton.UMVC.Instance.Settings.Save(out var updatedModel);
                Singleton.UMVC.Instance.Settings = updatedModel;
            }
                
        }
    }
}