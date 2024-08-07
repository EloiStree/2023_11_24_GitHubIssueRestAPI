using UnityEngine;

public abstract class A_GitHubApiKeyMono : MonoBehaviour, I_OwnGitHubAuthPrivateApiKeyGet
{
    public abstract string GetGitHubAuthPrivateApiKey();

}
