public class UnprotectedGitHubApiKeyMono : A_GitHubApiKeyMono
{
    public string m_apiKey;
    public override string GetGitHubAuthPrivateApiKey()
    {
        return m_apiKey;
    }
}
