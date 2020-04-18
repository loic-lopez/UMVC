using UMVC.Abstracts;
using UnityEngine;

namespace UMVC.Windows
{
    public class CreateModelWindow : Window
    {
        public static void ShowWindow()
        {
            Window.ShowWindow(typeof(CreateModelWindow));
            
            Instance.titleContent = new GUIContent("Create a Model");
        }
        
        private void OnGUI()
        {
            
            if (GUILayout.Button("Close"))
                Close();
        }
    }
}