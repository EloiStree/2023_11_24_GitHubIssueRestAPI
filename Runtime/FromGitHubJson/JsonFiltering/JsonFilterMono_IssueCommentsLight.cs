using UnityEngine;

public class JsonFilterMono_IssueCommentsLight : JsonFilter_GenericArrayTestingMono<JsonFilter_IssueCommentLight.GitHubIssueComment>
{

    [ContextMenu("Refresh")]
    public void RefreshContext() {

        base.Refresh();
    }
}
