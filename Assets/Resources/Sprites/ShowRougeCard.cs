using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class ShowRougeCard : MonoBehaviour , IPointerEnterHandler ,IPointerExitHandler
{
    public GameObject ShowBigImage;

    public ShowCardIamg ShowCardiamg;
    
    public string CardEffect; //顯示牌效果

    private RougeMgr RougeMgr;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => StopCoroutine("CardDelayScale"));
        
        gameObject.GetComponent<Button>().onClick.AddListener(() => ShowBigImage.SetActive(false));

        RougeMgr = FindObjectOfType<RougeMgr>();
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
        
        BigImageButton.onClick.AddListener(()=> RougeMgr.ChangeCard(gameObject.name));//綁定新按鈕事件

        BigImageButton.onClick.AddListener(()=> ShowBigImage.SetActive(false)); //關閉大圖

        ShowBigImage.GetComponentInChildren<Text>().text = CardEffect; //修改大圖文字
        
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
