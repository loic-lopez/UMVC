using System;
using System.Linq;

namespace UMVC.Tests.Extensions
{
    public static class TestsExtensions
    {
        private static readonly Random Random = new Random();
        
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, Random.Next(0, 21))
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}