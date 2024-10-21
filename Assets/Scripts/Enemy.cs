using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int grudge;  // 怨恨值(護盾)
    public int existence;  // 存在值

    // Start is called before the first frame update
    void Start()
    {
        grudge = 100;
        existence = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 減少怨恨值
    public void DecreaseGrudge(int amount)
    {
        grudge -= amount;
        if (grudge < 0) grudge = 0;
    }

    // 减少存在值
    public void DecreaseExistence(int amount)
    {
        if (grudge <= 0)
        {
            existence -= amount;
            if (existence <= 0)
            {
                Debug.Log("消滅敵人！");
                
            }
        }
        else
        {
            Debug.Log("護盾尚未被擊破");
        }
    }
}
