using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : Manager
{
    public bool autoSave;
    public float autoSaveInterval;
    private static string SaveFolderPath => Application.persistentDataPath + "/SaveFiles";

    private static List<ISaveable> _saveables;
    private static List<ISaveable> Saveables
    {
        get
        {
            if (_saveables == null)
            {
                _saveables = new List<ISaveable>();
            }

            return _saveables;
        }
    }

    public override void Init()
    {
        if (autoSave)
        {
            StopAllCoroutines();
            StartCoroutine(AutoSaveCycle());
        }
    }

    IEnumerator AutoSaveCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveInterval);
            Save();
        }
    }


    public static void Register(ISaveable saveable)
    {
        if (Saveables.Contains(saveable))
        {
            return;
        }

        Saveables.Add(saveable);
    }

    /*public static void Unregister(ISaveable saveable)
    {
        Save(saveable);
        Saveables.Remove(saveable);
    }*/


    public static void Save()
    {
        foreach (var saveable in Saveables)
        {
            Save(saveable);
        }
    }

    public static void Save(string saveableID)
    {
        for (int i = 0; i < Saveables.Count; i++)
        {
            if (Saveables[i].SaveableID == saveableID)
            {
                Save(Saveables[i]);
            }
        }
    }

    public static void Save(ISaveable saveable)
    {
        if (!Directory.Exists(SaveFolderPath))
        {
            Directory.CreateDirectory(SaveFolderPath);
        }

        try
        {
            string fileContents = JsonConvert.SerializeObject(saveable.GetSaveData());
            string saveFilePath = GetSaveFilePath(saveable.SaveableID);

            File.WriteAllText(saveFilePath, fileContents);

            Debug.Log($"Save to {saveFilePath}");

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

    }

    public static T Load<T>(ISaveable saveable) where T : class
    {
        return Load<T>(saveable.SaveableID);
    }

    public static T Load<T>(string saveId) where T : class
    {
        string saveFilePath = GetSaveFilePath(saveId);

        if (File.Exists(saveFilePath))
        {
            string fileContents = File.ReadAllText(saveFilePath);
            Debug.Log(fileContents);
            return JsonConvert.DeserializeObject<T>(fileContents); ;
        }

        return null;
    }

    public static bool HasSaveData(string saveId)
    {
        return File.Exists(GetSaveFilePath(saveId));
    }

    public static void DeleteSaveData()
    {
        Directory.Delete(SaveFolderPath, true);
    }

#if UNITY_EDITOR

    [UnityEditor.MenuItem("Tools/Framework/Delete Editor Save Data", priority = 0)]
    public static void DeleteSaveDataEditor()
    {
        DeleteSaveData();
    }

#endif

    private static string GetSaveFilePath(string saveId)
    {
        return string.Format("{0}/{1}.json", SaveFolderPath, saveId);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }

    private void OnDestroy()
    {
        Save();
    }
}