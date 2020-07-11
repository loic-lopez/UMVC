using System;
using UMVC.Core.MVC;
using UnityEngine;

namespace UseCases.Mvc
{
    
    
    [Serializable]
    public class MvcModel : BaseModel
    {
        [SerializeField] private int val;

        public int Value
        {
            get => val;
            set => Set(ref val, value, () => Value);
        }

    }
}