using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyTestMono_IssueHeavyToLight : MonoBehaviour
{

    public string m_issueTextHeavy;
    public JsonFilter_IssueLight.Issue m_issueLight;
    public string m_issueTextLight;
    public bool m_parsed;

    public void OnValidate()
    {
        if (m_issueTextHeavy.Length > 0) {

            JsonFilter_IssueLight filter = new JsonFilter_IssueLight();
            filter.ParseJsonRawToFiltered(m_issueTextHeavy, out  m_parsed, out m_issueLight, out m_issueTextLight);
            
        }
    }
}
