using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UseCases.Mvc;

namespace UMVC.Playmode.Tests
{
    internal class MvcViewMock : MvcView
    {
        protected override void Awake()
        {
            Model = new MvcModel();
            base.Awake();
        }
    }
    
    
    [TestFixture]
    public class UMVCUseCasesMVC
    {
        
        [UnityTest]
        public IEnumerator TestMVCUpdateValue()
        {
            var gameObject = new GameObject();
            var mvcView = gameObject.AddComponent<MvcViewMock>();
            yield return new WaitForFixedUpdate();
            mvcView.Model.Value = -1;
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(mvcView.Model.Value != 0);
        }
    }
}