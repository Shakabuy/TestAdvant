using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BalanceText : MonoBehaviour
{
    public TextMeshProUGUI target;
    public CurrencyType balanceType;

    private CurrencyData _currencyData;

    private void Start()
    {
        if (Wallet.TryGet(balanceType, out CurrencyData currencyData))
        {
            _currencyData = currencyData;
            _currencyData.OnChanged += RefreshView;

            RefreshView();
        }
    }

    private void RefreshView()
    {
        target.text = $"Баланс: {_currencyData.CurrentValue}{_currencyData.info.symbol}";
    }

    private void Reset()
    {
        target = GetComponent<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        if (_currencyData != null)
        {
            _currencyData.OnChanged -= RefreshView;
        }
    }
}