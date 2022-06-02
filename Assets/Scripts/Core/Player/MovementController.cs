using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    
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
        _moveDirection.x = _input.x;
        _moveDirection.z = _input.y;

        _rigidbody.velocity = _speed * Time.deltaTime * _moveDirection;
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