using System.IO;
using NUnit.Framework;
using UMVC.Core.MVC;

namespace UMVC.Core.Tests
{
    [TestFixture]
    public class GeneratorTests
    {
        [Test]
        public void TestIfControllerHasBeenGenerated()
        {
            const string namespaceName = "TestNamespace";
            const string modelName = "TestModel";
            const string controllerName = "TestController";
            const string extends = "BaseController";
            var currentDir = Directory.GetCurrentDirectory();

            var generatedObj = TestsUtils.GenerateController(controllerName, modelName, namespaceName, extends, currentDir);

            UMVCAssert.HasSameBaseClass(generatedObj.GetType(), typeof(BaseController<>));
            var desiredClass = $"{namespaceName}.{controllerName}";
            Assert.IsTrue(generatedObj.GetType().ToString() == desiredClass);
        }

        [Test]
        public void TestIfModelHasBeenGenerated()
        {
            const string namespaceName = "TestNamespace";
            const string modelName = "TestModel";
            const string extends = "BaseModel";
            var currentDir = Directory.GetCurrentDirectory();

            var generatedObj = TestsUtils.GenerateModel(modelName, namespaceName, extends, currentDir);
            
            UMVCAssert.HasSameBaseClass(generatedObj.GetType(), typeof(BaseModel));
            var desiredClass = $"{namespaceName}.{modelName}";
            Assert.IsTrue(generatedObj.GetType().ToString() == desiredClass);
        }

        // WONTFIX: We can't call Activator.CreateInstance() because BaseView extends Monobehavior 
        [Test]
        public void TestIfViewHasBeenGenerated()
        {
            const string namespaceName = "TestNamespace";
            const string modelName = "TestModel";
            const string controllerName = "TestController";
            const string viewName = "TestView";
            const string extends = "BaseView";
            
            var currentDir = Directory.GetCurrentDirectory();

            var generatedType = TestsUtils.GenerateView(viewName, controllerName, modelName, namespaceName, extends, currentDir);
            var desiredClass = $"{namespaceName}.{viewName}";
            UMVCAssert.HasSameBaseClass(generatedType, typeof(BaseView<,>));
            Assert.IsTrue(generatedType.ToString() == desiredClass);
        }
    }
}