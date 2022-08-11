using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private Transform _orientation;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private CapsuleCollider _playerCollider;
    
    private Vector2 _input;
    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;

    private event Action OnSpaceDown;
    
    private void ReadInput()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
        HandleSpaceBar();
    }

    private void HandleSpaceBar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpaceDown?.Invoke();
        }
    }

    private bool TryJump()
    {
        
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, _playerCollider.height/2 + 0.01f))
        {
            Debug.Log($"{hit.collider}");
            return true;
        }
        Debug.Log($"{hit.collider}");
        return false;
    }

    private void MoveByInput()
    {
        _moveDirection = (_orientation.forward * _input.y + _orientation.right * _input.x).normalized;
        
        if(_rigidbody.velocity.magnitude < _maxVelocity)
            _rigidbody.AddForce(_speed * Time.deltaTime * _moveDirection, ForceMode.Force);
    }

    private void InitRigidbody()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //_rigidbody.freezeRotation = true;
    }

    private void Start()
    {
        InitRigidbody();
        OnSpaceDown += () => TryJump();
        Observable.EveryUpdate()
            .Subscribe(_ => ReadInput())
            .AddTo(this);
        
        Observable.EveryFixedUpdate()
            .Subscribe(_ => MoveByInput())
            .AddTo(this);
    }
}