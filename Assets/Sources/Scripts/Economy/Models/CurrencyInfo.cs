using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New CurrencyInfo")]
public class CurrencyInfo : ScriptableObject
{
    public string symbol = "$";
    public float startValue;
    public CurrencyType type;
}