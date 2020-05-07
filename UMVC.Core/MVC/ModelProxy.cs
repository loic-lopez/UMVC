using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace UMVC.Core.MVC
{
    public class ModelProxy<TModel> : RealProxy where TModel : BaseModel
    {
        private readonly TModel _instance;
        public event Delegates.OnFieldUpdate OnFieldUpdate;
        public event Delegates.OnFieldUpdated OnFieldUpdated;

        private ModelProxy(TModel instance) : base(typeof(TModel))
        {
            _instance = instance;
        }

        public static ModelProxy<TModel> Bind(TModel instance)
        {
            return new ModelProxy<TModel>(instance);
        }

        public new TModel GetTransparentProxy()
        {
            return (TModel) base.GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage msg)
        {
            if (msg is IMethodCallMessage methodCall)
            {
                var arguments = methodCall.Args;
                
                if (methodCall.MethodBase.Name.Equals("FieldSetter"))
                {
                    BeforeFieldUpdate(arguments);
                }
                
                var result = methodCall.MethodBase.Invoke(_instance, arguments);
                
                if (methodCall.MethodBase.Name.Equals("FieldSetter"))
                {
                    AfterFieldUpdate(arguments);
                }

                return new ReturnMessage(
                    result, 
                    arguments, 
                    arguments.Length, 
                    methodCall.LogicalCallContext, 
                    methodCall
                );
            }

            return null;
        }


        private void BeforeFieldUpdate(IReadOnlyList<object> arguments)
        {
            var field = (string) arguments[1];
            
            OnFieldUpdate?.Invoke(field, arguments[2], _instance.GetType().GetField(field).GetValue(_instance));
        }

        private void AfterFieldUpdate(IReadOnlyList<object> arguments)
        {
            OnFieldUpdated?.Invoke((string) arguments[1], arguments[2]);
        }
    }
}