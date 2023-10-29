using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_Bullet : MonoBehaviour
{
    private RG_Inventory _playerInventoryScript;

    [SerializeField] private int _bulletAmount;
    void Start()
    {
        _playerInventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<RG_Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Destroy(this.gameObject);
            _playerInventoryScript.CollectBullet(_bulletAmount);
        }
    }


    private float GetBulletAmount(){
        return _bulletAmount;
    }
}
