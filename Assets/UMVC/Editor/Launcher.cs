using UnityEditor;

namespace UMVC
{
    public static class Launcher 
    {
        [MenuItem("UMVC/Create an MVC pattern")]
        public static void CreateMVCWindow()
        {
            Windows.CreateMVCWindow.ShowWindow();
        }
    }
}