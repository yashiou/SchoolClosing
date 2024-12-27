using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudieMusic : MonoBehaviour
{
    public AudioSource MusicSource; //音樂撥放器

    public AudioSource audioSource; //音效

    public float AudioSoundValue, MusicSoundValue,AllSoundValue; //兩個音量調整
    
    [SerializeField] 
    public List<AudioClip> Music = new List<AudioClip>(); //所有音樂
    
    [SerializeField] 
    public List<AudioClip> Audie = new List<AudioClip>(); //所有音效

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); //避免被卸載
    }

    // Start is called before the first frame update
    void Start()
    {

         AudioSoundValue = 1.0f;
         
         MusicSoundValue = 1.0f;
         
         AllSoundValue = 0.5f;
    }

    public void PlayerMusic(int index) //指定撥放音樂
    {
        MusicSource.clip = Music[index];

        MusicSource.Play(); 
    }

    public void StopAllSound()
    {
        MusicSource.Stop(); //停止所有音樂
    }


    public void PlayAudio(int Index)
    {
        audioSource.PlayOneShot(Audie[Index]);
    }

    // Update is called once per frame
    void Update()
    {
        MusicSource.volume = MusicSoundValue * AllSoundValue;

        audioSource.volume = AudioSoundValue * AllSoundValue;
    }
}
