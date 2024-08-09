using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitHubRest_RequestIssueJson 
{

    /// <summary>
    /// I am a methode that will fetch the text json on GitHub for a specific issue from a coroutine.
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="userId"></param>
    /// <param name="repositoryId"></param>
    /// <param name="issueId"></param>
    /// <returns></returns>
    public static IEnumerator MakeRequestIssue(
        TextDownloadedByCoroutine callback,
        I_OwnGitHubUserNameGet userId,
        I_OwnGitHubRepositoryNameGet repositoryId,
        I_OwnGitHubIssueIdGet issueId,
        I_OwnGitHubAuthPrivateApiKeyGet privateAuthApiKey)
    {

        yield return GitHubDownloadWebPageTool.MakeRequest(
            callback,
            string.Format("https://api.github.com/repos/{0}/{1}/issues/{2}",
            userId.GetGitHubUserName(),
            repositoryId.GetGitHubRepositoryName(),
            issueId.GetGitHubIssueId()
            ), privateAuthApiKey.GetGitHubAuthPrivateApiKey()
            );
        Debug.Log(privateAuthApiKey.GetGitHubAuthPrivateApiKey());
    }
}



public interface I_OwnGitHubWebRequestCallBack
{
    public void GetRequestUrl(out string url);
    public void GetReceivedText(out string text);
}


[System.Serializable]
public struct STRUCT_GitHubRawJsonIssue
{
    public string m_requestUrl;
    public string m_rawJson;
}

