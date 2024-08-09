using System;
using UnityEngine;

public abstract class JsonFilter_GenericArrayTestingMono<T> : MonoBehaviour, IJsonFilterArray<T>
{

    public string m_sourceJson;
    public T[] m_valueParsed;
    public bool m_parsed;
    public string m_jsonFiltered;
    [TextArea(2, 10)]
    public string m_error;
    public bool m_useValidate;

    private void OnValidate()
    {
        if (m_useValidate)
        Refresh();
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        ParseJsonRawToFiltered(m_sourceJson, out m_parsed, out m_valueParsed, out m_jsonFiltered);
    }

     public JsonContainterOfT m_container = new JsonContainterOfT();


    [System.Serializable]
    public class JsonContainterOfT : JsonContainer<T> { }

    [System.Serializable]
    public class JsonContainer<A> {

         string m_startWith = "{ \"array\" : ";
         string m_endWith = " }";
        public string Wrap(string text)
        {
            return m_startWith + text + m_endWith;
        }

        public A[] array;

    }

    JsonFilter_GenericArray <T> m_filter= new JsonFilter_GenericArray<T>();
    /// <summary>
    /// Parse jsonRaw to a lower version filter by the given class
    /// </summary>
    /// <param name="text"></param>
    /// <param name="foundObject"></param>
    public void ParseJsonRawToFiltered(string jsonRaw, out bool parsed, out T[] foundObject, out string jsonFiltered)
    {
        try { 

        m_filter.ParseJsonRawToFiltered(jsonRaw, out parsed, out foundObject, out jsonFiltered);
        }
        catch (Exception e)
        {
            parsed = false;
            foundObject = default(T[]);
            jsonFiltered = "";
            m_error = e.Message + "\n" + e.StackTrace;
            return;
        }



    }
}

public  class JsonFilter_GenericArray<T> : IJsonFilterArray<T>
{

    public JsonContainterOfT m_container = new JsonContainterOfT();

    [System.Serializable]
    public class JsonContainterOfT : JsonContainer<T> { }

    [System.Serializable]
    public class JsonContainer<A>
    {

        string m_startWith = "{ \"array\" : ";
        string m_endWith = " }";
        public string Wrap(string text)
        {
            return m_startWith + text + m_endWith;
        }

        public A[] array;

    }


    /// <summary>
    /// Apparently JsontUtility.FromJson is not able to parse generic types
    /// </summary>
    /// <param name="text"></param>
    /// <param name="foundObject"></param>
    public void ParseJsonRawToFiltered(string jsonRaw, out bool parsed, out T[] foundObject, out string jsonFiltered)
    {
        jsonFiltered = "";
        parsed = false;
        foundObject= default(T[]);

            string st = m_container.Wrap(jsonRaw);
            JsonContainterOfT container = JsonUtility.FromJson<JsonContainterOfT>(st);
            jsonFiltered = JsonUtility.ToJson(container, true);
            foundObject = container.array;
       
        parsed = true;
    }
}
