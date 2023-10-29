using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class RG_CamControl : MonoBehaviour
{

    public static event Action OnGameStarted;
    [SerializeField] private CinemachineVirtualCamera _mainMenuCamera;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;

    [SerializeField] private Canvas _mainCanvas;

    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _guideUI;

    private bool _isGameStarted = false;

    private RG_PlayerMovement _playerMovementScript;

    public void SwitchCamera(){
        _mainMenuCamera.gameObject.SetActive(false);
        _playerCamera.gameObject.SetActive(true);
        _mainCanvas.gameObject.SetActive(false);
    }  

    private void Start()
    {
        _mainMenuCamera.gameObject.SetActive(true);
        _playerMovementScript = _player.GetComponent<RG_PlayerMovement>();
        _playerMovementScript.enabled = false;
    } 
    public void Exit(){
        Application.Quit();
    }

    private void Update(){
        if(Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }

        if(_mainCamera.gameObject.transform.position.x == _playerCamera.transform.position.x){
            if(_isGameStarted == true) return;
            _isGameStarted = true;
            OnGameStarted?.Invoke();
            _guideUI.SetActive(true);
            _playerMovementScript.enabled = true;
        }
    }
}
