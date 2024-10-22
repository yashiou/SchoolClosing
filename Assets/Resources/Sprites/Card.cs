using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using System.Threading.Tasks;

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

    void Start()
    {

        senceSystem = FindObjectOfType<SenceSystem>(); //從倉庫抓數據過來

        for (int i = 0; i < 21; i++)
        {
            
            GameObject Card = Instantiate(Card_prefab, Card_prefab.transform.parent);

            Card.name = i.ToString();
            
            Card.SetActive(true);
            
            Showcard showcard = Card.AddComponent<Showcard>();//放大牌

            showcard.ShowBigImage = ShowCardBig;//放大牌

            Button Card_button = Card.GetComponent<Button>();
            
            Card_button.onClick.AddListener(() => CardEvent(Card.name));
            
        }
        
        
    }
    
     //放大卡牌

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
            
            Card.SetActive(true);

            Button Card_button = Card.GetComponent<Button>();
            
            ClearList.Add(Card);

            Showcard showcard = Card.AddComponent<Showcard>();

            showcard.ShowBigImage = ShowCardBig;
            
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