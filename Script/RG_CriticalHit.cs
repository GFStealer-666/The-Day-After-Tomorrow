using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_CriticalHit : MonoBehaviour
{
    void Start()
    {
        RG_PlayerMeleeAttack.onCriticalHit += OnCricicalAttackHit;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCricicalAttackHit(){
        Debug.Log("Critical Hit");
    }
}
