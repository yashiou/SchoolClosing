using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMusic : MonoBehaviour
{
    public AudieMusic audieMusic;

    void Start()
    {
        audieMusic = FindAnyObjectByType<AudieMusic>();
    }

    public void PlayAudio(int Index)
    {
        audieMusic.PlayAudio(Index);
    }
}
