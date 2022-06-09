using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialStep { START, GOLD, ROCK, DYNA, DYNA2, DONE }
public class Tutorial : MonoBehaviour
{
    private HookMovement hookMovement;
    private HookScript hookScript;
    private BoosterManager boosterManager;
    private GameplayManager gameplayManager;
    // Start is called before the first frame update
    public TutorialStep step;

    private bool once, once1, once2;

    public GameObject focusTutPanel, dynaTutText, grabDynaTxt, goldTutText, grabGoldTxt, rockTutText, grabRockTxt, timeTutText, scoreTxt1, scoreTxt2, scoreTxt3, timeTxt1,
        timeTxt2, dynaTxt, conTxt, doneTxt, startTutPan, startTutTxt, wallTutPan;

    private void Awake()
    {
        boosterManager = FindObjectOfType<BoosterManager>();
        gameplayManager = FindObjectOfType<GameplayManager>();
        LevelSystem.ResetData();
        BoosterManager.dynamiteNum = 0;
        BoosterSys.ResetData();
    }
    void Start()
    {
        hookMovement = FindObjectOfType<HookMovement>();
        hookScript = FindObjectOfType<HookScript>();
        step = TutorialStep.START;
        Invoke(nameof(StartTut), 1f);
    }

    void StartTut()
    {
        PanelOnOff(startTutPan, true);
        Time.timeScale = 0;

    }

    public void StartTutDown()
    {
        PanelOnOff(startTutPan, false);
        ScreenToScreen.world = true;
        PanelOnOff(focusTutPanel, true);
        PanelOnOff(startTutTxt, true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!once)
            if (hookMovement.tutorialWall)
            {
                once = true ;
                Invoke(nameof(WallTutor), 1);
            }
        switch (step)
        {
            case TutorialStep.GOLD:
                if ((hookMovement.rotate_Angle > -70) && (hookMovement.rotate_Angle < -60))
                    if (!once1)
                    {
                        once1 = true;
                        GoldTutor();
                    }
                break;
            case TutorialStep.ROCK:
                if ((hookMovement.rotate_Angle > -30) && (hookMovement.rotate_Angle < -20))
                    if (!once1)
                    {
                        once1 = true;
                        RockTutor();
                    }
                break;
            case TutorialStep.DYNA:
                if ((hookMovement.rotate_Angle > 10) && (hookMovement.rotate_Angle < 17))
                    if (!once1)
                    {
                        once1 = true;
                        DynaTutor1();
                    }
                break;
            case TutorialStep.DYNA2:
                if (BoosterManager.dynamiteNum > 0 && HookScript.itemAttached)
                    if (!once1)
                    {
                        once1 = true;
                        Invoke(nameof(DynaTutor2), 1);
                    }

                break;
        }
     
    }

    void WallTutor()
    {
        PanelOnOff(wallTutPan, true);
        Time.timeScale = 0;
    }

    public void WallTutorDown()
    {
        PanelOnOff(wallTutPan, false);
        Time.timeScale = 1;
    }

    void GoldTutor()
    {
        Time.timeScale = 0;
        PanelOnOff(goldTutText, true);
        PanelOnOff(grabGoldTxt, false);
    }

    public void DoneStepTut()
    {
        switch (step)
        {
            case TutorialStep.START:

                break;
            case TutorialStep.GOLD:
                if (goldTutText.activeSelf)
                {
                    Invoke(nameof(ScoreTutor), 7.7f);
                    PanelOnOff(goldTutText, false);
                }
                else
                {
                    ForceInput(grabGoldTxt);
                }
                break;
            case TutorialStep.ROCK:
                if (rockTutText.activeSelf)
                {
                    Invoke(nameof(TimeTutor), 6f);
                    PanelOnOff(rockTutText, false);
                }
                else
                {
                    ForceInput(grabRockTxt);
                }
                break;
            case TutorialStep.DYNA:
                if (dynaTutText.activeSelf)
                {
                    PanelOnOff(dynaTutText, false);
                    step = TutorialStep.DYNA2;
                    once1 = false;
                }
                else
                {
                    ForceInput(grabDynaTxt);
                }
                break;
        }

    }

    void ForceInput(GameObject m)
    {
        TouchArea.noInput = true;
        PanelOnOff(m, true);
    }

    void ScoreTutor()
    {
        PanelOnOff(focusTutPanel, true);
        PanelOnOff(scoreTxt1, true);
        PanelOnOff(grabGoldTxt, false);
        Time.timeScale = 0;
    }
    void TimeTutor()
    {
        PanelOnOff(focusTutPanel, true);
        PanelOnOff(timeTutText, true);
        PanelOnOff(grabRockTxt, false);
        Time.timeScale = 0;
    }

    public void FocusTutOn()
    {
        switch (step)
        {
            case TutorialStep.START:
                Time.timeScale = 1;
                PanelOnOff(startTutTxt, false);
                PanelOnOff(focusTutPanel, false);
                ScreenToScreen.world = false;
                step = TutorialStep.GOLD;
                break;
            case TutorialStep.GOLD:
                if (scoreTxt1.activeSelf)
                {
                    HookMovement.move_Speed = HookMovement.initialSpeed;
                    PanelOnOff(scoreTxt1, false);
                    PanelOnOff(scoreTxt2, true);
                }
                else if (scoreTxt2.activeSelf)
                {
                    PanelOnOff(scoreTxt2, false);
                    ScreenToScreen.world = true;
                    PanelOnOff(scoreTxt3, true);
                }
                else if (scoreTxt3.activeSelf)
                {
                    PanelOnOff(scoreTxt3, false);
                    PanelOnOff(focusTutPanel, false);
                    ScreenToScreen.world = false;
                    Time.timeScale = 1;
                    step = TutorialStep.ROCK;
                    once1 = false;
                }
                break;


            case TutorialStep.ROCK:
                if (timeTutText.activeSelf)
                {
                    PanelOnOff(timeTutText, false);
                    PanelOnOff(timeTxt1, true) ;
                }
                else if (timeTxt1.activeSelf)
                {
                    PanelOnOff(timeTxt1, false);
                    ScreenToScreen.world = true;
                    PanelOnOff(timeTxt2, true);
                }
                else if (timeTxt2.activeSelf)
                {
                    PanelOnOff(timeTxt2, false);
                    PanelOnOff(focusTutPanel, false);
                    Time.timeScale = 1;
                    step = TutorialStep.DYNA;
                    once1 = false;
                }
                break;
        }
        

    }

    void PanelOnOff(GameObject m, bool l)
    {
        if (m)
            m.SetActive(l);
    }

    void RockTutor()
    {
        Time.timeScale = 0;
        PanelOnOff(rockTutText, true);
        PanelOnOff(grabRockTxt, false);
    }
    void DynaTutor1()
    {
        Time.timeScale = 0;
        PanelOnOff(dynaTutText, true);
        PanelOnOff(grabDynaTxt, false);
    }
    void DynaTutor2()
    {
        PanelOnOff(focusTutPanel, true);
        PanelOnOff(dynaTxt, true);
        PanelOnOff(conTxt, false);
        PanelOnOff(grabDynaTxt, false);
        Time.timeScale = 0;
    }

    public void DynaTutorDown()
    {
        if (once1)
        {
            Time.timeScale = 1;
            PanelOnOff(dynaTxt, false);
            PanelOnOff(focusTutPanel, false);
            PanelOnOff(doneTxt, true);
            step = TutorialStep.DONE;
        }

    }

}
