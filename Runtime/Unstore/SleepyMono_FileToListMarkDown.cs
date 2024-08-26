using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class SleepyMono_FileToListMarkDown : MonoBehaviour
{

    public A_PathTypeAbsoluteDirectoryMono m_root;
    public A_PathTypeAbsoluteFileMono m_markdownResult;
    public A_PathTypeAbsoluteFileMono m_markdownIssueCommentResult;
    public A_PathTypeAbsoluteFileMono m_markdownIssueTitle;

    public string m_startWith = "Keyword:";
    public A_PathTypeAbsoluteFileMono m_markdownIssueTitleStartWith;

    public string[] m_issueJson;
    public string[] m_commentsJson;

    public string[] m_issueJsonText;
    public string[] m_commentsJsonText;

    public JsonFilter_GenericTesting<JsonFilter_IssueLight.Issue> m_filterIssue
        = new JsonFilter_GenericTesting<JsonFilter_IssueLight.Issue>();

    public JsonFilter_GenericArray<JsonFilter_IssueCommentLight.GitHubIssueComment> m_filterComment 
        = new JsonFilter_GenericArray<JsonFilter_IssueCommentLight.GitHubIssueComment>();

    public List<JsonFilter_IssueLight.Issue> m_issues = new List<JsonFilter_IssueLight.Issue>();
    public List<JsonFilter_IssueCommentLight.GitHubIssueComment> m_comments = new List<JsonFilter_IssueCommentLight.GitHubIssueComment>();

    public List<IssueReferenceToValueJsonIssue> m_issuesReference = new List<IssueReferenceToValueJsonIssue>();
    public List<IssueReferenceToValueJsonComment> m_commentsReference = new List<IssueReferenceToValueJsonComment>();

    public RepositoryIssueCommentDico m_repositoryIssueCommentDico = new RepositoryIssueCommentDico();

    [ContextMenu("Refresh files List")]
    public void RefreshFilesList()
    {
        m_issueJson = Directory.GetFiles(m_root.GetPath(), "*issue.json", SearchOption.AllDirectories);
        m_commentsJson = Directory.GetFiles(m_root.GetPath(), "*comments.json", SearchOption.AllDirectories);
        m_issueJsonText = new string[m_issueJson.Length];
        m_commentsJsonText = new string[m_commentsJson.Length];
        for (int i = 0; i < m_issueJson.Length; i++)
        {
            m_issueJsonText[i] = File.ReadAllText(m_issueJson[i]);
        }
        for (int i = 0; i < m_commentsJson.Length; i++)
        {
            m_commentsJsonText[i] = File.ReadAllText(m_commentsJson[i]);
        }
    }

    public string pathRoot;
    [ContextMenu("Refresh Text to filtered class")]
    public void RefreshTextToFilteredClass()
    {
        pathRoot = m_root.GetPath();
        STRUCT_UserRepositoryIssueId projectReference= new STRUCT_UserRepositoryIssueId();
       m_issues.Clear();
        m_comments.Clear();
        m_issuesReference.Clear();
        m_commentsReference.Clear();
          for (int i = 0; i < m_issueJsonText.Length; i++)
          {

            Get(in pathRoot,in  m_issueJson[i], ref projectReference);
            m_filterIssue.ParseJsonRawToFiltered(m_issueJsonText[i], out bool parsed, out var issue, out _);
            if(parsed)
            {
                m_issues.Add(issue);
                m_issuesReference.Add(new IssueReferenceToValueJsonIssue(projectReference, issue));
                m_repositoryIssueCommentDico.Add(projectReference, projectReference, issue);
            }
          }
          for (int i = 0; i < m_commentsJsonText.Length; i++)
          {
            Get(in pathRoot,in m_commentsJson[i], ref projectReference);
            m_filterComment.ParseJsonRawToFiltered(m_commentsJsonText[i], out bool parsed, out var comments, out _);
            if (parsed)
            {
                m_comments.AddRange(comments);
                m_commentsReference.Add(new IssueReferenceToValueJsonComment(projectReference, comments));
                m_repositoryIssueCommentDico.Add(projectReference, projectReference, projectReference.GetGitHubIssueId(), comments);
            }
          }
        
    }

    private void Get(in string root, in string filepath, ref STRUCT_UserRepositoryIssueId projectReference)
    {
        if (filepath.Length < root.Length)
        {
            Debug.LogError($"The file {filepath} is not in the right folder");
            return;
        }
        string relative = filepath.Substring(root.Length).Trim(new char[] { '\\', '/' });
        string[] split = relative.Split(new char[] { '\\','/'});
        //Debug.LogError($"Relative: " + relative);
        //Debug.LogError($"Join: " + string.Join("|", split));

        if (split.Length < 3)
        {
            projectReference.m_usernameId = "";
            projectReference.m_respositoryId = "";
            projectReference.m_issueId = 1;
            Debug.LogError($"The file {filepath} is not in the right folder");
            return;
        }
        projectReference.m_usernameId = split[0];
        projectReference.m_respositoryId = split[1];
        if(int.TryParse(split[2], out int result))
            projectReference.m_issueId = result;
    }

    [ContextMenu("Build MarkDown")]
    public void BuildMarkDown()
    {
        StringBuilder justTitle = new StringBuilder();
        StringBuilder justTitleFiltered = new StringBuilder();
        RefreshTextToFilteredClass();
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("# Issues");
        foreach (var item in m_issues)
        {
            builder.AppendLine("## Issue");
            builder.AppendLine($"- Title: {item.title}");
            builder.AppendLine($"- Body: {item.body}");
            builder.AppendLine($"- Number: {item.number}");
            builder.AppendLine($"- State: {item.state}");
            builder.AppendLine($"- Created At: {item.created_at}");
            builder.AppendLine($"- Updated At: {item.updated_at}");
            builder.AppendLine($"- Comments: {item.comments}");
            builder.AppendLine($"- User Id: {item.user.id}");
        }

        AbsoluteTypePathTool.OverwriteFile(m_markdownResult, builder.ToString());

        builder.Clear();
        m_repositoryIssueCommentDico.m_repositoryIssueCommentList.ForEach((item) => {
            builder.AppendLine($"# Repository: {item.m_repository.m_userId}/{item.m_repository.m_respositoryId}");
            builder.AppendLine();
            builder.AppendLine($"- Issue Count: {item.m_issueCount}");
            builder.AppendLine($"- Comment Count: {item.m_commentCount}");
            foreach (var issue in item.m_issues.Values)
            {
                string url = GitHubOpenUrlUtility.BuildIssueUrl(item.m_repository.GetGitHubUserName(), item.m_repository.GetGitHubRepositoryName(), issue.number);

                builder.AppendLine($"## [{issue.number}]({url})| {issue.title}");
                justTitle.AppendLine($"- [ ] [{string.Format("{0:00000}",issue.number)}]({url})| {issue.title}");
                if (issue.title.ToLower().StartsWith(m_startWith.ToLower().Trim())) {
                    string s = string.Format("{0}/{1}#{2:00000}",
                        item.m_repository.GetGitHubUserName(),
                        item.m_repository.GetGitHubRepositoryName(),
                        issue.number);
                    justTitleFiltered.AppendLine($"- [ ] [{issue.number}]({url})| - {s} | {issue.title}");

                }
                builder.AppendLine();
                builder.AppendLine($"{issue.body}");

                if (item.m_comments.TryGetValue(issue.number, out JsonFilter_IssueCommentLight.GitHubIssueComment[] comments)) {
                    foreach (var comment in comments)
                    {
                        string urlComment = GitHubOpenUrlUtility.BuildAtCommentUrl(item.m_repository.GetGitHubUserName(), item.m_repository.GetGitHubRepositoryName(), issue.number, comment.id);

                        builder.AppendLine($"### [{comment.id}]({urlComment})| Comment");
                        builder.AppendLine();
                        builder.AppendLine($"{comment.body}");
                    }
                }
                
            }
            
        });
        AbsoluteTypePathTool.OverwriteFile(m_markdownIssueCommentResult, builder.ToString());
        AbsoluteTypePathTool.OverwriteFile(m_markdownIssueTitle, justTitle.ToString());
        AbsoluteTypePathTool.OverwriteFile(m_markdownIssueTitleStartWith, justTitleFiltered.ToString());

    }
}

[System.Serializable]
public class IssueReferenceToValueJsonIssue : IssueReferenceToValue<JsonFilter_IssueLight.Issue>
{
    public IssueReferenceToValueJsonIssue(STRUCT_UserRepositoryIssueId reference, JsonFilter_IssueLight.Issue value)
    {
        m_uniqueId = reference.GetUniqueStringId();
        m_reference = reference;
        m_value = value;
    }
}
[System.Serializable]
public class IssueReferenceToValueJsonComment : IssueReferenceToValue<JsonFilter_IssueCommentLight.GitHubListIssueComment>
{
    public IssueReferenceToValueJsonComment(STRUCT_UserRepositoryIssueId reference, JsonFilter_IssueCommentLight.GitHubIssueComment[] value)
    {
        m_uniqueId = reference.GetUniqueStringId();
        m_reference = reference;
        m_value = new JsonFilter_IssueCommentLight.GitHubListIssueComment { comments = value };
    }
}
[System.Serializable]
public class IssueReferenceToValue<T> {

    public string m_uniqueId;
    public STRUCT_UserRepositoryIssueId m_reference;
    public T m_value;

    public T GetValue() { 
        return m_value;
    }
    public I_OwnGitHubIssueReference GetIssueReference() {
        return m_reference;
    }

}



[System.Serializable]
public class RepositoryIssueCommentDico { 

    public Dictionary<string, RepositoryIssueComment> m_repositoryIssueComment = new Dictionary<string, RepositoryIssueComment>();
    public List<RepositoryIssueComment> m_repositoryIssueCommentList = new List<RepositoryIssueComment>();

    public void Add(I_OwnGitHubUserNameGet user, I_OwnGitHubRepositoryNameGet repository, JsonFilter_IssueLight.Issue issue)
    {
        string key = GetKey(user, repository);
        if (m_repositoryIssueComment.ContainsKey(key))
        {
            m_repositoryIssueComment[key].Add(issue);
        }
        else
        {
            RepositoryIssueComment newRepo = new RepositoryIssueComment();
            newRepo.m_repository = new STRUCT_UserRepository() { 
                m_respositoryId= repository.GetGitHubRepositoryName(), 
                m_userId= user.GetGitHubUserName()};
            newRepo.Add(issue);
            m_repositoryIssueComment.Add(key, newRepo);
            m_repositoryIssueCommentList = m_repositoryIssueComment.Values.ToList();
        }
    }

    private static string GetKey(I_OwnGitHubUserNameGet user, I_OwnGitHubRepositoryNameGet repository)
    {
        return $"{user.GetGitHubUserName()}_{repository.GetGitHubRepositoryName()}";
    }

    public void Add(I_OwnGitHubUserNameGet user, I_OwnGitHubRepositoryNameGet repository, int issueId, JsonFilter_IssueCommentLight.GitHubIssueComment[] comment)
    {
        string key = GetKey(user, repository);
        if (m_repositoryIssueComment.ContainsKey(key))
        {
            m_repositoryIssueComment[key].Add(issueId, comment);
        }
        else
        {
            RepositoryIssueComment newRepo = new RepositoryIssueComment();
            newRepo.m_repository = new STRUCT_UserRepository()
            {
                m_respositoryId = repository.GetGitHubRepositoryName(),
                m_userId = user.GetGitHubUserName()
            };
            newRepo.Add(issueId, comment);
            m_repositoryIssueComment.Add(key, newRepo);
            m_repositoryIssueCommentList = m_repositoryIssueComment.Values.ToList();
        }
    }
}

[System.Serializable]
public class RepositoryIssueComment {
    public STRUCT_UserRepository m_repository;
    public int m_issueCount;
    public int m_commentCount;
    public Dictionary<int, JsonFilter_IssueCommentLight.GitHubIssueComment[]> m_comments = new Dictionary<int, JsonFilter_IssueCommentLight.GitHubIssueComment[]>();
    public Dictionary<int, JsonFilter_IssueLight.Issue> m_issues = new Dictionary<int, JsonFilter_IssueLight.Issue>();

    public void Add(JsonFilter_IssueLight.Issue issue)
    {
        if(m_issues.ContainsKey(issue.number))
            m_issues[issue.number] = issue;
        else
            m_issues.Add(issue.number, issue);
        m_issueCount= m_issues.Count;
    }
    public void Add(int issueId, JsonFilter_IssueCommentLight.GitHubIssueComment []comment)
    {
        if(m_comments.ContainsKey(issueId))
            m_comments[issueId] = comment;
        else
            m_comments.Add(issueId, comment);
        m_commentCount = m_comments.Count;
    }
}