using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int will;  // 意志值
    public int sanity;  // SAN值

    // Start is called before the first frame update
    void Start()
    {
        will = 100;
        sanity = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 减少意志值
    public void DecreaseWill(int amount)
    {
        will -= amount;
        if (will <= 0)
        {
            Debug.Log("玩家意志崩溃！");
        }
    }

    // 恢復SAN值
    public void RestoreSanity(int amount)
    {
        sanity += amount;
        if (sanity > 100) sanity = 100;  // 防止超出最大值
    }
}
