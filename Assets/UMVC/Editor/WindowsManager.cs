using UMVC.Editor.Interfaces;
using UMVC.Editor.Windows;
using UnityEditor;

namespace UMVC.Editor
{
    public static class WindowsManager
    {
        private static IWindow InstantiateWindow<T>() where T : IWindow
        {
            var window = (IWindow) EditorWindow.GetWindow(typeof(T));
            window.SetupWindow();

            return window;
        }

        [MenuItem("UMVC/Create an MVC pattern")]
        public static IWindow CreateMVCWindow()
        {
            return InstantiateWindow<CreateMVCWindow>();
        }

        [MenuItem("UMVC/Settings")]
        public static IWindow CreateSettingsWindow()
        {
            return InstantiateWindow<CreateSettingsWindow>();
        }
    }
}