using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;


namespace ProjectRunner.Core.MovementSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Transform _orientation;
        [SerializeField] private float _movingForce;
        [SerializeField] private float _jumpingForce;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _airForceMultiplier;

        [SerializeField] private float _requiredDistanceToEarthToJump;
        [SerializeField] private CapsuleCollider _playerCollider;

        private bool _isGrounded;
        private Vector2 _input;
        private Vector3 _moveDirection;
        private Rigidbody _rigidbody;

        private event Action OnSpaceDown;

        private void ReadInput()
        {
            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");
            HandleSpaceBar();
        }

        private void HandleSpaceBar()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSpaceDown?.Invoke();
            }
        }

        private void Jump()
        {
            _rigidbody.AddForce(_jumpingForce * Vector3.up, ForceMode.Impulse);
        }

        private void UpdateIsGrounded()
        {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down,
                _playerCollider.height / 2 + _requiredDistanceToEarthToJump);
        }

        private void JumpIfGrounded()
        {
            if (_isGrounded)
                Jump();
        }

        private void MoveByInput()
        {
            _moveDirection = (_orientation.forward * _input.y + _orientation.right * _input.x).normalized;

            var resultMovingForce = _movingForce * 10f * _moveDirection;
            if (_isGrounded)
                _rigidbody.AddForce(resultMovingForce, ForceMode.Force);
            else
                _rigidbody.AddForce(_airForceMultiplier * resultMovingForce, ForceMode.Force);
        }

        private void LimitVelocity()
        {
            var velocity = _rigidbody.velocity;
            var horizontalVelocity = new Vector2(velocity.x, velocity.z);
            if (horizontalVelocity.magnitude > _maxVelocity)
            {
                var limitedVelocity = horizontalVelocity.normalized * _maxVelocity;
                _rigidbody.velocity = new Vector3(limitedVelocity.x, _rigidbody.velocity.y, limitedVelocity.y);
            }
        }

        private void InitRigidbody()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }


        private void Update()
        {
            ReadInput();
        }

        private void FixedUpdate()
        {
            UpdateIsGrounded();
            MoveByInput();
            LimitVelocity();
        }

        private void Start()
        {
            InitRigidbody();
            OnSpaceDown += JumpIfGrounded;
        }
    }
}