using System.IO;
using UnityEngine;

public class LocalComputerGitHubApiKeyMono : A_GitHubApiKeyMono
{
    public string m_fileName="GitHubApiKey.txt";

    public string m_copyPastYourApiKeyHere;
    private string m_lastApiKey;

    private void OnValidate()
    {
        if (m_copyPastYourApiKeyHere.Length > 0) { 
            SetApiKey(m_copyPastYourApiKeyHere);
            m_copyPastYourApiKeyHere = "";
            Debug.Log("Api Key set: "+ GetStorePath());
        }
    }

    private void SetApiKey(string m_copyPastYourApiKeyHere)
    {
        m_lastApiKey = m_copyPastYourApiKeyHere;
        File.WriteAllText(GetStorePath(), m_copyPastYourApiKeyHere);
    }

    public string GetStorePath()
    {
        return Application.persistentDataPath + "/" + m_fileName;
    }
    public void CreateFileIfNotThere() { 
    
        if (!File.Exists(GetStorePath()))
        {
            File.WriteAllText(GetStorePath(), "");
        }
    }
    private void Awake()
    {
        LoadTheKey();
    }
    
    public override string GetGitHubAuthPrivateApiKey()
    {
        if (string.IsNullOrEmpty(m_lastApiKey))
            LoadTheKey();
        return m_lastApiKey;
    }

    private void LoadTheKey()
    {
        CreateFileIfNotThere();
        m_lastApiKey = File.ReadAllText(GetStorePath());
    }

    [ContextMenu("Open File")]
    public void OpenFile()
    {

        Application.OpenURL(GetStorePath());
    }
    [ContextMenu("Open Directory")]
    public void OpenDirectory()
    {

        Application.OpenURL(Application.persistentDataPath);
    }
}
