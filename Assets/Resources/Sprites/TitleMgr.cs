using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
    public SenceSystem senceSystem;

    public Playerseves playerseves;
    
    [SerializeField]
    public GameObject ContinueButton;

    // Start is called before the first frame update
    void Start()
    {
        senceSystem = FindAnyObjectByType<SenceSystem>();
        
        senceSystem.audieMusic.PlayerMusic(0); //抓音樂

        playerseves = FindAnyObjectByType<Playerseves>(); 

        if (playerseves.Load().HandCard.Count <= 0)
        {
            ContinueButton.GetComponent<Image>().color = Color.gray; //按鈕便灰色
            
            ContinueButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            ContinueButton.GetComponent<Image>().color = Color.white;
            //找到之前的存檔 啟用按鈕
            ContinueButton.GetComponent<Button>().onClick.AddListener(() => Loading());
        }
        
    }

    public void InToGame(string name)
    {
        if (name == "CardScene")
        {
            senceSystem.state = "free";
        }
        else
        {
            senceSystem.state = "endless";
        }
        
        senceSystem.LoadScene(name);
    }

    public void SeveMusic()
    {
        PlayerData data = new PlayerData();

        data.AudioSoundValue = senceSystem.audieMusic.AudioSoundValue;

        data.AllSoundValue = senceSystem.audieMusic.AllSoundValue;

        data.MusicSoundValue = senceSystem.audieMusic.MusicSoundValue;
        
        playerseves.Seve(data);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Loading()
    { 
        playerseves.Loading();
    }

    public async void Endless()
    {
        for (int i = 0; i < 7; i++)
        {
            senceSystem.CardBackpack.Add("advise_03");
            
            senceSystem.CardBackpack.Add("steadfast_08");

            senceSystem.CardBackpack.Add("resist_15");

            senceSystem.CardBackpack.Add("inspire_23");

        }

        senceSystem.Changechg();

        await Task.Delay(2000);

        InToGame("S1");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
