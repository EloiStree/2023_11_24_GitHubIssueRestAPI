[System.Serializable]
public class TextDownloadedByCoroutine : I_OwnGitHubWebRequestCallBack
{
    public string m_requestUrlUsed;
    public string m_text;
    public string m_error;
    public bool m_hadError;
    public bool m_isCoroutineDone;
    public string m_lastLoadedDate;

    public void GetReceivedText(out string text)
    {
        text = m_text;
    }

    public void GetRequestUrl(out string url)
    {
        url= m_requestUrlUsed;
    }
}
