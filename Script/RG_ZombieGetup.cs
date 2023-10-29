using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RG_ZombieGetup : MonoBehaviour
{
    public static event Action onZombieGetUp;

    [SerializeField] private Animator _zombieAnimator;

    [SerializeField] private float _detectionRange;
    [SerializeField] private float _distanceToPlayer ;

    [SerializeField] private Transform _attackTarget;
    void Start()
    {
        _attackTarget = GameObject.FindGameObjectWithTag("Player").transform;
        _zombieAnimator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _distanceToPlayer = Vector2.Distance(_attackTarget.position , transform.position);
         if(_distanceToPlayer <= _detectionRange){
            _zombieAnimator.SetBool("isGetup",true);
            
        }        
    }

    public void Getup(){
        onZombieGetUp?.Invoke();    
    }

}
