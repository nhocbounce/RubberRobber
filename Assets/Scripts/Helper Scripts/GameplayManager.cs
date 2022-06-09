using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayManager : MonoBehaviour {

    // UI Component
    [SerializeField]
    private Text countdownText;
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private TMP_Text worldText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text goldTargetText;
    [SerializeField]
    private TMP_Text missingGoldText;
    [SerializeField]
    private TMP_Text waitReviveTxt;
    [SerializeField]
    private TMP_Text x2WaitTxt;
    [SerializeField]
    private TMP_Text startGoal;
    [SerializeField]
    private GameObject[] runningText, runningText1;
    [SerializeField]
    private GameObject noThankTxt;


    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private GameObject contPanel;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject startPanel;
    [SerializeField]
    private GameObject shopPanel;

    // UI Booster
    [SerializeField]
    private Text dynaText;

    // Temp Var
    private float tempSpeed;
    private bool once, win;
    int i = 5, i1 = 3;

    // UI var
    public float countdownTimer;
    public int scoreTarget, curWorld, curLevel;


    static public int scoreCount;

    public bool Lightning, Police, disable;

    private HookMovement hookMovement;
    private SoundManager soundManager;
    private BoosterManager boosterManager;

    public GameObject lightningWarn, policeWarn, lightning, police;




    void Awake() {
        hookMovement = FindObjectOfType<HookMovement>();
        soundManager = FindObjectOfType<SoundManager>();
        boosterManager = FindObjectOfType<BoosterManager>();
        Time.timeScale = 1f;
        if (LevelSystem.LoadProgressData().curWorld == 0)
            curLevel = 5;
        else
            LoadProgresssData();
        scoreTarget = LevelSystem.TargetLevel[(curWorld - 1) * 5 + curLevel];
    }

    void PanelOnOff(GameObject m, bool l)
    {
        if (m)
            m.SetActive(l);
    }

    void Start() {
        DisplayScore(0);
        countdownText.text = countdownTimer.ToString();
        goldTargetText.text = scoreTarget.ToString();
        startGoal.text = "$ " + scoreTarget.ToString();
        if (LevelSystem.LoadProgressData() != null)
        {
            worldText.text = LevelSystem.LoadProgressData().curWorld.ToString();
            if (LevelSystem.LoadProgressData().curWorld == 0)
            {
                levelText.text = 0.ToString();
            }
            else
            {
                levelText.text = LevelSystem.LoadProgressData().curLevel.ToString();
            }

        }

        PanelOnOff(startPanel, true);
        Invoke(nameof(StartPanOff), 1);
        disable = true;
    }

    void StartPanOff()
    {
        PanelOnOff(startPanel, false);
        StartCoroutine(Countdown());
        HookMovement.move_Speed = HookMovement.initialSpeed;
        hookMovement.rotate_Speed = hookMovement.initialRotSpeed;
    }

    

    private void Update()
    {
        if (lightning.GetComponent<Police>().checking)
            if (HookMovement.canRotate)
            {
                lightning.GetComponent<Police>().runSpeed = 0;
                DieGame();
            }
        if(police.GetComponent<Police>().checking)
            if (!HookMovement.canRotate)
            {
                police.GetComponent<Police>().runSpeed = 0;
                DieGame();

            }
        if (losePanel.activeSelf == true)
            missingGoldText.text = "You only need " + (scoreTarget - scoreCount) + " gold";
        
            


        for (int i = 1; i < runningText.Length; i += 2)
        {
            switch(i)
            {
                case 1: runningText[i].GetComponent<RunningText>().point = scoreCount;
                    break;
                case 3: runningText[i].GetComponent<RunningText>().point = scoreTarget;
                    break;
                case 5: runningText[i].GetComponent<RunningText>().point = (scoreCount - scoreTarget);
                    break;
            }
        }
        for (int i = 1; i < runningText1.Length; i += 2)
        {
            switch(i)
            {
                case 1: runningText1[i].GetComponent<RunningText>().point = scoreCount;
                    break;
                case 3: runningText1[i].GetComponent<RunningText>().point = scoreTarget;
                    break;
            }
        }
    }



    // For Ui and Wincon
    IEnumerator Countdown() {
        yield return new WaitForSeconds(1f);

        countdownTimer -= 1;

        countdownText.text = countdownTimer.ToString();

        if(countdownTimer <= 10) {
           soundManager.TimeRunningOut(true);
        }

        StartCoroutine("Countdown");

        if(countdownTimer <= 0) {
            if (!boosterManager.DNK)
            {
                StopCoroutine("Countdown");
                tempSpeed = HookMovement.move_Speed;
                HookMovement.move_Speed = 0;
               //soundManager.GameEnd();
               soundManager.TimeRunningOut(false);
                if (scoreCount < scoreTarget)
                    EndGame();
                else
                    StartCoroutine(WinPanelOn());
            }
            if (countdownTimer <= -30)
            {
                StopCoroutine("Countdown");
               //soundManager.GameEnd();
                soundManager.TimeRunningOut(false);
                boosterManager.DNK = false;
                EndGame();
            }

            
        }

    } // countdown


    public void DieGame()
    {
        StopCoroutine("Countdown");
        tempSpeed = HookMovement.move_Speed;
        HookMovement.move_Speed = 0;
        //soundManager.GameEnd();
        soundManager.TimeRunningOut(false);
        StartCoroutine(LosePanelOn(1));
    }

    public void EndGame()
    {
        if ((SceneManager.GetActiveScene().buildIndex != 1) && (!once))
        {
            PanelOnOff(contPanel, true);
            once = true;
            StartCoroutine("ReviveWait");
        }
        else
            StartCoroutine(LosePanelOn(1));
    }

    public void Continue()
    {
        PanelOnOff(contPanel, false);
        Debug.Log("aha");
        StopCoroutine("ReviveWait");
        HookMovement.move_Speed = tempSpeed;
        StartCoroutine("Countdown");
    }

    public void LosePanelOnIm()
    {
        if (once)
            StartCoroutine(LosePanelOn(0.0000000001f));
    }

    IEnumerator LosePanelOn(float t) {
        yield return new WaitForSeconds(t);
        PanelOnOff(contPanel, false);
        PanelOnOff(losePanel, true);
        StopAllCoroutines();
        once = true;
        StartCoroutine(Display(runningText1));


    }
    IEnumerator WinPanelOn() {
        yield return new WaitForSeconds(1f);
        win = true;
        curLevel++;
        if (curLevel > 5)
        {
            curWorld++;
            curLevel = 1;
        }
        ChangeProgresssData();
        BoosterSys.ResetData();
        PanelOnOff(winPanel, true);
        StartCoroutine(Display(runningText));
    }
    IEnumerator ReviveWait() {
        waitReviveTxt.text = i.ToString();
        yield return new WaitForSeconds(1f);
        i--;
        StartCoroutine("ReviveWait");
        if (i < 0)
        {
            StopCoroutine("ReviveWait");
            PanelOnOff(contPanel, false);
            LosePanelOnIm();
        }

    }
    IEnumerator X2Wait() {
        x2WaitTxt.text = i1.ToString();
        yield return new WaitForSeconds(1f);
        i1--;
        StartCoroutine("X2Wait");
        if (i1 < 1)
        {
            StopCoroutine(X2Wait());
            NoThank();
        }

    }

    void NoThank()
    {
        PanelOnOff(x2WaitTxt.gameObject, false);
        PanelOnOff(noThankTxt, true);
    }

    public void ReloadLvl()
    {
        HookScript.itemAttached = false;
        disable = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }   

    public void LoadHome()
    {
        HookScript.itemAttached = false;
        disable = false;
        SceneManager.LoadScene("Home");
    }

    public void LoadShop()
    {
        if (!win)
            scoreCount = LevelSystem.LoadProgressData().curGold;
        PanelOnOff(shopPanel, true);
    }

    IEnumerator Display(GameObject[] runningText)
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject ha in runningText)
        {
                ha.SetActive(true);
            yield return new WaitForSeconds(0.7f);  
        }
        if (win)
            StartCoroutine("X2Wait");

    }

    public void Pause()
    {
        Time.timeScale = 0;
        PanelOnOff(pausePanel, true);
    }

    public void Resume()
    {
        PanelOnOff(pausePanel, false);
        Time.timeScale = 1;

    }

    public void TxtOn(GameObject m, float t)
    {
        m.SetActive(true);
        StartCoroutine(TxtDown(m, t));
    }

    IEnumerator TxtDown(GameObject m, float t)
    {
        yield return new WaitForSeconds(t);
        m.SetActive(false);
    }



    // For Booster
    public void DisplayDyna(bool y, int dynaCount)
    {

            if (dynaCount >= 2)
            {
                dynaText.text = "x" + dynaCount;
                dynaText.gameObject.SetActive(true);
            }
            else
                dynaText.gameObject.SetActive(false);
    }



    // For Item and Trap
    public void DisplayScore(int scoreValue)
    {
            if (scoreText == null)
                return;
            scoreCount += scoreValue;
            Invoke(nameof(DisplayScoreText), 1);
    }

    private void DisplayScoreText()
    {
        scoreText.text = "" + scoreCount;

        if (Lightning)
            if (countdownTimer <= 30)
            {
                Invoke(nameof(LightningWarning), 2);
                Lightning = false;
            }
        if (Police)
            if (countdownTimer <= 38)
            {
                Invoke(nameof(PoliceWarning), 2);
                Police = false;
            }
    }

    public void LightningWarning()
    {
        lightningWarn.SetActive(true);
        Invoke(nameof(LightningOn), 2);
    }

    public void LightningOn()
    {
        lightningWarn.SetActive(false);
        lightning.SetActive(true);
    }
    public void PoliceWarning()
    {
        policeWarn.SetActive(true);
        Invoke(nameof(PoliceOn), 2);
    }

    public void PoliceOn()
    {
        policeWarn.SetActive(false);
        police.SetActive(true);
    }

    public void ChangeProgresssData()
    {
        ProgressData progressData = new ProgressData
        {
            curWorld = curWorld,
            curLevel = curLevel,
            curGold = scoreCount,
        };
        LevelSystem.SaveProgressData(progressData);
    }
    
    public void LoadProgresssData()
    {
        ProgressData progressData = LevelSystem.LoadProgressData();
        curWorld = progressData.curWorld;
        curLevel = progressData.curLevel;
        scoreCount = progressData.curGold;
        LevelSystem.SaveProgressData(progressData);
    }

}
































