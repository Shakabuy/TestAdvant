using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    string SaveableID { get; }
    public void SetSaveData(object data);
    public object GetSaveData();
}