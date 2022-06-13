using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private bool _invertY;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _xRotationBorder;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _playerTransform;

    private float _xMouseDelta;
    private float _yMouseDelta;

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                UpdateInputValues();
                UpdateRotations();
            })
            .AddTo(this);
    }

    private void UpdateInputValues()
    {
        _xMouseDelta = Input.GetAxisRaw("Mouse X") * _sensitivity * Time.deltaTime;
        _yMouseDelta = Input.GetAxisRaw("Mouse Y") * _sensitivity * Time.deltaTime;
    }

    private void UpdateRotations()
    {
        _yRotation += _xMouseDelta;
        _xRotation += _yMouseDelta * ((-1) + 2 * Convert.ToInt32(_invertY));

        _xRotation = Mathf.Clamp(_xRotation, -_xRotationBorder, _xRotationBorder);

        var playerTransformRotation = _playerTransform.rotation;
        playerTransformRotation = Quaternion.Euler(playerTransformRotation.x, _yRotation, playerTransformRotation.z);
        
        _playerTransform.rotation = playerTransformRotation;
        _cameraTransform.rotation = Quaternion.Euler(_xRotation, _yRotation, _cameraTransform.rotation.z);
    }
}