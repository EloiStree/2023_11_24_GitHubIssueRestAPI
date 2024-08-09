public struct STRUCT_GitHubLabelId: I_OwnGitHubLabelNamedAndIdGet {

    public string m_labelName;
    public int m_labelId;

    public int GetLabelId()
    {
        return m_labelId;
    }

    public string GetLabelName()
    {
return m_labelName;
            }
}
