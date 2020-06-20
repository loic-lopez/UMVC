using System;
using NUnit.Framework;
using UMVC.Editor.Extensions;

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
            public void TestIsNotNullOrEmpty()
            {
                var str = "a";

                Assert.That(str.IsNotNullOrEmpty(), Is.True);
            }

            [Test]
            public void TestIsNullOrEmpty()
            {
                string str = null;

                Assert.That(str.IsNullOrEmpty(), Is.True);

                str = "";
                Assert.That(str.IsNullOrEmpty(), Is.True);
            }

            [Test]
            public void TestToPascalCase()
            {
                var str = "unit test".ToPascalCase();

                Assert.That(str, Is.EqualTo("UnitTest"));
            }
        }

        [TestFixture]
        public class SystemExtensionsTests
        {
            [Test]
            public void TestGetDeclaredType()
            {
                var adog = new ADog();

                Assert.That(adog.GetType().ToString(), Is.EqualTo(adog.GetDeclaredType().ToString()));
            }

            [Test]
            public void TestGetDeclaredTypeSubDog()
            {
                var adog2 = new ADog2();
                adog2.Bark();
                Assert.AreNotEqual(adog2.GetType().ToString(), adog2.GetDogType().ToString());
            }
        }
    }
}