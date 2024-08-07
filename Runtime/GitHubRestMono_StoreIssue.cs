using Eloi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitHubRestMono_StoreIssue : MonoBehaviour
{
    public A_PathTypeAbsoluteDirectoryMono m_whereToStore;

    public void OverwriteJsonIssue(
        I_OwnGitHubRepositoryIssueIdGet issueRef,
        TextDownloadedByCoroutine textDownloaded) { 
    
        if(textDownloaded.m_hadError || textDownloaded.m_text.Length>0)
            return;
        GitHubStoragePathUtility.GetIssueMainTextPathJson(m_whereToStore,issueRef, out I_PathTypeAbsoluteFileGet whereToStoreFile);
        AbsoluteTypePathTool.OverwriteFile(whereToStoreFile, textDownloaded.m_text);

    }

    public void ExistsIssueFolder(
        I_OwnGitHubRepositoryIssueIdGet issueRef,
        out bool exists)
    {

        GitHubStoragePathUtility.GetIssueDirecotryPath(m_whereToStore, issueRef, out I_PathTypeAbsoluteDirectoryGet whereToStoreFolder);
        exists = AbsoluteTypePathTool.Exists(whereToStoreFolder);
    }
    public void ExistsIssueJsonFolder(
        I_OwnGitHubRepositoryIssueIdGet issueRef,
        out bool exists)
    {

        GitHubStoragePathUtility.GetIssueMainTextPathJson(m_whereToStore, issueRef, out I_PathTypeAbsoluteFileGet whereToStore);
        exists = AbsoluteTypePathTool.Exists(whereToStore);
    }
    public void ExistsIssueMarkdownFolder(
        I_OwnGitHubRepositoryIssueIdGet issueRef,
        out bool exists)
    {

        GitHubStoragePathUtility.GetIssueMainTextPathMarkdown(m_whereToStore, issueRef, out I_PathTypeAbsoluteFileGet whereToStore);
        exists = AbsoluteTypePathTool.Exists(whereToStore);
    }

}
