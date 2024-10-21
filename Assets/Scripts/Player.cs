using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int will = 3;  //意志值
    public int currentSanity = 100;  //當前SAN值
    public int maxSanity = 100; //最大SAN值
    
    // Start is called before the first frame update
    void Start()
    {
        will = 3;
        currentSanity = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentSanity -= damage;
        
        if (currentSanity <= 0)
        {
            will--; // 減少意志值

            if (will > 0)
            {
                currentSanity = maxSanity; // 重置條血
                Debug.Log("One health bar destroyed. Remaining Willpower: " + will);
            }
            else
            {
                Debug.Log("Player defeated."); //gameover
            }
        }
        else
        {
            Debug.Log("Player took damage. Current Health: " + currentSanity);
        }
    }


    // 恢復SAN值
    public void RestoreSanity(int amount)
    {
        currentSanity += amount;
        if (currentSanity > 100) currentSanity = 100;  // 防止超出最大值
    }
}
