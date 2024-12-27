using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class AnimeMgr : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> Enemys = new List<GameObject>(); //統整敵人

    public int NowEnemyIndex = 0; //當前敵人索引值

    private GameObject NowEnemy; //當前的敵人

    [SerializeField] 
    public GameObject BackGround;

    public Animator PlayerAnimeter;

    public Animator BossAnimeter;

    public void CallNextEnemys()
    {
        NowEnemy = Enemys[NowEnemyIndex];
        
        NowEnemy.SetActive(true);

        NowEnemyIndex = NowEnemyIndex + 1 > Enemys.Count ? NowEnemyIndex++ : 0;

        NowEnemy.GetComponent<Animator>().SetTrigger("in");
    }

    public async void EnemyDie()
    {
        NowEnemy.GetComponent<Animator>().SetBool("out", true);

        await Task.Delay(1000);

        NowEnemy.GetComponent<Animator>().SetBool("out", false);

        NowEnemy.SetActive(false);
    }

    public void BackGroundMove()
    {
        BackGround.GetComponent<Animator>().SetTrigger("MOVE");
    }

    public void PlayerAnime(string move)
    {
        PlayerAnimeter.SetTrigger(move);
    }

    public void BossATK()
    {
        BossAnimeter.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("BossATK");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
