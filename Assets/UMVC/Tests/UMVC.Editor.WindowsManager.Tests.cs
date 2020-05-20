using System.Collections;
using NUnit.Framework;
using UMVC.Editor;
using UMVC.Editor.Interfaces;
using UMVC.Editor.Windows;
using UnityEngine;
using UnityEngine.TestTools;

namespace UMVC.Tests
{
    public class DerivedWindowsManager : WindowsManager
    {
        public new static IWindow CreateSettingsWindow()
        {
            return WindowsManager.CreateSettingsWindow();
        }

        public new static IWindow CreateMVCWindow()
        {
            return WindowsManager.CreateMVCWindow();
        }
    }


    [TestFixture]
    public class UMVCEditorWindowsManagerTests
    {
        [UnityTest]
        public IEnumerable TestCreateMVCWindow()
        {
            var window = DerivedWindowsManager.CreateMVCWindow();
            yield return null;
            Assert.IsNotNull(Object.FindObjectOfType<CreateMVCWindow>());
            Assert.That(window.IsOpen);
        }

        [UnityTest]
        public IEnumerable TestCreateSettingsWindow()
        {
            var window = DerivedWindowsManager.CreateSettingsWindow();
            yield return null;
            Assert.IsNotNull(Object.FindObjectOfType<CreateSettingsWindow>());
            Assert.That(window.IsOpen);
        }
    }
}