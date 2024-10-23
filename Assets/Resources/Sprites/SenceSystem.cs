using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class SenceSystem : MonoBehaviour //儲存牌組數據
{
    [SerializeField] 
    public List<string> CardBackpack = new List<string>();

    public List<Sprite> AllCardIame = new List<Sprite>();
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        LoadAsset2CreateImage();

        LoadScene("CardScene");
    }

    public void LoadAsset2CreateImage()
    {
        int Id_give = 0; 
        foreach (Texture2D obj in Resources.LoadAll<Texture2D>("Models/IMG/"))
        {

            Sprite sprite = Sprite.Create(obj, new Rect(0, 0, obj.width, obj.height)
                , new Vector2(1, 1));

            sprite.name = Id_give.ToString();

            AllCardIame.Add(sprite);
            
            Resources.UnloadAsset(obj);

            Id_give++;
        }
    }

    public void LoadScene(string name) //換場景紀錄卡牌
    {
        SceneManager.LoadScene(name);
    }

    public void BidingImageToObject(GameObject gameObject,int id)
    {
        Image card_image = gameObject.GetComponent<Image>();
            
        try
        {
            card_image.sprite =AllCardIame[id];
        }
        catch
        {
            Debug.Log("出錯 找不到id對應圖片");
        }
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
