using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using JetBrains.Annotations;
using System.Net.Http;
using System.Threading.Tasks;
public class TryCodeMono_AddRemoveLabel : MonoBehaviour
{
    //https://docs.github.com/en/rest/issues/issues?apiVersion=2022-11-28#update-an-issue
    public A_GitHubApiKeyMono m_apiKey;
    public STRUCT_UserRepository m_userRepository;
    public int m_issueNumber;
    public TextDownloadedByCoroutine m_callBack;
    public string m_label ="bug";

    [ContextMenu("Add label")]
    void AddLabel()
    {

        StartCoroutine(GitHubPushRequestTool.AddLabelToIssue(
            m_userRepository.GetGitHubUserName(),
            m_userRepository.GetGitHubRepositoryName()
            , m_issueNumber.ToString(),
            m_label,
            m_apiKey.GetGitHubAuthPrivateApiKey(),
            m_callBack
            ));
    }
    [ContextMenu("Remove label")]
    void RemoveLabel()
    {

        StartCoroutine(GitHubPushRequestTool.RemoveLabelFromIssue(
            m_userRepository.GetGitHubUserName(),
            m_userRepository.GetGitHubRepositoryName()
            , m_issueNumber.ToString(),
            m_label,
            m_apiKey.GetGitHubAuthPrivateApiKey(),
            m_callBack
            ));
    }
}

/// <summary>
/// I am a class that containt the payload for the GitHub API to be use by @GitHubPushRequestTool
/// </summary>
public partial class GitHubPushRequestPayload {
    [System.Serializable]
    public class PayloadGitHub_AddLabels
    {
        public string[] labels;
    }
    [System.Serializable]
    public class PayloadGitHub_RemoveLabels
    {

        public string[] labels;
    }

}



/// <summary>
/// I am a class that containt the methods to interact with the GitHub API from coroutine push request to the server
/// </summary>
public partial class GitHubPushRequestTool {



    public static IEnumerator AddLabelToIssue(string repoOwner, string repoName, string issueNumber, string label, string personalAccessToken, TextDownloadedByCoroutine callback)
    {
        callback.m_isCoroutineDone = false;
        string url = $"https://api.github.com/repos/{repoOwner}/{repoName}/issues/{issueNumber}/labels";

        GitHubPushRequestPayload.PayloadGitHub_AddLabels add = new GitHubPushRequestPayload.PayloadGitHub_AddLabels();
        add.labels = new string[] { label };
        string jsonPayload = JsonUtility.ToJson(add);
        Debug.Log("json:" + jsonPayload);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("User-Agent", "UnityGitHubClient");
        request.SetRequestHeader("Authorization", $"Bearer {personalAccessToken}");


        yield return request.SendWebRequest();


        // Handle the response
        if (request.result == UnityWebRequest.Result.Success)
        {
            callback.m_hadError = false;
            callback.m_error = "";
        }
        else
        {
            callback.m_hadError = true;
            callback.m_error = $"Failed to add label: {request.error}";
        }

        callback.m_text = request.downloadHandler.text;
        callback.m_lastLoadedDate = DateTime.Now.ToString();
        callback.m_isCoroutineDone = true;

        request.Dispose();
    }


    

    static public IEnumerator RemoveLabelFromIssue(string repoOwner, string repoName, string issueNumber, string label, string personalAccessToken, TextDownloadedByCoroutine callback)
    {

        callback.m_isCoroutineDone = false;
        string url = $"https://api.github.com/repos/{repoOwner}/{repoName}/issues/{issueNumber}/labels/{label}";

        //GitHubPushRequestPayload.PayloadGitHub_AddLabels add = new GitHubPushRequestPayload.PayloadGitHub_AddLabels();
        //add.labels = label;
//        string jsonPayload = JsonUtility.ToJson(add);
        string jsonPayload ="";

        UnityWebRequest request = new UnityWebRequest(url, "DELETE");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("User-Agent", "UnityGitHubClient");
        request.SetRequestHeader("Authorization", $"Bearer {personalAccessToken}");

        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            callback.m_hadError = false;
            callback.m_error = "";
        }
        else
        {
           
            callback.m_hadError = true;
            callback.m_error = $"Failed to remove label: {request.error}";
        }
        callback.m_text = request.downloadHandler.text;
        callback.m_lastLoadedDate = DateTime.Now.ToString();
        callback.m_isCoroutineDone = true;
        request.Dispose();
    }

}