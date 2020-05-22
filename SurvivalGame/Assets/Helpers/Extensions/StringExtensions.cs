using System.Text.RegularExpressions;

namespace Extensions.String
{
    public static class StringExtensions
    {
        public static Regex capitalRegex = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);



    }
}
