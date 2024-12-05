using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMgr : MonoBehaviour
{
    [SerializeField] 
    public GameObject settingUI;
    
    [SerializeField]
    public AudieMusic audieMusic;
    
    [SerializeField] 
    private Slider MusicSlider;
    
    [SerializeField] 
    private Slider AudioSlider;
    
    [SerializeField] 
    private Slider AllSoundSlider;
    
    void Start()
    {
        audieMusic = FindObjectOfType<AudieMusic>();
    }
    
    void Update()
    {
        MusicSlider.value = audieMusic.MusicSoundValue; //同步音樂管理員

        AudioSlider.value = audieMusic.AudioSoundValue; 

        AllSoundSlider.value = audieMusic.AllSoundValue;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingUI.activeInHierarchy)
            {
                settingUI.SetActive(false);
            }
            else
            {
                settingUI.SetActive(true);
            }
        }
    }

    public void MusicSoundChange() //改變音樂
    {
        audieMusic.MusicSoundValue = MusicSlider.value;
    }

    public void AllSoundChange() //改變全部
    {
        audieMusic.AllSoundValue = AllSoundSlider.value;
    }

    public void AudioSoundChange() //改變音效
    {
        audieMusic.AudioSoundValue = AudioSlider.value;

    }
}
