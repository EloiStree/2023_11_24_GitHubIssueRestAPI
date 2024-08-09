[System.Serializable]
public struct STRUCT_UserRepository: I_OwnGitHubRepositoryNameGet, I_OwnGitHubUserNameGet
{
    public string m_userId;
    public string m_respositoryId;
    public string GetGitHubUserName()
    {
        return m_userId;
    }
    public string GetGitHubRepositoryName()
    {
        return m_respositoryId;
    }
}


/// <summary>
/// I am a holder of a Github Integer Id common to GitHub Community
/// </summary>
public interface I_OwnGitHubGlobalIntId
{
    int GetIntId();
}
/// <summary>
/// I am a holder of a GitHub Integer Id inside a project
/// </summary>
public interface I_OwnGitHubRelativeIntId
{
    int GetSubIntId();
}
public interface I_OwnGitHubRepositoryIntID : I_OwnGitHubGlobalIntId { }
public interface I_OwnGitHubUserIntId : I_OwnGitHubGlobalIntId { }
public interface I_OwnGitHubLabelIntId : I_OwnGitHubRelativeIntId { }
public interface I_OwnGitHubIssueSubIntId : I_OwnGitHubRelativeIntId { }
public interface I_OwnGitHubCommentSubIntId : I_OwnGitHubRelativeIntId { }

public struct STRUCT_GitHubRepositoryIntID : I_OwnGitHubRepositoryIntID
{

    public int m_repositoryIntId;

    public int GetIntId()
    {
        return m_repositoryIntId; 
    }
}
public struct STRUCT_GitHubUserIntId: I_OwnGitHubUserIntId
{
    public int m_userIntId;

    public int GetIntId()
    {
        return m_userIntId; 
    }
}
public struct STRUCT_GitHubLabelIntId: I_OwnGitHubLabelIntId
{
    public int m_labelIntId;

    public int GetSubIntId()
    {
        return m_labelIntId;  
            }
}
public struct STRUCT_GitHubIssueSubIntId: I_OwnGitHubIssueSubIntId
{
    public int m_labelIntId;

    public int GetSubIntId()
    {
        return m_labelIntId;
    }
}
public struct STRUCT_GitHubCommentSubIntId: I_OwnGitHubCommentSubIntId
{
    public int m_labelIntId;

    public int GetSubIntId()
    {
        return m_labelIntId;
    }
}
