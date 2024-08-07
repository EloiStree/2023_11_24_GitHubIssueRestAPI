using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitHubOpenUrlUtility : MonoBehaviour
{

    public static void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
    public static void OpenIssueUrl(string owner, string repo, int issueNumber)
    {
        OpenUrl(string.Format($"https://github.com/{owner}/{repo}/issues/{issueNumber}"));
    }
    public static void OpenRepositoryUrl(string owner, string repo)
    {
        OpenUrl(string.Format($"https://github.com/{owner}/{repo}"));
    }
    public static void OpenUserUrl(string owner)
    {
        OpenUrl(string.Format($"https://github.com/{owner}"));
    }
    public static void OpenUrlToSubmitIssue(string owner, string repo, string title, string body)
    {
        OpenUrl(string.Format($"https://github.com/{owner}/{repo}/issues/new?title={title}&body={body}"));
    }

    public static void OpenUrlToReleaseOfRepository(string owner, string repo)
    {
        OpenUrl(string.Format($"https://github.com/{owner}/{repo}/releases"));
    }
    public static void OpenUrlToSubmitIssue(I_OwnGitHubRepositoryOfUserGet toOpen, string title, string body)
    {
        OpenUrlToSubmitIssue(toOpen.GetGitHubUserName(), toOpen.GetGitHubRepositoryName(), title, body);
    }

    public static void OpenUrl(I_OwnGitHubRepositoryIssueIdGet toOpen)
    {
        OpenIssueUrl(toOpen.GetGitHubRepositoryName(), toOpen.GetGitHubUserName(), toOpen.GetGitHubIssueId());
    }
    public static void OpenUrl(I_OwnGitHubRepositoryOfUserGet toOpen)
    {
        OpenRepositoryUrl(toOpen.GetGitHubRepositoryName(), toOpen.GetGitHubUserName());
    }
    public static void OpenUrl(I_OwnGitHubUserNameGet toOpen)
    {
        OpenUserUrl(toOpen.GetGitHubUserName());
    }

}
