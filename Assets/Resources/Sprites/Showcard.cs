using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Showcard : MonoBehaviour , IPointerEnterHandler ,IPointerExitHandler
{
    public GameObject ShowBigImage;

    public ShowCardIamg ShowCardiamg;

    public Card card;

    public string CardEffect; //顯示牌效果

    public string CardName; //顯示名字
    

    void Start()
    {
        ShowCardiamg = ShowBigImage.GetComponent<ShowCardIamg>();
        
        gameObject.GetComponent<Button>().onClick.AddListener(() => StopCoroutine("CardDelayScale"));
        
        gameObject.GetComponent<Button>().onClick.AddListener(() => ShowBigImage.SetActive(false));
        
        
    }
    
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine("CardDelayScale");
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine("CardDelayScale");

        CloseCardScale();
    }
    

    private IEnumerator CardDelayScale()
    {
        yield return new WaitForSeconds(1);

        ShowBigImage.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;//圖片

        Button BigImageButton = ShowBigImage.GetComponent<Button>();
        
        BigImageButton.onClick.RemoveAllListeners();//清除按鈕綁定事件
        
        BigImageButton.onClick.AddListener(()=> card.CardEvent(name));//綁定新按鈕事件

        BigImageButton.onClick.AddListener(()=> ShowBigImage.SetActive(false)); //關閉大圖

        ShowBigImage.transform.GetChild(2).GetComponent<Text>().text = CardEffect; //修改大圖文字
        
        ShowBigImage.transform.GetChild(1).GetComponent<Text>().text = CardName;
        
        ShowBigImage.SetActive(true);

            yield return null;
    }
    
    private async void CloseCardScale()
    {
        await Task.Delay(100);

        if (ShowCardiamg.OnTop == false)
        {
            ShowBigImage.SetActive(false);
        }
    }

}
