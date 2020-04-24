using System;
using NUnit.Framework;

namespace Tests
{
    public static class UMVCAssert
    {
        public static void HasSameBaseClass(Type type1, Type type2)
        {
            Assert.IsTrue(type1.BaseType?.FullName?.Contains(type2.FullName));
        }

    }
}