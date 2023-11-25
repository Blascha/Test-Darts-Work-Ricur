using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class SaveManager
{
    public static SaveData data = new SaveData();
    static string path;

    public static void PrepareSave()
    {
        path = Application.persistentDataPath;
        path += "/" + Application.companyName + "/" + Application.productName;

        Debug.Log($"El path es: <color=cyan>{path}</color>");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);

            path += "/SaveData.json";
            return;
        }

        path += "/SaveData.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, data);

            LoadInfo();
        }
    }


    public static void Save()
    {
        Debug.Log("<color=cyan> Saving </color>");

        data.HighScore = Points.HighScore;

        string json = JsonUtility.ToJson(data);
        Debug.Log($"<color=cyan>Saving: {json}</color>");
        File.WriteAllText(path, json);
    }

    static void LoadInfo()
    {
        Debug.Log("<color=cyan>Loading Save </color>");

        //Setting the variables to what they must be
        Points.HighScore = data.HighScore;
    }
}
