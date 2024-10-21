using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceSystem : MonoBehaviour
{
    [SerializeField] 
    public List<string> CardBackpack = new List<string>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string name)
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
