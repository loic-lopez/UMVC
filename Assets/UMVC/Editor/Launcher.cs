using UMVC.Abstracts;
using UMVC.Interfaces;
using UMVC.Windows;
using UnityEditor;
using UnityEngine;

namespace UMVC
{
    public static class Launcher
    {

        /*
        static Launcher()
        {
            Debug.Log("[UMVC] Initialize UMVC");
            Singleton.UMVC.SetupInstance();
            Debug.Log("[UMVC] Initialized UMVC");
        }
        */
        
        private static void InstantiateWindow<T>() where T : IWindow
        {
            IWindow window = (IWindow) EditorWindow.GetWindow(typeof(T));
            window.SetupWindow();
        }
        
        [MenuItem("UMVC/Create an MVC pattern")]
        private static void CreateMVCWindow()
        {
            InstantiateWindow<CreateMVCWindow>();
        }

        [MenuItem("UMVC/Create a Model")]
        private static void CreateModelWindow()
        {
            InstantiateWindow<CreateModelWindow>();
        }
        
        [MenuItem("UMVC/Configure UMVC")]
        private static void ShowWindow()
        {
            InstantiateWindow<SettingsWindow>();
        }
    }
}