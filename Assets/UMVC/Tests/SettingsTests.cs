using NUnit.Framework;
using UMVC.Editor.Models;
using System;
using UMVC.Tests.Extensions;


namespace UMVC.Tests
{
    public class SettingsTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestIfSettingsAreSuccessfullySerialized()
        {
            var settings = SettingsModel.Initialize("Assets/UMVC/Tests/Settings.asset");
            
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
