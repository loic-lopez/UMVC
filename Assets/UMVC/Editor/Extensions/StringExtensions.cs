using System.Linq;

namespace UMVC.Editor.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }


        public static string ToNamespacePascalCase(this string str)
        {
            if (str.IsNullOrEmpty()) return "";

            static string CapitalizeWord(string word) => char.ToUpper(word[0]) + word[1..];

            var wordList = str.Replace("[^A-Za-z0-9]", "").Split(' ');

            return wordList.Aggregate(string.Empty, (current, word) => current + CapitalizeWord(word));
        }
    }
}