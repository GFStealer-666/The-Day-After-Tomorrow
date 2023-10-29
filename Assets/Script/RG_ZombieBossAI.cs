using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_ZombieBossAI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _zombieSpeed;
    [SerializeField] private float _detectionRange;
    [SerializeField] private float _distanceToPlayer ;
    [SerializeField] private float _attackRange;

    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackDamage;

    private float _attackRate = 1f;
    private float _nextAttackTime;
    [SerializeField] private Transform _attackTarget;

    [SerializeField] private Transform _attackPoint;

    [SerializeField] private LayerMask  _playerLayer;

    // private bool _isZombieGetUp = false;

    private Animator _zombieAnimator;

    private Rigidbody2D _zombieRb;

    [SerializeField] private AudioSource _zombieGrownSound;

    [SerializeField] private AudioClip _zombieGrownSoundClip;

    private RG_ZombieGetup _zombieGetupScript;

    private RG_ZombieBossHealthScript _zombieHealthScript;

    private float _zombieHealth;

    private void OnEnable() {
        RG_CamControl.OnCameraArriveToBoss += BossAttackAnimationForTimeline;
    }

    private void OnDisable() {
        RG_CamControl.OnCameraArriveToBoss -= BossAttackAnimationForTimeline;
    }

    private void BossAttackAnimationForTimeline(){
        _zombieGrownSound.PlayOneShot(_zombieGrownSoundClip);
        AttackingAnimation();
        AttackingAnimation();   
    }
    void Start()
    {

        _attackTarget = GameObject.FindGameObjectWithTag("Player").transform;
        _zombieAnimator = GetComponent<Animator>();
        _zombieRb = GetComponent<Rigidbody2D>();
        _zombieGetupScript = GetComponent<RG_ZombieGetup>();
        _zombieHealthScript = GetComponent<RG_ZombieBossHealthScript>();
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
                    if(_zombieHealth > 0)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position , _detectionRange);
        Gizmos.DrawWireSphere(this.transform.position ,_attackRadius);
        Gizmos.DrawWireSphere(_attackPoint.position , _attackRange);
    }

    private void Chasing()
    {
        transform.position = Vector2.MoveTowards(this.transform.position , _attackTarget.position , _zombieSpeed * Time.deltaTime); 
    }

    private void AttackingAnimation()
    {
        _zombieAnimator.SetTrigger("isAttacking");
        Invoke("Attacking" , 0.6f);
    }
    private void Attacking(){
        if(_zombieHealth <= 0) return;
        Collider2D[] _player = Physics2D.OverlapCircleAll(_attackPoint.position ,_attackRange , _playerLayer);
        
        foreach(Collider2D player in _player){
            player.GetComponent<RG_PlayerHealth>().TakeDamage(_attackDamage);
        }
        
    }
}
