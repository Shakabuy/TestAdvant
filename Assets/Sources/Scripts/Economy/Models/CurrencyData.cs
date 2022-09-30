using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrencyData
{
    public CurrencyData()
    {

    }

    public CurrencyData(CurrencyInfo currencyInfo)
    {
        Type = currencyInfo.type;
        CurrentValue = currencyInfo.startValue;
        info = currencyInfo;
    }

    public CurrencyType Type { get; }

    [JsonIgnore]
    public CurrencyInfo info;
    public event Action OnChanged;

    private float _currentValue;
    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            if (_currentValue != value)
            {
                _currentValue = value;
                OnChanged?.Invoke();
            }
        }
    }

    public virtual bool TryChange(float sum)
    {
        if (CurrentValue + sum >= 0)
        {
            CurrentValue += sum;
            return true;
        }

        return false;
    }
}