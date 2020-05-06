using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using NUnit.Framework;
using UMVC.Core.MVC;
using UMVC.Core.MVC.Interfaces;
using UnityEngine;

namespace UMVC.Core.Tests
{
    internal class ModelProxy<T> : RealProxy where T : IBaseModel
    {
        private readonly T _instance;

        private ModelProxy(T instance) : base(typeof(T))
        {
            _instance = instance;
        }

        public static T Bind<TModel>() where TModel : T, new()
        {
            return (T) new ModelProxy<T>(new TModel()).GetTransparentProxy();
        }

        public static T Bind<TModel>(TModel instance) where TModel : T
        {
            return (T) new ModelProxy<T>(instance).GetTransparentProxy();
        }
        
        public override IMessage Invoke(IMessage msg)
        {
            try
            {
                return ParseEvent(msg);
            }
            catch (Exception e)
            {
                if (e is TargetInvocationException && e.InnerException != null)
                {
                    return new ReturnMessage(e.InnerException, (IMethodCallMessage) msg);
                }

                return new ReturnMessage(e, msg as IMethodCallMessage);
            }
        }

        private ReturnMessage ParseEvent(IMessage msg)
        {
            var methodCall = (IMethodCallMessage)msg;
            var methodInfo = (MethodInfo)methodCall.MethodBase;

            if (IsPublicAutoProperties(methodInfo))
            {
                
            }
            
            Console.WriteLine("Before invoke: " + methodInfo.Name);
            var result = methodInfo.Invoke(_instance, methodCall.InArgs);
            Console.WriteLine("After invoke: " + methodInfo.Name);

            return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
        }

        private static bool IsPublicAutoProperties(MethodInfo methodInfo)
        {
            return methodInfo.IsPublic && methodInfo.IsSpecialName;
        }
    } 
    
    [Serializable]
    public class TestModel : BaseModel, ITestModel
    {
        public int Val { get; set; }

        public int test;

        public void SetTest(int t) => test = t;
    }


    public interface ITestModel : IBaseModel
    {
        int Val { get; set; }
        void SetTest(int t);
    }
    
    [TestFixture]
    public class MVCTests
    {
        
        [Test]
        public void TestEvent()
        {
            ITestModel testModel = ModelProxy<ITestModel>.Bind<TestModel>();

            Assert.True(true);
            testModel.Val = 2;
            testModel.SetTest(20);
        }
    }
}