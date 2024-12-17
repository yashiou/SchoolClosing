using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        _playerseves = FindFirstObjectByType<Playerseves>();
    }
    
    

    public void upgrade(int id)
    {
        switch (id)
        {
            case 0 :
                if ( _playerData.AllPoint >= 200 && Rebirths.value < Rebirths.maxValue)
                {
                    _playerData.AllPoint -= 200;

                    _playerData.Rebirth += 1;

                    Rebirths.value += 1;
                }
                break;
            
            case 1 :
                if ( _playerData.AllPoint >= 100 && Lifes.value < Lifes.maxValue)
                {
                    _playerData.AllPoint -= 100;

                    _playerData.Life += 1;

                    Lifes.value += 1;
                }
                break;
            
            case 2 :
                if ( _playerData.AllPoint >= 50 && EnergyUps.value < EnergyUps.maxValue)
                {
                    _playerData.AllPoint -= 50;

                    _playerData.EnergyUp += 1;

                    EnergyUps.value += 1;
                }
                break;
            
            case 3 :
                if ( _playerData.AllPoint >= 100 && EnergyMps.value < EnergyMps.maxValue)
                {
                    _playerData.AllPoint -= 100;

                    _playerData.EnergyMp += 0.1f;

                    EnergyMps.value += 1f;
                }
                break;
            
            case 4 :
                if ( _playerData.AllPoint >= 300 && KnockDws.value < KnockDws.maxValue)
                {
                    _playerData.AllPoint -= 300;

                    _playerData.KnockDw += 0.1f;

                    KnockDws.value += 1f;
                }
                break;
            
            case 5 :
                if ( _playerData.AllPoint >= 600 && Totalscores.value < Totalscores.maxValue)
                {
                    _playerData.AllPoint -= 600;

                    _playerData.Totalscore += 0.1f;

                    Totalscores.value += 1f;
                }
                break;
            default:
                break;
        }
        _playerseves.Seve(_playerData);
    }
    
    private void OnEnable()
    {
        _playerData = _playerseves.Load();

        if (_playerData == null)
        {
            _playerData = new PlayerData();
        }
        EXP.text = _playerData.AllPoint.ToString();
    }
}
