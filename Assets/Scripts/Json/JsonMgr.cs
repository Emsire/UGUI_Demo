using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public enum E_JsonType
{
    JsonUtility,
    LitJson,
}

public class JsonMgr
{
    private static JsonMgr instance = new JsonMgr();
    public static JsonMgr Instance => instance;

    private JsonMgr() { }

    public void SaveData(object data, string fileName, E_JsonType type=E_JsonType.LitJson)
    {
        //得到路径
        string path = Application.persistentDataPath + "/" + fileName + ".json";

        //序列化 得到json字符串
        string JsonStr = "";
        switch (type)
        {
            case E_JsonType.JsonUtility:
                JsonStr = JsonUtility.ToJson(data);
                break;
            case E_JsonType.LitJson:
                JsonStr = JsonMapper.ToJson(data);
                break;
        }

        //存入硬盘
        File.WriteAllText(path, JsonStr);
    }

    public T LoadData<T>(string fileName, E_JsonType type=E_JsonType.LitJson) where T:new()
    {
        //得到路径
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        if (!File.Exists(path))
            return new T();

        //反序列化 得到字符串
        string JsonStr = File.ReadAllText(path);

        //返回对象
        T data = default(T);
        switch (type)
        {
            case E_JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(JsonStr);
                break;
            case E_JsonType.LitJson:
                data = JsonMapper.ToObject<T>(JsonStr);
                break;
        }
        return data;
    }
}