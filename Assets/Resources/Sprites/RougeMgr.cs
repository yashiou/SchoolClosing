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
    public GameObject AdviseCard, SteadfastCard, ResistCard, InspireCard;

    public SenceSystem senceSystem;
    
    private Dictionary<string, List<string>> categorized = new Dictionary<string, List<string>>();

    [SerializeField] 
    public GameObject RougeGameObject;

    private List<GameObject> ClearList = new List<GameObject>();

    [SerializeField] 
    public GameObject ChoseCardUI,Card_prefab,CardGrid; //選牌UI

    private string Chosed_Card_Id; //選擇牌
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
        RougeGameObject.SetActive(true);
        
        //抽選 advise 隨機牌
        string RandomCard = categorized["advise"][Random.Range(0, categorized["advise"].Count)];
        
        //牌圖片賦予
        senceSystem.BidingImageToObject(AdviseCard, RandomCard);
        List<TextData> textData = senceSystem.CardText.TextFormat.Where(x => x.id == RandomCard.Split("_")[1] ).ToList();

        AdviseCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Name;
        
        AdviseCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Effect;
        
        AdviseCard.GetComponent<Button>().onClick.AddListener(() => ToChange(textData[0].Name));
        
        AdviseCard.GetComponent<Button>().onClick.AddListener(() => ChoseCard());
        
        //抽選 Steadfast 隨機牌
        RandomCard = categorized["steadfast"][Random.Range(0, categorized["steadfast"].Count)];
        
        //牌圖片賦予
        senceSystem.BidingImageToObject(SteadfastCard, RandomCard);
        
        SteadfastCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Name;
        
        SteadfastCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Effect;
        
        SteadfastCard.GetComponent<Button>().onClick.AddListener(() => ToChange(textData[0].Name));

        SteadfastCard.GetComponent<Button>().onClick.AddListener(() => ChoseCard());

        
        //抽選 resist 隨機牌
        RandomCard = categorized["resist"][Random.Range(0, categorized["resist"].Count)];
        
        //牌圖片賦予
        senceSystem.BidingImageToObject(ResistCard, RandomCard);
        
        ResistCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Name;
        
        ResistCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Effect;
        
        ResistCard.GetComponent<Button>().onClick.AddListener(() => ToChange(textData[0].Name));

        ResistCard.GetComponent<Button>().onClick.AddListener(() => ChoseCard());

        
        //抽選 inspire 隨機牌
        RandomCard = categorized["inspire"][Random.Range(0, categorized["inspire"].Count)];
        
        //牌圖片賦予
        senceSystem.BidingImageToObject(InspireCard, RandomCard);
        
        InspireCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Name;
        
        InspireCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Effect;
        
        InspireCard.GetComponent<Button>().onClick.AddListener(() => ToChange(textData[0].Name));

        InspireCard.GetComponent<Button>().onClick.AddListener(() => ChoseCard());
    }

    public void ChoseCard()
    {
        for (int i = 0; i < ClearList.Count; i++)
        {
            Destroy(ClearList[i]);
        }

        for (int i = 0; i < senceSystem.CardBackpack.Count; i++)
        {
            GameObject Card = Instantiate(Card_prefab, CardGrid.transform);

            Card.name = senceSystem.CardBackpack[i];
            
            senceSystem.BidingImageToObject(Card, senceSystem.CardBackpack[i]);
            
            Card.SetActive(true);

            Button Card_button = Card.GetComponent<Button>();
            
            ClearList.Add(Card);

            Text Cardtext = Card.GetComponentInChildren<Text>();

            
            //根據ID找到所有符合條件的物件TextData物件 並整理成數據 交給.ToList()轉成陣列
            List<TextData> textData = senceSystem.CardText.TextFormat.Where(x => x.id == Card.name.Split("_")[1] ).ToList();

            Cardtext.text = textData[0].Name;//給予名字

            
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
            if (Chosed_Card_Id == id)
            {
                senceSystem.CardBackpack[i] = Chosed_Card_Id;
            }
        }
        
        ChoseCardUI.transform.gameObject.SetActive(false);
    }
    
    void Update()
    {
        
    }
}
