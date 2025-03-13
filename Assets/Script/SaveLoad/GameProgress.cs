using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public static class GameProgress {
    private static Dictionary<String, bool> chapter;
    private static Dictionary<String, float> anxStatus;
    private static Dictionary<String, float[]> position;   

    public static bool getChatper(String key){
        return chapter[key];
    }
    public static float getAnxStatus(String key){
        return anxStatus[key];
    }
    public static float[] getPosition(String key){
        return position[key];
    }

    public static Dictionary<String, bool> getAllChapter(){
        return chapter;
    }
    public static Dictionary<String, float> getAllAnxStatus(){
        return anxStatus;
    }
    public static Dictionary<String, float[]> getAllPosition(){
        return position;
    }
    public static void setChapter(String key, bool value){
        if (chapter==null){
            chapter=new Dictionary<String, bool>();
        }
        if (!chapter.ContainsKey(key)){
            chapter.Add(key, value);
        }else{
        chapter[key]=value;
        }
    }
    public static void setAnxStatus(String key,float value){
        if (anxStatus==null)
        {
            anxStatus=new Dictionary<String, float>();
        }
        if (!anxStatus.ContainsKey(key)){
            anxStatus.Add(key, value);
       }else{
          anxStatus[key]=value;
      }
      }
    public static void setPositionX(String key, float x, float y){
        if (position==null){
            position=new Dictionary<String, float[]>();
        }
        if (!position.ContainsKey(key)){
            position.Add(key, new float[]{x,y});
        }else{
            position[key][0]=x;
            position[key][1]=y;
        }
    }
}
