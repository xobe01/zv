using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveAndLoad : MonoBehaviour
{
    public static void SaveData(SerializableData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SerializableData LoadData(bool isSerialized)
    {
        string path = Application.persistentDataPath + "/data.fun";
        SerializableData data = new SerializableData();
        if (File.Exists(path) && isSerialized)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            data = formatter.Deserialize(stream) as SerializableData;
            stream.Close();
            return data;
        }
        data.isLearned = new List<bool>();
        for (int i = 0; i < 3052; i++) data.isLearned.Add(false);
        return data;
    }
}
