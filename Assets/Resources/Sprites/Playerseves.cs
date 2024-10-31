using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]

public class PlayerData
{
    public int point; //擊倒敵人數
    
    public List<string> UsedCard; //棄牌堆
    
    public List<string> HandCard; //手牌
    
    public float AudioSoundValue, MusicSoundValue,AllSoundValue; //音量

    public List<string> CardBackpack;//牌庫

    public float BoossAnger, BoossHealth, PlayerHealth; //所有血量

    public int PlayerLife = 0; 
}
public class Playerseves : MonoBehaviour
{
    private string filepath;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        filepath = Path.Combine(Application.persistentDataPath, "PlayerData.json"); //預設路徑
    }

    public void Seve(PlayerData data) //存檔
    {
        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(filepath,json);
    }

    public PlayerData Load() //讀取
    {
        if (File.Exists(filepath)) //已存在
        {
            string json = File.ReadAllText(filepath);

            return JsonUtility.FromJson<PlayerData>(json);
        }

        return null; //不存在
    }

    void Update()
    {
        
    }
}
