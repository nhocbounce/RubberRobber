using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelSystem
{
    public static readonly int[] TargetLevel ={ 1000, 4500, 6000, 8000, 11000, 12500, 15000, 17000, 19000, 22000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    
    public static ProgressData LoadProgressData()
    {
        string json = SaveSystem.Load(SaveSystem.PROGRESS);
        if (json != null)
        {
            ProgressData progressData = JsonUtility.FromJson<ProgressData>(json);
            return progressData;
        }
        else
        {
            SaveProgressData(CreateProgressData());
            return (CreateProgressData());
            
        }
    }

    public static ProgressData CreateProgressData()
    {
        ProgressData progressData = new ProgressData
        {
            curWorld = 0,
            curGold = 0,
            curLevel = 0
        };

        return progressData;    
    }

    public static void SaveProgressData(ProgressData progressData)
    {
        string json = JsonUtility.ToJson(progressData);
        SaveSystem.Save(json, SaveSystem.PROGRESS);
    }

    public static void ResetData()
    {
        SaveProgressData(CreateProgressData());
    }
}

public class ProgressData
{
    public int curWorld;
    public int curLevel;
    public int curGold;


}
