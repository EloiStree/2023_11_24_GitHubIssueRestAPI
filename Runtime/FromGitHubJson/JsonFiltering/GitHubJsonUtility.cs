using System.Text.RegularExpressions;

public static class GitHubJsonUtility {
    public static void FindLikeDislikeValues(string text, out int like, out int dislike)
    {
        // Regular expressions to find the values for +1 and -1
        Regex regexPlusOne = new Regex(@"\+1""\s*:\s*(\d+)");
        Regex regexMinusOne = new Regex(@"\-1""\s*:\s*(\d+)");

        // Finding matches
        Match matchPlusOne = regexPlusOne.Match(text);
        Match matchMinusOne = regexMinusOne.Match(text);

        // Parsing the values
        like = matchPlusOne.Success ? int.Parse(matchPlusOne.Groups[1].Value) : 0;
        dislike = matchMinusOne.Success ? int.Parse(matchMinusOne.Groups[1].Value) : 0;


    }
}
