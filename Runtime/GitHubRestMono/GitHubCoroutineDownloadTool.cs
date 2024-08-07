using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class GitHubCoroutineDownloadTool {

        public static IEnumerator MakeRequest(TextDownloadedByCoroutine jsonTextRecovered, string urlOfRequest, string authToken)
        {
            if (jsonTextRecovered == null)
                yield break;
        jsonTextRecovered.m_requestUrlUsed = urlOfRequest;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlOfRequest))
            {
                if (!string.IsNullOrWhiteSpace(authToken))
                    webRequest.SetRequestHeader("Authorization", "Bearer " + authToken);

                yield return webRequest.SendWebRequest();

                jsonTextRecovered.m_text = webRequest.downloadHandler.text;
                jsonTextRecovered.m_error = webRequest.downloadHandler.error;
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    jsonTextRecovered.m_hadError = true;
                }
                else
                {
                    jsonTextRecovered.m_hadError = false;
                
                }
                    jsonTextRecovered.m_isCoroutineDone = true;
                    jsonTextRecovered.m_lastLoadedDate = System.DateTime.UtcNow.ToString();
        }
        }
       
    public static IEnumerator MakeRequest(TextDownloadedByCoroutine jsonTextRecovered, string urlOfRequest)
    {
        if (jsonTextRecovered == null)
            yield break;

        jsonTextRecovered.m_requestUrlUsed = urlOfRequest;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlOfRequest))
        {
            yield return webRequest.SendWebRequest();
            jsonTextRecovered.m_text = webRequest.downloadHandler.text;
            jsonTextRecovered.m_error = webRequest.downloadHandler.error;

            if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    jsonTextRecovered.m_hadError = true;

                }
                else
                {
                    jsonTextRecovered.m_hadError = false;
                
                }
                jsonTextRecovered.m_lastLoadedDate = System.DateTime.UtcNow.ToString();
                jsonTextRecovered.m_isCoroutineDone = true;
        }
    }
}
