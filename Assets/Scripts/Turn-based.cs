using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_based : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    private bool isPlayerTurn;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerTurn = true;  //玩家先行
        StartPlayerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //玩家回合
    void StartPlayerTurn()
    {
        Debug.Log("玩家的回合开始！");
        isPlayerTurn = true;
    }

    //结束玩家回合
    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        StartEnemyTurn();
    }

    //敌人回合
    void StartEnemyTurn()
    {
        Debug.Log("敌人的回合开始！");
        enemy.IncreaseGrudge(10);  //敌人增加怨恨值
        EndEnemyTurn();
    }

    //结束敌人回合
    void EndEnemyTurn()
    {
        Debug.Log("敌人的回合结束！");
        StartPlayerTurn();
    }
}
