using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class RG_PlayerMeleeAttack : MonoBehaviour
{
    Animator _playerAnimator;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _normalattackDamage = 40f;

    [SerializeField] private LayerMask _enemyLayers;

    private float _attackDamage;
    private float _attackRate = 1f;
    private float _nextAttackTime;
    [SerializeField] private float _criticalChance ;
    [SerializeField] private float _criticalDefaultChance = 10;
    private float _criticalMultiplier = 3;
    private bool _isCriticalable = true;

    public static event Action onCriticalHit;

    public static event Action OnDebuffStart;
    public static event Action OnDebuffWoreOff;


    [SerializeField] private AudioSource _attackkingSound;
    void Start()
    {
        _criticalChance = _criticalDefaultChance;
        _attackDamage = _normalattackDamage;
        _playerAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F ))
        {
            
            PlayerAttackingAnimation();
             
        }
    }

    public void PlayerAttackingAnimation()
    {
        if(_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shooting")) return;
        if(_nextAttackTime < Time.time ){
             _nextAttackTime = Time.time + _attackRate;
            _playerAnimator.SetTrigger("isAttacking");
            _attackkingSound.enabled = true;
            Invoke("PlayAttacking" , 0.6f);
        
        }
       
        
    }

    private float CritcalDamage(){

        _attackDamage = _normalattackDamage;
        if(_isCriticalable == false) return _attackDamage;

        int _criticalRandomChance = UnityEngine.Random.Range(1,100);
        if(_criticalRandomChance <= _criticalChance){
            _attackDamage *= _criticalMultiplier;
            onCriticalHit?.Invoke();
        }
        
        

        return _attackDamage;
    }

    private void PlayAttacking()
    {
         Collider2D[] _hitEnemies  = Physics2D.OverlapCircleAll(_attackPoint.position , _attackRadius , _enemyLayers);

        foreach (Collider2D _enemy in _hitEnemies)
        {
            if(_enemy.tag == "ZombieBoss"){
                _enemy.GetComponent<RG_ZombieBossHealthScript>().TakeDamage(CritcalDamage());
            }
            else{
                _enemy.GetComponent<RG_ZombieHealth>().TakeDamage(CritcalDamage());
            }
            
        
        }

         _attackkingSound.enabled = false;
         
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(_attackPoint.position , _attackRadius);
    }

    public void BoostCriticalChance(float _criticalChanceBoost , float _duration){
        OnDebuffStart?.Invoke();
        _criticalChance += _criticalChanceBoost;
        StartCoroutine(BuffDurationWoreOff(_duration));

    }

    private IEnumerator BuffDurationWoreOff(float _duration){
        yield return new WaitForSeconds(_duration);
        OnDebuffWoreOff?.Invoke();       
        _criticalChance = _criticalDefaultChance;
    }

}
