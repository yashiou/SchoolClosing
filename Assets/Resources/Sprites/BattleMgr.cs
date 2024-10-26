using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using Task = System.Threading.Tasks.Task;

public class BattleMgr : MonoBehaviour
{
    public SenceSystem senceSystem; //場景管理員

    [SerializeField] 
    public GameObject Card_deck;

    private List<string> UsedCard = new List<string>();

    private List<string> HandCard = new List<string>();

    [SerializeField] 
    public GameObject ShowCard, PlayrCard, EnemyCard, CardStack; //顯示手牌卡牌 卡牌堆

    public string PlayrCardId, EnemyCardId;

    public List<string> HandCardId = new List<string>(); 

    public Text result; //文字綁定

    private bool OnJudge = false; //審判中
    
    [SerializeField]
    public Slider BoossAnger, BoossHealth, PlayerHealth; //Booss生命 Booss血量 玩家生命條

    private string DeffCardId = "8"; //替換牌用 紀錄格檔卡牌ID

    [SerializeField] 
    public GameObject PlayrEffectBar, BoossEffectBer; //玩家效果 敵人效果

    [SerializeField] 
    public List<GameObject> AllEffect = new List<GameObject>(); //所有效果

    private int PlayerLife = 3; //玩家生命

    //堅定效果 迴避效果 反擊 預測牌 稟告 自我打氣 自我保護
    private Dictionary<string, int> EffectAndCount = new Dictionary<string, int>()
    {
        { "firm",0 },
        { "avoid",0},
        { "counterattack",0},
        {"KnowBossCard",0 },
        { "report",0},
        { "cheer",0},
        { "protect",0}
    };


    void Start()
    {
        senceSystem = FindObjectOfType<SenceSystem>();
        
        for (int i = 0; i < 3; i++) //獲取手牌
        {
            GetCard();
        }
    }

    public void GetCard(string TargetId = "", string repleseCardId = "")
    {
        if (senceSystem.CardBackpack.Count > 0)
        {
            int GetRandTarger = Random.Range(0, senceSystem.CardBackpack.Count); //得到卡牌索引

            if (TargetId!= "")
            {
                GetRandTarger = senceSystem.CardBackpack.IndexOf(TargetId); //改成指定卡牌用
            }
            
            GameObject NowCard = Instantiate(Card_deck, Card_deck.transform.parent); //生成卡片
            
            senceSystem.BidingImageToObject(NowCard, senceSystem.CardBackpack[GetRandTarger]);

            NowCard.name = senceSystem.CardBackpack[GetRandTarger]; //賦予ID

            Button CardButton = NowCard.GetComponent<Button>(); //每張卡牌上的按鈕

            CardButton.onClick.AddListener(()=>UseCard(NowCard, NowCard.name, repleseCardId)); //將按鈕綁定事件
            
            NowCard.SetActive(true); //啟用卡牌
            
            HandCardId.Add(senceSystem.CardBackpack[GetRandTarger]); //加入列表
        }

        if (HandCardId.Count == 0) //手上沒牌
        {
            Washcard();
        }
    }

    public void Washcard()
    {
        for (int i = 0; i < 21; i++)
        {
            int RandIndex = Random.Range(0, UsedCard.Count);//檢查牌堆數量
            
            senceSystem.CardBackpack.Add(UsedCard[RandIndex]);
            
            UsedCard.RemoveAt(RandIndex); //刪除指定牌

            
        }
        for (int j = 0; j < 2; j++)
        {
            GetCard();
        }
    }
    public void UseCard(GameObject gameObject,string id, string replaceCardId)
    {
        if (OnJudge)//審判中
            return;

        Destroy(gameObject); //消失指定牌

        HandCardId.Remove(id); //移除手牌堆
        
        PlayrCardId = id;

        senceSystem.BidingImageToObject(PlayrCard, PlayrCardId); // 設定圖片
        
        PlayrCard.SetActive(true);

        if (EffectAndCount["KnowBossCard"] == 0)//當沒有中預測牌效果在丟牌
        {
            BossChooseCard(); //對手丟牌
        }
        
        judgeSystem(id);
        
        
        if (replaceCardId !="")
        {
            UsedCard.Add(replaceCardId); //加入棄牌堆
        }
        else
        {
            UsedCard.Add(id); //加入棄牌堆
        }
        senceSystem.CardBackpack.Remove(id); //移除指定的卡
        CardStack.transform.GetChild(0).GetComponent<Text>().text = senceSystem.CardBackpack.Count.ToString(); //卡牌數
    }

    public void GetEffect(GameObject who, string Effect) //效果賦予
    {
        GameObject useEffect = null;
        
        if (who.transform.Find(Effect) == null) //當未顯示效果時 啟用效果
        {
            //將指定效果給予指定的人
            useEffect = Instantiate(AllEffect.Find(x => x.name == Effect), who.transform);
            
            useEffect.name = Effect;

            useEffect.SetActive(true);
        }
        //如果已經啟用效果 重製回和數
        EffectAndCount[Effect] = 2 ; //給予存在兩回合
        
        
    }

    //public void UseEffect(bool Open, string terEffect = "")//使用後移除效果 如果是移除要指定
    //{
        //Destroy(PlayrEffectBar.transform.Find(terEffect).gameObject); //一除指定效果 
    //}

    public async void judgeSystem(string id)
    {
        OnJudge = true;
        
        int WinOrLose = 2; //1是勝利2是平手0是敗

        string PlayerType = PlayrCardId.Split("_")[0]; //玩家的牌分類 

        string EnemyTyoe = EnemyCardId.Split("_")[0]; //敵人的牌分類
        
        if (PlayerType == "advise") //0~6勸導
        {
            if (EnemyTyoe == "defense") //0~6防禦
            {
                result.text = "勝";
                WinOrLose = 1;
            }
            else if(EnemyTyoe == "damage") //7~13傷害
            {
                result.text = "敗";
                WinOrLose = 0;
            }
            else if (EnemyTyoe == "scare")//14~20驚嚇
            {
                result.text = "平手";
                
                WinOrLose = 2;

            }
        }
        else if(PlayerType == "inspire") //7~13堅定
        {
            if (EnemyTyoe == "defense") //0~6防禦
            {
                result.text = "平手";
                
                WinOrLose = 2;
            }
            else if(EnemyTyoe == "damage") //7~13傷害
            {
                result.text = "勝";
                WinOrLose = 1;

            }
            else if (EnemyTyoe =="scare")//14~20驚嚇
            {
                result.text = "敗";

                WinOrLose = 0;

            }
        }
        else if (PlayerType =="steadfast") //14~20激勵
        {
            if (EnemyTyoe == "defense") //0~6防禦
            {
                result.text = "敗";

                WinOrLose = 0;

            }
            else if(EnemyTyoe == "damage") //7~13傷害
            {
                result.text = "平手";
                
                WinOrLose = 2;
            }
            else if (EnemyTyoe =="scare")//14~20驚嚇
            {
                result.text = "勝";

                WinOrLose = 1;

            }
        }

        //UseEffect(true);//處發效果
        
        if(WinOrLose ==0 && EffectAndCount["protect"] > 0) //當具有自我保護效果時輸掉時 改成平手
        {
            result.text = "觸發自我保護 平手";

            WinOrLose = 2;
        }
        
        //當具有小雨傘效果時輸掉時 並且玩家的牌不是堅定牌 扭轉成贏的
        if (WinOrLose == 0 && EffectAndCount["firm"] > 0 && PlayerType !="steadfast") //當具有堅定效果時輸掉時 扭轉成贏的
        {
            result.text = "觸發小雨傘 勝";

            WinOrLose = 1;
        }

        if(WinOrLose == 1 && PlayerType !="inspire" && EffectAndCount["report"] > 0)
        {
            result.text = "觸發稟告 勝";

            if (BoossAnger.value <=0)
            {
                BoossHealth.value -= 20;
            }

        }

        if (WinOrLose == 1 && PlayerType =="inspire" && EffectAndCount["cheer"] > 0)
        {
            result.text = "觸發自我打氣效果 勝";

            if (BoossAnger.value <=0)
            {
                BoossHealth.value -= 20;
            }
        }
        
        result.gameObject.SetActive(true);

        await Task.Delay(2000);//等待兩秒
        
        result.gameObject.SetActive(false);
        
        if (WinOrLose == 1) //成功時
        {
            switch (id)
            {
                case "0": //唯一的的方法
                    BoossAnger.value -= 10; 
                    break;
                case "1"://心理疏導
                    BoossAnger.value -= 50; 
                    break;
                case "2"://嘴砲 
                    BoossAnger.value -= 25;
                    break;
                case "3"://反擊譏諷
                    BoossAnger.value -= 100;
                    break;
                case "4"://心理弱點
                    BoossAnger.value -= 50;
                    break;
                case "5"://撕破偽裝
                    BoossAnger.value -= 25;

                    PlayerHealth.value += 20;
                    break;
                case "6"://堅持
                    BoossAnger.value -= 50;

                    PlayerHealth.value -= 10;
                    break;
                case "7"://格檔
                    break;
                case "8"://替換
                    if (HandCardId.Count > 0)
                    {
                        int RandChangeCard = Random.Range(0, HandCardId.Count);
                        {
                            //刪掉指定的物體
                            Destroy(Card_deck.transform.parent.GetChild(RandChangeCard + 1).gameObject);

                            //將格檔牌加入手牌堆 -> 換成格檔牌 並紀錄替換原ID
                            GetCard(DeffCardId, HandCardId[RandChangeCard]);

                            HandCardId.RemoveAt(RandChangeCard); //刪除手牌堆id                           
                        }
                    }
                    break;
                case "9": //小雨傘
                    GetEffect(PlayrEffectBar,"firm"); //給予玩家堅定效果

                    break;
                case "10":
                    break;
                case "11":
                    break;
                case "12": //危機意識
                    EffectAndCount["KnowBossCard"] = 2;
                    break;
                case "13": //蜷縮
                    PlayerHealth.value += 5;
                    break;
                case "14": //振奮
                    if(BoossAnger.value <= 0)
                        BoossHealth.value -= 10;
                    break;
                case "15"://自我信任
                    if (BoossAnger.value <= 0)
                        BoossHealth.value -= 15;
                    break;
                case "16"://稟告
                    GetEffect(PlayrEffectBar,"report"); //給予玩家稟告效果
                    break;
                case "17":
                    GetEffect(PlayrEffectBar, "cheer"); //給予玩家自我打氣效果
                    break;
                case "18":
                    if (BoossAnger.value <= 0)
                        BoossHealth.value -= 10;
                    
                    GetEffect(PlayrEffectBar, "protect"); //給予Boss自我保護負面效果
                    break;
                case "19": //反抗
                    if (BoossAnger.value <= 0)
                        BoossHealth.value -= 10;
                    
                    
                    break;
                case "20"://箝制
                    if (BoossAnger.value <= 0)
                        BoossHealth.value -= 15;
                    break;
                default:
                    break;
            }
            
        }
        else if (WinOrLose == 0)//失敗時
        {
            switch (id)
            {
                case "0" :
                    BoossAnger.value -= 75; //失敗時減少75怨恨
                    break;
                case "1" :
                    
                    break;
                case "2":
                    break;
                case "3" :
                    break;
                case "4":
                    break;
                case "5" :
                    break;
                case "6":
                    break;
                case "7" :
                    break;
                case "8":
                    break;
                case "9" :
                    break;
                case "10": //小雨傘
                    EffectAndCount["avoid"] = 2; //打開迴避效果
                    break;
                case "11" : //應激
                    EffectAndCount["counterattack"] = 2; //打開反擊效果
                    break;
                case "12": 
                    break;
                case "13" :
                    break;
                case "14":
                    break;
                case "15" :
                    break;
                case "16":
                    break;
                case "17" :
                    break;
                case "18":
                    break;
                case "19" :
                    break;
                case "20":
                    break;
                default: 
                    break;
            }
            if (EffectAndCount["avoid"] > 0)
            {
                result.text = "觸發迴避效果";
                
                result.gameObject.SetActive(true);

                await Task.Delay(1000);//等待兩秒
        
                result.gameObject.SetActive(false);
            }
            else if (EffectAndCount["counterattack"] > 0)
            {
                result.text = "觸發反擊效果";
                
                result.gameObject.SetActive(true);

                BoossAnger.value -= 5;
                
                await Task.Delay(1000);//等待兩秒
        
                result.gameObject.SetActive(false);
            }
                else
            {
                if (EnemyTyoe == "defense") //0~6防禦
                {
                
                }
                else if(EnemyTyoe == "damage") //7~13傷害
                {
                    PlayerDamage(10); //減少san值
                }
                else if (EnemyTyoe == "scare")//14~20驚嚇
                {
                    PlayerDamage(5); //減少san值
                }
            }
            
        }
        else if (WinOrLose == 2)
        {
            if (id == "20") //反抗
            {
                if (BoossAnger.value <= 0)
                {
                    BoossHealth.value -= 20;
                }
            }   
        }
        PlayrCard.SetActive(false);
        
        EnemyCard.SetActive(false);

        
        
        for (int i = 0; i < 2; i++) //抽牌
        {
            GetCard();
            
            if (HandCardId.Count >=5)
            {
                break;
            }
        }

        List<string> tempEffectname = new List<string>(EffectAndCount.Keys);
        
        foreach(string s in tempEffectname) //瀏覽所有效果
        {
            if(EffectAndCount[s] > 0) //減所有效果1回合
            {
                EffectAndCount[s] -= 1; //將所有效果-1回合
            }
            
            if(EffectAndCount[s] == 0 && //當玩家或敵人有效果 
               (PlayrEffectBar.transform.childCount > 0 || BoossHealth.transform.childCount > 0))
            {
                Transform DelEffect = PlayrEffectBar.transform.Find(s); //找到對應的效果
                
                if (DelEffect != null) //找到效果
                {
                    Destroy(DelEffect.gameObject); //刪除效果為零的效果
                }
                
                DelEffect = BoossEffectBer.transform.Find(s); //找到對應的效果
                
                if (DelEffect != null) //找到效果
                {
                    Destroy(DelEffect.gameObject); //刪除效果為零的效果
                }
            }
        }

        if (EffectAndCount["KnowBossCard"] > 0) //預測牌效果
        {
            BossChooseCard(); //提前讓敵人打牌
        }
        
        OnJudge = false; //結束審判
    }

    public void PlayerDamage(int value) //玩家受到傷害
    {
        PlayerHealth.value -= value;

        if (PlayerHealth.value == 0) //當玩家沒血
        {
            if (PlayerLife == 0) //當玩家沒意志
            {
                LoseGameEvent();
            }
        }
        else //還有意志值
        {
            PlayerLife -= 1; //-1意志

            PlayerHealth.value = PlayerHealth.maxValue; //回復至滿血
        }
    }

    public async Task LoseGameEvent() //遊戲結束 失敗
    {
        await Task.Delay(1000);
        
        senceSystem.LoadScene("s0");
    }
    
    public void BossChooseCard()
    {
        string[] AllEnemyTag = new string[] { "defense", "damage", "scare" };

        EnemyCardId = AllEnemyTag[Random.Range(0, AllEnemyTag.Length)];
        
        senceSystem.BidingImageToObject(EnemyCard, EnemyCardId, true);//設定圖片

        EnemyCard.SetActive(true);

    }
    void Update()
    {
        
    }
}
