using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Wallet
{
    public static List<CurrencyData> Currencies = new List<CurrencyData>();

    public static bool TryGet(CurrencyType currencyType, out CurrencyData value)
    {
        return Currencies.TryFind((currency) => currency.Type == currencyType, out value);
    }

    public static void Add(CurrencyData currencyData)
    {
        foreach (var currency in Currencies)
        {
            if (currency.Type.Equals(currency.Type))
            {
                return;
            }
        }

        Currencies.Add(currencyData);
    }

    public static bool TryChange(CurrencyType currencyType, float sum)
    {
        if (TryGet(currencyType, out var currency))
        {
            return currency.TryChange(sum);
        }

        return false;
    }
}