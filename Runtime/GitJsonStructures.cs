using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GitHubJsonBeans 
{
    
    [System.Serializable]
    public class IssueGroup {
        public Issue[] m_issue= new Issue[0] ;
        public void ConvertDate() {
            if(m_issue!=null )
            foreach (var item in m_issue)
            {
                item.ConvertDate();
            }
        }
    }
    [System.Serializable]
    public class User
    {
        public string login;
        public int id;
        public string node_id;
        public string avatar_url;
        public string gravatar_id;
        public string url;
        public string html_url;
        public string followers_url;
        public string following_url;
        public string gists_url;
        public string starred_url;
        public string subscriptions_url;
        public string organizations_url;
        public string repos_url;
        public string events_url;
        public string received_events_url;
        public string type;
        public bool site_admin;
    }

    [System.Serializable]
    public class Label
    {
        // Add properties for Label if needed
    }

    [System.Serializable]
    public class Reactions
    {
        public string url;
        public int total_count;
        public int _1;
        public int laugh;
        public int hooray;
        public int confused;
        public int heart;
        public int rocket;
        public int eyes;
    }

    [System.Serializable]
    public class Issue
    {
        public string url;
        public string repository_url;
        public string labels_url;
        public string comments_url;
        public string events_url;
        public string html_url;
        public int id;
        public string node_id;
        public int number;
        public string title;
        public User user;
        public List<Label> labels;
        public string state;
        public bool locked;
        public User assignee;
        public List<User> assignees;
        public string milestone;
        public int comments;
        public string created_at;
        public string updated_at;
        public string closed_at;
        public DateTime created_at_date;
        public DateTime updated_at_date;
        public DateTime closed_at_Date;

        public void ConvertDate() {
            try { 
            created_at_date = DateTime.ParseExact(created_at, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.RoundtripKind);
            }
            catch { }
            try
            {
                updated_at_date = DateTime.ParseExact(updated_at, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.RoundtripKind);

        } catch { }
            try
            {
                closed_at_Date = DateTime.ParseExact(closed_at, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.RoundtripKind);
            } catch { }            //string dateString = "2023-11-24T16:43:09Z";
            //DateTime dateTime = DateTime.ParseExact(dateString, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.RoundtripKind);

        }

        public string author_association;
        public string active_lock_reason;
        public string body;
        public Reactions reactions;
        public string tim;
    }
}
