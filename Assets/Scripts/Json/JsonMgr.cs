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
        //�õ�·��
        string path = Application.persistentDataPath + "/" + fileName + ".json";

        //���л� �õ�json�ַ���
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

        //����Ӳ��
        File.WriteAllText(path, JsonStr);
    }

    public T LoadData<T>(string fileName, E_JsonType type=E_JsonType.LitJson) where T:new()
    {
        //�õ�·��
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        if (!File.Exists(path))
            return new T();

        //�����л� �õ��ַ���
        string JsonStr = File.ReadAllText(path);

        //���ض���
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