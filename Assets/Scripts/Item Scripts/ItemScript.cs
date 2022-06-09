using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemScript : MonoBehaviour {

    public float hook_Speed;

    public bool beingCollected = false;

    public int scoreValue;

    public bool once;

    private BoosterManager boosterManager;
    private GameplayManager gameplayManager;
    private HookScript hookScript;

    // For UI

    private GameObject goldAddText;

    private void Awake()
    {
        beingCollected = false;
        boosterManager = FindObjectOfType<BoosterManager>();
        gameplayManager = FindObjectOfType<GameplayManager>();
        hookScript = FindObjectOfType<HookScript>();
        goldAddText = GameObject.Find("GoldAddText");
    }

    private void Start()
    {
        if (boosterManager.diamondUP)
            if (this.tag == Tags.DIAMOND || this.tag == Tags.PIGDIA)
                scoreValue += 300;
        if (boosterManager.stoneUP)
            if (this.tag == Tags.LARGE_STONE || this.tag == Tags.MIDDLE_STONE)
                scoreValue *= 3;
    }

    private void Update()
    {

    }
    void OnDisable() {
        if ((beingCollected) && (gameplayManager != null) && (gameplayManager.disable))
        {

            if (scoreValue > 0)
            {
                if (!once)
                    goldAddText.GetComponent<TMP_Text>().text = "+" + scoreValue.ToString();
                else
                {
                    goldAddText.GetComponent<TMP_Text>().text = "+" + hookScript.fishNetGold.ToString();
                }

                gameplayManager.TxtOn(goldAddText, 1);
                gameplayManager.DisplayScore(scoreValue);
            }

            switch (this.tag)
            {
                case (Tags.SUPERMAN):
                    boosterManager.SuperManOn();
                    break;
                case (Tags.THAIROPE):
                    boosterManager.ThaiRope();
                    break;
                case (Tags.TIMER):
                    boosterManager.TimeAdd();
                    break;
                case (Tags.DYNAMITEPICK):
                    BoosterManager.dynamiteNum++;
                    break;
                case (Tags.FISHNET):
                    boosterManager.FishNetEnable();
                    break;
                case (Tags.DNK):
                    boosterManager.DoNgheoKhiEnable();
                    break;
                case (Tags.MWALL):
                    boosterManager.MagneticWallEnable();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HookScript.isBomb)
        {
            if (this.tag == Tags.EXPLODE)
            {
                return;
            }
            else if (collision.CompareTag(Tags.EXPLODE))
            {
                this.gameObject.SetActive(false);
            }
        }
    }


}
