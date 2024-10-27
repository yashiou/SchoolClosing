using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMgr : MonoBehaviour
{
    [SerializeField]
    public AudieMusic audieMusic;
    
    [SerializeField] 
    private Slider MusicSlider;
    
    [SerializeField] 
    private Slider AudioSlider;
    
    [SerializeField] 
    private Slider AllSoundSlider;

    // Start is called before the first frame update
    void Start()
    {
        audieMusic = FindObjectOfType<AudieMusic>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
