using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    private float _horizontalInput,_verticalInput;
    [SerializeField] private float _playerSpeed;
    private Vector3 _direction;
    private void FixedUpdate()
    {
        _horizontalInput = _joystick.Horizontal;
        _verticalInput = _joystick.Vertical;
        
        _rigidbody.velocity=new Vector3(_horizontalInput*_playerSpeed*Time.fixedDeltaTime,_rigidbody.velocity.y,_verticalInput*_playerSpeed*Time.fixedDeltaTime) ;

        if(_joystick.Horizontal!=0 ||_joystick.Vertical!=0 )
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
        
    }
    
}
