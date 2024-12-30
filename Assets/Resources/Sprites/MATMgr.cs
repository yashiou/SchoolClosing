using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MATMgr : MonoBehaviour
{

    public GameObject MATBack;

    public Image MAT;

    private bool IsClick = false;

    private Action OnupDate;

    public Text MATtext;

    private Playerseves playerseves;

    void Start()
    {
        playerseves = FindAnyObjectByType<Playerseves>();


        if (!playerseves.Load().LookMAT)
        {
            ShowMAT();
        }

        
    }

    public async void ShowMAT() 
    {
        PlayerData playerData = playerseves.Load();

        playerData.LookMAT = true;

        playerseves.Seve(playerData);

        MATBack.gameObject.SetActive(true);

        MAT.gameObject.SetActive(true);

        MAT.rectTransform.localPosition = new Vector2(0, -350) ;

        MAT.rectTransform.sizeDelta = new Vector2(1282, 362);

        MATtext.rectTransform.parent.localPosition = new Vector2(0, 230);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 60);

        MATtext.text = "這是你的手牌";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(390, 430);

        MAT.rectTransform.sizeDelta = new Vector2(1136, 220);

        MATtext.rectTransform.parent.localPosition = new Vector2(0, -158);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 60);

        MATtext.text = "這是敵人的血條 上面為怨氣值";

        await WaitClick();

        MATtext.text = "怨氣值為空時會昏厥";

        await WaitClick();

        MATtext.text = "下面為存在值";

        await WaitClick();

        MATtext.text = "存在值為空時會跳到下一個敵人";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(-702, -247);

        MAT.rectTransform.sizeDelta = new Vector2(514, 94);

        MATtext.rectTransform.parent.localPosition = new Vector2(42, 95);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 60);

        MATtext.text = "這是你的理智值";

        await WaitClick();

        MATtext.text = "理智值為空則結束遊戲";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(-805, -416);

        MAT.rectTransform.sizeDelta = new Vector2(306, 247);

        MATtext.rectTransform.parent.localPosition = new Vector2(326, 100);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 45);

        MATtext.text = "此為你的總牌數";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(-881, 130);

        MAT.rectTransform.sizeDelta = new Vector2(65, 379);

        MATtext.rectTransform.parent.localPosition = new Vector2(247, 259);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(480, 60);

        MATtext.text = "這是你的希望值";

        await WaitClick();

        MATtext.text = "希望值會隨著回合上漲";

        await WaitClick();

        MATtext.text = "當被攻擊時希望值也會上漲";

        await WaitClick();

        MATtext.text = "敵人也會有隱藏的怒氣值";

        await WaitClick();

        MATtext.rectTransform.parent.localPosition = new Vector2(287, 140);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(480, 95);

        MATtext.text = "只有當希望值高於敵人怒氣值時才能成功攻擊";
        
        await WaitClick();

        MATtext.rectTransform.parent.localPosition = new Vector2(247, 259);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(480, 60);

        MATtext.text = "攻擊成功時希望值會歸零";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(-873, 453);

        MAT.rectTransform.sizeDelta = new Vector2(150, 150);

        MATtext.rectTransform.parent.localPosition = new Vector2(351, 0);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(497, 45);

        MATtext.text = "你可以在設定再次觀看教學";

        await WaitClick();

        MATBack.gameObject.SetActive(false);

        MAT.gameObject.SetActive(false);
    }

    public async void RougeMAT()
    {
        PlayerData playerData = playerseves.Load();

        playerData.LookRougeMAT = true;

        playerseves.Seve(playerData);

        MATBack.gameObject.SetActive(true);

        MAT.gameObject.SetActive(true);

        MAT.rectTransform.localPosition = new Vector2(0, 0);

        MAT.rectTransform.sizeDelta = new Vector2(1362, 480);

        MATtext.rectTransform.parent.localPosition = new Vector2(0, -280);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(653, 45);

        MATtext.text = "你可以選擇一張你想要的牌進行替換";

        await WaitClick();

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(252, 45);

        MATtext.text = "共有四種卡牌";

        await WaitClick();

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(665, 45);

        MATtext.text = "四種卡牌只能替換相對應類型的卡牌";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(853, -451);

        MAT.rectTransform.sizeDelta = new Vector2(150, 150);

        MATtext.rectTransform.parent.localPosition = new Vector2(-350, 0);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(502, 45);

        MATtext.text = "你可以點擊此再次觀看教學";

        MATBack.gameObject.SetActive(false);

        MAT.gameObject.SetActive(false);
    }

    public async Task WaitClick() 
    {

        OnupDate += KeyDown;

        while (!IsClick)
        {
            await Task.Yield();
        }

        await Task.Delay(100);

        IsClick = false;

        OnupDate -= KeyDown;
    }

    private void KeyDown()
    {
        if (Input.anyKeyDown) IsClick = true;
    }



    // Update is called once per frame
    void Update()
    {
        OnupDate?.Invoke();
    }
}
