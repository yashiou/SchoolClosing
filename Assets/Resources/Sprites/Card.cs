using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using System.Linq;

public class Card : MonoBehaviour
{
    [SerializeField]//<---保護程式碼用
    public GameObject Card_prefab;
    
    public SenceSystem senceSystem;
    
    public List<GameObject> ClearList = new List<GameObject>();
    
    [SerializeField]
    public GameObject CardBackpack;
    
    [SerializeField]
    public Text Card_Hint;
    
    [SerializeField]
    public GameObject ShowCardBig; //放大卡牌

    [SerializeField] 
    private bool Tester;//測試者模式

    /*[SerializeField] 
    private int testcard = 0;*/ //要測試號碼



    void Start()
    {
        
        senceSystem = FindObjectOfType<SenceSystem>(); //從倉庫抓數據過來

        /*if (Tester)
        {
            for (int i = 0; i < 21; i++)
            {
                senceSystem.CardBackpack.Add(testcard.ToString());
            }
        }*/

        for (int i = 0; i < senceSystem.AllCardIame.Count; i++)
        {
            
            GameObject Card = Instantiate(Card_prefab, Card_prefab.transform.parent);//生成

            Card.name = senceSystem.AllCardIame[i].name;
            
            senceSystem.BidingImageToObject(Card, Card.name); //綁定id與圖片
            
            Card.SetActive(true);
            
            Showcard showcard = Card.AddComponent<Showcard>();//放大牌

            showcard.ShowBigImage = ShowCardBig;//放大牌綁定

            Text Cardtext = Card.GetComponentInChildren<Text>();

            //根據ID找到所有符合條件的物件TextData物件 並整理成數據 交給.ToList()轉成陣列
            List<TextData> textData = senceSystem.CardText.TextFormat.Where(x => x.id == Card.name.Split("_")[1] ).ToList();

            showcard.CardEffect = textData[0].Effect;//效果
            
            Cardtext.text = textData[0].Name;//給予名字
            
            showcard.card = this;

            Button Card_button = Card.GetComponent<Button>();
            
            
            
            Card_button.onClick.AddListener(() => CardEvent(Card.name)); //驅動事件
            
            
            
        }

    }

    public void DeleetCard(string id,GameObject CardObject) //刪除卡牌
    {
        if (senceSystem.CardBackpack.Contains(id))
        {
            Destroy(CardObject);
            
            senceSystem.CardBackpack.Remove(id); //刪除卡牌id
        }
    }

    public void OpenBackPack()//背包
    {
        for (int i = 0; i < ClearList.Count; i++)
        {
            Destroy(ClearList[i]);
        }

        for (int i = 0; i < senceSystem.CardBackpack.Count; i++)
        {
            GameObject Card = Instantiate(Card_prefab, CardBackpack.transform);

            Card.name = senceSystem.CardBackpack[i];
            
            senceSystem.BidingImageToObject(Card, senceSystem.CardBackpack[i]);
            
            Card.SetActive(true);

            Button Card_button = Card.GetComponent<Button>();
            
            ClearList.Add(Card);

            Text Cardtext = Card.GetComponentInChildren<Text>();

            
            //根據ID找到所有符合條件的物件TextData物件 並整理成數據 交給.ToList()轉成陣列
            List<TextData> textData = senceSystem.CardText.TextFormat.Where(x => x.id == Card.name.Split("_")[1] ).ToList();

            Cardtext.text = textData[0].Name;//給予名字

            
            Card_button.onClick.AddListener(() => DeleetCard(Card.name, Card));

            
        }
        CardBackpack.transform.parent.gameObject.SetActive(true);
    }

    public void ToBattle()//卡牌不夠時顯示
    {
        if (senceSystem.CardBackpack.Count < 21)
        {
            ShowHint("牌數不夠21張!!!");
        }
        else
        {
            senceSystem.LoadScene("S1");
        }
        
    }

    public async void ShowHint(string text)//卡牌不夠時顯示(顯示的字效果)
    {
        Card_Hint.gameObject.SetActive(true);

        Card_Hint.text = text;
        
        await Task.Delay(500);
        
        Card_Hint.gameObject.SetActive(false);
    }

    public void CardEvent(string id)
    {
        int cardLimit = 0;
        if (senceSystem.CardBackpack.Count >= 21)
        {
            ShowHint("牌數已達21張!");
                
            return;
            
        }
        
        foreach (string s in senceSystem.CardBackpack) //檢查每張卡ID
        {
            if (id == s )
            {
                cardLimit++;
            }

            if (cardLimit == 3)
            {
                return; 
            }
        }
        
        
        senceSystem.CardBackpack.Add(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}