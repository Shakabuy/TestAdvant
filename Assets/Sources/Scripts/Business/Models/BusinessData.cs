using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BusinessData
{
    public BusinessData() { }
    public BusinessData(BusinessInfo businessInfo)
    {
        Id = businessInfo.id;
        Level = (businessInfo.isStartOpened) ? 1 : 0;
        Info = businessInfo;

        RefreshUpgrades(businessInfo.upgrades);
    }

    public event Action OnChangedLevel;
    public event Action OnChangedWorkTime;

    public string Id;
    public float CurrentProfit => Level * Info.baseProfit * GetProfitMultiplier();
    public float Progress => Mathf.Clamp(WorkTime / Info.profitInterval, 0f, 1f);
    public float CurrentPrice => (Level + 1) * Info.basePrice;

    public List<UpgradeData> Upgrades = new List<UpgradeData>();

    private int _level;
    public int Level
    {
        get => _level;
        set
        {
            if (_level != value)
            {
                _level = value;
                OnChangedLevel?.Invoke();
            }
        }
    }

    private float _workTime;
    public float WorkTime
    {
        get => _workTime;
        set
        {
            _workTime = value;
            OnChangedWorkTime?.Invoke();
        }
    }

    [JsonIgnore]
    public BusinessInfo Info;

    public void RefreshUpgrades(List<UpgradeInfo> upgradeInfos)
    {
        foreach (var upgradeInfo in upgradeInfos)
        {
            if (Upgrades.TryFind((x) => x.Id.Equals(upgradeInfo.id), out var data))
            {
                data.Info = upgradeInfo;
            }
            else
            {
                Upgrades.Add(new UpgradeData(upgradeInfo));
            }
        }
    }

    private float GetProfitMultiplier()
    {
        float result = 1f;

        foreach (var upgrade in Upgrades)
        {
            if (upgrade.IsOpened)
            {
                result += upgrade.Info.profitMultiplier;
            }
        }

        return result;
    }
}