using System;
using System.IO;
using System.Reflection;
using UMVC.Abstracts;
using UMVC.Interfaces;
using UnityEditor;
using UnityEngine;

namespace UMVC.Windows
{
    public sealed class CreateMVCWindow : Window
    {

        public override void SetupWindow()
        {
           base.SetupWindow();
           titleContent.text = "Create an MVC pattern";
        }


        protected override void OnGUI()
        {
            base.OnGUI();
        }
    }
}