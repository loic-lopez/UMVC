using System;
using System.ComponentModel;
using NUnit.Framework;
using UMVC.Core.MVC;

namespace UMVC.Core.Tests
{
    public enum TestEnum
    {
        Test
    }

    [Serializable]
    public class SubModel : BaseModel
    {
        public TestEnum TestEnum = TestEnum.Test;
    }
    
    [Serializable]
    public class TestModel : BaseModel
    {
        public const int DefaultValue = -1;
        public const int DefaultValueViewStart = -2;
        public SubModel SubModel;
        public bool IsTest { get; set; } = true;
        
        private int _value = DefaultValue;

        public int Value
        {
            get => _value;
            set => Set(ref _value, value, () => Value);
        }

        public int valueInitFromViewStart = DefaultValueViewStart;

        public override void Initialize()
        {
            base.Initialize();
            SubModel = new SubModel();
        }
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
            Model.Value = new Random().Next();
        }
    }

    public class TestView : BaseView<TestModel, TestController>
    {

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

        public void UpdateVar(int val)
        {
            Model.Value = val;
        }
    }
    
    [TestFixture]
    public class MVCTests
    {
        private bool _onFieldWillUpdateRunned;
        private bool _onFieldDidUpdateRunned;
        
        [Test]
        public void TestEvents()
        {
            TestModel testModel = new();
            testModel.Initialize();
            int value = new Random().Next();
            const string fieldName = "Value";
            

            void OnFieldWillUpdate(object model, object newValue, object oldValue, PropertyChangedEventArgs eventArgs)
            {
                Assert.True((int)oldValue == ((TestModel)model).Value);
                Assert.True(eventArgs.PropertyName == fieldName);
                Assert.True(newValue != oldValue);
                Assert.True((int)newValue == value);
                _onFieldWillUpdateRunned = true;
            }
            
            void OnFieldDidUpdate(object model, object newValue, PropertyChangedEventArgs eventArgs)
            {
                Assert.True(eventArgs.PropertyName == fieldName);
                Assert.True(value == (int) newValue);
                _onFieldDidUpdateRunned = true;
            }
            
            testModel.OnFieldWillUpdate += OnFieldWillUpdate;
            testModel.OnFieldDidUpdate += OnFieldDidUpdate;

            // Call setter and check if the events are fired
            testModel.Value = value;
            Assert.True(testModel.Value == value);
            Assert.IsTrue(_onFieldWillUpdateRunned);
            Assert.IsTrue(_onFieldDidUpdateRunned);
            
            // disable OnFieldWillUpdate event and check not runned
            _onFieldWillUpdateRunned = false;
            _onFieldDidUpdateRunned = false;
            value = 42;
            testModel.isOnFieldWillUpdateEnabled = false;
            testModel.Value = value;
            Assert.True(testModel.Value == value);
            Console.WriteLine(_onFieldDidUpdateRunned);
            Assert.IsFalse(_onFieldWillUpdateRunned);
            Assert.IsTrue(_onFieldDidUpdateRunned);
            
            // disable OnFieldDidUpdate event and check not runned
            _onFieldWillUpdateRunned = false;
            _onFieldDidUpdateRunned = false;
            value++;
            testModel.isOnFieldWillUpdateEnabled = true;
            testModel.isOnFieldDidUpdateEnabled = false;
            testModel.Value = value;
            Assert.True(testModel.Value == value);
            Console.WriteLine(_onFieldDidUpdateRunned);
            Assert.IsTrue(_onFieldWillUpdateRunned);
            Assert.IsFalse(_onFieldDidUpdateRunned);
            
            // disable all events and check not runned
            _onFieldWillUpdateRunned = false;
            _onFieldDidUpdateRunned = false;
            value++;
            testModel.isOnFieldWillUpdateEnabled = false;
            testModel.isOnFieldDidUpdateEnabled = false;
            testModel.Value = value;
            Assert.True(testModel.Value == value);
            Console.WriteLine(_onFieldDidUpdateRunned);
            Assert.IsFalse(_onFieldWillUpdateRunned);
            Assert.IsFalse(_onFieldDidUpdateRunned);
            
            // remove events
            testModel.OnFieldWillUpdate -= OnFieldWillUpdate;
            testModel.OnFieldDidUpdate -= OnFieldDidUpdate;
        }
        
        
        [Test]
        public void TestViewEventsAndControllerEvents()
        {
            var view = new TestView {Model = new TestModel()};
            // mock awake method from unity
            view.CustomAwake();
            
            var controllerModel = view.GetController().GetModel();
            var viewModel = view.Model;
            
            Assert.IsTrue(viewModel != null);
            Assert.IsTrue(controllerModel != null);
            
            // mock start method from unity
            view.CustomStart();
            Assert.IsTrue(view.GetController().GetModel().Value != TestModel.DefaultValue);
            Assert.IsTrue(view.Model.Value != TestModel.DefaultValue);
            
            Assert.IsTrue(view.GetController().GetModel().valueInitFromViewStart != TestModel.DefaultValueViewStart);
            Assert.IsTrue(view.Model.valueInitFromViewStart != TestModel.DefaultValueViewStart);
        }

        [Test]
        public void TestVarUpdatedForViewAndController()
        {
            var view = new TestView {Model = new TestModel()};
            // mock awake method from unity
            view.CustomAwake();
            const int value = 5;
            
            view.UpdateVar(value);
            Assert.IsTrue(view.GetController().GetModel().Value == value);
            Assert.IsTrue(view.GetModel().Value == value);
        }
    }
}