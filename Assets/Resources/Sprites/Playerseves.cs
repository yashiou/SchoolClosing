using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

    public int UesPlayCard;//總打牌數
    
    public int PlayerWins; //總獲勝數
    
    public int AllPoint; //總分

    public float 
        Rebirth,
        Life,
        EnergyUp,
        EnergyMp,
        KnockDw,
        Totalscore;
    public bool LookMAT;

    public bool LookRougeMAT;
}
public class Playerseves : MonoBehaviour
{
    private string filepath;

    [SerializeField] 
    public SenceSystem senceSystem; //場景管理
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        senceSystem = FindAnyObjectByType<SenceSystem>();

        filepath = Path.Combine(Application.persistentDataPath, "PlayerData.json"); //預設路徑
    
        if (Load() == null)
        {
            PlayerData data = new PlayerData();

            data.AudioSoundValue = 1.0f;

            data.MusicSoundValue = 1.0f;

            data.AllSoundValue = 0.5f;

            Seve(data);
        }
    }

    public void Seve(PlayerData data) //存檔
    {
        
        
        if (senceSystem.state == "free")
        {
            return;
        }
        
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

    public async void Loading()
    { 
        
        PlayerData data = Load();
        //載入卡包
        senceSystem.CardBackpack = data.CardBackpack;

        senceSystem.LoadScene("S1");

        await Task.Delay(500);

        BattleMgr battleMgr = FindObjectOfType<BattleMgr>();

        battleMgr.PlayerHealth.value = data.PlayerHealth; //玩家血量

        battleMgr.PlayerLife = data.PlayerLife; //玩家意志值

        battleMgr.HandCardId = data.HandCard; //手牌堆

        battleMgr.UsedCard = data.UsedCard; //棄牌堆

        battleMgr.PlayerHealthText.text = data.PlayerHealth.ToString(); //改變顯示

        battleMgr.point = data.point;
        
        battleMgr.showpoint.text =$"分數:{data.point}";
    }
    
    
    void Update()
    {
        
    }
}
