using UnityEngine;

public class JsonFilterMono_LabelsLight : JsonFilter_GenericArrayTestingMono<JsonFilter_IssueLabels.Labels>
{

    [ContextMenu("Refresh")]
    public void RefreshContext()
    {

        base.Refresh();
    }
}
