using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Task = System.Threading.Tasks.Task;

public class BattleMgr : MonoBehaviour
{
    public SenceSystem senceSystem; //場景管理員

    [SerializeField] 
    public GameObject Card_deck;

    public List<string> UsedCard = new List<string>();

    public List<string> HandCard = new List<string>();

    [SerializeField] 
    public GameObject ShowCard, PlayrCard, EnemyCard, CardStack; //顯示手牌卡牌 卡牌堆

    private string PlayrCardId, EnemyCardId;

    public List<string> HandCardId = new List<string>(); 

    public Text result; //文字綁定

    private bool OnJudge = false; //審判中
    
    [SerializeField]
    public Slider BoossAnger, BoossHealth, PlayerHealth; //Booss生命 Booss血量 玩家生命條

    public Text PlayerHealthText;
    
    private string DeffCardId = "steadfast_08"; //替換牌用 紀錄格檔卡牌ID

    [SerializeField] 
    public GameObject PlayrEffectBar, BoossEffectBer; //玩家效果 敵人效果

    [SerializeField] 
    public List<GameObject> AllEffect = new List<GameObject>(); //所有效果

    public int PlayerLife = 0; //玩家生命

    public int point = 0; //擊倒敵人數

    private Playerseves playerseves;
        
    [SerializeField] 
    public AnimeMgr animeMgr;

    [SerializeField] 
    public GameObject PlayerWill;

    [SerializeField] 
    public Text CardNameShow;

    [SerializeField] 
    public Text showpoint; //顯示擊倒數

    [SerializeField] 
    public GameObject SHSButnUI; //默念心經
    
    [SerializeField] 
    public GameObject beliefButnUI; //信念支持
    //堅定效果 迴避效果 反擊 預測牌 稟告 自我打氣 自我保護
    private Dictionary<string, int> EffectAndCount = new Dictionary<string, int>()
    {
        { "firm",0 },
        { "avoid",0},
        { "counterattack",0},
        {"KnowBossCard",0 },
        { "report",0},
        { "cheer",0},
        { "protect",0},
        {"block",0},
        {"recover",0},
        {"adrenaline",0},
        {"SHS" ,0},
        {"belief", 0}
    };
    
    private string SHS_BossCard_id = ""; //強制指定敵人ID
    
    private string belief_PlaterCard_id = ""; //強制指定玩家ID

    private RougeMgr rougeMgr;

    private bool NowEnd; 
    
    [SerializeField]
    public Slider Hope; //希望值

    public Slider BossHope; //對手希望值
    
    private int UesPlayCards;//總打牌數
    
    private int PlayerWinss; //總獲勝數
    
    private int AllPoints; //總分
    
    public float 
        EnergyMpss,
        KnockDwss,
        Totalscoress;

    public GameObject LoseGameUI;
    void Start()
    {
        rougeMgr = FindObjectOfType<RougeMgr>();
        
        senceSystem = FindObjectOfType<SenceSystem>();

        animeMgr = FindObjectOfType<AnimeMgr>();

        animeMgr.CallNextEnemys();

        playerseves = FindObjectOfType<Playerseves>();
        
        Initial(); //重設值
        
    }//起始

    public async void Initial()
    {
        PlayerData playerData = playerseves.Load();

        PlayerLife = (int)playerData.Rebirth;

        PlayerHealth.maxValue += playerData.Life;

        PlayerHealth.value = PlayerHealth.maxValue;

        Hope.maxValue += playerData.EnergyUp;

        EnergyMpss = 1 + playerData.EnergyMp; //能量倍率

        KnockDwss = 1 + playerData.KnockDw; //敵人倍率

        Totalscoress = 1 + playerData.Totalscore; //分數倍率
        
        await Task.Delay(750);

        for (int i = 0; i < PlayerLife; i++)
        {
            GameObject obj = Instantiate(PlayerWill, PlayerWill.transform.parent);//生成意志值
            
            obj.SetActive(true);
        }

        if (HandCardId.Count > 0) //如有讀取的手牌 優先讀取 不抽牌
        {
            string[] hand = HandCardId.ToArray(); //先讀取已有卡牌
            
            foreach (string s in hand)
            {
                GetCard(s); //讀取卡牌數值
            }
        }
        for (int i = 0; i < 3; i++) //獲取手牌
        {
            GetCard();
        }

        

    }
    public void GetCard(string TargetId = "", string repleseCardId = "")
    {
        if (senceSystem.CardBackpack.Count > 0)
        {
            GameObject NowCard = Instantiate(Card_deck, Card_deck.transform.parent); //生成卡片
            
            
            if (TargetId != "")
            {
                senceSystem.BidingImageToObject(NowCard, TargetId);

                NowCard.name = TargetId; //賦予指定的ID
            }
            else
            {
                int GetRandTarger = Random.Range(0, senceSystem.CardBackpack.Count); //得到卡牌索引
                
                senceSystem.BidingImageToObject(NowCard, senceSystem.CardBackpack[GetRandTarger]);

                NowCard.name = senceSystem.CardBackpack[GetRandTarger]; //賦予ID
                
            }

            Button CardButton = NowCard.GetComponent<Button>(); //每張卡牌上的按鈕

            CardButton.onClick.AddListener(()=>UseCard(NowCard, NowCard.name, repleseCardId)); //將按鈕綁定事件
            
            //根據ID找到所有符合條件的物件TextData物件 並整理成數據 交給.ToList()轉成陣列
            List<TextData> textData = senceSystem.CardText.TextFormat.Where(x => x.id == NowCard.name.Split("_")[1] ).ToList();

            NowCard.GetComponent<ShowHandCard>().CardName = textData[0].Name;
            
            NowCard.GetComponent<ShowHandCard>().CardEffect = textData[0].Effect;

            
            NowCard.SetActive(true); //啟用卡牌
            
            HandCardId.Add(NowCard.name); //加入列表
        }

        if (HandCardId.Count == 0) //手上沒牌
        {
            Washcard();
        }
    } //獲得手牌

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
    } //洗牌
    public void UseCard(GameObject gameObject,string id, string replaceCardId)
    {
        if (OnJudge || NowEnd)//審判中
            return;

        Destroy(gameObject); //消失指定牌

        PlayrCardId = id;
        
        HandCardId.Remove(id); //移除手牌堆
        
        senceSystem.BidingImageToObject(PlayrCard, PlayrCardId); // 設定圖片
        
        PlayrCard.SetActive(true);

        if (EffectAndCount["KnowBossCard"] == 0)//當沒有中預測牌效果在丟牌
        {
            BossChooseCard(); //對手丟牌
        }
        else if (SHS_BossCard_id != "")
        {
            BossChooseCard(SHS_BossCard_id); //對手丟牌
        }

        BossHope.value = Random.Range(0,100);
        
        Hope.value += Random.Range(10,20) * EnergyMpss;
        
        CheckCard(id);
        
        judgeSystem();
        
        
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

        UesPlayCards += 1;
    } //使用牌

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
        
        
    } //賦予效果

    

    //public void UseEffect(bool Open, string terEffect = "")//使用後移除效果 如果是移除要指定
    //{
        //Destroy(PlayrEffectBar.transform.Find(terEffect).gameObject); //一除指定效果 
    //}

    public async void judgeSystem()
    {
        OnJudge = true;

        string CardIdNumber = PlayrCardId.Split("_")[1];//取得排ID

        //UseEffect(true);//處發效果
        int WinOrLose = WhoWin(); //1是勝利2是平手0是敗
        
        await Task.Delay(500);//等待0.5秒
        
        result.gameObject.SetActive(true);

        await Task.Delay(2000);//等待兩秒
        
        result.gameObject.SetActive(false);
        
        if (EffectAndCount["adrenaline"] > 0)
        {
            result.text = "觸發腎上腺素效果";
                    
            BossGetDamage(15,false);//-15存在
                    
            result.gameObject.SetActive(true);
                    
            await Task.Delay(1000);//等待兩秒
            
            result.gameObject.SetActive(false);
        }
        
        if (WinOrLose == 1) //成功時
        {
            PlayerWin(CardIdNumber);
        }
        else if (WinOrLose == 0)//失敗時
        {
            BossWin(CardIdNumber);
        }
        else if (WinOrLose == 2)
        {
            if (CardIdNumber == "20") //反抗
            {
                BossGetDamage(20, false);
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

        DecreaseEffect();//檢索有效果1回合

        if (EffectAndCount["KnowBossCard"] > 0) //預測牌效果
        {
            BossChooseCard(); //提前讓敵人打牌
        }
        else if (EffectAndCount["SHS"] > 0)
        {
            SHSButnUI.SetActive(true);
        }
        else if (EffectAndCount["belief"] > 0)
        {
            beliefButnUI.SetActive(true);
        }
        OnJudge = false; //結束審判
    }

    public async void BossWin(string id)
    {
        string EnemyTyoe = EnemyCardId.Split("_")[0];
        switch (id)
            {
                case "01" :
                    BossGetDamage(75, true); //失敗時減少75怨恨
                    break;
                case "02" :
                    
                    break;
                case "03":
                    break;
                case "04" :
                    break;
                case "05":
                    break;
                case "06" :
                    break;
                case "07":
                    break;
                case "08" :
                    break;
                case "09":
                    break;
                case "10" :
                    break;
                case "11":
                    EffectAndCount["avoid"] = 2; //打開迴避效果
                    break;
                case "12" :
                    EffectAndCount["counterattack"] = 2; //打開反擊效果
                    break;
                case "13": 
                    break;
                case "14" :
                    break;
                case "15":
                    break;
                case "16" :
                    break;
                case "17":
                    break;
                case "18" :
                    break;
                case "19":
                    break;
                case "20" :
                    break;
                case "21":
                    break;
                default: 
                    break;
            }
            if (EffectAndCount["avoid"] > 0)
            {
                result.text = "觸發迴避效果";
                
                result.gameObject.SetActive(true);

                await Task.Delay(1000);//等待秒
        
                result.gameObject.SetActive(false);
            }
            else if (EffectAndCount["counterattack"] > 0)
            {
                result.text = "觸發反擊效果";
                
                result.gameObject.SetActive(true);

                BossGetDamage(5, true);

                await Task.Delay(1000);//等待兩秒
        
                result.gameObject.SetActive(false);
            }
            else if (EffectAndCount["block"] > 0)
            {
                result.text = "觸發格檔傷害效果";
                
                result.gameObject.SetActive(true);
                
                await Task.Delay(1000);//等待兩秒
        
                result.gameObject.SetActive(false);
            }
            else if (EffectAndCount["recover"] > 0)
            {
                result.text = "觸發轉變傷害效果";
                
                if(EnemyTyoe == "damage") //7~13傷害
                {
                    PlayerDamage(-10); //增加san值
                }
                else if (EnemyTyoe == "scare")//14~20驚嚇
                {
                    PlayerDamage(-5); //增加san值
                }
                
                result.gameObject.SetActive(true);
                
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
                else if (EnemyTyoe == "enhance")//強化
                {
                    BoossHealth.value += 10;//回復牌
                }
            }
    } //敵人獲勝

    public void PlayerWin(string id)
    {
        PlayerWinss += 1;
        
        switch (id)
            {
                case "01": //唯一的的方法
                    BossGetDamage(10, true);
                    break;
                case "02"://心理疏導
                    BossGetDamage(50, true);
                    break;
                case "03"://嘴砲 
                    BossGetDamage(25, true);
                    break;
                case "04"://反擊譏諷
                    BossGetDamage(100, true);
                    break;
                case "05"://心理弱點
                    BossGetDamage(50, true);
                    break;
                case "06"://撕破偽裝
                    BossGetDamage(25, true);

                    PlayerHealth.value += 20;
                    break;
                case "07"://堅持
                    BossGetDamage(50, true);

                    PlayerHealth.value -= 10;
                    break;
                case "15": //振奮
                    BossGetDamage(10, false);
                    break;
                case "16"://自我信任
                    BossGetDamage(15, false);
                    break;
                case "19":
                    BossGetDamage(10, false);

                    GetEffect(PlayrEffectBar, "protect"); //給予Boss自我保護負面效果
                    break;
                case "20": //反抗
                    BossGetDamage(10, false);
                    
                    break;
                case "21"://箝制
                    BossGetDamage(15, false);

                    break;
                default:
                    break;
            }
    } //玩家獲勝

    public void CheckCard(string id)
    {
        switch (id)
            {
                case "09"://替換
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
                case "10": //小雨傘
                    GetEffect(PlayrEffectBar,"firm"); //給予玩家堅定效果
                    break;
                case "13": //危機意識
                    EffectAndCount["KnowBossCard"] = 2;
                    break;
                case "14": //蜷縮
                    GetEffect(PlayrEffectBar,"block");
                    PlayerHealth.value += 5;
                    break;
                case "17"://稟告
                    GetEffect(PlayrEffectBar,"report"); //給予玩家稟告效果
                    break;
                case "18":
                    GetEffect(PlayrEffectBar, "cheer"); //給予玩家自我打氣效果
                    break;
                case "22"://深呼吸
                    PlayerDamage(-5);
                    break;
                case "23"://肌肉放鬆
                    GetEffect(PlayrEffectBar, "block"); //給予玩家格檔傷害效果
                    break;
                
                case "24"://心跳加速
                    GetEffect(PlayrEffectBar, "recover"); //給予玩家傷害變為恢復效果
                    break;
                
                case "25": //腎上腺素
                    GetEffect(PlayrEffectBar, "adrenaline"); //給予玩家腎上腺素效果
                    break;
                case "26": //默念心經
                    EffectAndCount["SHS"] = 2; //給予玩家默念心經效果
                    break;
                case "27": //回憶
                    if (HandCardId.Count > 0)
                    {
                        int RandChangeCard = Random.Range(0, HandCardId.Count);
                        {
                            //刪掉指定的物體
                            Destroy(Card_deck.transform.parent.GetChild(RandChangeCard + 1).gameObject);

                            //將牌庫隨機牌加入手牌堆 ->  並紀錄替換原ID
                            GetCard(senceSystem.CardBackpack[Random.Range(0,senceSystem.CardBackpack.Count)], HandCardId[RandChangeCard]);

                            HandCardId.RemoveAt(RandChangeCard); //刪除手牌堆id                           
                        }
                    }
                    break;
                case "28": //信念支持
                    EffectAndCount["belief"] = 2; //給予玩家信念支持效果
                    break;
                default:
                    break;
            }
    }
    
    public int WhoWin()
    {
        int WinOrLose = 2; //1是勝利2是平手0是敗
        string PlayerType = PlayrCardId.Split("_")[0]; //玩家的牌分類 

        string EnemyTyoe = EnemyCardId.Split("_")[0]; //敵人的牌分類
        
        if (SHS_BossCard_id != "")
        {
            EnemyTyoe = SHS_BossCard_id;

            SHS_BossCard_id = "";
        }
        
        if (belief_PlaterCard_id != "")
        {
            PlayerType = belief_PlaterCard_id;

            belief_PlaterCard_id = "";
        }

        if (Hope.value > BossHope.value)
        {
            result.text = "勝";
                
            WinOrLose = 1;
        }
        else
        {
            result.text = "敗";
            WinOrLose = 0;
        }
        /*if (PlayerType == "advise") //勸導
        {
            if (EnemyTyoe == "defense") //防禦
            {
                result.text = "平手";
                
                WinOrLose = 2;
            }
            else if(EnemyTyoe == "damage") //傷害
            {
                result.text = "敗";
                WinOrLose = 0;
            }
            else if (EnemyTyoe == "scare")//驚嚇
            {
                result.text = "平手";
                
                WinOrLose = 2;

            }
            else if (EnemyTyoe == "enhance") //強化
            {
                result.text = "勝";
                
                WinOrLose = 1;
            }
        }
        else if(PlayerType == "resist") //堅定
        {
            if (EnemyTyoe == "defense") //防禦
            {
                result.text = "平手";
                
                WinOrLose = 2;
            }
            else if(EnemyTyoe == "damage") //傷害
            {
                result.text = "勝";
                
                WinOrLose = 1;

            }
            else if (EnemyTyoe =="scare")//驚嚇
            {
                result.text = "平手";
                
                WinOrLose = 2;

            }
            else if (EnemyTyoe == "enhance") //強化
            {
                result.text = "敗";

                WinOrLose = 0;
            }
        }
        else if (PlayerType =="inspire") //強化
        {
            if (EnemyTyoe == "defense") //防禦
            {
                result.text = "勝";

                WinOrLose = 1;

            }
            else if (EnemyTyoe == "enhance") //強化
            {
                result.text = "平手";
                
                WinOrLose = 2;
            }
            else if(EnemyTyoe == "damage") //傷害
            {
                result.text = "平手";
                
                WinOrLose = 2;
            }
            else if (EnemyTyoe =="scare")//驚嚇
            {
                result.text = "敗";

                WinOrLose = 0;

            }
        }
        else if(PlayerType == "steadfast") //堅定
        {
            if (EnemyTyoe == "defense") //防禦
            {
                result.text = "平手";
                
                WinOrLose = 2;
            }
            else if (EnemyTyoe == "enhance") //強化
            {
                result.text = "平手";
                
                WinOrLose = 2;
            }
            else if(EnemyTyoe == "damage") //傷害
            {
                result.text = "勝";
                WinOrLose = 1;

            }
            else if (EnemyTyoe =="scare")//驚嚇
            {
                result.text = "敗";

                WinOrLose = 0;

            }
        }*/

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

            BossGetDamage(20, false);

        }

        if (WinOrLose == 1 && PlayerType =="inspire" && EffectAndCount["cheer"] > 0)
        {
            result.text = "觸發自我打氣效果 勝";

            BossGetDamage(20, false);
        }
        
        return WinOrLose;
    } //檢查誰贏
    
    

    public void PlayerDamage(int value) //玩家受到傷害
    {
        PlayerHealth.value -= value;

        PlayerHealthText.text = PlayerHealth.value.ToString() + "%";
        
        Hope.value += Random.Range(5,10) * EnergyMpss;
        
        if (PlayerHealth.value == 0) //當玩家沒血
        {
            if (PlayerLife == 0) //當玩家沒意志
            {
                LoseGameEvent();
            }
            else //還有意志值
            {
                PlayerLife -= 1; //-1意志

                Destroy(PlayerWill.transform.parent.GetChild(1).gameObject); //扣除顯示意志值
                
                PlayerHealth.value = PlayerHealth.maxValue; //回復至滿血
                
                PlayerHealthText.text = PlayerHealth.value.ToString() + "%";

            }
        }
        
    }

    public void DecreaseEffect(bool Clear = false)
    {
        List<string> tempEffectname = new List<string>(EffectAndCount.Keys);
        
        foreach(string s in tempEffectname) //瀏覽所有效果
        {
            if(EffectAndCount[s] > 0) //減所有效果1回合
            {

                if (Clear)
                {
                    EffectAndCount[s] -= EffectAndCount[s]; //將所有效果-1回合

                }
                else
                {
                    EffectAndCount[s] -= 1; //將所有效果-1回合

                }
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
    }
    
    public void BossGetDamage(int value, bool isAnger) //敵人受傷
    {
        if (isAnger)
        {
            if (BoossAnger.value > 0)
            {
                Hope.value = 0;
            }
            BoossAnger.value -= value;
        }
        else
        {
            if (BoossAnger.value <=0)
            {
                if (BoossHealth.value > 0)
                {
                    Hope.value = 0;
                }
                
                BoossHealth.value -= value;
            }
        }
        if (BoossHealth.value == 0)
        {
            PlayerWinEnd();
        }
    }

    public async void PlayerWinEnd() //玩家獲勝
    {
        NowEnd = true;
        
        point += 1; //擊倒值+1

        showpoint.text = $"分數:{point}"; //同步顯示
            
        animeMgr.EnemyDie();

        await Task.Delay(1000); //因為無盡模式 要再次呼叫敵人
        
        DecreaseEffect(false);

        PlayerData data = new PlayerData();

        data.UesPlayCard = UesPlayCards;

        data.PlayerWins = PlayerWinss;
        
        data.point = point; //分數紀錄

        data.CardBackpack = senceSystem.CardBackpack; //紀錄卡堆

        data.HandCard = HandCardId; //記錄手牌堆

        data.UsedCard = UsedCard; //紀錄棄牌堆

        data.PlayerHealth = PlayerHealth.value; //紀錄玩家血量

        data.PlayerLife = PlayerLife; //紀錄玩家意志值

        BoossAnger.value = BoossAnger.maxValue; //怒氣植回滿血

        BoossHealth.value = PlayerHealth.maxValue; //存在植滿血

        playerseves.Seve(data); //使用紀錄函數
        
        if (senceSystem.state != "free")
        {
            animeMgr.BackGroundMove();
        }
        
        await Task.Delay(1000);

        if (senceSystem.state != "free")
        {
            rougeMgr.NewRound();
        }
        
        
        
        NowEnd = false;
    }

    public void LoseGameEvent() //遊戲結束 失敗
    {
        AllPoints = (int)(((point * 100 * KnockDwss) + UesPlayCards + PlayerWinss * 5) * Totalscoress);

        PlayerData data = playerseves.Load();

        if (data == null)
        {
            data = new PlayerData();
        }
        
        data.AllPoint += AllPoints;
        
        playerseves.Seve(data);

        LoseGameUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = point.ToString();
        
        LoseGameUI.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = UesPlayCards.ToString();

        LoseGameUI.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = PlayerWinss.ToString();

        LoseGameUI.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = AllPoints.ToString();
        
        LoseGameUI.transform.parent.gameObject.SetActive(true);
    }
    
    public void BossChooseCard(string BossType = "")
    {
        if (BossType == "")
        {
            string[] AllEnemyTag = new string[] { "defense", "damage", "scare" ,"enhance"};

            EnemyCardId = AllEnemyTag[Random.Range(0, AllEnemyTag.Length)];
        
            senceSystem.BidingImageToObject(EnemyCard, EnemyCardId, true);//設定圖片

            EnemyCard.SetActive(true);
        }
        else
        {
            string[] AllEnemyTag = new string[] { "defense", "damage", "scare" ,"enhance" };

            EnemyCardId = BossType;

            SHS_BossCard_id = "";

            EnemyCardId = AllEnemyTag[Random.Range(0, AllEnemyTag.Length)];
        
            senceSystem.BidingImageToObject(EnemyCard, EnemyCardId, true);//設定圖片

            EnemyCard.SetActive(true);//打開圖片
        }

    }

    public void BackTitle()
    {
        senceSystem.LoadScene("s0");
    }

    public void SHSUI(string n) //按鈕事件
    {
        SHS_BossCard_id = n; //強制更改
    }
    
    public void beliefUI(string n) //按鈕事件
    {
        belief_PlaterCard_id = n; //強制更改
    }
    
    void Update()
    
    {
        if (Input.GetKeyDown(KeyCode.K)) //必殺按鍵
        {
            BossGetDamage(100,true);
            
            BossGetDamage(100,false);
        }
    }
}
