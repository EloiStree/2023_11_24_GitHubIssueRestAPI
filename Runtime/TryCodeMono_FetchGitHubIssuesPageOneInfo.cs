using UnityEngine;

public class TryCodeMono_FetchGitHubIssuesPageOneInfo : MonoBehaviour
{
    public A_GitHubApiKeyMono m_apiKey;
    public TextDownloadedByCoroutine m_textDownloaded;
    public STRUCT_UserRepository m_userRepository;

    public string m_requestFormat = "https://api.github.com/repos/{0}/{1}/issues";

    [ContextMenu("Download page")]
    void DownloadIssuesFirstPage()
    {
        string url = string.Format(m_requestFormat, m_userRepository.GetGitHubUserName(), m_userRepository.GetGitHubRepositoryName());
        StartCoroutine(GitHubDownloadWebPageTool.MakeRequest(m_textDownloaded, url, m_apiKey.GetGitHubAuthPrivateApiKey()));
    }

}
