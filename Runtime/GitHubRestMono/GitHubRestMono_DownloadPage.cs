using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitHubRestMono_DownloadPage : MonoBehaviour
{

    public A_PathTypeAbsoluteDirectoryMono m_whereToStore;
    public A_GitHubApiKeyMono m_apiKey;
    public STRUCT_UserRepositoryIssueId m_issueToDownload;

    public string m_respAPIFormatComment = "https://api.github.com/repos/{0}/{1}/issues/{2}/comments";
    public string m_respAPIFormatIssues = "https://api.github.com/repos/{0}/{1}/issues/{2}";

    public TextDownloadedByCoroutine m_jsonResultIssue;
    public TextDownloadedByCoroutine m_jsonResultComment;

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        StartCoroutine(MakeRequestIssue());
        StartCoroutine(MakeRequestIssueComments());
    }

    private IEnumerator MakeRequestIssueComments()
    {


        yield return GitHubCoroutineDownloadTool.MakeRequest(
            m_jsonResultComment,
            string.Format(m_respAPIFormatComment, m_issueToDownload.m_usernameId, m_issueToDownload.m_respositoryId, m_issueToDownload.m_issueId)
            );
    }

    private IEnumerator MakeRequestIssue()
    {
        GitHubRest_RequestIssueJson.MakeRequestIssue(m_jsonResultIssue, m_issueToDownload, m_issueToDownload, m_issueToDownload, m_apiKey);

        yield return GitHubCoroutineDownloadTool.MakeRequest(
            m_jsonResultIssue,
            string.Format(m_respAPIFormatIssues, m_issueToDownload.m_usernameId, m_issueToDownload.m_respositoryId, m_issueToDownload.m_issueId)
            );

        string filePath = string.Format("I{0:00000}.json",m_issueToDownload.m_issueId);
        STRUCT_RelativeFilePath file = new STRUCT_RelativeFilePath(filePath);
        I_PathTypeAbsoluteFileGet fileWhereToStore = TypePathCombineTool.Combine(m_whereToStore, file);
        AbsoluteTypePathTool.OverwriteFile( fileWhereToStore, m_jsonResultIssue.m_text);

    }
}

[System.Serializable]
public struct STRUCT_UserRepository: I_OwnGitHubRepositoryNameGet, I_OwnGitHubUserNameGet
{
    public string m_userId;
    public string m_respositoryId;
    public string GetGitHubUserName()
    {
        return m_userId;
    }
    public string GetGitHubRepositoryName()
    {
        return m_respositoryId;
    }
}
[System.Serializable]
public struct STRUCT_User: I_OwnGitHubUserNameGet, I_OwnGitHubUserNameSet
{
    public string m_userNamedId;
    public STRUCT_User(string name = "")
    {
        m_userNamedId = name;
    }
    public string GetGitHubUserName()
    {
        return m_userNamedId;
    }

    public void SetGitHubUserName(string name)
    {
        m_userNamedId = name;
    }
}

public interface I_OwnGitHubRepositoryOfUserGet: I_OwnGitHubUserNameGet, I_OwnGitHubRepositoryNameGet
{

}


[System.Serializable]
public struct STRUCT_Repository: I_OwnGitHubRepositoryNameGet, I_OwnGitHubRepositoryNameSet
{
    public string m_respositoryId;

    public STRUCT_Repository(string name="") { 
        m_respositoryId = name;
    }
    public string GetGitHubRepositoryName()
    {
        return m_respositoryId;
    }

    public void SetGitHubRepositoryName(string name)
    {
        m_respositoryId = name;
    }
}

[System.Serializable]
public struct STRUCT_IssueId: I_OwnGitHubIssueIdGet, I_OwnGitHubIssueIdSet
{
        public int m_issueId;
    public STRUCT_IssueId(int issueId=0)
    {
        m_issueId = issueId;
    }

    public int GetGitHubIssueId()
    {
        return m_issueId;
    }

    public void SetGitHubIssueId(int id)
    {
        m_issueId = id;
    }
}
[System.Serializable]
public struct STRUCT_UserRepositoryIssueId: I_OwnGitHubRepositoryIssueIdGet, I_OwnGitHubRepositoryIssueIdSet
{
    public string m_usernameId;
    public string m_respositoryId;
    public int m_issueId; 
    public string GetGitHubUserName()
    {
        return m_usernameId;
    }
    public string GetGitHubRepositoryName()
    {
        return m_respositoryId;
    }
    public int GetGitHubIssueId()
    {
        return m_issueId;
    }

    public void SetGitHubUserName(string name)
    {
        m_usernameId = name;
    }

    public void SetGitHubRepositoryName(string name)
    {
        m_respositoryId = name;
    }

    public void SetGitHubIssueId(int id)
    {
        m_issueId = id;
    }
}

public interface I_OwnGitHubRepositoryIssueIdGet: I_OwnGitHubUserNameGet, I_OwnGitHubRepositoryNameGet, I_OwnGitHubIssueIdGet
{ 

}

public interface I_OwnGitHubRepositoryIssueIdSet : I_OwnGitHubIssueIdSet, I_OwnGitHubRepositoryNameSet, I_OwnGitHubUserNameSet { 
}
public interface I_OwnGitHubAuthPrivateApiKeyGet
{
    string GetGitHubAuthPrivateApiKey();
}



public interface I_OwnGitHubRepositoryNameGet
{
    string GetGitHubRepositoryName();
}
public interface I_OwnGitHubUserNameGet
{
    string GetGitHubUserName();
}
public interface I_OwnGitHubIssueIdGet
{
    int GetGitHubIssueId();
}

public interface I_OwnGitHubIssueIdSet 
{
    void SetGitHubIssueId(int id);
}
public interface I_OwnGitHubRepositoryNameSet
{
    void SetGitHubRepositoryName(string name);
}
public interface I_OwnGitHubUserNameSet
{
    void SetGitHubUserName(string name);
}



[System.Serializable]
public struct STRUCT_GitHubIssueBasic
{
    public string m_title;
    [TextArea(2,4)]
    public string m_body;
    public string m_createdBy;
    public bool m_isIssueOpen;
    public int m_commentCount;
    public string[] m_labels;
    public STRUCT_UserRepositoryIssueId m_reference;
}