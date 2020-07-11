using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UMVC.Core.MVC.Interfaces;
using INotifyPropertyChanged = UMVC.Core.MVC.Interfaces.INotifyPropertyChanged;

namespace UMVC.Core.MVC
{
    [Serializable]
    public abstract class BaseModel : IBaseModel, INotifyPropertyChanged
    {
        public bool isOnFieldWillUpdateEnabled = true;
        public bool isOnFieldDidUpdateEnabled = true;
        
        public virtual event Delegates.OnFieldWillUpdate OnFieldWillUpdate;
        public virtual event Delegates.OnFieldDidUpdate OnFieldDidUpdate;

        public virtual void Initialize()
        {
        }


        protected virtual void RaiseOnFieldWillUpdate<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            var eventArgs = new PropertyChangedEventArgs(propertyName);

            object oldValue;

            var type = GetType();
            var field = type.GetField(eventArgs.PropertyName);

            if (field != null)
            {
                oldValue = field.GetValue(this);
            }
            else
            {
                oldValue = type.GetProperty(eventArgs.PropertyName)?.GetValue(this);
            }
            
            OnFieldWillUpdate?.Invoke(this, newValue, oldValue, eventArgs);
        }
        
        protected virtual void RaiseOnFieldDidUpdate<T>(T newValue,[CallerMemberName] string propertyName = null)
        {
            OnFieldDidUpdate?.Invoke(this, newValue, new PropertyChangedEventArgs(propertyName));
        }
        
        protected virtual bool Set<T>(ref T field, T value, Expression<Func<T>> property)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            
            string propertyName = ((MemberExpression)property.Body).Member.Name;

            if (isOnFieldWillUpdateEnabled)
                RaiseOnFieldWillUpdate(value, propertyName);
            field = value;
            if (isOnFieldDidUpdateEnabled)
                RaiseOnFieldDidUpdate(field, propertyName);

            return true;
        }

        
    }
}