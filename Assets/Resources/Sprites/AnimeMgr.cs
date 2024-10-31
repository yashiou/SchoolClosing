using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class AnimeMgr : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> Enemys = new List<GameObject>(); //統整敵人

    public int NowEnemyIndex = 0; //當前敵人索引值

    private GameObject NowEnemy; //當前的敵人

    public void CallNextEnemys()
    {
        NowEnemy = Enemys[NowEnemyIndex];

        NowEnemyIndex = NowEnemyIndex + 1 > Enemys.Count ? NowEnemyIndex++ : 0;

        NowEnemy.GetComponent<Animator>().SetTrigger("in");
    }

    public async void EnemyDie()
    {
        NowEnemy.GetComponent<Animator>().SetTrigger("out");

        await Task.Delay(1000);
        
        NowEnemy.SetActive(false);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
