using UMVC.Abstracts;
using UMVC.Windows;
using UnityEditor;
using UnityEngine;

namespace UMVC
{
    public static class Launcher 
    {
        
        private static void InstantiateWindow<T>() where T : Window
        {
            T window = (T) ScriptableObject.CreateInstance(typeof(T));
            window.ShowWindow();
        }
        
        [MenuItem("UMVC/Create an MVC pattern")]
        public static void CreateMVCWindow()
        {
            InstantiateWindow<CreateMVCWindow>();
        }

        [MenuItem("UMVC/Create a Model")]
        public static void CreateModelWindow()
        {
            InstantiateWindow<CreateModelWindow>();
        }
    }
}