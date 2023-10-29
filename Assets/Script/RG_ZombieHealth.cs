using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RG_ZombieHealth : MonoBehaviour
{
    public static event Action<GameObject>  onZombieDeath;
    [SerializeField] private float _enemyHealth;

    [SerializeField] private float _enemyMaxHealth = 100.0f;

    [SerializeField] private float _enemyDefense = 0;
    [SerializeField] private RG_DamageFlash _flashScript;

    [SerializeField] private RG_HealthBar _healthbar;

    private RG_ZombieAI _zombieAIScript;

    private Rigidbody2D _zombieRB;
    private BoxCollider2D _zombieBC;

    private Animator _zombieAnimator;

    void Start()
    {
        _enemyHealth = _enemyMaxHealth;
        _healthbar.SetHealthBar(_enemyHealth,_enemyMaxHealth);
        _zombieAnimator = GetComponent<Animator>();
        _zombieAIScript = GetComponent<RG_ZombieAI>();
        _zombieRB = GetComponent<Rigidbody2D>();
        _zombieBC = GetComponent<BoxCollider2D>();

    }

    public void TakeDamage(float _damage)
    {
        _enemyHealth -=  _damage - _enemyDefense ;
        _healthbar.SetHealthBar(_enemyHealth,_enemyMaxHealth);
        _flashScript.Flash();

        if(_enemyHealth <= 0)
        {
            onZombieDeath?.Invoke(this.gameObject);
            _zombieAIScript.enabled = false;
            _zombieBC.enabled = false;
            Destroy(_zombieRB);
            _healthbar._heatlhBarSlider.gameObject.SetActive(false);
            _zombieAnimator.SetTrigger("isDie");
        }
    }

    public float GetHealth(){
        return _enemyHealth;
    }

    public void DestroyZombie(){
        Destroy(this.gameObject);
    }
}
