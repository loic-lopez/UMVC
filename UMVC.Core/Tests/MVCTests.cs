using System;
using NUnit.Framework;
using UMVC.Core.MVC;

namespace UMVC.Core.Tests
{

    [Serializable]
    public class TestModel : BaseModel
    {
        public int value;
    }
    
    [TestFixture]
    public class MVCTests
    {
        
        [Test]
        public void TestEvent()
        {
            ModelProxy<TestModel> modelProxy = ModelProxy<TestModel>.Bind(new TestModel());
            TestModel testModel = modelProxy.GetTransparentProxy();

            testModel.value = 2;
            Assert.True(testModel.value == 2);
        }
    }
}