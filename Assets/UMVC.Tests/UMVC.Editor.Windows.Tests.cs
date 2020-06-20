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
            [SetUp]
            public void SetUp()
            {
                _mvcWindow = (CreateMVCWindow) WindowsManager.CreateMVCWindow();
            }

            [TearDown]
            public void TearDown()
            {
                _mvcWindow.Close();
            }

            private CreateMVCWindow _mvcWindow;

            [Test]
            public void TestSetupWindow()
            {
                Assert.AreEqual(_mvcWindow.WindowName(), _mvcWindow.titleContent.text);
                Assert.IsTrue(((IWindow) _mvcWindow).IsOpen);
            }
        }

        [TestFixture]
        public class CreateSettingsWindowTests
        {
            [SetUp]
            public void SetUp()
            {
                _settingsWindow = (CreateSettingsWindow) WindowsManager.CreateSettingsWindow();
            }

            [TearDown]
            public void TearDown()
            {
                _settingsWindow.Close();
            }

            private CreateSettingsWindow _settingsWindow;

            [Test]
            public void TestSetupWindow()
            {
                Assert.AreEqual(_settingsWindow.WindowName(), _settingsWindow.titleContent.text);
                Assert.IsTrue(((IWindow) _settingsWindow).IsOpen);
            }
        }
    }
}