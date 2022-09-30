using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : SaveableManager<List<CurrencyData>>
{
    public List<CurrencyInfo> CurrencyInfos = new List<CurrencyInfo>();

    public override string SaveableID => "Wallet";

    public override object GetSaveData()
    {
        return Wallet.Currencies;
    }

    public override void SetSaveData(object data)
    {
        var balances = (List<CurrencyData>)data;
        foreach (var currencyInfo in CurrencyInfos)
        {
            if (balances.TryFind((x) => x.Type == currencyInfo.type, out var balance))
            {
                balance.info = currencyInfo;
                Wallet.Add(balance);
            }
        }
    }

    public override void SetSaveDefaultData()
    {
        foreach (var currencyInfo in CurrencyInfos)
        {
            Wallet.Add(new CurrencyData(currencyInfo));
        }
    }
}