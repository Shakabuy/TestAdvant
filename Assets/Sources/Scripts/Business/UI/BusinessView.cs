using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessView : MonoBehaviour
{
    public GameObject businessItemViewPrefab;
    public RectTransform parent;

    private void Start()
    {
        parent.Clear();

        foreach (var business in BusinessStorage.Businesses)
        {
            Instantiate(businessItemViewPrefab, parent).GetComponent<BusinessItemView>().Init(business);
        }
    }
}