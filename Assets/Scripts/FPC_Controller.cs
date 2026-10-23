using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPC_Controller : MonoBehaviour
{
    [SerializeField] private Vector3 _characterVelocity;
    [SerializeField] private float _speed;
    public float tiredSpeedMultiplier = 0.4f;
    [SerializeField] private LayerMask _groundCheckLayers;
    [SerializeField] private float _jumpHeight;
    private CharacterController _characterController;
    private float _droundCheckDistance;
    private bool _isGrounded;
    private bool _isWalled;
    public PlayerStats stats;
    private void Awake(){
        _characterController = GetComponent <CharacterController>();
        _droundCheckDistance = _characterController.skinWidth + Physics.defaultContactOffset;
    }

    public event Action<bool>Moving;
    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        float speedMultiplier = 1f;
        if (stats != null && !stats.HasStamina())
            speedMultiplier = tiredSpeedMultiplier;
        Vector3 input = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        _characterVelocity = new Vector3(input.x * _speed, _characterVelocity.y, input.z * _speed);
        if (_isGrounded){
            _characterVelocity.y = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
                _characterVelocity.y = Mathf.Sqrt(-2 * Physics.gravity.y * _jumpHeight);
        }
        else
            _characterVelocity.y += Physics.gravity.y * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _speed = _speed * 4;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            _speed = _speed / 4;
        _characterController.Move(_characterVelocity * Time.deltaTime * speedMultiplier);
        if (stats != null)
            stats.TickStamina(Time.fixedDeltaTime/2);
        bool Moving = _characterVelocity.x != 0 || _characterVelocity.z != 0;
        if (stats != null)
            stats.isMoving = Moving;
        //Moving?.Invoke(Moving);
    }
    private void GroundCheck(){
        _isGrounded = false;
        Vector3 bottomSphere = transform.position - Vector3.up * _characterController.radius;
        if(Physics.SphereCast(
            bottomSphere, _characterController.radius, Vector3.down, out RaycastHit hit, _droundCheckDistance, _groundCheckLayers, 
            QueryTriggerInteraction.Ignore)){
                _isGrounded = true;
                if (hit.distance > _characterController.skinWidth){
                    _characterController.Move(Vector3.down * hit.distance);
                }
            }
    }
}
