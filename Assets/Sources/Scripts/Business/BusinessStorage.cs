using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BusinessStorage
{
    public static List<BusinessData> Businesses = new List<BusinessData>();

    public static bool TryBuy(BusinessData businessData)
    {
        if (Wallet.TryChange(CurrencyType.Dollars, -businessData.CurrentPrice))
        {
            businessData.Level += 1;
            return true;
        }


        return false;
    }

    public static bool TryBuyUpgrade(UpgradeData upgradeData)
    {
        if (upgradeData.IsOpened)
        {
            return false;
        }

        if (Wallet.TryChange(CurrencyType.Dollars, -upgradeData.Price))
        {
            upgradeData.IsOpened = true;
        }

        return false;
    }
}