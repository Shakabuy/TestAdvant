using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindedElement<T> : MonoBehaviour
{
    [SerializeField]
    protected T target;
    private Action<T> _onChange;
    public void Bind(Action<T> onChange)
    {
        _onChange = onChange;
        RefreshView();
    }

    public void RefreshView()
    {
        _onChange?.Invoke(target);
    }
}