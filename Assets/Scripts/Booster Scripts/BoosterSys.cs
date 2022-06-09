using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSys
{
    public static BoosterData LoadBoosterData()
    {
        string json = SaveSystem.Load(SaveSystem.BOOSTER);
        if (json != null)
        {
            BoosterData BoosterData = JsonUtility.FromJson<BoosterData>(json);
            return BoosterData;
        }
        else
        {
            SaveBoosterData(CreateBoosterData());
            return (CreateBoosterData());

        }
    }

    public static BoosterData CreateBoosterData()
    {
        BoosterData BoosterData = new BoosterData
        {
                dynaNum = BoosterManager.dynamiteNum,
                superMan = false,
                thaiRope = false,
                timeAdd = false,
                magneticWall = false,
                diamondUP = false,
                stoneUP = false,
                DNK = false,
                fishNet = false
        };

        return BoosterData;
    }

    public static void SaveBoosterData(BoosterData BoosterData)
    {
        string json = JsonUtility.ToJson(BoosterData);
        SaveSystem.Save(json, SaveSystem.BOOSTER);
    }

    public static void ResetData()
    {
        SaveBoosterData(CreateBoosterData());
    }
}

public class BoosterData
{
    public int dynaNum;
    public bool superMan, thaiRope, timeAdd, magneticWall, diamondUP, stoneUP, DNK, fishNet;
}
