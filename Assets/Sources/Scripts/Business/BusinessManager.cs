using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessManager : SaveableManager<List<BusinessData>>
{
    public List<BusinessInfo> businessInfos = new List<BusinessInfo>();
    public override string SaveableID => "Business";

    public override void Init()
    {
        base.Init();
        new GameObject(nameof(BusinessUpdater)).AddComponent<BusinessUpdater>().Run();
    }

    public override object GetSaveData()
    {
        return BusinessStorage.Businesses;
    }

    public override void SetSaveData(object data)
    {
        var businesses = (List<BusinessData>)data;
        foreach (var info in businessInfos)
        {
            if (businesses.TryFind((x) => x.Id.Equals(info.id), out var business))
            {
                business.Info = info;
                business.RefreshUpgrades(info.upgrades);
                BusinessStorage.Businesses.Add(business);
            }
            else
            {
                BusinessStorage.Businesses.Add(new BusinessData(info));
            }
        }
    }

    public override void SetSaveDefaultData()
    {
        foreach (var info in businessInfos)
        {
            BusinessStorage.Businesses.Add(new BusinessData(info));
        }
    }
}