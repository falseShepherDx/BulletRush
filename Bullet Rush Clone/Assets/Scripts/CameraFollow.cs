using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _offset, _playerPos;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _offset = transform.position - target.position;

    }
    private void LateUpdate()
    {
        _playerPos = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, _playerPos, ref _velocity, smoothTime);
    }
}
