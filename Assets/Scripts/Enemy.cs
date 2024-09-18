using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int grudge;  // 怨恨值
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

    // 增加怨恨值
    public void IncreaseGrudge(int amount)
    {
        grudge += amount;
    }

    // 减少存在值
    public void DecreaseExistence(int amount)
    {
        existence -= amount;
        if (existence <= 0)
        {
            Debug.Log("敌人被击败！");
        }
    }
}
