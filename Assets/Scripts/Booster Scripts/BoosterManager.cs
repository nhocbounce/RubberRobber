using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    // For Scene Change
    static public int dynamiteNum;


    [SerializeField]
    private GameObject dynamitePrefab;
    [SerializeField]
    private GameObject dynamiteButton;


    // For UI
    [SerializeField]
    private GameObject timeAddTxt;
    [SerializeField]
    private GameObject superManText;
    [SerializeField]
    private GameObject thaiRopeText;
    [SerializeField]
    private GameObject DNKTut;
    [SerializeField]
    private GameObject ThaiTut;
    [SerializeField]
    private GameObject MagnetWallTut;
    [SerializeField]
    private GameObject FishNetTut;


    private SaveData sd;
    private GameObject dynamite;
    private HookScript hookScript;
    private GameplayManager gameplayManager;
    private DNKTut tut;

    public bool noDyna, superMan, thaiRope, timeAdd, magneticWall, diamondUP, stoneUP, DNK, fishNet;

    BoosterData boosterData;

    GameObject dynaThrow;

    private bool win;

    //private Vector3 initialDyna;

    private void Awake()
    {
    }
    void Start()
    {  
        gameplayManager = FindObjectOfType<GameplayManager>();
        hookScript = FindObjectOfType<HookScript>();
        tut = FindObjectOfType<DNKTut>();
        LoadBoosterData();
    }


    // Update is called once per frame
    void Update()
    {
        DynamiteGen();
        if (superMan)
            SuperManOn();
        if (thaiRope)
            ThaiRope();
        if (timeAdd)
            TimeAdd();

        
    }
    
    public void MagneticWallEnable()
    {
        magneticWall = true;
        if (tut != null)
            tut.On();
    }

    public void DoNgheoKhiEnable()
    {
        DNK = true;
        if (tut != null)
            tut.On();
    }
    public void FishNetEnable()
    {
        fishNet = true;
        if (tut != null)
            tut.On();
    }
    public void DynamiteGen()
    {
        if ((dynamiteNum > 0) && (GameObject.FindGameObjectsWithTag(Tags.DYNAMITETHROW).Length == 0))
        {
            dynaThrow = Instantiate(dynamitePrefab);
        }

        if (dynamiteNum == 0)
        {
            noDyna = true;
            dynamiteButton.SetActive(false);
        }
        else
        {
            noDyna = false;
            dynamiteButton.SetActive(true);
        }
        gameplayManager.DisplayDyna(noDyna, dynamiteNum);
    }

    public void TriggerDyna()
    {
        dynaThrow.GetComponent<Dynamite>().TriggerDynamite();
    }

    public void SuperManOn()
    {
        HookMovement.playerStrength = 10f;
        gameplayManager.TxtOn(superManText, 0.5f);
        hookScript.SuperManAnim();
        superMan = false;
        
    }

    public void ThaiRope()
    {
        HookMovement.initialSpeed = 5f;
        gameplayManager.TxtOn(thaiRopeText, 0.5f);
        hookScript.SuperManAnim();
        thaiRope = false;
    }

    public void TimeAdd()
    {
        gameplayManager.TxtOn(timeAddTxt, 1);
        if (gameplayManager.countdownTimer <= -1)
            gameplayManager.countdownTimer = 15f;
        else
            gameplayManager.countdownTimer += 15;
        timeAdd = false;
    }

    public void LoadBoosterData()
    {
        BoosterData boosterData = BoosterSys.LoadBoosterData();

        dynamiteNum = boosterData.dynaNum;
        superMan = boosterData.superMan;
        thaiRope = boosterData.thaiRope;
        timeAdd = boosterData.timeAdd;
        magneticWall = boosterData.magneticWall;
        diamondUP = boosterData.diamondUP;
        stoneUP = boosterData.stoneUP;
        DNK = boosterData.DNK;
        fishNet = boosterData.fishNet;
    }



}
