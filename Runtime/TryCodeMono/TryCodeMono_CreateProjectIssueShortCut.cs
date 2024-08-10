using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryCodeMono_CreateProjectIssueShortCut : MonoBehaviour
{

    public STRUCT_UserRepositoryIssueId userRepoIssueId;
    public string m_referenceAsShortcut;
    public string m_referenceAsURL;

    private void OnValidate()
    {

        m_referenceAsShortcut= GitHubOpenUrlUtility.BuildIssueShortcut((I_OwnGitHubIssueReferenceGet) userRepoIssueId);
        m_referenceAsURL= GitHubOpenUrlUtility.BuildIssueUrl((I_OwnGitHubIssueReferenceGet) userRepoIssueId);
        
    }

}
