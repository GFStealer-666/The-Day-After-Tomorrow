using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerControl _playerControls;
    private float _horizontal; 
    [SerializeField] private float _playerSpeed;

    [SerializeField] private float _playerDefaultSpeed = 2f;

     [SerializeField] private float _playerSprintSpeed = 5f;
    [SerializeField] private float _jumpingForce = 4f;

    private bool _rightSide = true;
    private float _playerDirection = 0f;
    Animator _playerAnimator;
    [SerializeField] private Rigidbody2D _playerRB;
    [SerializeField] private Transform _groundCheck;

    [SerializeField] private RG_HealthBar _healthbar;
    [SerializeField] private LayerMask _groundLayer ;

    [SerializeField] private AudioSource _characterSound;

    private void Awake() {
        _playerControls = new PlayerControl();
        _playerControls.Enable();

        _playerControls.Land.Move.performed += ctx =>{
            _playerDirection = ctx.ReadValue<float>();
        };
    }
    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if(_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shooting")){
            _playerSpeed = 0.1f;
        }
        else{
            _playerSpeed = _playerDefaultSpeed;
        }
        _playerRB.velocity = new Vector2(_playerDirection * _playerSpeed , _playerRB.velocity.y);
        // Debug.Log(_playerDirection);    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")){
            Jumping();
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            _playerSpeed = _playerSprintSpeed;
        }
        else if(_rightSide && _playerDirection == -1 || !_rightSide && _playerDirection == 1){
            FlipPlayer();
        }
        else if(_playerDirection == 0){
            _characterSound.enabled = false;
             animationIdle();   
        }
        else{
            animationRun();
            _characterSound.enabled = true;
        }
        // _horizontal = Input.GetAxis("Horizontal");
    }

    private void FlipPlayer()
    {       
        // Debug.Log("Flippin");
        Debug.Log(_rightSide);
        _rightSide = !_rightSide;
        transform.Rotate(0f,180f,0f);
            // Vector3 _localScale = transform.localScale;
            // _localScale.x *= -1f;
            // _localScale.z *= -1f;
            // transform.localScale = _localScale;
            // Debug.Log(_playerScale);
    }

    public void Jumping()
    {
            if(IsGrounded()){
                _playerRB.velocity = new Vector2(_playerRB.velocity.x , _jumpingForce);
                _playerAnimator.SetTrigger("isJumping");
                _playerAnimator.SetBool("isGrounded",false);
            }
         

    }

    // private bool GroundCheck()
    // {
    //     return Physics2D.OverlapCircle(_groundCheck.position,0.2f , _groundLayer);
    // }

    public bool IsGrounded()
    {
        _playerAnimator.SetBool("isGrounded",true);
        return Physics2D.OverlapCircle(_groundCheck.position,0.2f,_groundLayer);
        
    }

    private void animationRun()
    {
        _playerAnimator.SetBool("isWalking" , true);
        _playerAnimator.SetBool("isIdling" , false);
    }

    private void animationIdle()
    {
        _playerAnimator.SetBool("isWalking" , false);
        _playerAnimator.SetBool("isIdling" , true);
    }

}
