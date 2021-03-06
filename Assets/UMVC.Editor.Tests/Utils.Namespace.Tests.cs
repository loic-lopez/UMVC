using NUnit.Framework;
using UMVC.Editor.Utils;

namespace UMVC.Editor.Tests
{
    [TestFixture]
    public class UtilsNamespaceTests
    {
        private const string BasePath = "UseCases";
        private const string NewSubDir = "/UMVCTest";

        [Test]
        public void TestGenerateOutputNamespaceWithSubDir()
        {
            var generatedNamespace = Namespace.GenerateOutputNamespace(true, BasePath + NewSubDir, BasePath);

            Assert.That(generatedNamespace, Is.EqualTo("UseCases.UMVCTest"));
        }
    }
}