using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitHubOpenUrlUtility : MonoBehaviour
{

    public static void OpenUrlAtComment(string owner, string repo, int issueNumber, ulong commentId)
    {
        OpenUrl(BuildAtCommentUrl(owner, repo, issueNumber, commentId));
    }

    public static string BuildAtCommentUrl(string owner, string repo, int issueNumber, ulong commentId)
    {
        return string.Format($"https://github.com/{owner}/{repo}/issues/{issueNumber}#issuecomment-{commentId}");
    }

    public static void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
    public static void OpenIssue(string owner, string repo, int issueNumber)
    {
        OpenUrl(BuildIssueUrl(owner, repo, issueNumber));
    }

    public static string BuildIssueUrl(string owner, string repo, int issueNumber)
    {
        return string.Format($"https://github.com/{owner}/{repo}/issues/{issueNumber}");
    }

    public static void OpenRepository(string owner, string repo)
    {
        OpenUrl(string.Format($"https://github.com/{owner}/{repo}"));
    }
    public static void OpenUser(string owner)
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
    public static void OpenUrlToSubmitIssue(I_OwnGitHubRepositoryReferenceGet toOpen, string title, string body)
    {
        OpenUrlToSubmitIssue(toOpen.GetGitHubUserName(), toOpen.GetGitHubRepositoryName(), title, body);
    }

    public static void OpenUrl(I_OwnGitHubIssueReferenceGet toOpen)
    {
        OpenIssue(toOpen.GetGitHubRepositoryName(), toOpen.GetGitHubUserName(), toOpen.GetGitHubIssueId());
    }
    public static void OpenUrl(I_OwnGitHubRepositoryReferenceGet toOpen)
    {
        OpenRepository(toOpen.GetGitHubRepositoryName(), toOpen.GetGitHubUserName());
    }
    public static void OpenUrl(I_OwnGitHubUserNameGet toOpen)
    {
        OpenUser(toOpen.GetGitHubUserName());
    }

    public static void OpenUrl(I_OwnGitHubCommentReferenceGet toOpen) { 
    
        OpenUrlAtComment(toOpen.GetGitHubUserName(), toOpen.GetGitHubRepositoryName(), toOpen.GetGitHubIssueId(), toOpen.GetGitHubCommentId());
    }

    public static string BuildIssueShortcut(I_OwnGitHubIssueReferenceGet userRepoIssueId)
    {
        return   $"{userRepoIssueId.GetGitHubUserName()}/{userRepoIssueId.GetGitHubRepositoryName()}#{userRepoIssueId.GetGitHubIssueId()}";
      }

    public static string BuildIssueUrl(I_OwnGitHubIssueReferenceGet userRepoIssueId)
    {
         return  BuildIssueUrl(userRepoIssueId.GetGitHubUserName(), userRepoIssueId.GetGitHubRepositoryName(), userRepoIssueId.GetGitHubIssueId());

    }
}
