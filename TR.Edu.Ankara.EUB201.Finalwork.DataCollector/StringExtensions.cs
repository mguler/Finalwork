using System.Text.RegularExpressions;

namespace TR.Edu.Ankara.EUB201.Finalwork.DataCollector
{
    public static class StringExtensions
    {
        public static bool IsMatch(this string s, string pattern) => Regex.IsMatch(s, pattern);
        public static string Match(this string s, string pattern) => Regex.Match(s, pattern).Value;
        public static string RegexReplace(this string s, string pattern,string replacement) => Regex.Replace(s, pattern, replacement);
    }
}
