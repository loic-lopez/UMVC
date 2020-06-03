﻿using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using UMVC.Core.Exceptions;

namespace UMVC.Core.MVC
{
    public class ModelProxy<TModel> : RealProxy where TModel : BaseModel
    {
        private readonly TModel _instance;
        public event Delegates.OnFieldWillUpdate OnFieldWillUpdate;
        public event Delegates.OnFieldDidUpdate OnFieldDidUpdate;

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
            if (_instance == null)
                throw new ModelProxyInstanceNull(typeof(TModel));
            
            
            if (msg is IMethodCallMessage methodCall)
            {
                var arguments = methodCall.Args;
                
                if (methodCall.MethodBase.Name.Equals("FieldSetter") && _instance.isOnFieldWillUpdateEnabled)
                {
                    BeforeFieldUpdate(arguments);
                }
                
                var result = methodCall.MethodBase.Invoke(_instance, arguments);
                
                if (methodCall.MethodBase.Name.Equals("FieldSetter") && _instance.isOnFieldDidUpdateEnabled)
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
            
            OnFieldWillUpdate?.Invoke(field, arguments[2], _instance.GetType().GetField(field).GetValue(_instance));
        }

        private void AfterFieldUpdate(IReadOnlyList<object> arguments)
        {
            OnFieldDidUpdate?.Invoke((string) arguments[1], arguments[2]);
        }
    }
}