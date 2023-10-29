using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RG_PlayerEffect : MonoBehaviour
{
    [SerializeField] private GameObject _healingEffect;

    [SerializeField] private GameObject _adrenalineBoostEffect;

    private void Start(){
        _healingEffect.SetActive(false);
    }
    
    public void OnEnable(){
        RG_PlayerHealth.OnPlayerHealing += OnCharacterHealing;
        RG_PlayerHealth.OnPlayerStopHealing += OnCharacterDefaultState;
        RG_Bonfire.OnPlayerVisitBonfire += OnCharacterHealing;       
        RG_Bonfire.OnPlayerLeaveBonfire += OnCharacterDefaultState;
        RG_PlayerMeleeAttack.OnDebuffStart += OnAdrenalineBoost;
        RG_PlayerMeleeAttack.OnDebuffWoreOff += OnAdrenalineWoreOff;


    }

    public void OnDisable(){
        RG_PlayerHealth.OnPlayerHealing -= OnCharacterHealing;
        RG_PlayerHealth.OnPlayerStopHealing -= OnCharacterDefaultState;
        RG_Bonfire.OnPlayerLeaveBonfire -= OnCharacterDefaultState;
        RG_Bonfire.OnPlayerVisitBonfire -= OnCharacterHealing;
        RG_PlayerMeleeAttack.OnDebuffStart -= OnAdrenalineBoost;
        RG_PlayerMeleeAttack.OnDebuffWoreOff -= OnAdrenalineWoreOff;


    }

    // Update is called once per frame

    private void OnCharacterHealing(){
        _healingEffect.SetActive(true);
    }

    private void OnCharacterDefaultState(){
        _healingEffect.SetActive(false);
    }

    private void OnAdrenalineBoost(){
        _adrenalineBoostEffect.SetActive(true);
    }

    private void OnAdrenalineWoreOff(){
        _adrenalineBoostEffect.SetActive(false);
    }
}
