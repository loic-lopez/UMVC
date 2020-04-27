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

        
        public static string Capitalize(this string str)
        {
            if (str.IsNullOrEmpty()) return "";
            
            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}