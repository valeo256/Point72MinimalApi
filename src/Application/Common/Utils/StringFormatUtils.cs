using System.Text.RegularExpressions;

namespace Application.Common.Utils;

public class StringFormatUtils
{
    public static string InvertWordsInSentence(string sentence)
    {
        string pattern = @"[\s\p{P}]"; // Matches whitespace characters and punctuation marks
        string[] words = Regex.Split(sentence.Trim(), pattern);
        Array.Reverse(words);
        return string.Join(" ", words);
    }
}
