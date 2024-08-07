using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GitHubUIMono_BasicIssue : MonoBehaviour
{


    public STRUCT_GitHubIssueBasic m_userRepositoryIssueId;

    public string m_labelsSpliter = ", ";

    [ContextMenu("Refresh UI")]
    public void RefreshUI() { 
    
        m_onRepositoryChanged.Invoke(m_userRepositoryIssueId.m_reference.m_respositoryId);
        m_onRepositoryUserNameChanged.Invoke(m_userRepositoryIssueId.m_reference.m_usernameId);
        m_onIssueIdChanged.Invoke(m_userRepositoryIssueId.m_reference.m_issueId.ToString());
        m_onTitleChanged.Invoke(m_userRepositoryIssueId.m_title);
        m_onBodyChanged.Invoke(m_userRepositoryIssueId.m_body);
        m_onCommentCountChanged.Invoke(m_userRepositoryIssueId.m_commentCount);
        m_onLabelsChanged.Invoke(m_userRepositoryIssueId.m_labels);
        m_onIsIssueOpenChanged.Invoke(m_userRepositoryIssueId.m_isIssueOpen);
        m_onCreatedByNameChanged.Invoke(m_userRepositoryIssueId.m_createdBy);
        m_onLabelsAsStringChanged.Invoke(string.Join(m_labelsSpliter, m_userRepositoryIssueId.m_labels));
    }

    [Header("Source")]
    public UnityEvent<string> m_onRepositoryUserNameChanged;
    public UnityEvent<string> m_onRepositoryChanged;
    public UnityEvent<string> m_onIssueIdChanged;

    [Header("Issue")]
    public UnityEvent<string> m_onTitleChanged;
    public UnityEvent<string> m_onBodyChanged;

    [Header("Other")]
    public UnityEvent<int> m_onCommentCountChanged;
    public UnityEvent<string[]> m_onLabelsChanged;
    public UnityEvent<string> m_onLabelsAsStringChanged;
    public UnityEvent<bool> m_onIsIssueOpenChanged;
    public UnityEvent<string> m_onCreatedByNameChanged;
}
