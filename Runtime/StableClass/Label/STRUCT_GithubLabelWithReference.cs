public struct STRUCT_GithubLabelWithReference : I_OwnLabelWithOrigine{
    public STRUCT_GitHubLabelWithSideInfo m_label;
    public STRUCT_UserRepository m_userRepository;

    public string GetGitHubRepositoryName()
    {
        return m_userRepository.GetGitHubRepositoryName();
    }

    public string GetGitHubUserName()
    {
        return m_userRepository.GetGitHubUserName();
    }

    public int GetLabelId()
    {
        return m_label.GetLabelId();
    }

    public string GetLabelName()
    {
        return m_label.GetLabelName();
    }
}
