using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New BusinessInfo")]
public class BusinessInfo : ScriptableObject
{
    public string id;
    public new string name;
    public float profitInterval;
    public float basePrice;
    public float baseProfit;
    public bool isStartOpened;
    public List<UpgradeInfo> upgrades = new List<UpgradeInfo>();
}
