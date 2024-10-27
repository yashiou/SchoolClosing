using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMgr : MonoBehaviour
{
    public SenceSystem senceSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        senceSystem = FindObjectOfType<SenceSystem>();
        
        senceSystem.audieMusic.PlayerMusic(0); //抓音樂

    }

    public void InToGame(string name)
    {
        senceSystem.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
