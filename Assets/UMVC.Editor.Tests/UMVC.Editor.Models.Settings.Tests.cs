using NUnit.Framework;
using UMVC.Editor.Models;
using UMVC.Editor.Tests.Extensions;

namespace UMVC.Editor.Tests
{
    [TestFixture]
    public class UMVCEditorModelsSettingsTests
    {
        [Test]
        public void TestIfSettingsAreSuccessfullySerialized()
        {
            var settings = SettingsModel.Initialize("Assets/UMVC.Editor.Tests/Settings.asset");

            Assert.NotNull(settings);

            settings.logo = TestsExtensions.RandomString();
            settings.Save(out var newSettings);

            Assert.NotNull(newSettings);
            Assert.IsTrue(newSettings != settings);
            Assert.IsTrue(newSettings.logo == settings.logo);

            settings.Delete();
        }
    }
}