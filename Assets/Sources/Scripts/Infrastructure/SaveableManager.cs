using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveableManager<T> : Manager, ISaveable where T : class
{
    public abstract string SaveableID { get; }
    public abstract object GetSaveData();
    public abstract void SetSaveData(object data);
    public abstract void SetSaveDefaultData();

    public override void Init()
    {
        InitSave();
    }

    protected virtual void InitSave()
    {
        SaveManager.Register(this);

        var saveData = SaveManager.Load<T>(this);

        if (saveData == null)
        {
            SetSaveDefaultData();
        }
        else
        {
            SetSaveData(saveData);
        }
    }
}