using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


public interface IJsonFilter<T>
{
    void ParseJsonRawToFiltered(string jsonRaw,out bool parsed, out T foundObject, out string jsonFiltered);
}

public  class JsonFilter_IssueLight: IJsonFilter<JsonFilter_IssueLight.Issue>
{
    
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


    public void ParseJsonRawToFiltered(string jsonRaw, out bool parsed, out Issue foundObject, out string jsonFiltered)
    {
        try {
            foundObject = JsonUtility.FromJson<Issue>(jsonRaw);
            FindLikeDislikeValues(jsonRaw, out foundObject.reactions.plus_1, out foundObject.reactions.minus_1);
            jsonFiltered = JsonUtility.ToJson(foundObject);
        }
        catch(Exception e)
        {
            parsed = false;
            foundObject = null;
            jsonFiltered = "";
            return;
        }
        parsed = true;
    }

    [Serializable]
    public class Issue
    {
        public int number;
        public string title;
        public User user;
        public string state;
        public int comments;
        public string created_at;
        public string updated_at;
        public string author_association;
        public string body;
        public Reactions reactions;
    }

    [Serializable]
    public class User
    {
        public string login;
    }
    [Serializable]
    public class Reactions
    {
        public string url;
        public int total_count;
        public int plus_1;
        public int minus_1;
        public int laugh;
        public int hooray;
        public int confused;
        public int heart;
        public int rocket;
        public int eyes;
    }
}
