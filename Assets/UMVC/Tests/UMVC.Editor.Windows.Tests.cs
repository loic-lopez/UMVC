using NUnit.Framework;
using UMVC.Editor;
using UMVC.Editor.Interfaces;
using UMVC.Editor.Windows;

namespace UMVC.Tests
{
    [TestFixture]
    public class UMVCEditorWindowsTests
    {
        [TestFixture]
        public class CreateMVCWindowTests
        {
            private CreateMVCWindow _mvcWindow;

            [SetUp]
            public void SetUp()
            {
                _mvcWindow = (CreateMVCWindow) WindowsManager.CreateMVCWindow();
            }
            
            [Test]
            public void TestSetupWindow()
            {
                Assert.AreEqual(_mvcWindow.WindowName(), _mvcWindow.titleContent.text);
                Assert.IsTrue(((IWindow)_mvcWindow).IsOpen);
            }

            [TearDown]
            public void TearDown()
            {
                _mvcWindow.Close();
            }
        }

        [TestFixture]
        public class CreateSettingsWindowTests
        {
            private CreateSettingsWindow _settingsWindow;
            
            [SetUp]
            public void SetUp()
            {
                _settingsWindow = (CreateSettingsWindow) WindowsManager.CreateSettingsWindow();
            }
            
            [Test]
            public void TestSetupWindow()
            {
                Assert.AreEqual(_settingsWindow.WindowName(), _settingsWindow.titleContent.text);
                Assert.IsTrue(((IWindow)_settingsWindow).IsOpen);
            }

            [TearDown]
            public void TearDown()
            {
                _settingsWindow.Close();
            }
        }
    }
}