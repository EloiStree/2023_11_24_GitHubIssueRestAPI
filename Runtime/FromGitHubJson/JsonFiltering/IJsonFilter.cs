public interface IJsonFilter<T>  
{
    void ParseJsonRawToFiltered(string jsonRaw,out bool parsed, out T foundObject, out string jsonFiltered);
}
