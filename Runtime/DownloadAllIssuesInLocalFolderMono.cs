using Eloi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DownloadAllIssuesInLocalFolderMono : MonoBehaviour
{
    public A_GitHubApiKeyMono m_apiKey;

    public float m_timeBetweenRequest = 0.5f;

    public bool m_skipExistingFile = true;
    public int m_maxIndex = 30;
    public STRUCT_UserRepository [] m_userRepository;

    public int m_failToLoadCount = 0;
    public int m_maxErrorBeforeStop = 10;
    public int m_loadingIndex = 0;
    public A_PathTypeAbsoluteDirectoryMono m_whereToStoreRoot;

    public TextDownloadedByCoroutine m_lastJsonDownloaded;

    public UnityEvent<I_OwnGitHubRepositoryNameGet, int, TextDownloadedByCoroutine> m_onIssueDownloaded;

    public bool m_loadAtStart = false;
    private void Start()
    {
        if (m_loadAtStart)
        {
            StartCoroutine(CoroutineLoadAll());
        }
    }

    public IEnumerator CoroutineLoadAll()
    {
        yield return new WaitForEndOfFrame();
        bool continueSearch = false;
        STRUCT_UserRepositoryIssueId reference = new STRUCT_UserRepositoryIssueId();
        foreach (var item in m_userRepository)
        {
            m_loadingIndex = 0;
            m_failToLoadCount = 0;
            reference.m_respositoryId = item.m_respositoryId;
            reference.m_usernameId = item.m_userId;


            continueSearch = true;
            for (int i = 1; i < m_maxIndex && continueSearch; i++)
            {
                m_loadingIndex = i;
                STRUCT_IssueId index= new STRUCT_IssueId(m_loadingIndex);
                reference.m_issueId = index.GetGitHubIssueId();
                GitHubStoragePathUtility.GetIssueMainTextPathJson(
                        m_whereToStoreRoot, reference,
                        out I_PathTypeAbsoluteFileGet whereToStoreFile);

                if (m_skipExistingFile) { 
                   if( AbsoluteTypePathTool.Exists(whereToStoreFile))
                    {

                        continue;
                    }
                }
            yield return new WaitForSeconds(m_timeBetweenRequest);
            yield return GitHubRest_RequestIssueJson.MakeRequestIssue
                (m_lastJsonDownloaded,
                 item, item, index, m_apiKey);

                if (m_lastJsonDownloaded.m_hadError || m_lastJsonDownloaded.m_error.Length>0)
                {
                    if (m_lastJsonDownloaded.m_text.IndexOf("\"status\": \"404\"")>-1 && 
                        m_lastJsonDownloaded.m_text.IndexOf("#get-an-issue") > -1) {
                        //{
                        //    "message": "Not Found",
                        //  "documentation_url": "https://docs.github.com/rest/issues/issues#get-an-issue",
                        //  "status": "404"
                        ////    }
                        //Debug.Log("Deleted issue");
                        //AbsoluteTypePathTool.OverwriteFile(whereToStoreFile, "Deleted");
                    }
                    m_failToLoadCount++;
                    if (m_failToLoadCount >= m_maxErrorBeforeStop)
                    {
                        continueSearch = false;
                    }
                }
                else {
                    m_failToLoadCount = 0;

                }

                if (!m_lastJsonDownloaded.m_hadError) {
                    

                    AbsoluteTypePathTool.OverwriteFile(whereToStoreFile, m_lastJsonDownloaded.m_text);
                }
             
            }
            
        }

        
    }

}
