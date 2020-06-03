using System;
using NUnit.Framework;
using UMVC.Core.Exceptions;
using UMVC.Core.MVC;
using Random = System.Random;

namespace UMVC.Core.Tests
{

    [Serializable]
    public class TestModel : BaseModel
    {
        public const int DefaultValue = -1;
        public const int DefaultValueViewStart = -2;
        
        public int value = DefaultValue;
        public int valueInitFromViewStart = DefaultValueViewStart;
    }

    public class TestController : BaseController<TestModel>
    {
        public TestModel GetModel()
        {
            return Model;
        }

        public override void LateSetup()
        {
            base.LateSetup();
            Model.value = new Random().Next();
        }
    }

    public class TestView : BaseView<TestModel, TestController>
    {
        public override void OnFieldWillUpdate(string field, object newObject, object oldObject)
        {
        }

        public override void OnFieldDidUpdate(string field, object value)
        {
        }

        public void CustomAwake()
        {
            base.Awake();
        }

        public void CustomStart()
        {
            base.Start();
            Model.valueInitFromViewStart = new Random().Next();
        }

        public TestController GetController()
        {
            return Controller;
        }
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
            
            modelProxy.OnFieldWillUpdate += (field, newObject, oldObject) =>
            {
                Assert.True((int)oldObject == TestModel.DefaultValue);
                Assert.True(field == fieldName);
                Assert.True(newObject != oldObject);
                Assert.True((int)newObject == newValue);
            };

            modelProxy.OnFieldDidUpdate += (field, value) =>
            {
                Assert.True(field == fieldName);
                Assert.True((int)value == newValue);
            };

            testModel.value = newValue;
            Assert.True(testModel.value == newValue);
        }

        [Test]
        public void ModelProxyMustThrowNullInstance()
        {
            Assert.Throws<ModelProxyInstanceNull>(() =>
            {
                var view = new TestView();

                var controller = new TestController();
                controller.Setup(view);

                // Model not initialized so throws null
                controller.GetModel();
            });
        }
        
        

        [Test]
        public void TestViewEventsAndControllerEvents()
        {
            var view = new TestView();
            view.Model = new TestModel();
            // mock awake method from unity
            view.CustomAwake();
            
            var controllerModel = view.GetController().GetModel();
            var viewModel = view.Model;
            
            Assert.IsTrue(viewModel is TestModel);
            Assert.IsTrue(controllerModel is TestModel);
            
            // mock start method from unity
            view.CustomStart();
            Assert.IsTrue(view.GetController().GetModel().value != TestModel.DefaultValue);
            Assert.IsTrue(view.Model.value != TestModel.DefaultValue);
            
            Assert.IsTrue(view.GetController().GetModel().valueInitFromViewStart != TestModel.DefaultValueViewStart);
            Assert.IsTrue(view.Model.valueInitFromViewStart != TestModel.DefaultValueViewStart);
        }
    }
}