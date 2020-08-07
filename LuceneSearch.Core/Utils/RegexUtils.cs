using System.Text.RegularExpressions;

namespace LuceneSearch.Core.Utils
{
    public static class RegexUtils
    {
        public static string RemoveHtmlTags(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }
    }
}
