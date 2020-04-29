using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string data = "{\"items\":" + json + "}";

        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(data);
        return wrapper.items;
    }

    public static string ToJson<T>(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}
