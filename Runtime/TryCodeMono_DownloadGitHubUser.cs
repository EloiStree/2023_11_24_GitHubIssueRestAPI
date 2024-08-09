using UnityEngine;

public class TryCodeMono_DownloadGitHubUser : MonoBehaviour
{
    public A_GitHubApiKeyMono m_apiKey;
    public TextDownloadedByCoroutine m_textDownloaded;
    public STRUCT_GitHubUser m_userRepository;


    [ContextMenu("Refresh")]
    void Refresh()
    {
        StartCoroutine(GitHubFetchJsonTool.FetchUser(m_userRepository, m_apiKey, m_textDownloaded));
    }

}
