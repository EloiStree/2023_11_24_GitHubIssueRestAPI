using UnityEngine;

public class JsonFilterMono_IssueLight : JsonFilter_GenericTestingMono<JsonFilter_IssueLight.Issue>
{
    [ContextMenu("Refresh")]
    public void RefreshContext()
    {
        base.Refresh();
    }
}
