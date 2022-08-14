using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace ProjectRunner.Core.MovementSystem
{
    public class RotationController : MonoBehaviour
    {
        [SerializeField] private bool _invertY;
        [SerializeField] private float _sensitivity;
        [SerializeField] private float _xRotationBorder;
        [SerializeField] private Transform _playerTransform;

        private float _xMouseDelta;
        private float _yMouseDelta;

        private float _xRotation;
        private float _yRotation;

        private void Start()
        {
            InitCursor();
            /*Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    UpdateInputValues();
                    UpdateRotations();
                })
                .AddTo(this);*/
        }

        private void Update()
        {
            UpdateInputValues();
            UpdateRotations();
        }

        private static void InitCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void UpdateInputValues()
        {
            _xMouseDelta = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
            _yMouseDelta = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
        }

        private void UpdateRotations()
        {
            _yRotation += _xMouseDelta;
            _xRotation += _yMouseDelta * (-1 + 2 * Convert.ToInt32(_invertY));

            _xRotation = Mathf.Clamp(_xRotation, -_xRotationBorder, _xRotationBorder);

            var playerTransformRotation = _playerTransform.rotation;
            playerTransformRotation =
                Quaternion.Euler(playerTransformRotation.x, _yRotation, playerTransformRotation.z);

            _playerTransform.rotation = playerTransformRotation;
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, transform.rotation.z);
        }
    }
}