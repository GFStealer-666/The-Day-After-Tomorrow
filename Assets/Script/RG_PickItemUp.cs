using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RG_PickItemUp : MonoBehaviour
{
    [SerializeField] private AudioSource _pickUpItemSound;

    static public event Action OnGunPickUp;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Collectible")){
            _pickUpItemSound.Play();
            Destroy(other.gameObject);

            if(other.gameObject.name == "Pistol"){
                RG_Inventory _playerInventoryScript = GetComponent<RG_Inventory>();
                OnGunPickUp?.Invoke();
                _playerInventoryScript.PickUpGun();
            }
        }
        
    }
}
