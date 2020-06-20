using System.Collections;
using NUnit.Framework;
using UMVC.Editor;
using UMVC.Editor.Abstracts;
using UMVC.Editor.Interfaces;
using UMVC.Editor.Windows;
using UnityEngine.TestTools;

namespace UMVC.Tests
{
    [TestFixture]
    public class UMVCEditorWindowsManagerTests
    {
        [UnityTest]
        public IEnumerator TestCreateMVCWindow()
        {
            var window = (CreateMVCWindow) WindowsManager.CreateMVCWindow();
            yield return null;
            Assert.IsNotNull(Window.Instance);
            Assert.That(((IWindow) window).IsOpen, Is.True);
            window.Close();
        }

        [UnityTest]
        public IEnumerator TestCreateSettingsWindow()
        {
            var window = (CreateSettingsWindow) WindowsManager.CreateSettingsWindow();
            yield return null;
            Assert.IsNotNull(Window.Instance);
            Assert.That(((IWindow) window).IsOpen, Is.True);
            window.Close();
        }
    }
}