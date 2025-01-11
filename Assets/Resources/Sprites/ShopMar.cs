using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ShopMar : MonoBehaviour
{

    private Playerseves _playerseves;

    private PlayerData _playerData;

    public Text EXP;

    public Slider 
        Rebirths,
        Lifes,
        EnergyUps,
        EnergyMps,
        KnockDws,
        Totalscores;

    public List<GameObject> NPCS = new List<GameObject>();

    private int nowpage;


    // Start is called before the first frame update
    void Start()
    {
        _playerseves = FindAnyObjectByType<Playerseves>();

        _playerData = _playerseves.Load();

        if (_playerData == null)
        {
            _playerData = new PlayerData();
        }
        EXP.text = _playerData.AllPoint.ToString();

        Rebirths.value = _playerData.Rebirth;

        Lifes.value = _playerData.Life;

        EnergyUps.value = _playerData.EnergyUp;

        EnergyMps.value = _playerData.EnergyMp * 10;

        KnockDws.value = _playerData.KnockDw * 10;

        Totalscores.value = _playerData.Totalscore * 10;

        Rebirths.GetComponentInChildren<Text>().text = (200 * (int)(Rebirths.value + 1)).ToString();

        if (Rebirths.value == Rebirths.maxValue)
        {
            Rebirths.GetComponentInChildren<Text>().gameObject.SetActive(false);
        }

        Lifes.GetComponentInChildren<Text>().text = (100 * (int)(Lifes.value + 1)).ToString();

        if (Lifes.value == Lifes.maxValue)
        {
            Lifes.GetComponentInChildren<Text>().gameObject.SetActive(false);
        }

        EnergyUps.GetComponentInChildren<Text>().text = (50 * (int)(EnergyUps.value + 1)).ToString();

        if (EnergyUps.value == EnergyUps.maxValue)
        {
            EnergyUps.GetComponentInChildren<Text>().gameObject.SetActive(false);
        }

        EnergyMps.GetComponentInChildren<Text>().text = (100 * (int)(EnergyMps.value + 1)).ToString();

        if (EnergyMps.value == EnergyMps.maxValue)
        {
            EnergyMps.GetComponentInChildren<Text>().gameObject.SetActive(false);
        }

        KnockDws.GetComponentInChildren<Text>().text = (300 * (int)(KnockDws.value + 1)).ToString();

        if (KnockDws.value == KnockDws.maxValue)
        {
            KnockDws.GetComponentInChildren<Text>().gameObject.SetActive(false);
        }

        Totalscores.GetComponentInChildren<Text>().text = (600 * (int)(Totalscores.value + 1)).ToString();

        if (Totalscores.value == Totalscores.maxValue)
        {
            Totalscores.GetComponentInChildren<Text>().gameObject.SetActive(false);
        }
    }
    
    

    public void upgrade(int id)
    {
        switch (id)
        {
            case 0 :
                if ( _playerData.AllPoint >= 200 * (Rebirths.value + 1) && Rebirths.value < Rebirths.maxValue)
                {
                    ShopCost(-200 * (int)(Rebirths.value + 1));

                    _playerData.Rebirth += 1;

                    Rebirths.value += 1;

                    Rebirths.GetComponentInChildren<Text>().text = (200 * (int)(Rebirths.value + 1)).ToString();

                    if (Rebirths.value == Rebirths.maxValue)
                    {
                        Rebirths.GetComponentInChildren<Text>().gameObject.SetActive(false);
                    }
                }
                break;
            
            case 1 :
                if ( _playerData.AllPoint >= 100 * (int)(Lifes.value + 1) && Lifes.value < Lifes.maxValue)
                {
                    ShopCost(-100 * (int)(Lifes.value + 1));

                    _playerData.Life += 1;

                    Lifes.value += 1;

                    Lifes.GetComponentInChildren<Text>().text = (100 * (int)(Lifes.value + 1)).ToString();

                    if (Lifes.value == Lifes.maxValue)
                    {
                        Lifes.GetComponentInChildren<Text>().gameObject.SetActive(false);
                    }
                }
                break;
            
            case 2 :
                if ( _playerData.AllPoint >= 50 * (int)(EnergyUps.value + 1) && EnergyUps.value < EnergyUps.maxValue)
                {
                    ShopCost(-50 * (int)(EnergyUps.value + 1));

                    _playerData.EnergyUp += 1;

                    EnergyUps.value += 1;

                    EnergyUps.GetComponentInChildren<Text>().text = (50 * (int)(EnergyUps.value + 1)).ToString();

                    if (EnergyUps.value == EnergyUps.maxValue)
                    {
                        EnergyUps.GetComponentInChildren<Text>().gameObject.SetActive(false);
                    }
                }
                break;
            
            case 3 :
                if ( _playerData.AllPoint >= 100 * (int)(EnergyMps.value + 1) && EnergyMps.value < EnergyMps.maxValue)
                {
                    ShopCost(-100 * (int)(EnergyMps.value + 1));

                    _playerData.EnergyMp += 0.1f;

                    EnergyMps.value += 1f;

                    EnergyMps.GetComponentInChildren<Text>().text = (100 * (int)(EnergyMps.value + 1)).ToString();

                    if (EnergyMps.value == EnergyMps.maxValue)
                    {
                        EnergyMps.GetComponentInChildren<Text>().gameObject.SetActive(false);
                    }
                }
                break;
            
            case 4 :
                if ( _playerData.AllPoint >= 300 * (int)(KnockDws.value + 1) && KnockDws.value < KnockDws.maxValue)
                {
                    ShopCost(-300 * (int)(KnockDws.value + 1));

                    _playerData.KnockDw += 0.1f;

                    KnockDws.value += 1f;

                    KnockDws.GetComponentInChildren<Text>().text = (300 * (int)(KnockDws.value + 1)).ToString();

                    if (KnockDws.value == KnockDws.maxValue)
                    {
                        KnockDws.GetComponentInChildren<Text>().gameObject.SetActive(false);
                    }
                }
                break;
            
            case 5 :
                if ( _playerData.AllPoint >= 600 * (int)(Totalscores.value + 1) && Totalscores.value < Totalscores.maxValue)
                {
                    ShopCost(-600 * (int)(Totalscores.value + 1));

                    _playerData.Totalscore += 0.1f;

                    Totalscores.value += 1f;

                    Totalscores.GetComponentInChildren<Text>().text = (600 * (int)(Totalscores.value + 1)).ToString();

                    if (Totalscores.value == Totalscores.maxValue)
                    {
                        Totalscores.GetComponentInChildren<Text>().gameObject.SetActive(false);
                    }
                }
                break;
            default:
                break;
        }
        _playerseves.Seve(_playerData);
    }

    public void NPCchange(int pag)
    {
        if (NPCS.Count > nowpage + pag && nowpage + pag > -1)
        {
            NPCS[nowpage].SetActive(false);

            nowpage += pag;

            NPCS[nowpage].SetActive(true);
        }
    }
    private void ShopCost(int coint)
    {
        _playerData.AllPoint += coint;

        EXP.text = _playerData.AllPoint.ToString();
    }
}
