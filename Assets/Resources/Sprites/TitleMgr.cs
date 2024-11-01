using System.Collections;
using System.Collections.Generic;
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

        if (playerseves.Load() ==null)
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
