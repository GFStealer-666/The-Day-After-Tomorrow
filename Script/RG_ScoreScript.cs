using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RG_ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Canvas _scoreCanvas;

    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private TMP_Text _victoryText;
    [SerializeField] private int _zombieRemainning;

    [SerializeField] private List<GameObject> _zombieList;

    public static event Action OnGameWin;

    private RG_PlayerMovement _playerMovementScript;
    void Start()
    {
        _zombieList = FindObjectsOfType<GameObject>().Where(t=> t.name.ToLower().Contains("zombie")).ToList();
        _zombieRemainning = _zombieList.Count;
        UpdateText();
        _playerMovementScript = GetComponent<RG_PlayerMovement>();
    }

    void OnEnable(){
        RG_ZombieHealth.onZombieDeath += IncreaseTheScore;
        RG_ZombieBossHealthScript.onZombieBossDeath += IncreaseTheScore;
    }

    void OnDisable(){
        RG_ZombieHealth.onZombieDeath -= IncreaseTheScore;
        RG_ZombieBossHealthScript.onZombieBossDeath -= IncreaseTheScore;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseTheScore(GameObject _deadZombie){

        _zombieList.Remove(_deadZombie);
        UpdateText();
        if(_zombieList.Count == 0){
            _victoryText.gameObject.SetActive(true);
            Invoke("ReloadGame",3.0f);
        }
    }

    private void UpdateText(){
        _scoreText.text = _zombieList.Count.ToString();
    }

    private void ReloadGame(){
        SceneManager.LoadScene(0);

    }



}
