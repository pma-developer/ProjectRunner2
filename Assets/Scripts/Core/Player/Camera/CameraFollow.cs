using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.MovementSystem
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _followTransform;

        private void LateUpdate()
        {
            transform.position = _followTransform.position;
        }
    }
}
