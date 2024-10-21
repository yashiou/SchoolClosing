using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<Card> deck = new List<Card>();  // 牌庫
    public List<Card> hand = new List<Card>();  // 手牌
    public int maxHandSize = 7;  // 最大手牌數

    // Start is called before the first frame update
    void Start()
    {
        InitializeDeck();
        DrawInitialHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 初始化牌庫
    void InitializeDeck()
    {
        // 添加卡牌範例
        for (int i = 0; i < 20; i++) //添加20張.....
        {
            deck.Add(new AttackCard());  // 攻擊卡牌進牌庫
        }
    }

    void DrawInitialHand()
    {
        for (int i = 0; i <= 6 ; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (deck.Count > 0 && hand.Count < maxHandSize)
        {
            Card drawnCard = deck[0];  // 抽取第一张卡牌
            deck.RemoveAt(0);  // 从牌库移除这张卡牌
            hand.Add(drawnCard);  // 将其加入手牌
        }
        else
        {
            Debug.Log("手牌已满或牌库为空！");
        }
    }

    public void DiscardCard(Card card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            Debug.Log($"{card.cardName} 被丢弃！");
        }
        else
        {
            Debug.Log("手牌中没有此卡牌！");
        }
    }

}
