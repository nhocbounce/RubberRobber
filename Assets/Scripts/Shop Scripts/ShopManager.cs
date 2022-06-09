using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text gold;

    
    public struct Item {
        public Sprite img;
        public int cost;
    }
    public Item[] shopItemImgList;

    [SerializeField]
    private Sprite[] imgItem;
    [SerializeField]
    private int[] costItem;

    private GameplayManager gameplayManager;

    private BoosterManager boosterManager;

    BoosterData boosterData = new BoosterData();



    // Start is called before the first frame update
    private void Awake()
    {
        shopItemImgList = new Item[imgItem.Length];
        for (int i = 0; i < shopItemImgList.Length; i++)
        {
            shopItemImgList[i].img = imgItem[i];
            shopItemImgList[i].cost = costItem[i];
        }
    }

    void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
        boosterManager = FindObjectOfType<BoosterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gold.text = "$" + GameplayManager.scoreCount.ToString();
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("Home");
    }
   

    public void NextLvl()
    {
        int index = (LevelSystem.LoadProgressData().curWorld - 1) * 5 + LevelSystem.LoadProgressData().curLevel + 2;
        boosterData.dynaNum = BoosterManager.dynamiteNum;
        BoosterSys.SaveBoosterData(boosterData);
        gameplayManager.ChangeProgresssData();
        SceneManager.LoadSceneAsync(index);
        
    }




    
    public void ItemBuy(int i)
    {
        GameplayManager.scoreCount -= costItem[i];
        switch (i)
        {
            case 0:
                boosterData.superMan = true;
                break;
            case 1:
                boosterData.thaiRope = true;
                break;
            case 2:
                boosterData.timeAdd = true;
                break;
            case 3:
                BoosterManager.dynamiteNum++;
                break;
            case 4:
                boosterData.fishNet = true;
                break;
            case 5:
                boosterData.DNK = true;
                break;
            case 6:
                boosterData.magneticWall = true;
                break;
        }
    }
}
