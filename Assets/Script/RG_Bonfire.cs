using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RG_Bonfire : MonoBehaviour
{
    static public event Action OnPlayerVisitBonfire;
    static public event Action OnPlayerLeaveBonfire;
    static public event Action OnBonfireFuelRunOut;
    
    [SerializeField] private AudioSource _fireAudio;

    [SerializeField] private float _bonfireFuel = 20;

    [SerializeField] private float _bonfireHealAmount = 0.10f;

    [SerializeField] private GameObject _fireSprite;

    
    void Start()
    {
        _fireAudio = GetComponent<AudioSource>();
        OnBonfireFuelRunOut += OnBonfireFuelRunOut;
        if(_bonfireFuel > 0){
            _fireSprite.SetActive(true);
        }
    }


    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag != "Player") return;
        RG_PlayerHealth _playerHealthScript = other.GetComponent<RG_PlayerHealth>();
        if(_playerHealthScript.GetHealth() < 150){
            HealPlayer(_playerHealthScript);
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag != "Player") return;
        
        _fireAudio.Play();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag != "Player") return;
        Debug.Log("Player leave"); 
        OnPlayerLeaveBonfire?.Invoke();   
        _fireAudio.Pause();
    }

    private void HealPlayer(RG_PlayerHealth _playerHealthScript){
        if(_bonfireFuel > 0f){
            OnPlayerVisitBonfire?.Invoke();
            _playerHealthScript.Heal(_bonfireHealAmount);
            if(_playerHealthScript.GetHealth() >= _playerHealthScript.GetDefaultHealth()){
                OnPlayerLeaveBonfire?.Invoke();
            }
            BurnBonfireFuel();
        }     
        
    }

    private void BurnBonfireFuel(){
        _bonfireFuel -= _bonfireHealAmount;
        if(_bonfireFuel < 0f){
            _bonfireFuel = 0;
            BonfireFuelRunOut();
        }
    }

    private void BonfireFuelRunOut(){
        Debug.Log("RunOutbro");
        OnPlayerLeaveBonfire.Invoke();
        _fireSprite.SetActive(false);
        _fireAudio.Stop();
    }
}
