using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RG_Adrenaline : MonoBehaviour
{
    private RG_PlayerHealth _playerHealthScript ;

    private RG_Inventory _playerInventoryScript;

    static public event Action OnAdrenalinePickUp;

    private bool _isFirstTimeItemPickup = false;

    private float _healingAmount;
    private float _defendBoost = 20f;
    private float _criticalBoost = 20f;

    private float _buffDuration = 20f;
    void Start()
    {
        _playerHealthScript = FindObjectOfType<RG_PlayerHealth>();
        _playerInventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<RG_Inventory>();
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Destroy(this.gameObject);
            _playerInventoryScript.CollectAdrenaline(1);
            if(_isFirstTimeItemPickup == false){
                OnAdrenalinePickUp?.Invoke();
                _isFirstTimeItemPickup = true;
            }
           
            
        }
    }
    public float GetHealingAmount(){
        return _healingAmount;
    }

    public float GetDefendBoostAmount(){
        return _defendBoost;
    }
    public float GetCrticialBoostAmount(){
        return _criticalBoost;
    }

    public float GetBuffDuration(){
        return _buffDuration;
    }
}
