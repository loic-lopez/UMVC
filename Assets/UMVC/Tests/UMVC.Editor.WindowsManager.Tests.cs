using System.Collections;
using NUnit.Framework;
using UMVC.Editor;
using UMVC.Editor.Interfaces;
using UMVC.Editor.Windows;
using UnityEngine;
using UnityEngine.TestTools;

namespace UMVC.Tests
{
    [TestFixture]
    public class UMVCEditorWindowsManagerTests
    {
        [UnityTest]
        public IEnumerable TestCreateMVCWindow()
        {
            var window = WindowsManager.CreateMVCWindow();
            yield return null;
            Assert.IsNotNull(Object.FindObjectOfType<CreateMVCWindow>());
            Assert.That(window.IsOpen);
        }

        [UnityTest]
        public IEnumerable TestCreateSettingsWindow()
        {
            var window = WindowsManager.CreateSettingsWindow();
            yield return null;
            Assert.IsNotNull(Object.FindObjectOfType<CreateSettingsWindow>());
            Assert.That(window.IsOpen);
        }
    }
}