using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeachingMgr : MonoBehaviour
{
    [SerializeField]
    public List<Text> Pagetext = new List<Text>();

    [SerializeField]
    public List<Image> ShowImage = new List<Image>();

    private Image NowShowImage;

    private Text NowShowText; //當前顯示

    [SerializeField]
    private int PageNum;

    [SerializeField] 
    public GameObject dot_prefab;

    private List<Image> all_dot = new List<Image>();

    void Start()
    {
        Pagetext.ForEach(x => x.gameObject.SetActive(false)); //關閉所有文字

        ShowImage.ForEach(x => x.gameObject.SetActive(false)); //關閉所有圖片

        //檢查最大頁數
        int PagCount = ShowImage.Count > Pagetext.Count ? ShowImage.Count : Pagetext.Count;

        for (int i = 0; i < PagCount; i++)
        {
            GameObject dot = Instantiate(dot_prefab, dot_prefab.transform.parent); //生成點

            dot.GetComponent<Image>().color = Color.gray; //將顏色設為灰色

            all_dot.Add(dot.GetComponent<Image>()); //加入列表

            dot.SetActive(true);
        }
        
        ShowTargetPage(0); //打開第一頁
    }
    public void ShowTargetPage(int index)
    {
        bool Already = false;
        
        //當前要操作的物件

        if (index >= 0 && index < ShowImage.Count) //有符合範圍
        {
            Already = TurnOffLast(); //檢查是否執行過關閉上次事件
            
            NowShowImage = ShowImage[index]; //將當前圖片修改

            NowShowImage.gameObject.SetActive(true);

            PageNum = index; //紀錄這次的頁數 ( 下一次呼叫時 會視為"上次的物件"

            all_dot[index].color = Color.black;
        }

        if (index >= 0 && index < Pagetext.Count)
        {
            if (!Already)TurnOffLast();
            
            TurnOffLast();
            
            NowShowText = Pagetext[index];

            NowShowText.gameObject.SetActive(true);

            PageNum = index;
            
            all_dot[index].color = Color.black;
        }
    }

    public bool TurnOffLast()
    {
        if (NowShowImage !=null)
        {
            //上次的物件
            NowShowImage.gameObject.SetActive(false); //關閉上次的
        }

        if (NowShowText !=null)
        {
            NowShowText.gameObject.SetActive(false);
        }
        
        all_dot[PageNum].color = Color.gray; //將上一次的點顏色變為灰色
        
        return true;
    }

    public void TurnRight()
    {
        ShowTargetPage(PageNum +1);
    }

    public void TurnLeft()
    {
        ShowTargetPage(PageNum -1);
    }

    // Update is called once per frame
        void Update()
        {

        }
    }
//圖片會消失 要做存檔跟無盡模式