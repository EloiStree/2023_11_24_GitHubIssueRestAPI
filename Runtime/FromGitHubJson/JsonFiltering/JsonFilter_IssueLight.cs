using System;
using System.Collections;
using UnityEngine;


public  class JsonFilter_IssueLight
{
    

    [Serializable]
    public struct Issue
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
        public Labels[] labels;
    }

    [Serializable]
    public struct User
    {
        public string login;
        public string id;
    }

    [Serializable]
    public struct Labels {

        public string name;
        public string id;
    }

    [Serializable]
    public struct Reactions
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
