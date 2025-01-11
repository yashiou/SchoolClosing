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

        MATtext.rectTransform.parent.localPosition = new Vector2(0, 250);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 210);

        MATtext.text = "�o�O�A����P";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(390, 430);

        MAT.rectTransform.sizeDelta = new Vector2(1136, 220);

        MATtext.rectTransform.parent.localPosition = new Vector2(0, -220);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 210);

        MATtext.text = "�o�O�ĤH����� �W��������";

        await WaitClick();

        MATtext.text = "���Ȭ��Ůɷ|����";

        await WaitClick();

        MATtext.text = "�U�����s�b��";

        await WaitClick();

        MATtext.text = "�s�b�Ȭ��Ůɷ|����U�@�ӼĤH";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(-702, -247);

        MAT.rectTransform.sizeDelta = new Vector2(514, 94);

        MATtext.rectTransform.parent.localPosition = new Vector2(42, 115);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 210);

        MATtext.text = "�o�O�A���z����";

        await WaitClick();

        MATtext.text = "�z���Ȭ��ūh�����C��";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(-805, -416);

        MAT.rectTransform.sizeDelta = new Vector2(306, 247);

        MATtext.rectTransform.parent.localPosition = new Vector2(326, 120);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 195);

        MATtext.text = "�����A���`�P��";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(-881, 130);

        MAT.rectTransform.sizeDelta = new Vector2(65, 379);

        MATtext.rectTransform.parent.localPosition = new Vector2(247, 279);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(650, 210);

        MATtext.text = "�o�O�A���Ʊ��";

        await WaitClick();

        MATtext.rectTransform.parent.localPosition = new Vector2(247, 279);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(650, 210);

        MATtext.text = "�Ʊ�ȷ|�H�ۦ^�X�W��";

        await WaitClick();

        MATtext.text = "��Q�����ɧƱ�Ȥ]�|�W��";

        await WaitClick();

        MATtext.text = "�ĤH�]�|�����ê�����";

        await WaitClick();

        MATtext.rectTransform.parent.localPosition = new Vector2(360, 160);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 175);

        MATtext.text = "�u����Ʊ�Ȱ���ĤH���Ȯ�\n�~�ন�\����";
        
        await WaitClick();

        MATtext.rectTransform.parent.localPosition = new Vector2(247, 279);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(480, 210);

        MATtext.text = "�������\�ɧƱ�ȷ|�k�s";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(-873, 453);

        MAT.rectTransform.sizeDelta = new Vector2(150, 150);

        MATtext.rectTransform.parent.localPosition = new Vector2(351, 20);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(530, 195);

        MATtext.text = "�A�i�H�b�]�w�A���[�ݱо�";

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

        MATtext.rectTransform.parent.localPosition = new Vector2(0, -350);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(820, 195);

        MATtext.text = "�A�i�H��ܤ@�i�A�Q�n���P�i�����";

        await WaitClick();

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 195);

        MATtext.text = "�@���|�إd�P";

        await WaitClick();

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(820, 195);

        MATtext.text = "�|�إd�P�u������۹����������d�P";

        await WaitClick();

        MAT.rectTransform.localPosition = new Vector2(853, -451);

        MAT.rectTransform.sizeDelta = new Vector2(150, 150);

        MATtext.rectTransform.parent.localPosition = new Vector2(-350, 20);

        MATtext.rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(610, 195);

        MATtext.text = "�A�i�H�I�����A���[�ݱо�";

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
