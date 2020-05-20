namespace UMVC.Editor.Interfaces
{
    public interface IWindow
    {
        bool IsOpen { get; }
        void SetupWindow();
        string WindowName();
    }
}