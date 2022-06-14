using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private Transform _orientation;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxVelocity;
    
    private Vector2 _input;
    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;

    private void ReadInput()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
    }

    private void MoveByInput()
    {
        _input.Log();
        _moveDirection = (_orientation.forward * _input.y + _orientation.right * _input.x).normalized;

        Debug.DrawLine(_orientation.position, _moveDirection, Color.red);
        _moveDirection.Log();
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
        Observable.EveryUpdate()
            .Subscribe(_ => ReadInput())
            .AddTo(this);
        
        Observable.EveryFixedUpdate()
            .Subscribe(_ => MoveByInput())
            .AddTo(this);
    }
}