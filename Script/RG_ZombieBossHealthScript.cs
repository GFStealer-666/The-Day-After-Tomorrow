using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_ZombieBossHealthScript : MonoBehaviour
{
   public static event Action<GameObject> onZombieBossDeath;
    [SerializeField] private float _zombieBossHealth;

    [SerializeField] private float _zombieBossMaxHealth = 400.0f;

    [SerializeField] private float _zombieBossDefense = 10;

    [SerializeField] private RG_DamageFlash _flashScript;

    [SerializeField] private RG_HealthBar _healthbar;

    private RG_ZombieBossAI _zombieAIScript;

    private Rigidbody2D _zombieRB;
    private BoxCollider2D _zombieBC;

    private Animator _zombieAnimator;

    void Start()
    {
        _zombieBossHealth = _zombieBossMaxHealth;
        _healthbar.SetHealthBar(_zombieBossHealth,_zombieBossMaxHealth);
        _zombieAnimator = GetComponent<Animator>();
        _zombieAIScript = GetComponent<RG_ZombieBossAI>();
        _zombieRB = GetComponent<Rigidbody2D>();
        _zombieBC = GetComponent<BoxCollider2D>();

    }

    public void TakeDamage(float _damage)
    {
        _zombieBossHealth -=  _damage - _zombieBossDefense;
        _healthbar.SetHealthBar(_zombieBossHealth,_zombieBossMaxHealth);
        _flashScript.Flash();

        if(_zombieBossHealth <= 0)
        {
            onZombieBossDeath?.Invoke(this.gameObject);
            _zombieAIScript.enabled = false;
            _zombieBC.enabled = false;
            Destroy(_zombieRB);
            _healthbar._heatlhBarSlider.gameObject.SetActive(false);
            _zombieAnimator.SetTrigger("isDie");
        }
    }

    public float GetHealth(){
        return _zombieBossHealth;
    }

    public void DestroyZombie(){
        Destroy(this.gameObject);
    }
}
