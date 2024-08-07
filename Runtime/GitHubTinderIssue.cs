using System.Collections;
using System.Collections.Generic;
using UnityEngine;






/// <summary>
/// I am a issue that represents a keyword to learn
/// </summary>
[System.Serializable]

public struct GitHubIssueKeyword
{
    public string m_keyword;
    public GitHubIssueReference m_reference;
}

/// <summary>
/// I am a issue that represents a topic to learn
/// </summary>
[System.Serializable]
public struct GitHubIssueTopic
{

    public string m_topic;
    public GitHubIssueReference m_reference;
}
/// <summary>
/// iam a issue that represents a software to know and install
/// </summary>
[System.Serializable]
public struct GitHubIssueSoftware
{

    public string m_software;
    public GitHubIssueReference m_reference;
}




[System.Serializable]
public struct GitHubTinderTopic
{
    public string m_title;
    public GitHubIssueReference m_reference;
    public MasteringGitHubIssue m_mastering;
}
[System.Serializable]
public struct GitHubIssueReference
{
    public string m_repositoryName;
    public string m_repositoryUser;
    public int m_issueNumber;
}

[System.Serializable]
public struct MasteringGitHubIssue
{
    public bool m_isKnown;
    public float m_percentMastered;
}









