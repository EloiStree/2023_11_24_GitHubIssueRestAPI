using Eloi;
using System.IO;

public class GitHubStoragePathUtility {

    public static void GetIssueDirecotryPath(I_PathTypeAbsoluteDirectoryGet root, I_OwnGitHubRepositoryIssueIdGet issueReference, out I_PathTypeAbsoluteDirectoryGet pathBuild)
    {
        pathBuild = new STRUCT_AbsoluteDirectoryPath(Path.Combine(root.GetPath(),
            issueReference.GetGitHubRepositoryName(),
            issueReference.GetGitHubUserName(),
            issueReference.GetGitHubIssueId().ToString()));

    }

  
    public static void GetIssueMainTextPathJson(I_PathTypeAbsoluteDirectoryGet root, I_OwnGitHubRepositoryIssueIdGet issueReference, out I_PathTypeAbsoluteFileGet pathBuild)
    => GetIssueStoragePath(root, issueReference, out pathBuild, "issue", "json");
    public static void GetIssueMainTextPathMarkdown(I_PathTypeAbsoluteDirectoryGet root, I_OwnGitHubRepositoryIssueIdGet issueReference, out I_PathTypeAbsoluteFileGet pathBuild)
    => GetIssueStoragePath(root, issueReference, out pathBuild, "issue", "md");

    public static void GetIssueCommentsPathJson(I_PathTypeAbsoluteDirectoryGet root, I_OwnGitHubRepositoryIssueIdGet issueReference, out I_PathTypeAbsoluteFileGet pathBuild)
    => GetIssueStoragePath(root, issueReference, out pathBuild, "comments", "json");
    public static void GetIssueCommentsPathMarkdown(I_PathTypeAbsoluteDirectoryGet root, I_OwnGitHubRepositoryIssueIdGet issueReference, out I_PathTypeAbsoluteFileGet pathBuild)
    => GetIssueStoragePath(root, issueReference, out pathBuild, "comments", "md");

    public static void GetIssueStoragePath(
        I_PathTypeAbsoluteDirectoryGet root,
        I_OwnGitHubRepositoryIssueIdGet issueReference,
        out I_PathTypeAbsoluteFileGet pathBuild,
        string fileName = "unnamed",
        string fileExtension = "json"
        )
    {
        pathBuild = new STRUCT_AbsoluteFilePath(Path.Combine(root.GetPath(),
            issueReference.GetGitHubRepositoryName(),
            issueReference.GetGitHubUserName(), 
            issueReference.GetGitHubIssueId().ToString(),
            fileName+ "."+  fileExtension.Trim('.')));
    }
}