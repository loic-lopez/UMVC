using NUnit.Framework;
using UMVC.Editor.Tests.Extensions;

namespace UMVC.Editor.Tests
{
    public class StaticDerivedUMVC : Editor.Singleton.UMVC
    {
        public static Editor.Singleton.UMVC GetInternalInstance()
        {
            return _instance;
        }
    }

    public class DerivedUMVC : StaticDerivedUMVC
    {
        public new static DerivedUMVC Instance
        {
            get
            {
                if (_instance == null) SetupInstance<DerivedUMVC>();
                return (DerivedUMVC) _instance;
            }
        }

        public string GetRootPath()
        {
            return RootPath;
        }

        public string GetRelativePath()
        {
            return RelativePath;
        }
    }


    [TestFixture]
    public class SingletonUMVCTests
    {
        [Test]
        public void TestSingletonInitializeField()
        {
            var instance = DerivedUMVC.Instance;
            Assert.NotNull(StaticDerivedUMVC.GetInternalInstance());
            Assert.NotNull(instance.GetRelativePath());
            Assert.NotNull(instance.GetRootPath());
            Assert.NotNull(instance.LogoPath);
        }

        [Test]
        public void TestSingletonSetupInstance()
        {
            // ReSharper disable once UnusedVariable
            var instance = Singleton.UMVC.Instance;
            Assert.NotNull(StaticDerivedUMVC.GetInternalInstance());
        }

        [Test]
        public void TestSingletonUpdateModel()
        {
            var logo = DerivedUMVC.Instance.Settings.logo;
            DerivedUMVC.Instance.Settings.logo = TestsExtensions.RandomString();
            DerivedUMVC.Instance.UpdateSettingsModel();

            var settingsModelLogo = DerivedUMVC.Instance.Settings.logo;

            DerivedUMVC.Instance.Settings.logo = TestsExtensions.RandomString();
            DerivedUMVC.Instance.UpdateSettingsModel();

            Assert.IsTrue(settingsModelLogo != DerivedUMVC.Instance.Settings.logo);

            DerivedUMVC.Instance.Settings.logo = logo;
            DerivedUMVC.Instance.UpdateSettingsModel();
        }
    }
}