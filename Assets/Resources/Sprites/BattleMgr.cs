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
        {"KnowBossEng",0 },
        { "report",0},
        { "cheer",0},
        { "protect",0},
        {"adrenaline",0},
        {"SHS" ,0},
        {"belief", 0}
    };
    
    private string SHS_BossCard_id = ""; //強制指定敵人ID

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

    public SettingMgr settingMgr;

    public GameObject NoErrorPNG;

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

        PlayerHealth.maxValue += playerData.Life * 10;

        PlayerHealth.value = PlayerHealth.maxValue;

        PlayerHealthText.text = PlayerHealth.value.ToString() + "%";

        Hope.maxValue += playerData.EnergyUp * 10;

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

        BossHope.value = Random.Range(0, 100);


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

        NoErrorPNG.SetActive(true);

        PlayrCardId = id;
        
        HandCardId.Remove(id); //移除手牌堆
        
        senceSystem.BidingImageToObject(PlayrCard, PlayrCardId); // 設定圖片

        
        Hope.value += Random.Range(10,20) * EnergyMpss;
        
        CheckCard(id.Split("_")[1]);
        
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

        result.gameObject.SetActive(true);

        if (WinOrLose == 1) //成功時
        {
            PlayerWin(CardIdNumber);
        }
        else if (WinOrLose == 0)//失敗時
        {
            BossWin(CardIdNumber);
        }

        result.gameObject.SetActive(true);

        await Task.Delay(1000);//等待兩秒

        result.gameObject.SetActive(false);
        
        PlayrCard.SetActive(false);
        
        EnemyCard.SetActive(false);
        
        BossHope.value = Random.Range(0, 100);

        for (int i = 0; i < 2; i++) //抽牌
        {
            GetCard();
            
            if (HandCardId.Count >=5)
            {
                break;
            }
        }

        NoErrorPNG.SetActive(false);

        DecreaseEffect();//檢索有效果1回合

        if (EffectAndCount["KnowBossEng"] > 0) //預測牌效果
        {
            BossHope.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            BossHope.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (EffectAndCount["SHS"] > 0)
        {
            SHSButnUI.SetActive(true);
        }
        else if (EffectAndCount["belief"] > 0)
        {
            beliefButnUI.SetActive(true);
        }

        if (EffectAndCount["adrenaline"] > 0)
        {
            BossGetDamage(15, false);

            
        }

        OnJudge = false; //結束審判
    }

    public void BossWin(string id)
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
            result.text = "格擋";
            return;
            case "09":
                break;
            case "10" :
                break;
            case "11":
            result.text = "迴避";
            return;
            case "12" :

            if (EnemyTyoe == "damage") //7~13傷害
            {
                if (BoossAnger.value > 0)
                {
                    BossGetDamage(10, true); 
                }
                else
                {
                    BossGetDamage(10, false);
                }
                result.text = "反擊傷害";
            }
            else if (EnemyTyoe == "scare")//14~20驚嚇
            {
                if (BoossAnger.value > 0)
                {
                    BossGetDamage(5, true);
                }
                else
                {
                    BossGetDamage(5, false);
                }
                result.text = "反擊傷害";
            }
            return;
        case "13": 
            break;
        case "14" :
        result.text = "格擋";
        return;
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
            BossGetDamage(5, false);
            break;
        case "21":
            break;
        case "23":
            result.text = "格擋";
            return;
        case "24":
            result.text = "轉變傷害";

        if (EnemyTyoe == "damage") //7~13傷害
        {
            PlayerDamage(-10); //增加san值
        }
        else if (EnemyTyoe == "scare")//14~20驚嚇
        {
            PlayerDamage(-5); //增加san值
        }
        return;
        default: 
            break;
        }

        if (EffectAndCount["firm"] >= 1 ) //當具有小雨傘時格擋傷害
        {
            result.text = "觸發效果 格擋";

            return;
        }

        if (EffectAndCount["report"] >= 1 && (EnemyTyoe == "damage" || EnemyTyoe == "scare")) //當具有小雨傘時格擋傷害
        {
            

            if (EnemyTyoe == "damage") //7~13傷害
            {
                result.text = "敵人剝奪了你10存在";
                PlayerDamage(10); //減少san值
            }
            else if (EnemyTyoe == "scare")//14~20驚嚇
            {
                result.text = "敵人剝奪了你5存在";
                PlayerDamage(5); //減少san值

            }

            result.text += "觸發稟告";

            BossGetDamage(20, false);

            return;

        }

        if (EnemyTyoe == "defense") //0~6防禦
        {
        result.text = "敵人觀察中";
        }
        else if (EnemyTyoe == "damage") //7~13傷害
        {
            result.text = "敵人剝奪了你10存在";
            PlayerDamage(10); //減少san值
        }
        else if (EnemyTyoe == "scare")//14~20驚嚇
        {
            result.text = "敵人剝奪了你5存在";
            PlayerDamage(5); //減少san值

        }
        else if (EnemyTyoe == "enhance")//強化
        {
            result.text = "敵人回復了10存在";
            BoossHealth.value += 10;//回復牌
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
                    BossGetDamage(20, false);
                    
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
                GetEffect(PlayrEffectBar, "KnowBossEng");
                    break;
                case "14": //蜷縮
                    GetEffect(PlayrEffectBar,"firm");
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
                    break;
                
                case "24"://心跳加速
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

        if (Hope.value > BossHope.value)
        {
            PlayrCard.SetActive(true);

            result.text = "";
                
            WinOrLose = 1;
        }
        else
        {
            result.text = "";
            WinOrLose = 0;

            if (SHS_BossCard_id != null)
            {
                BossChooseCard(SHS_BossCard_id);
            }
            else
            {
                BossChooseCard(); //對手丟牌
            }

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
        
        return WinOrLose;
    } //檢查誰贏
    
    

    public void PlayerDamage(int value) //玩家受到傷害
    {
        if (EffectAndCount["protect"] == 1)
        {
            result.text = "敵人傷害無效";
        }

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
    
    public async void BossGetDamage(int value, bool isAnger) //敵人受傷
    {

        if (isAnger)
        {
            if (BoossAnger.value > 0)
            {
                Hope.value = 0;

                result.text = "命中";
            }
            else
            {
                result.text = "無效";
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

                    result.text = "命中";
                }
                BoossHealth.value -= value;
            }
            else
            {
                result.text = "無效";
            }
        }
        
        if (BoossHealth.value == 0)
        {
            PlayerWinEnd();
        }
        if (EffectAndCount["cheer"] >= 1)
        {
            if (BoossAnger.value <= 0)
            {
                if (BoossHealth.value > 0)
                {
                    BoossHealth.value -= 20;
                }
            }
            else
            {
                result.text = "無效";
            }

            result.text += "觸發自我打氣";

            if (BoossHealth.value == 0)
            {
                PlayerWinEnd();
            }

        }

        result.gameObject.SetActive(true);

        await Task.Delay(1000);//等待兩秒

        result.gameObject.SetActive(false);
    }

    public void cheers()
    {
        
    }
        

public async void PlayerWinEnd() //玩家獲勝
    {
        NowEnd = true;
        
        point += 1; //擊倒值+1

        showpoint.text = $"分數:{point}"; //同步顯示
            
        animeMgr.EnemyDie();

        await Task.Delay(1000); //因為無盡模式 要再次呼叫敵人
        
        DecreaseEffect(false);

        PlayerData data = playerseves.Load();

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

        data.HandCard.Clear();

        data.AllPoint += AllPoints;

        senceSystem.CardBackpack.Clear();

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
    
    public void beliefUI(int n) //按鈕事件
    {
        if (n == 1)
        {
            PlayerDamage(-5);
        }
        else if (n == 2)
        {
            BossGetDamage(5, false);
            BossGetDamage(15, true);
        }
        else if (n == 3)
        {
            GetEffect(PlayrEffectBar, "firm");

            EffectAndCount["firm"] -= 1 ;

        }
    }
    
    void Update()
    
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //設定
        {
            settingMgr.gameObject.SetActive(!settingMgr.gameObject.activeInHierarchy);
        }

        /*if (Input.GetKeyDown(KeyCode.K)) //必殺按鍵
        {
            BossGetDamage(100,true);
            
            BossGetDamage(100,false);
        }

        if (Input.GetKeyDown(KeyCode.L)) //必殺按鍵
        {
            PlayerDamage(1000);

            PlayerDamage(1000);
        }*/
    }
}
