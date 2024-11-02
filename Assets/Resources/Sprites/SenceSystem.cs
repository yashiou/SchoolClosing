using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class TextData //編碼格式
{ 
    public string id;
    public string Name;
    public string Type;
    public string Effect;
}

public class AllText
{
    public List<TextData> TextFormat; //陣列存取
}

public class SenceSystem : MonoBehaviour //儲存牌組數據
{
    [SerializeField] 
    public List<string> CardBackpack = new List<string>();

    public List<Sprite> AllCardIame = new List<Sprite>();

    public List<Sprite> EnemyCardIameg = new List<Sprite>();

    public AudieMusic audieMusic; //音樂管理員

    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        LoadAsset2CreateImage();

        LoadScene("s0");
    }

    public void LoadAsset2CreateImage()
    {
        foreach (Texture2D obj in Resources.LoadAll<Texture2D>("Models/advise"))
        {

            Sprite sprite = Sprite.Create(obj, new Rect(0, 0, obj.width, obj.height)
                , new Vector2(1, 1));

            sprite.name = "advise_" + obj.name;

            AllCardIame.Add(sprite);
            
            Resources.UnloadAsset(obj);

            Resources.UnloadAsset(obj);
        }
        
        foreach (Texture2D obj in Resources.LoadAll<Texture2D>("Models/inspire"))
        {

            Sprite sprite = Sprite.Create(obj, new Rect(0, 0, obj.width, obj.height)
                , new Vector2(1, 1));

            sprite.name = "inspire_" + obj.name;

            AllCardIame.Add(sprite);
            
            Resources.UnloadAsset(obj);

            Resources.UnloadAsset(obj);
        }
        
        foreach (Texture2D obj in Resources.LoadAll<Texture2D>("Models/steadfast"))
        {

            Sprite sprite = Sprite.Create(obj, new Rect(0, 0, obj.width, obj.height)
                , new Vector2(1, 1));

            sprite.name = "steadfast_" + obj.name;

            AllCardIame.Add(sprite);
            
            Resources.UnloadAsset(obj);

            Resources.UnloadAsset(obj);
        }
        
        foreach (Texture2D obj in Resources.LoadAll<Texture2D>("Models/EnemyCard"))
        {

            Sprite sprite = Sprite.Create(obj, new Rect(0, 0, obj.width, obj.height)
                , new Vector2(1, 1));

            sprite.name = obj.name;

            EnemyCardIameg.Add(sprite);
            
            Resources.UnloadAsset(obj);
            
        }
    }

    
    
    public void LoadScene(string name) //換場景紀錄卡牌
    {
        SceneManager.LoadScene(name);
    }

    public void BidingImageToObject(GameObject gameObject,string id, bool isEnemy = false)
    {
        Image card_image = gameObject.GetComponent<Image>();

        if (isEnemy)
        {
            try
            {
                card_image.sprite =EnemyCardIameg.Find(x => x.name == id);
            }
            catch
            {
                Debug.Log("出錯 找不到id對應圖片");
            }
        }
        else
        {
            try
            {
                card_image.sprite =AllCardIame.Find(x => x.name == id);
            }
            catch
            {
                Debug.Log("出錯 找不到id對應圖片");

            }
        }
    }

    public AllText CardText; //接收器

    // Start is called before the first frame update
    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Json/CardText"); //讀取檔案
        
        CardText = JsonUtility.FromJson<AllText>(jsonFile.text); //加載檔案
        
        
        
        audieMusic = FindObjectOfType<AudieMusic>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
