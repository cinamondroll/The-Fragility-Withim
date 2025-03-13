using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class SaveSystem 
{
    static string path = Application.persistentDataPath + "/savefile.json";
    static void SaveProgress(){
        SaveData data = new SaveData{
            chapter=new List<Chapter>(),
            anxStatus=new List<AnxStatus>(),
            position=new List<Position>()
        };
        foreach (var item in GameProgress.getAllChapter()){
            data.chapter.Add(new Chapter(item.Key, item.Value));
        }
        foreach (var item in GameProgress.getAllAnxStatus())
        {
            data.anxStatus.Add(new AnxStatus(item.Key, item.Value));
        }
        foreach (var item in GameProgress.getAllPosition())
        {
            data.position.Add(new Position(item.Key, item.Value));
        }
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(path, json);
    }
    static void LoadProgrss(){
        if (File.Exists(path))
        {
            
        }
    }

}



public class SaveData{
    public List<Chapter> chapter;
    public List<AnxStatus> anxStatus;
    public List<Position> position;

}

public class Chapter{
    String key;
    bool value;
    public Chapter(String key, bool value){
        this.key=key;
        this.value=value;
    }
}
public class AnxStatus{
    String key;
    float value;
    public AnxStatus(String key, float value){
        this.key=key;
        this.value=value;
    }
}
public class Position{
    String key;
    float[] value;
    public Position(String key, float[] value){
        this.key=key;
        this.value=value;
    }
}


