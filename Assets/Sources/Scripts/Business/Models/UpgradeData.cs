using Newtonsoft.Json;
using System;

public class UpgradeData
{
    public UpgradeData() { }
    public UpgradeData(UpgradeInfo upgradeInfo)
    {
        Id = upgradeInfo.id;
        Info = upgradeInfo;
    }
    public string Id;
    public float Price => Info.price;

    private bool _isOpened;
    public bool IsOpened
    {
        get => _isOpened;
        set
        {
            if(_isOpened != value)
            {
                _isOpened = value;
                OnChangedIsOpened?.Invoke();
            }
        }
    }

    [JsonIgnore]
    public UpgradeInfo Info;
    public event Action OnChangedIsOpened;
}
