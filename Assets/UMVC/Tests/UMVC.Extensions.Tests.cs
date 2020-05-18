using System;
using NUnit.Framework;
using UMVC.Editor.Extensions;
using UnityEngine;

namespace UMVC.Tests
{
    public abstract class Dog
    {
        public abstract void Bark();
    }

    public class ADog : Dog
    {
        public override void Bark()
        {
            Console.WriteLine("Bark!");
        }

        public Type GetDogType()
        {
            return this.GetDeclaredType();
        }
    }

    public class ADog2 : ADog
    {
        
    }

    [TestFixture]
    public class UMVCExtensionsTests
    {
        [TestFixture]
        public class StringExtensionsTests
        {
            [Test]
            public void TestIsNullOrEmpty()
            {
                string str = null;
            
                Assert.That(str.IsNullOrEmpty());

                str = "";
                Assert.That(str.IsNullOrEmpty());
            }
        
            [Test]
            public void TestIsNotNullOrEmpty()
            {
                string str = "a";
            
                Assert.That(str.IsNotNullOrEmpty());
            }
        }
        
        [TestFixture]
        public class SystemExtensionsTests
        {
            [Test]
            public void TestGetDeclaredType()
            {
                var adog = new ADog();
                
                Assert.True(adog.GetType().ToString() == adog.GetDeclaredType().ToString());
            }

            [Test]
            public void TestGetDeclaredTypeSubDog()
            {
                var adog2 = new ADog2();
                Assert.True(adog2.GetType().ToString() != adog2.GetDogType().ToString());
            }
        }
    }
}