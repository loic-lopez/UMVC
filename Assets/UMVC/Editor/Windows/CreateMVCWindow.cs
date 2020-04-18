using UMVC.Abstracts;
using UnityEngine;

namespace UMVC.Windows
{
    public class CreateMVCWindow : Window
    {
        public static void ShowWindow()
        {
            Window.ShowWindow(typeof(CreateMVCWindow));
            
            Instance.titleContent = new GUIContent("Create an MVC pattern");
        }
        

        private void OnGUI()
        {
            
            if (GUILayout.Button("Close"))
                Close();
        }
    }
}