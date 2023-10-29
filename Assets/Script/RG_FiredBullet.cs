using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_FiredBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    private Rigidbody2D _bulletRB;

    private float _bulletDamage = 50;
    void Start()
    {
        _bulletRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _bulletRB.velocity = transform.right * _bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Zombie")){
            Destroy(this.gameObject);
            RG_ZombieHealth _zombieHealth = other.GetComponent<RG_ZombieHealth>();
            _zombieHealth.TakeDamage(_bulletDamage);
        }
        else if(other.gameObject.CompareTag("ZombieBoss")){
            Destroy(this.gameObject);
            RG_ZombieBossHealthScript _zombieHealth = other.GetComponent<RG_ZombieBossHealthScript>();
            _zombieHealth.TakeDamage(_bulletDamage);
        }
        else if(other.gameObject.CompareTag("Ground")){
            Destroy(this.gameObject);
        }
    }
}
