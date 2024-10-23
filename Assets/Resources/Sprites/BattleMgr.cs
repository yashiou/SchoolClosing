using System.Collections;
using System.Collections.Generic;
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

    public int PlayrCardId, EnemyCardId;

    public List<string> HandCardId = new List<string>(); 

    public Text result; //文字綁定

    private bool OnJudge = false; //審判中
    void Start()
    {
        senceSystem = FindObjectOfType<SenceSystem>();
        for (int i = 0; i < 3; i++) //獲取手牌
        {
            GetCard();
        }
    }

    public void GetCard()
    {
        if (senceSystem.CardBackpack.Count > 0)
        {
            int GetRandTarger = Random.Range(0, senceSystem.CardBackpack.Count); //得到卡牌索引

            GameObject NowCard = Instantiate(Card_deck, Card_deck.transform.parent); //生成卡片
            
            senceSystem.BidingImageToObject(NowCard, int.Parse(senceSystem.CardBackpack[GetRandTarger]));

            NowCard.name = senceSystem.CardBackpack[GetRandTarger]; //賦予ID

            Button CardButton = NowCard.GetComponent<Button>(); //每張卡牌上的按鈕

            CardButton.onClick.AddListener(()=>UseCard(NowCard, NowCard.name)); //將按鈕綁定事件
            
            NowCard.SetActive(true); //啟用卡牌
            
            HandCardId.Add(senceSystem.CardBackpack[GetRandTarger]); //加入列表
            
            UsedCard.Add(senceSystem.CardBackpack[GetRandTarger]); //加入棄牌堆

            senceSystem.CardBackpack.Remove(senceSystem.CardBackpack[GetRandTarger]); //移除指定的卡

            CardStack.transform.GetChild(0).GetComponent<Text>().text = senceSystem.CardBackpack.Count.ToString(); //卡牌數
            
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
    public void UseCard(GameObject gameObject,string id)
    {
        
        
        if (OnJudge)
            return;

        Destroy(gameObject); //消失指定牌

        HandCardId.Remove(id); //移除手牌堆
        
        PlayrCardId = int.Parse(id);

        PlayrCard.GetComponent<Image>().sprite = senceSystem.AllCardIame[PlayrCardId]; // 設定圖片
        
        PlayrCard.SetActive(true);
        
        BossChooseCard(); //對手丟牌
        
        judgeSystem(id);
    }
    public async void judgeSystem(string id)
    {
        OnJudge = true;
        
        int WinOrLose = 2; //1是勝利2是平手0是敗
         
        if (PlayrCardId <= 6) //0~6勸導
        {
            if (EnemyCardId <= 6) //0~6防禦
            {
                result.text = "勝";
                WinOrLose = 1;
            }
            else if(7 <= EnemyCardId && EnemyCardId <= 14) //7~13傷害
            {
                result.text = "敗";
                WinOrLose = 0;
            }
            else if (EnemyCardId >=15)//14~20驚嚇
            {
                result.text = "平手";
                
                WinOrLose = 2;

            }
        }
        else if(7 <= PlayrCardId && PlayrCardId <= 14) //7~13堅定
        {
            if (EnemyCardId <= 6) //0~6防禦
            {
                result.text = "平手";
                
                WinOrLose = 2;
            }
            else if(7 <= EnemyCardId && EnemyCardId <= 14) //7~13傷害
            {
                result.text = "勝";
                WinOrLose = 1;

            }
            else if (EnemyCardId >=15)//14~20驚嚇
            {
                result.text = "敗";

                WinOrLose = 0;

            }
        }
        else if (PlayrCardId >=15) //14~20激勵
        {
            if (EnemyCardId <= 6) //0~6防禦
            {
                result.text = "敗";

                WinOrLose = 0;

            }
            else if(7 <= EnemyCardId && EnemyCardId <= 14) //7~13傷害
            {
                result.text = "平手";
                
                WinOrLose = 2;
            }
            else if (EnemyCardId >=15)//14~20驚嚇
            {
                result.text = "勝";

                WinOrLose = 1;

            }
        }
        
        result.gameObject.SetActive(true);

        await Task.Delay(2000);//等待兩秒
        
        result.gameObject.SetActive(false);

        
        if (WinOrLose == 1)
        {
            switch (id)
            {
                case "0" :
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
                case "10":
                    break;
                case "11" :
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
            
        }
        else if (WinOrLose == 0)
        {
            if (EnemyCardId <= 6) //0~6防禦
            {
                
            }
            else if(7 <= EnemyCardId && EnemyCardId <= 14) //7~13傷害
            {
                
            }
            else if (EnemyCardId >=15)//14~20驚嚇
            {
                
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
        OnJudge = false; //結束審判

    }

    public void BossChooseCard()
    {
        EnemyCardId = Random.Range(0, 21);
        
        EnemyCard.GetComponent<Image>().sprite = senceSystem.AllCardIame[EnemyCardId];
        
        EnemyCard.SetActive(true);

    }

    public void SetShowCard(Image samll_image) //設定顯示用大卡牌
    {
        
    }
    void Update()
    {
        
    }
}
