using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HomeManagement : MonoBehaviour
{
    [SerializeField]
    Sprite[] bgSpriteList;

    [SerializeField]
    RectTransform content;

    float transSpeed = 7500;

    [SerializeField]
    GameObject leftBtn, rightBtn, playBtn, levelPan, lockedBtn, replayBtn, btnPanel;

    [SerializeField]
    private TMP_Text curLevelTxt, curGoldTxt, curTargetTxt, requireTxt;

    Vector3 end;
    float step;
    int i, curTarget;
    float endPosX;

    ProgressData progress;


    private void Awake()
    {
        progress = LevelSystem.LoadProgressData();
    }

    void Start()
    {
        Debug.Log(SaveSystem.SAVE_FOLDER);
        i = progress.curWorld-1;
        RightChapter();
        content.localPosition = new Vector3(endPosX, content.localPosition.y, 0);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        curLevelTxt.text = $"Level {progress.curLevel}/5";
        curGoldTxt.text = progress.curGold.ToString();
        curTarget = LevelSystem.TargetLevel[(progress.curWorld - 1) * 5 + progress.curLevel];
        if (curTarget > 9999)
            curTargetTxt.rectTransform.position = new Vector2(720.5f, curTargetTxt.rectTransform.position.y);
        curTargetTxt.text = curTarget.ToString();
        requireTxt.text = $"Chapter {i-1} Completed Require";
        step = transSpeed * Time.deltaTime;
        content.localPosition = Vector3.MoveTowards(content.localPosition, end, step);
        if (content.localPosition == end)
            PanelOnOff(btnPanel, true);

        if (i > progress.curWorld)
        {
            PanelOnOff(lockedBtn, true);
            PanelOnOff(playBtn, false);
            PanelOnOff(replayBtn, false);
        }
        else if (i == progress.curWorld)
        {
            PanelOnOff(playBtn, true);
            PanelOnOff(replayBtn, false);
            PanelOnOff(lockedBtn, false);
        }
        else
        {
            PanelOnOff(replayBtn, true);
            PanelOnOff(playBtn, false);
            PanelOnOff(lockedBtn, false);
        }


        if (i >= (bgSpriteList.Length))
            PanelOnOff(rightBtn, false);
        else
            PanelOnOff(rightBtn, true);


        if (i <= 1)
            PanelOnOff(leftBtn, false);
        else
            PanelOnOff(leftBtn, true);
                
    }

    void PanelOnOff(GameObject m, bool l)
    {
        if (m)
            m.SetActive(l);
    }

    public void RightChapter()
    {
        PanelOnOff(btnPanel, false);
        i++;
        endPosX = -1080 * (i-1);
        end = new Vector3(endPosX, content.localPosition.y, 0);
        float step = transSpeed * Time.deltaTime;
        GetComponent<Image>().sprite = bgSpriteList[i-1];

    } 
    public void LeftChapter()
    {
        PanelOnOff(btnPanel, false);
        i--;
        endPosX = -1080 * (i-1);
        end = new Vector3(endPosX, content.localPosition.y, 0);


        GetComponent<Image>().sprite = bgSpriteList[i-1];
    }

    public void PlayGame()
    {
        ProgressData progress = LevelSystem.LoadProgressData();
        int index = (progress.curWorld - 1) * 5 + progress.curLevel + 2;
        SceneManager.LoadSceneAsync(index);
    }

    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void LoadLevelPan()
    {
        PanelOnOff(levelPan, true);
    }

    public void CallResetData()
    {
        LevelSystem.ResetData();
    }
}
