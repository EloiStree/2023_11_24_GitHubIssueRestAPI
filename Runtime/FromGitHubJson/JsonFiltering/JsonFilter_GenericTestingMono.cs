using System;
using UnityEngine;

public abstract class JsonFilter_GenericTestingMono<T> : MonoBehaviour, IJsonFilter<T>
{

    public string m_sourceJson;
    public T m_valueParsed;
    public bool m_parsed;
    public string m_jsonFiltered;
    [TextArea(2, 10)]
    public string m_error;

    public JsonFilter_GenericTesting<T> m_filter = new JsonFilter_GenericTesting<T>();

    private void OnValidate()
    {
        Refresh();
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        ParseJsonRawToFiltered(m_sourceJson, out m_parsed, out m_valueParsed, out m_jsonFiltered);
    }

    /// <summary>
    /// Apparently JsontUtility.FromJson is not able to parse generic types
    /// </summary>
    /// <param name="text"></param>
    /// <param name="foundObject"></param>
    public void ParseJsonRawToFiltered(string jsonRaw, out bool parsed, out T foundObject, out string jsonFiltered)
    {
        m_error = "";
        m_jsonFiltered = "";
        parsed = false;
        try
        {
            m_filter.ParseJsonRawToFiltered(jsonRaw, out parsed, out foundObject, out jsonFiltered);
        }
        catch (Exception e)
        {
            parsed = false;
            foundObject = default(T);
            jsonFiltered = "";
            m_error = e.Message + "\n" + e.StackTrace;
            return;
        }
        parsed = true;
    }
}

public  class JsonFilter_GenericTesting<T> : IJsonFilter<T>
{

   

    /// <summary>
    /// Parse jsonRaw to a lower version filter by the given class
    /// </summary>
    /// <param name="text"></param>
    /// <param name="foundObject"></param>
    public void ParseJsonRawToFiltered(string jsonRaw, out bool parsed, out T foundObject, out string jsonFiltered)
    {
       
            parsed = false;
            foundObject = default(T);
            foundObject = JsonUtility.FromJson<T>(jsonRaw);
            jsonFiltered = JsonUtility.ToJson(foundObject, true);
            parsed = true;
       
    }
}
