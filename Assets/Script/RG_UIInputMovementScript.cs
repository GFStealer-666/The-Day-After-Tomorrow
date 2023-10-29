using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_UIInputMovementScript : MonoBehaviour
{
    [SerializeField] private float _playerSPeed = 2f;
    [SerializeField] private float _playerSprintSPeed = 5f;

    [SerializeField] private Rigidbody2D _playerRB;

    private bool _rightSide = true;

    private bool _notMove = true;
    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveRightSide(){
        if(_rightSide == true){

        }
    }

}
