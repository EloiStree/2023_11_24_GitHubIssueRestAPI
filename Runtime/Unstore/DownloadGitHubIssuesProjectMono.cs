using Eloi;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadGitHubIssuesProjectMono : MonoBehaviour
{
    public string m_userId="EloiStree";
    public string m_respositoryId= "HelloLynxR1";
    public int m_issueId=1;
    public string m_respAPIFormatComment = "https://api.github.com/repos/{0}/{1}/issues/{2}/comments";
    public string m_respAPIFormatEvents = "https://api.github.com/repos/EloiStree/HelloLynxR1/issues/events";
    public string m_respAPIFormatIssues = "https://api.github.com/repos/{0}/{1}/issues/{2}";
    public string m_respAPIFormatPages = "https://api.github.com/repos/{0}/{1}/issues?per_page={3}&page={2}";
    public string m_respAPIFormatPagesComments = "https://api.github.com/repos/{0}/{1}/issues/comments/?per_page={3}&page={2}";
    public string m_rateLimite = "https://docs.github.com/rest/overview/resources-in-the-rest-api#rate-limiting";


    // https://api.github.com/repos/EloiStree/HelloLynxR1/issues/events?per_page=1&page=4
    // https://api.github.com/repos/EloiStree/HelloLynxR1/issues/events/2
    // All issue in a json
    // https://api.github.com/repos/EloiStree/HelloLynxR1/issues/events
    //Get info on issue from id
    // https://api.github.com/repos/EloiStree/HelloLynxR1/issues/events/10866976185
    // https://api.github.com/repos/EloiStree/HelloLynxR1/issues/2


    public int m_elementPerPage=20;
    public int m_pageToLoad = 100;
    string m_jsonResult;
    public Eloi.A_PathTypeAbsoluteDirectoryMono m_whereToStoreitDirectory;
    [ContextMenu("Refresh")]

    public void Refresh()
    {
        StartCoroutine(MakeRequest());
    }
    [ContextMenu("Refresh Pages")]

    public void RefreshPages()
    {

        StartCoroutine(MakeRequestAllPage());
    }
   [ContextMenu("Refresh All")]
    public void RefreshAllIssues()
    {

        StartCoroutine(MakeRequestAll());
    }
    IEnumerator MakeRequestAll()
    {
        m_endReach = false;
        while (m_issueId < 200)//!m_endReach)
        {
            yield return new WaitForEndOfFrame();
            yield return MakeRequest();
            m_issueId++;

        }

    }
    IEnumerator MakeRequestAllPage()
    {
        m_endReach = false;
        for (int i = 1; i < m_pageToLoad && !m_endReach; i++)
        {
            yield return new WaitForSeconds(1);
            yield return MakeRequestPage(i);

        }

    }
    public bool m_useAuthToken;
    public string m_authToken = "";
    public bool m_endReach = false;

    public string GetRepoRelative() { return $"/GitIssues/{m_userId}/{m_respositoryId}/"; }
    IEnumerator MakeRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(string.Format(m_respAPIFormatIssues, m_userId, m_respositoryId, m_issueId)))
        {
            if(m_useAuthToken)
                webRequest.SetRequestHeader("Authorization", "Bearer " + m_authToken);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                m_endReach = true;
                yield return null;
            }
            else
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
                string t = webRequest.downloadHandler.text;
                m_jsonResult = webRequest.downloadHandler.text;
                Eloi.I_PathTypeAbsoluteFileGet file = new Eloi.PathTypeAbsoluteFile(m_whereToStoreitDirectory.GetPath() +
                    string.Format(GetRepoRelative ()+ "I{0:0000}.json", m_issueId));
                if (m_whereToStoreitDirectory)
                    AbsoluteTypePathTool.OverwriteFile(file, m_jsonResult);

            }
        }
        using (UnityWebRequest webRequest = UnityWebRequest.Get(string.Format(m_respAPIFormatComment, m_userId, m_respositoryId, m_issueId)))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                m_endReach = true;
                yield return null;
            }
            else
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
                string t = webRequest.downloadHandler.text;
                Eloi.I_PathTypeAbsoluteFileGet file = new Eloi.PathTypeAbsoluteFile(m_whereToStoreitDirectory.GetPath() +
                    string.Format(GetRepoRelative() + "C{0:0000}.json", m_issueId));
                if (m_whereToStoreitDirectory)
                    AbsoluteTypePathTool.OverwriteFile(file, m_jsonResult);

            }
        }
    }
    IEnumerator MakeRequestPage(int page)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(string.Format(m_respAPIFormatPages, m_userId, m_respositoryId, page, m_elementPerPage)))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer " + m_authToken);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                m_endReach = true;
                yield return null;
            }
            else
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
                string t = webRequest.downloadHandler.text;
                m_jsonResult = webRequest.downloadHandler.text;
                Eloi.I_PathTypeAbsoluteFileGet file = new Eloi.PathTypeAbsoluteFile(m_whereToStoreitDirectory.GetPath() +
                    string.Format(GetRepoRelative() + "P{0:0000}.json", page));
                if (m_whereToStoreitDirectory)
                    AbsoluteTypePathTool.OverwriteFile(file, m_jsonResult);
                if (m_jsonResult.Length < 500)
                    m_endReach = true;
            }
        }
        
    }


}
