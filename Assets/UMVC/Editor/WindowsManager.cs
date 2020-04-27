using UMVC.Editor.Interfaces;
using UMVC.Editor.Windows;
using UnityEditor;

namespace UMVC.Editor
{
    public static class WindowsManager
    {
        private static void InstantiateWindow<T>() where T : IWindow
        {
            var window = (IWindow) EditorWindow.GetWindow(typeof(T));
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
    }
}