using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GitHubJsonToPageSharpMono : MonoBehaviour
{
    public string m_userId = "EloiStree";
    public string m_respositoryId = "HelloLynxR1";
    public Eloi.A_PathTypeAbsoluteDirectoryMono m_whereToStoreitDirectory;
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
        string path = m_path + $"\\GitIssues\\{m_userId}\\{m_respositoryId}";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        m_filesFound= Directory.GetFiles(path, "*I*.json", SearchOption.AllDirectories);
        foreach (var item in m_filesFound)
        {
            string t = File.ReadAllText(item);
                //"{\"m_issue\":" + File.ReadAllText(item) + "}";

            GitHubJsonBeans.Issue  issue = JsonUtility.FromJson<GitHubJsonBeans.Issue>(t);
            issue.ConvertDate();
            m_issue.Add(issue);

        }
    }

    
}
