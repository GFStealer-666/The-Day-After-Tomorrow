using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RG_GuideControl : MonoBehaviour
{
    [SerializeField] private GameObject _bonfireGuidePanel;
    [SerializeField] private GameObject _sprinterGuidePanel;
    [SerializeField] private GameObject _collectibleGuidePanel;
    [SerializeField] private GameObject _gunGuidePanel;
    [SerializeField] private GameObject _bossGuidePanel;
    static public event Action OnPanelClosing;

    static public event Action OnPanelOpening;

    private void OnEnable() {
        RG_PlayerPanelTrigger.OnPlayerTriggerBonfire += TriggerBonfireUI;
        RG_PlayerPanelTrigger.OnPlayerTriggerCollectible += TriggerCollectibleUI;
        RG_PlayerPanelTrigger.OnPlayerTriggerSprinter += TriggerSprinterUI;
        RG_PlayerPanelTrigger.OnPlayerTriggerGun += TriggerGunUI;
        RG_PlayerPanelTrigger.OnPlayerTriggerBoss += TriggerBossUI;

    }

    private void OnDisable(){
        RG_PlayerPanelTrigger.OnPlayerTriggerBonfire -= TriggerBonfireUI;
        RG_PlayerPanelTrigger.OnPlayerTriggerCollectible -= TriggerCollectibleUI;
        RG_PlayerPanelTrigger.OnPlayerTriggerSprinter -= TriggerSprinterUI;
        RG_PlayerPanelTrigger.OnPlayerTriggerGun -= TriggerGunUI;
        RG_PlayerPanelTrigger.OnPlayerTriggerBoss -= TriggerBossUI;

    }

    private void TriggerBonfireUI(){
        _bonfireGuidePanel.SetActive(true);
        OnPanelOpening?.Invoke();
    }

    private void TriggerSprinterUI(){
        _sprinterGuidePanel.SetActive(true);
        OnPanelOpening?.Invoke();      
    }

    private void TriggerCollectibleUI(){
        _collectibleGuidePanel.SetActive(true);
        OnPanelOpening?.Invoke();
    }
    private void TriggerGunUI(){
        _gunGuidePanel.SetActive(true);
        OnPanelOpening?.Invoke();
    }
    private void TriggerBossUI(){
        _bossGuidePanel.SetActive(true);
        OnPanelOpening?.Invoke();
    }
    public void PanelClosing(){
        OnPanelClosing?.Invoke();
    }
}
