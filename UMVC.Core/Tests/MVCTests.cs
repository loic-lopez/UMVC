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
        public void TestEvents()
        {
            ModelProxy<TestModel> modelProxy = ModelProxy<TestModel>.Bind(new TestModel());
            TestModel testModel = modelProxy.GetTransparentProxy();
            int newValue = new Random().Next();
            const string fieldName = "value";
            
            modelProxy.OnFieldUpdate += (field, newObject, oldObject) =>
            {
                Assert.True((int)oldObject == 0);
                Assert.True(field == fieldName);
                Assert.True(newObject != oldObject);
                Assert.True((int)newObject == newValue);
            };

            modelProxy.OnFieldUpdated += (field, value) =>
            {
                Assert.True(field == fieldName);
                Assert.True((int)value == newValue);
            };

            testModel.value = newValue;
            Assert.True(testModel.value == newValue);
        }
    }
}