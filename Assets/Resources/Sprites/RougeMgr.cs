using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Net.Mime;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class RougeMgr : MonoBehaviour

{

    [SerializeField]
    public GameObject Chose_Prefb  ;

    public SenceSystem senceSystem;
    
    private Dictionary<string, List<string>> categorized = new Dictionary<string, List<string>>();

    [SerializeField] 
    public GameObject RougeGameObject;

    private List<GameObject> ClearList = new List<GameObject>();

    [SerializeField] 
    public GameObject ChoseCardUI,Card_prefab,CardGrid; //選牌UI

    private string Chosed_Card_Id; //選擇牌

    [SerializeField]
    public BattleMgr battleMgr;

    private List<GameObject> typeGameObject = new List<GameObject>();
    void Start()
    {
        senceSystem = FindAnyObjectByType<SenceSystem>();

        List<string> TypeCard = new List<string>();

        foreach (var sprite in senceSystem.AllCardIame) //檢查所有排
        {
            string name = sprite.name.Split("_")[0]; //找出前贅詞

            if (!categorized.ContainsKey(name))
            {
                categorized[name] = new List<string>();
            }
            
            categorized[name].Add(sprite.name);
        }
    }

    public void NewRound()
    {
        for (int i = 0; i < typeGameObject.Count; i++)
        {
            Destroy(typeGameObject[i]);
        }
        typeGameObject.Clear();
        
        RougeGameObject.SetActive(true);

        string[] AllCardTypes = {"advise", "steadfast", "resist", "inspire"};

        foreach (string card in AllCardTypes)
        {
            string RandomCard = categorized[card][Random.Range(0, categorized[card].Count)]; //找出分類隨機抽取

            GameObject newCard = Instantiate(Chose_Prefb, Chose_Prefb.transform.parent); //生成卡牌
            
            senceSystem.BidingImageToObject(newCard, RandomCard); //將圖片賦予
            
            List<TextData> textData = senceSystem.CardText.TextFormat.Where(x => x.id == RandomCard.Split("_")[1] ).ToList();

            newCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Name; //綁名字文字
            
            newCard.transform.GetChild(1).GetComponent<Text>().text = textData[0].Effect;//綁效果文字

            newCard.GetComponent<Button>().onClick.AddListener(() => ToChange(RandomCard));
        
            newCard.GetComponent<Button>().onClick.AddListener(() => ChoseCard());
            
            newCard.SetActive(true);
            
            typeGameObject.Add(newCard);
        }
    }

    public void ChoseCard()
    {
        for (int i = 0; i < ClearList.Count; i++)
        {
            Destroy(ClearList[i]);
        }
        
        ClearList.Clear();

        List<string> GetMycard = battleMgr.UsedCard.Concat(senceSystem.CardBackpack).ToList(); //合併所有排

        List<string> Typecard = new List<string>();

        foreach (string card in GetMycard) //檢查所有排
        {
            string type = card.Split("_")[0]; //找出前贅詞
            
            if (type == Chosed_Card_Id.Split("_")[0])
            {
                Typecard.Add(card);
            }
        }

        for (int i = 0; i < Typecard.Count; i++)
        {
            GameObject Card = Instantiate(Card_prefab, CardGrid.transform);

            Card.name = Typecard[i];
            
            senceSystem.BidingImageToObject(Card, Typecard[i]);

            Card.SetActive(true);

            Button Card_button = Card.GetComponent<Button>();
            
            ClearList.Add(Card);

            Text Cardtext = Card.GetComponentInChildren<Text>();

            Text CardEffect = Card.GetComponentInChildren<Text>();
            
            //根據ID找到所有符合條件的物件TextData物件 並整理成數據 交給.ToList()轉成陣列
            List<TextData> textData = senceSystem.CardText.TextFormat.Where(x => x.id == Card.name.Split("_")[1] ).ToList();
            
            Cardtext.text = textData[0].Name;//給予名字

            Cardtext.text = textData[0].Effect; //給予效果
            
            Card_button.onClick.AddListener(() => ChangeCard(Card.name));

            
        }
        ChoseCardUI.SetActive(true);
    }

    public void ToChange(string id) //點選牌後 將ID賦予
    {
        Chosed_Card_Id = id;
    }
    
    public void ChangeCard(string id)
    {
        for (int i = 0; i < senceSystem.CardBackpack.Count; i++)
        {
            if (id == senceSystem.CardBackpack[i])
            {
                senceSystem.CardBackpack[i] = Chosed_Card_Id;
                
                RougeGameObject.SetActive(false);
        
                ChoseCardUI.SetActive(false);
            
                return;
            }
        }
        
        for (int i = 0; i < battleMgr.UsedCard.Count; i++)
        {
            if (id == battleMgr.UsedCard[i])
            {
                battleMgr.UsedCard[i] = Chosed_Card_Id;
                
                RougeGameObject.SetActive(false);
        
                ChoseCardUI.SetActive(false);
                
                return;
            }
        }
    }
    
    void Update()
    {
        
    }
}
