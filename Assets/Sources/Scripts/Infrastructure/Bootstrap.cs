using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public List<Manager> managers = new List<Manager>();
    private void Awake()
    {
        Application.targetFrameRate = 60;
        managers.ForEach((manager) => manager.Init());
    }
}