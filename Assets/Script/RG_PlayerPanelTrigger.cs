using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RG_PlayerPanelTrigger : MonoBehaviour
{
    public static event Action OnPlayerTriggerBonfire;
    public static event Action OnPlayerTriggerCollectible;
    public static event Action OnPlayerTriggerSprinter;
    public static event Action OnPlayerTriggerGun;
    public static event Action OnPlayerTriggerBoss;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "CampfireTrigger"){
            OnPlayerTriggerBonfire?.Invoke();
            Destroy(other.gameObject);
        }
        else if(other.name == "CollectibleTrigger"){
            OnPlayerTriggerCollectible?.Invoke();
            Destroy(other.gameObject);
        }
        else if(other.name == "SprinterTrigger"){
            OnPlayerTriggerSprinter?.Invoke();
            Destroy(other.gameObject);
        }
        else if(other.name == "GunTrigger"){
            OnPlayerTriggerGun?.Invoke();
            Destroy(other.gameObject);
        }
        else if(other.name == "BossTrigger"){
            OnPlayerTriggerBoss?.Invoke();
            Destroy(other.gameObject);
        }
    }
}
