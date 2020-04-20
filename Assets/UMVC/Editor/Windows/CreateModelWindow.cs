using UMVC.Abstracts;
using UnityEngine;

namespace UMVC.Windows
{
    public class CreateModelWindow : Window
    {
        public override void ShowWindow()
        {
            base.ShowWindow();
            
            titleContent = new GUIContent("Create a Model");
        }
        
        protected override void OnGUI()
        {
            base.OnGUI();
        }
    }
}