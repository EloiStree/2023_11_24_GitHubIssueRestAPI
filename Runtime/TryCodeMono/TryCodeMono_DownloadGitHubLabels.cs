using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TryCodeMono_DownloadGitHubLabels : MonoBehaviour
{
    public A_GitHubApiKeyMono m_apiKey;
    public TextDownloadedByCoroutine m_textDownloaded;
    public STRUCT_UserRepository m_userRepository;


    [ContextMenu("Refresh")]
    void DownloadLabels()
    {
       StartCoroutine(GitHubFetchJsonTool.FetchLabels(m_userRepository, m_userRepository, m_apiKey, m_textDownloaded));
    }

}

public partial class GitHubFetchJsonTool
{



    /// <summary>
    /// I am a methode that will fetch the text json on GitHub for a specific issue from a coroutine.
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="userId"></param>
    /// <param name="repositoryId"></param>
    /// <param name="issueId"></param>
    /// <returns></returns>
    public static IEnumerator FetchTargetIssue(

        I_OwnGitHubUserNameGet userId,
        I_OwnGitHubRepositoryNameGet repositoryId,
        I_OwnGitHubIssueIdGet issueId,
        I_OwnGitHubAuthPrivateApiKeyGet privateAuthApiKey,
        TextDownloadedByCoroutine callback)
    {

        yield return GitHubDownloadWebPageTool.MakeRequest(
            callback,
            string.Format("https://api.github.com/repos/{0}/{1}/issues/{2}/comments",
            userId.GetGitHubUserName(),
            repositoryId.GetGitHubRepositoryName(),
            issueId.GetGitHubIssueId()
            ), privateAuthApiKey.GetGitHubAuthPrivateApiKey()
            );
    }

    public static IEnumerator FetchIssueComments(
         I_OwnGitHubUserNameGet userId,
        I_OwnGitHubRepositoryNameGet repositoryId,
        I_OwnGitHubIssueIdGet issueId,
        I_OwnGitHubAuthPrivateApiKeyGet privateAuthApiKey,
        TextDownloadedByCoroutine callback
        )
    {
        yield return GitHubDownloadWebPageTool.MakeRequest(
            callback,
            string.Format("https://api.github.com/repos/{0}/{1}/issues/{2}/comments",
            userId.GetGitHubUserName(),
            repositoryId.GetGitHubRepositoryName(),
            issueId.GetGitHubIssueId()
            ), privateAuthApiKey.GetGitHubAuthPrivateApiKey()
            );
    }


    public static IEnumerator FetchLabels(
          I_OwnGitHubUserNameGet userName,
          I_OwnGitHubRepositoryNameGet repositoryName,
          I_OwnGitHubAuthPrivateApiKeyGet apiKey,
          TextDownloadedByCoroutine textDownloaded)
    {
        string url = "https://api.github.com/repos/{0}/{1}/labels";
        url = string.Format(url, userName.GetGitHubUserName(), repositoryName.GetGitHubRepositoryName());
        yield return GitHubDownloadWebPageTool.MakeRequest(textDownloaded, url, apiKey);
    }


    public static IEnumerator FetchRepository(
        I_OwnGitHubUserNameGet userName,
        I_OwnGitHubRepositoryNameGet repositoryName,
        I_OwnGitHubAuthPrivateApiKeyGet apiKey,
        TextDownloadedByCoroutine textDownloaded)
    {
        string url = "https://api.github.com/repos/{0}/{1}";
        url = string.Format(url, userName.GetGitHubUserName(), repositoryName.GetGitHubRepositoryName());
        yield return GitHubDownloadWebPageTool.MakeRequest(textDownloaded, url, apiKey.GetGitHubAuthPrivateApiKey());
    }


    public static IEnumerator FetchUser(
      I_OwnGitHubUserNameGet userName,
      I_OwnGitHubAuthPrivateApiKeyGet apiKey,
      TextDownloadedByCoroutine textDownloaded)
    {
        string url = "https://api.github.com/users/{0}";
        url = string.Format(url, userName.GetGitHubUserName());
        yield return GitHubDownloadWebPageTool.MakeRequest(textDownloaded, url, apiKey.GetGitHubAuthPrivateApiKey());
    }


}

public class Holding {

    // https://docs.github.com/en/rest/issues/labels?apiVersion=2022-11-28#add-labels-to-an-issue
    ///repos/{owner}/{repo}/issues/{issue_number}/labels
    ///https://docs.github.com/en/rest/issues/labels?apiVersion=2022-11-28#add-labels-to-an-issue
    ////repos/{owner}/{repo}/issues/{issue_number}/labels 
    ///https://docs.github.com/en/rest/issues/labels?apiVersion=2022-11-28#remove-a-label-from-an-issue
    ////repos/{owner}/{repo}/issues/{issue_number}/labels/{name}
    ///
    //https://docs.github.com/en/rest/issues/labels?apiVersion=2022-11-28#list-labels-for-a-repository
}
