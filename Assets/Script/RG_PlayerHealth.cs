using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RG_PlayerHealth : MonoBehaviour
{

    static public event Action OnPlayerHealing;
    static public event Action OnPlayerStopHealing;

    static public event Action OnGameOver;
    [SerializeField] private RG_DamageFlash _flashScript;

    [SerializeField] private RG_HealthBar _healthbar;
    [SerializeField] private float _playerHealth;
    private float _playerHealthCheckValue;
    [SerializeField] private float _playerDefaultHealth = 150.0f;

    [SerializeField] private float _playerDefaultDefense = 10f;
    [SerializeField] private float _playerDefense;
    [SerializeField] private TMP_Text _defeatText;

    [SerializeField] private GameObject _playerHealthBarCanvas;

    bool _isHealing = false;

    private GameObject _player;

    private RG_PlayerMovement _playerMovementScript;

    void Start()
    {
        _playerDefense = _playerDefaultDefense;
        _playerHealth = _playerDefaultHealth;
        _healthbar.SetHealthBar(_playerHealth,_playerDefaultHealth);
         _playerMovementScript = GetComponent<RG_PlayerMovement>();
    }

    void Update(){
        if(_playerHealth >= _playerDefaultHealth){
            _playerHealth = _playerDefaultHealth;
        }
    }

    public void TakeDamage(float _damage)
    {
        Debug.Log("damage received" + _damage);
        _damage -= _playerDefense;
        if(_damage < 0){
            _damage = 0;
        } 
        _playerHealth -= _damage;
        Debug.Log("PlayerHealth" + _playerHealth);
        _healthbar.SetHealthBar(_playerHealth,_playerDefaultHealth);
        _flashScript.Flash();
        if(_playerHealth <= 0)
        {
            _playerHealth =0;
            _defeatText.gameObject.SetActive(true);
            _playerMovementScript.enabled = false;
            this.gameObject.SetActive(false);            
            _playerHealthBarCanvas.SetActive(false);
        
            Invoke("GameOver",2.0f);
        }
    }

    public float GetDefaultHealth(){
        return _playerDefaultHealth;
    }

    public float GetHealth(){
        return _playerHealth;
    }

    public void BoostDefense(float _boostedAmount , float _duration){
        _playerDefense += _boostedAmount;
        StartCoroutine(BuffDurationWoreOff(_duration));
    }
    public void Heal(float _healingAmount){

        if(_playerHealth >= _playerDefaultHealth){
            _playerHealth = _playerDefaultHealth;
            return;
        }
        _playerHealth += _healingAmount;
        _healthbar.SetHealthBar(_playerHealth,_playerDefaultHealth);

    }

    private void GameOver(){
        OnGameOver?.Invoke();
        SceneManager.LoadScene(0);
    }

     private IEnumerator BuffDurationWoreOff(float _duration){
        yield return new WaitForSeconds(_duration);
        _playerDefense = _playerDefaultDefense;
    }
}
