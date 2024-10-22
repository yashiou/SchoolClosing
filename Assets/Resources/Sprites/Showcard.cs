using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Showcard : MonoBehaviour , IPointerEnterHandler ,IPointerExitHandler
{
    public GameObject ShowBigImage;

    public ShowCardIamg ShowCardiamg;
    void Start()
    {
        ShowCardiamg = ShowBigImage.GetComponent<ShowCardIamg>();
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
        
        //ShowBigImage.GetComponent<Image>().sprite = //等圖片
        
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
