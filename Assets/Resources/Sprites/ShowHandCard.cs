using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowHandCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler //放大手牌
{
    public BattleMgr battleMgr;

    private RectTransform reetTransform;


    private float offset = 10;

    private Image myImage; //此卡圖片
    void Start()
    {
        battleMgr = FindObjectOfType<BattleMgr>();

        reetTransform = gameObject.GetComponent<RectTransform>();

        myImage = gameObject.GetComponent<Image>();
    }

    
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        reetTransform.anchoredPosition += Vector2.up * offset;

        //將大圖片改成小圖
        battleMgr.ShowCard.GetComponent<Image>().sprite = myImage.sprite;
        //顯示大圖片
        battleMgr.ShowCard.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        reetTransform.anchoredPosition -= Vector2.up * offset;
        
        battleMgr.ShowCard.SetActive(false);
    }
}
