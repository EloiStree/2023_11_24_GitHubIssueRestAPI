using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GitHubJsonToPageSharpMono : MonoBehaviour
{
    public string m_userId = "EloiStree";
    public string m_respositoryId = "HelloLynxR1";
    public Eloi.AbstractMetaAbsolutePathDirectoryMono m_whereToStoreitDirectory;
    public string[] m_filesFound;
    public string m_path;
    public List<GitHubJsonBeans.Issue> m_issue;
    public List<GitHubJsonBeans.IssueGroup> m_issueGroup;
    [ContextMenu("Refresh")]
    void Refresh()
    {
        m_issue.Clear();
        m_issueGroup.Clear();
        m_path = m_whereToStoreitDirectory.GetPath();
        Debug.Log(m_path);
        m_filesFound= Directory.GetFiles(m_path+ $"\\GitIssues\\{m_userId}\\{m_respositoryId}", "*P*.json", SearchOption.AllDirectories);
        foreach (var item in m_filesFound)
        {
            string t = "{\"m_issue\":" + File.ReadAllText(item) + "}";
            GitHubJsonBeans.IssueGroup  issue = JsonUtility.FromJson<GitHubJsonBeans.IssueGroup>(t);
            issue.ConvertDate();
            m_issueGroup.Add(issue);
            m_issue.AddRange(issue.m_issue);

        }
    }

    
}
