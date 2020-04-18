using UMVC.Abstracts;
using UMVC.Interfaces;
using UnityEditor;
using UnityEngine;

namespace UMVC.Windows
{
    public class CreateMVCWindow : Window
    {
        public new static void ShowWindow()
        {
            Instance = CreateInstance<CreateMVCWindow>();
            Window.ShowWindow();
            
            Instance.titleContent = new GUIContent("Create an MVC pattern");
        }
        

        private void OnGUI()
        {
            
            if (GUILayout.Button("Close"))
                Close();
        }
    }
}