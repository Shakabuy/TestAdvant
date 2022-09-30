using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessUpdater : MonoBehaviour
{
    public void Run()
    {
        StopAllCoroutines();
        StartCoroutine(UpdaterCycle());
    }

    IEnumerator UpdaterCycle()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            AddProgress(Time.deltaTime);
        }
    }

    private void AddProgress(float dt)
    {
        foreach (var business in BusinessStorage.Businesses)
        {
            if (business.Level > 0)
            {
                business.WorkTime += dt;

                if (business.WorkTime > business.Info.profitInterval)
                {
                    business.WorkTime = 0f;
                    Wallet.TryChange(CurrencyType.Dollars, business.CurrentProfit);
                }
            }
        }
    }
}
