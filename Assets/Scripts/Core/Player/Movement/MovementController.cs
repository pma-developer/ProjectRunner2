using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
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
        var selfTransform = transform;
        _moveDirection = (selfTransform.forward * _input.y + selfTransform.right * _input.y).normalized;

        Debug.DrawLine(_moveDirection, selfTransform.position);
        _rigidbody.AddForce(_speed * Time.deltaTime * _moveDirection, ForceMode.Force);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        Observable.EveryUpdate()
            .Subscribe(_ => ReadInput())
            .AddTo(this);
        
        Observable.EveryFixedUpdate()
            .Subscribe(_ => MoveByInput())
            .AddTo(this);
    }
}