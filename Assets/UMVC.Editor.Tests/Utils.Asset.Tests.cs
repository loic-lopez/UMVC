using System.IO;
using NUnit.Framework;
using UMVC.Editor.Models;
using UMVC.Editor.Utils;

namespace UMVC.Editor.Tests
{
    [TestFixture]
    public class UtilsAssetTests
    {
        [Test]
        public void TestCreateAssetIfNotExistsWhenAssetDoesExists()
        {
            var file = "Assets/test.asset";

            var obj = Asset.CreateAssetIfNotExists<SettingsModel>("settings", file);

            Assert.That(File.Exists(file), Is.True);
            Assert.NotNull(obj);

            obj = Asset.CreateAssetIfNotExists<SettingsModel>("settings", file);
            Assert.NotNull(obj);
            File.Delete(file);
            File.Delete(file + ".meta");
        }

        [Test]
        public void TestCreateAssetIfNotExistsWhenAssetDoesNotExists()
        {
            var file = "Assets/test.asset";

            var obj = Asset.CreateAssetIfNotExists<SettingsModel>("settings", file);

            Assert.That(File.Exists(file), Is.True);
            Assert.NotNull(obj);
            File.Delete(file);
            File.Delete(file + ".meta");
        }
    }
}