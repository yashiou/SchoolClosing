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
        Debug.Log("玩家的回合開始！");
        isPlayerTurn = true;
        deckManager.DrawCard();
    }

     public void DiscardAndEndTurn(Card card)
    {
        deckManager.DiscardCard(card);  // 丢弃指定手牌
        EndPlayerTurn();
    }

    //结束玩家回合
    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
    
        StartEnemyTurn();
    }

    //敵人回合
    void StartEnemyTurn()
    {
        Debug.Log("敵人的回合開始！");
        EndEnemyTurn();
    }

     // 敌人攻击玩家的逻辑
    void AttackPlayer()
    {
        Debug.Log("敌人正在攻击玩家！");
        player.LoseWill();  // 玩家损失一条血条
    }

    //结束敵人回合
    void EndEnemyTurn()
    {
        Debug.Log("敵人的回合结束！");
        StartPlayerTurn();
    }
}
