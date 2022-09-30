using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BindedText : BindedElement<TextMeshProUGUI>
{
    private void Reset()
    {
        target = GetComponent<TextMeshProUGUI>();
    }
}