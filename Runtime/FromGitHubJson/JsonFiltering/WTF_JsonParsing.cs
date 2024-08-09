using UnityEngine;
using static JsonFilter_IssueCommentLight;

public class WTF_JsonParsing :MonoBehaviour{

    public string jsonRaw;
    public GitHubIssueComment foundObject;
    public string jsonFiltered;

    public A m_a1;
    public string m_a;
    public A m_a2;
    public void OnValidate()
    {
       
        m_a= JsonUtility.ToJson(m_a1, true);
        m_a2 = JsonUtility.FromJson<A>(m_a);



        //foundObject = JsonUtility.FromJson<RootComments>(jsonRaw);
       
        //jsonFiltered = JsonUtility.ToJson(foundObject, true);
    }

}

[System.Serializable]
public class A {

    public int life;
}