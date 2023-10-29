using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RG_Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text _medicCountText;

    [SerializeField] private TMP_Text _adrenalineText;

    [SerializeField] private TMP_Text _bulletText;

    [SerializeField] private int _medicCount;

    [SerializeField] private int _adrenalineCount;

    [SerializeField] private int _bulletAmount;

    [SerializeField] private bool _isPlayerOwnedGun = false;

    private float _medictUsedCoolDown;
    private float _medictUsedCoolDownMax = 15f;

    private RG_Medkit _medkitScript;

    private RG_Adrenaline _adrenalineScript;

    private RG_PlayerHealth _playerHealthScript;

    private RG_PlayerMeleeAttack _playerMeleeAttackScript;

    void Start()
    {
        _medkitScript = FindObjectOfType<RG_Medkit>();
        _adrenalineScript = FindObjectOfType<RG_Adrenaline>();
        _playerHealthScript = FindObjectOfType<RG_PlayerHealth>();
        _playerMeleeAttackScript = GetComponent<RG_PlayerMeleeAttack>();
        _medicCount = 0;
        _adrenalineCount = 0;
        _bulletAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _medicCountText.text = $"{_medicCount}";
        _adrenalineText.text = $"{_adrenalineCount}";
        _bulletText.text = $"{_bulletAmount}";
       
    }

    public void CollectMedic(int _getMedicCount)
    {
        _medicCount += _getMedicCount;
    }

    public void CollectAdrenaline(int _getAdrenalineCount)
    {
        _adrenalineCount += _getAdrenalineCount;
    }

    public void CollectBullet(int _getBulletCount){
        _bulletAmount += _getBulletCount;
    }

    public void UseMedic(int _usedMedicCount){
        if(_medicCount > 0 && _playerHealthScript.GetHealth() < 150){
            _medicCount -= _usedMedicCount;
            _playerHealthScript.Heal(_medkitScript.GetHealingAmount());
        }
    }

    public void UseAdrenaline(int _usedAdrenalineCount){
        if(_adrenalineCount > 0) {
             _adrenalineCount -= _usedAdrenalineCount;
            _playerHealthScript.BoostDefense(_adrenalineScript.GetDefendBoostAmount() , _adrenalineScript.GetBuffDuration());
            _playerMeleeAttackScript.BoostCriticalChance(_adrenalineScript.GetCrticialBoostAmount() ,  _adrenalineScript.GetBuffDuration());
        
        }
    }

    public void PickUpGun(){
        _isPlayerOwnedGun = true;
    }

    public bool IsPLayerOwnedGun(){
        return _isPlayerOwnedGun;
    }

    public int GetBulletAmount(){
        return _bulletAmount;
    }

    public void Fired(){
        _bulletAmount -= 1;
    }
}
