using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RG_Medkit : MonoBehaviour
{
    private RG_PlayerHealth _playerHealthScript ;

    private RG_Inventory _playerInventoryScript;

    private float _healingAmount = 60.0f;

    private bool _isFirstTimeItemPickup = false;

    static public event Action OnFirstMedkitPickup;

    void Start()
    {
        _playerHealthScript = FindObjectOfType<RG_PlayerHealth>();
        _playerInventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<RG_Inventory>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            _playerInventoryScript.CollectMedic(1);
            if(_isFirstTimeItemPickup == false){
                OnFirstMedkitPickup?.Invoke();
                _isFirstTimeItemPickup = true;
            }
        }
    }

    public float GetHealingAmount(){
        return _healingAmount;
    }
}
