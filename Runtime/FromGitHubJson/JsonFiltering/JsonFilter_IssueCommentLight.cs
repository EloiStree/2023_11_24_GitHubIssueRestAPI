using System;
using System.Collections.Generic;
using UnityEngine;
using static JsonFilter_IssueCommentLight;


public interface IJsonFilterArray<T> { 

    void ParseJsonRawToFiltered(string jsonRaw, out bool parsed, out T[] foundObject, out string jsonFiltered);

}

[System.Serializable]
public class JsonFilter_IssueCommentLight 
{


    [System.Serializable]
    public struct User
    {
        public string login ;
        public ulong id ;
    }

    [System.Serializable]
    public struct Reactions
    {
        public int total_count ;
        public int plusOne ;
        public int minusOne ;
        public int laugh ;
        public int hooray ;
        public int confused ;
        public int heart ;
        public int rocket ;
        public int eyes ;
    }

    [System.Serializable]
    public struct GitHubIssueComment
    {
        public ulong id ;
        public User user ;
        public string created_at;
        public string updated_at ;
        public string author_association ;
        public string body ;
        public Reactions reactions;
        public string html_url; // "https://github.com/EloiStree/2023_11_24_GitHubIssueRestAPI/issues/2#issuecomment-2273912391",
    }

    [System.Serializable]
    public struct GitHubListIssueComment
    {
        public GitHubIssueComment[] comments;
    }

   
}
