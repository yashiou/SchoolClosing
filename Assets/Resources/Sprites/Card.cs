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
    [SerializeField]//保護
    public GameObject Card_prefab;
    
    public SenceSystem senceSystem;
    
    public List<GameObject> ClearList = new List<GameObject>();
    
    [SerializeField]
    public GameObject CardBackpack;
    
    [SerializeField]
    public Text Card_Hint;

    void Start()
    {

        senceSystem = FindObjectOfType<SenceSystem>();

        for (int i = 0; i < 21; i++)
        {
            
            GameObject Card = Instantiate(Card_prefab, Card_prefab.transform.parent);

            Card.name = i.ToString();
            
            Card.SetActive(true);

            Button Card_button = Card.GetComponent<Button>();
            
            Card_button.onClick.AddListener(() => CardEvent(Card.name));
            
        }
        
        
    }

    public void OpenBackPack()
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
            
            
            
        }
        CardBackpack.transform.parent.gameObject.SetActive(true);
    }

    public void ToBattle()
    {
        if (senceSystem.CardBackpack.Count < 14)
        {
            ShowHint("牌數不夠14張");
        }
        else
        {
            senceSystem.LoadScene("S1");
        }
        
    }

    public async void ShowHint(string text)
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
                cardLimit++; //
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