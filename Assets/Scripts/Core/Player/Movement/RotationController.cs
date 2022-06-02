using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _playerTransform;

    private float _xMouseDelta;
    private float _yMouseDelta;

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
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
        _xRotation += _yMouseDelta;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        var rotation = _playerTransform.rotation;
        rotation = Quaternion.Euler(rotation.x, _yRotation, rotation.z);
        
        _playerTransform.rotation = rotation;
        _cameraTransform.rotation = Quaternion.Euler(_xRotation, _yRotation, _cameraTransform.rotation.z);
    }
}