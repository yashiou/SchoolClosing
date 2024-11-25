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

    public SenceSystem sencesystem;
    
    private Dictionary<string, List<string>> categorized = new Dictionary<string, List<string>>();

    [SerializeField] 
    public GameObject RougeGameObject;
    void Start()
    {
        sencesystem = FindAnyObjectByType<SenceSystem>();

        List<string> TypeCard = new List<string>();

        foreach (var sprite in sencesystem.AllCardIame) //檢查所有排
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
        //抽選 advise 隨機牌
        string RandomCard = categorized["advise"][Random.Range(0, categorized["advise"].Count)];
        
        //牌圖片賦予
        sencesystem.BidingImageToObject(AdviseCard, RandomCard);
        List<TextData> textData = sencesystem.CardText.TextFormat.Where(x => x.id == RandomCard.Split("_")[1] ).ToList();

        AdviseCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Name;
        
        AdviseCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Effect;
        
        //抽選 Steadfast 隨機牌
        RandomCard = categorized["steadfast"][Random.Range(0, categorized["steadfast"].Count)];
        
        //牌圖片賦予
        sencesystem.BidingImageToObject(SteadfastCard, RandomCard);
        
        SteadfastCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Name;
        
        SteadfastCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Effect;
        
        //抽選 resist 隨機牌
        RandomCard = categorized["resist"][Random.Range(0, categorized["resist"].Count)];
        
        //牌圖片賦予
        sencesystem.BidingImageToObject(ResistCard, RandomCard);
        
        ResistCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Name;
        
        ResistCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Effect;
        
        //抽選 inspire 隨機牌
        RandomCard = categorized["inspire"][Random.Range(0, categorized["inspire"].Count)];
        
        //牌圖片賦予
        sencesystem.BidingImageToObject(InspireCard, RandomCard);
        
        ResistCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Name;
        
        ResistCard.transform.GetChild(0).GetComponent<Text>().text = textData[0].Effect;
        
    }

    public void ChoseCard(string id)
    {
        
    }
    
    void Update()
    {
        
    }
}
