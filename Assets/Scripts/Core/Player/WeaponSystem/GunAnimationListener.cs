using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimationListener : MonoBehaviour
{
    public event Action OnFireEnded;
    public event Action OnFireStarted;

    private Animator _animator;
    private static readonly int Fire = Animator.StringToHash("Fire");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnFireEnd()
    {
        _animator.ResetTrigger(Fire);
        OnFireEnded?.Invoke();
    }

    private void OnFireStart()
    {
        OnFireStarted?.Invoke();
    }
}
