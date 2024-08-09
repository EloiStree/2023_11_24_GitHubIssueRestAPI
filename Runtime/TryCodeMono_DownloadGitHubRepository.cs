using UnityEngine;

public class TryCodeMono_DownloadGitHubRepository : MonoBehaviour
{
    public A_GitHubApiKeyMono m_apiKey;
    public TextDownloadedByCoroutine m_textDownloaded;
    public STRUCT_UserRepository m_userRepository;


    [ContextMenu("Refresh")]
    void Refresh()
    {
        StartCoroutine(GitHubFetchJsonTool.FetchRepository(m_userRepository, m_userRepository, m_apiKey, m_textDownloaded));
    }

}
