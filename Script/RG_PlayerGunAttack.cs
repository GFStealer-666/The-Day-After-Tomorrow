using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RG_PlayerGunAttack : MonoBehaviour
{
    static public event Action OnPlayerShoot;
    [SerializeField] private Transform _playerShootPoint;

    [SerializeField] private float _attackRate = 1.5f;
    private float _nextAttackTime;
    private Animator _playerAnimator;
    [SerializeField] private AudioSource _shootingSound;
    [SerializeField] private GameObject _bulletPrefab;

    RG_PlayerMovement _playerMovementScript;

    RG_Inventory _playerInventoryScript;

    void Start()
    {
        _playerMovementScript = GetComponent<RG_PlayerMovement>();
        _playerInventoryScript = GetComponent<RG_Inventory>();
        _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootingCondition(){
        if(_playerInventoryScript.IsPLayerOwnedGun() == false) return;
        if(_playerInventoryScript.GetBulletAmount() <= 0) return;
        if(_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Running")) return;
        if(_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking(Knife)")) return;
        if(_playerMovementScript.IsGrounded() == false) return;

        Shooting(); 

    }

    private void Shooting(){
        if(_nextAttackTime < Time.time){
            _nextAttackTime = Time.time + _attackRate;
            _playerAnimator.SetTrigger("isShooting");
            
        }
    }
    private void SpawnBullet(){    
        Instantiate(_bulletPrefab , _playerShootPoint.position , _playerShootPoint.rotation);
        _shootingSound.Play();
        _playerInventoryScript.Fired();
    }

    

    
}
