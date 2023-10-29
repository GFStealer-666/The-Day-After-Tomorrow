using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_ZombieAI : MonoBehaviour

{
    [SerializeField] private float _zombieSpeed;
    [SerializeField] private float _detectionRange;
    [SerializeField] private float _distanceToPlayer ;
    [SerializeField] private float _attackRange;

    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackDamage;

    [SerializeField] private float _attackRate = 1f;

    private float _nextAttackTime;
    [SerializeField] private Transform _attackTarget;

    [SerializeField] private Transform _attackPoint;

    [SerializeField] private LayerMask  _playerLayer;

    // private bool _isZombieGetUp = false;

    private Animator _zombieAnimator;

    private Rigidbody2D _zombieRb;

    [SerializeField] private AudioSource _zombieEatSound;
    [SerializeField] private AudioSource _zombieGrownSound;

    private RG_ZombieGetup _zombieGetupScript;

    private RG_ZombieHealth _zombieHealthScript;

    private float _zombieHealth;

    [SerializeField] private bool _isZombieGetUp = false;

    void Start()
    {
        RG_ZombieGetup.onZombieGetUp += ZombieGetUp;
        _attackTarget = GameObject.FindGameObjectWithTag("Player").transform;
        _zombieAnimator = GetComponent<Animator>();
        _zombieRb = GetComponent<Rigidbody2D>();
        _zombieGetupScript = GetComponent<RG_ZombieGetup>();
        _zombieHealthScript = GetComponent<RG_ZombieHealth>();
        _zombieHealth = _zombieHealthScript.GetHealth();
    }
    void Update()
    {
        _zombieHealth = _zombieHealthScript.GetHealth();
        if(_zombieHealth > 0){
            _distanceToPlayer = Vector2.Distance(_attackTarget.position , transform.position);
        
            if(_distanceToPlayer <= _detectionRange)
            {
                Chasing();

                if(_distanceToPlayer <= _attackRadius && _nextAttackTime < Time.time){
                    AttackingAnimation();
                    _nextAttackTime = Time.time + _attackRate;
                }
                else{
                    _zombieAnimator.SetTrigger("isRunning");
                }
            
                _zombieAnimator.SetTrigger("isRunning");
            
            }
            else{
                _zombieAnimator.SetTrigger("isIdling");
            }
        }
        
    }

    public void ZombieGetUp(){
        if(_distanceToPlayer >= _detectionRange) return;
        _isZombieGetUp = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position , _detectionRange);
        Gizmos.DrawWireSphere(this.transform.position ,_attackRadius);
        Gizmos.DrawWireSphere(_attackPoint.position , _attackRange);
    }

    private void Chasing()
    {
        if(_isZombieGetUp == false) return;
        transform.position = Vector2.MoveTowards(this.transform.position , _attackTarget.position , _zombieSpeed * Time.deltaTime); 
    }

    private void AttackingAnimation()
    {
        _zombieAnimator.SetTrigger("IsAttacking");
        Invoke("Attacking" , 0.6f);
    }
    private void Attacking(){
        if(_zombieHealth <= 0) return;
        Collider2D[] _player = Physics2D.OverlapCircleAll(_attackPoint.position ,_attackRange , _playerLayer);
        
        foreach(Collider2D player in _player){
            player.GetComponent<RG_PlayerHealth>().TakeDamage(_attackDamage);
        }
        
    }
    private void OnDisable() {
        RG_ZombieGetup.onZombieGetUp -= ZombieGetUp;
    }


}

