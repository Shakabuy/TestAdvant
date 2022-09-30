using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BusinessItemView : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public BindedText levelText;
    public BindedText profitText;
    public BindedText priceText;
    public BindedImage progressBar;

    [Space]
    public GameObject upgradeItemViewPrefab;
    public RectTransform upgradeItemViewParent;

    private BusinessData _businessData;
    public void Init(BusinessData businessData)
    {
        _businessData = businessData;

        nameText.text = _businessData.Info.name;

        levelText.Bind((target) => { target.text = $"Level\n{_businessData.Level}"; });
        profitText.Bind((target) => { target.text = $"Profit\n{_businessData.CurrentProfit}"; });
        priceText.Bind((target) => { target.text = $"Price: {_businessData.CurrentPrice}"; });
        progressBar.Bind((target) => { target.fillAmount = _businessData.Progress; });

        _businessData.OnChangedLevel += levelText.RefreshView;
        _businessData.OnChangedLevel += profitText.RefreshView;
        _businessData.OnChangedLevel += priceText.RefreshView;
        _businessData.OnChangedWorkTime += progressBar.RefreshView;
        foreach (var upgradeData in _businessData.Upgrades)
        {
            upgradeData.OnChangedIsOpened += profitText.RefreshView;
        }


        upgradeItemViewParent.Clear();
        foreach (var upgradeData in _businessData.Upgrades)
        {
            Instantiate(upgradeItemViewPrefab, upgradeItemViewParent).GetComponent<UpgradeItemView>().Init(upgradeData);
        }
    }

    public void OnClick_Buy()
    {
        BusinessStorage.TryBuy(_businessData);
    }

    private void OnDestroy()
    {
        if (_businessData == null)
        {
            return;
        }

        _businessData.OnChangedLevel -= levelText.RefreshView;
        _businessData.OnChangedLevel -= profitText.RefreshView;
        _businessData.OnChangedLevel -= priceText.RefreshView;
        _businessData.OnChangedWorkTime -= progressBar.RefreshView;

        foreach (var upgradeData in _businessData.Upgrades)
        {
            upgradeData.OnChangedIsOpened -= profitText.RefreshView;
        }
    }
}