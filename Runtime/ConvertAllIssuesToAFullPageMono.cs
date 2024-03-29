using Eloi;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ConvertAllIssuesToAFullPageMono : MonoBehaviour
{
    public GitHubJsonToPageSharpMono m_source;

    public Eloi.AbstractMetaAbsolutePathDirectoryMono m_whereToStoreitDirectory;
    public Eloi.MetaFileNameWithExtension m_fileName= new Eloi.MetaFileNameWithExtension("FullIssues","md");
    public bool m_orderBy;
    public bool m_orderByTitle;

    [ContextMenu("Create")]
    public void Create()
    {
        StringBuilder sb = new StringBuilder();

        IEnumerable<GitHubJsonBeans.Issue> issues;

        if(m_orderByTitle)
            issues = m_orderBy ? m_source.m_issue.OrderBy(k => k.title) : m_source.m_issue.OrderByDescending(k => k.title);
        else 
            issues = m_orderBy ? m_source.m_issue.OrderBy(k => k.number) : m_source.m_issue.OrderByDescending(k => k.number);

        foreach (var issue in issues)
        {
            sb.Append("  \n\r");
            sb.Append($"# {issue.number} | [{issue.title}]({issue.html_url})  \n\r");
            sb.Append("  \n\r");
            sb.Append($" {issue.body}  \n\r");
            sb.Append("  \n\r");
            sb.Append("-----------------------------------------  \n\r");

        }

        Eloi.E_FileAndFolderUtility.CreateOrOverrideFile(GetFilePath(), sb.ToString());
    }

    [ContextMenu("Create All In Scene")]
    public void CreateAllInScene() {
        GameObject.FindObjectsOfType<ConvertAllIssuesToAFullPageMono>().AsParallel().ForAll(k=>k.Create());
    }
    public IMetaAbsolutePathFileGet GetFilePath() {
        return  Eloi.E_FileAndFolderUtility.Combine(m_whereToStoreitDirectory, m_fileName);
    }
    [ContextMenu("Open File")]
    public void OpenFile()
    {
        Application.OpenURL(GetFilePath().GetPath());
    }
    [ContextMenu("Show File")]
    public void ShowFile()
    {
        Application.OpenURL(m_whereToStoreitDirectory.GetPath());
    }
}
