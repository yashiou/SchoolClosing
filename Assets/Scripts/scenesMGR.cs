using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scenesMGR : MonoBehaviour
{
    public Button Btn1, Btn2, Btn3;

    // Start is called before the first frame update
    void Start()
    {
        Btn1.onClick.AddListener(Start1);
        Btn2.onClick.AddListener(Quit);
        Btn3.onClick.AddListener(Setting);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start1()
    {
        SceneManager.LoadScene("s1");
    }

    void Quit()
    {
        SceneManager.LoadScene("s2");
    }

    void Setting()
    {
        SceneManager.LoadScene("s3");
    }
}
