using System.IO;
using NUnit.Framework;
using UMVC.Core.Generation.GeneratorParameters;
using UMVC.Core.MVC;

namespace UMVC.Core.Tests
{
    [TestFixture]
    public class GeneratorTests
    {
        private const string DefaultBaseNamespace = "UMVC.Core.MVC";
        private const string NamespaceName = "TestNamespace";
        private const string ModelName = "TestModel";
        private const string ControllerName = "TestController";
        private const string ViewName = "TestView";


        [Test]
        public void TestIfControllerHasBeenGenerated()
        {
            var currentDir = Directory.GetCurrentDirectory();

            var parameters = new GeneratorParameters.Builder()
                .WithModel(
                    new GeneratorParameters.Component
                    {
                        BaseNamespace = DefaultBaseNamespace,
                        Extends = "BaseModel",
                        Name = ModelName
                    })
                .WithController(
                    new GeneratorParameters.Component
                    {
                        BaseNamespace = DefaultBaseNamespace,
                        Extends = "BaseController",
                        Name = ControllerName
                    })
                .WithNamespaceName(NamespaceName)
                .WithOutputDir(currentDir);

            var generatedObj = TestsUtils.GenerateController(parameters.Build());

            UMVCAssert.HasSameBaseClass(generatedObj.GetType(), typeof(BaseController<>));
            var desiredClass = $"{NamespaceName}.{ControllerName}";
            Assert.IsTrue(generatedObj.GetType().ToString() == desiredClass);
        }

        [Test]
        public void TestIfModelHasBeenGenerated()
        {
            var currentDir = Directory.GetCurrentDirectory();

            var parameters = new GeneratorParameters.Builder()
                .WithModel(
                    new GeneratorParameters.Component
                    {
                        BaseNamespace = DefaultBaseNamespace,
                        Extends = "BaseModel",
                        Name = ModelName
                    })
                .WithNamespaceName(NamespaceName)
                .WithOutputDir(currentDir);

            var generatedObj = TestsUtils.GenerateModel(parameters.Build());

            UMVCAssert.HasSameBaseClass(generatedObj.GetType(), typeof(BaseModel));
            var desiredClass = $"{NamespaceName}.{ModelName}";
            Assert.IsTrue(generatedObj.GetType().ToString() == desiredClass);
        }

        // WONTFIX: We can't call Activator.CreateInstance() because BaseView extends Monobehavior 
        [Test]
        public void TestIfViewHasBeenGenerated()
        {
            var currentDir = Directory.GetCurrentDirectory();

            var parameters = new GeneratorParameters.Builder()
                .WithModel(
                    new GeneratorParameters.Component
                    {
                        BaseNamespace = DefaultBaseNamespace,
                        Extends = "BaseModel",
                        Name = ModelName
                    })
                .WithController(
                    new GeneratorParameters.Component
                    {
                        BaseNamespace = DefaultBaseNamespace,
                        Extends = "BaseController",
                        Name = ControllerName
                    })
                .WithView(
                    new GeneratorParameters.Component
                    {
                        BaseNamespace = DefaultBaseNamespace,
                        Extends = "BaseView",
                        Name = ViewName
                    })
                .WithNamespaceName(NamespaceName)
                .WithOutputDir(currentDir);

            var generatedType = TestsUtils.GenerateView(parameters.Build());
            var desiredClass = $"{NamespaceName}.{ViewName}";
            UMVCAssert.HasSameBaseClass(generatedType, typeof(BaseView<,>));
            Assert.IsTrue(generatedType.ToString() == desiredClass);
        }
    }
}