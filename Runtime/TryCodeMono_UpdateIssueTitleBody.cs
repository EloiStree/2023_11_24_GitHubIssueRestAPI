using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using static GitHubPushRequestPayload;

public class TryCodeMono_UpdateIssueTitleBody : MonoBehaviour
{
    //https://docs.github.com/en/rest/issues/issues?apiVersion=2022-11-28#update-an-issue


    public A_GitHubApiKeyMono m_apiKey;
    public STRUCT_UserRepository m_userRepository;
    public int m_issueNumber;
    public string m_title;
    [TextArea(2, 6)]
    public string m_body;

    public TextDownloadedByCoroutine m_callBack;

    [ContextMenu("Update body")]
    void UpdateBody()
    {
        StartCoroutine(GitHubPushRequestTool.UpdateIssuePatch(
            new PayloadGitHub_UpdateIssueBody { body = m_body },
            m_userRepository.GetGitHubUserName(),
            m_userRepository.GetGitHubRepositoryName(),
             m_issueNumber,
            m_apiKey.GetGitHubAuthPrivateApiKey(),
            m_callBack
            ));
    }
    [ContextMenu("Update Title")]
    void UpdateTitle()
    {
        StartCoroutine(GitHubPushRequestTool.UpdateIssuePatch(
            new PayloadGitHub_UpdateIssueTitle { title = m_title },
            m_userRepository.GetGitHubUserName(),
            m_userRepository.GetGitHubRepositoryName(),
             m_issueNumber,
            m_apiKey.GetGitHubAuthPrivateApiKey(),
            m_callBack
            ));
    }
    [ContextMenu("Update Title And Body")]
    void UpdateBodyTitle()
    {
        StartCoroutine(GitHubPushRequestTool.UpdateIssuePatch(
            new PayloadGitHub_UpdateIssueTitleBody { body = m_body, title= m_title },
            m_userRepository.GetGitHubUserName(),
            m_userRepository.GetGitHubRepositoryName(),
             m_issueNumber,
            m_apiKey.GetGitHubAuthPrivateApiKey(),
            m_callBack
            ));
    }

}

public partial class GitHubPushRequestPayload
{

    [System.Serializable]
    public class PayloadGitHub_UpdateIssueTitleBody
    {
        public string title;
        public string body;
    }

    [System.Serializable]
    public class PayloadGitHub_UpdateIssueTitle
    {
        public string title;
    }
    [System.Serializable]
    public class PayloadGitHub_UpdateIssueBody
    {
        public string body;
    }
}
public partial class GitHubPushRequestTool
{

    static public IEnumerator UpdateIssuePatch(PayloadGitHub_UpdateIssueTitleBody jsonPayload, string owner, string repo, int issueNumber, string token, TextDownloadedByCoroutine callBack)
    {
        yield return UpdateIssuePatch(JsonUtility.ToJson(jsonPayload, true), owner, repo, issueNumber, token, callBack);
    }
    static public IEnumerator UpdateIssuePatch(PayloadGitHub_UpdateIssueTitle jsonPayload, string owner, string repo, int issueNumber,  string token, TextDownloadedByCoroutine callBack)
    {
        yield return UpdateIssuePatch(JsonUtility.ToJson(jsonPayload, true), owner, repo, issueNumber, token, callBack);
    }
    static public IEnumerator UpdateIssuePatch(PayloadGitHub_UpdateIssueBody jsonPayload, string owner, string repo, int issueNumber,  string token, TextDownloadedByCoroutine callBack)
    {
        yield return UpdateIssuePatch(JsonUtility.ToJson(jsonPayload, true), owner, repo, issueNumber, token, callBack);
    }

    static public IEnumerator UpdateIssuePatch(string jsonPayload, string owner, string repo, int issueNumber, string token, TextDownloadedByCoroutine callBack)
    {

        string url = $"https://api.github.com/repos/{owner}/{repo}/issues/{issueNumber}";
        UnityWebRequest request = new UnityWebRequest(url, "PATCH");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("User-Agent", "UnityGitHubClient");
        request.SetRequestHeader("Authorization", $"Bearer {token}");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            callBack.m_hadError = false;
            callBack.m_error = "";
        }
        else
        {
            callBack.m_hadError = true;
            callBack.m_error = request.error;
        }

        callBack.m_text = request.downloadHandler.text;
        callBack.m_lastLoadedDate = DateTime.Now.ToString();
        callBack.m_isCoroutineDone = true;
        request.Dispose();
    }
}