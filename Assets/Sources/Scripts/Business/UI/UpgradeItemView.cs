using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeItemView : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI profitMultiplierText;
    public TextMeshProUGUI statusText;

    private UpgradeData _upgradeData;

    public void Init(UpgradeData upgradeData)
    {
        _upgradeData = upgradeData;

        nameText.text = upgradeData.Info.name;
        profitMultiplierText.text = $"Profit: + {(int)(_upgradeData.Info.profitMultiplier * 100)}%";
        RefreshView();

        _upgradeData.OnChangedIsOpened += RefreshView;
    }

    private void RefreshView()
    {
        statusText.text = (_upgradeData.IsOpened) ? "Куплено" : $"Price: {_upgradeData.Info.price}$";
    }

    public void OnClick_Buy()
    {
        BusinessStorage.TryBuyUpgrade(_upgradeData);
    }

    private void OnDestroy()
    {
        if (_upgradeData != null)
        {
            _upgradeData.OnChangedIsOpened -= RefreshView;
        }
    }
}