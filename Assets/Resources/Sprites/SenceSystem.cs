using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceSystem : MonoBehaviour //儲存牌組數據
{
    [SerializeField] 
    public List<string> CardBackpack = new List<string>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string name) //換場景紀錄卡牌
    {
        SceneManager.LoadScene("S1");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
