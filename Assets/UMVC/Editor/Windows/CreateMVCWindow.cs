using System;
using System.IO;
using System.Reflection;
using UMVC.Abstracts;
using UnityEditor;
using UnityEngine;

namespace UMVC.Windows
{
    public sealed class CreateMVCWindow : Window
    {

        public override void ShowWindow()
        {
            base.ShowWindow();

            var script = MonoScript.FromScriptableObject(this);
            var path = AssetDatabase.GetAssetPath(script);
            Debug.LogWarning(path);

            titleContent = new GUIContent
            {
                text = "Create an MVC pattern",
                image = (Texture) EditorGUIUtility.Load("Assets/Scripts/Editor/video-icon.png")
            };
        }


        protected override void OnGUI()
        {
            base.OnGUI();
        }
    }
}