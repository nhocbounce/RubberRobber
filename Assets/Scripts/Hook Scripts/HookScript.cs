using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour {

    [SerializeField]
    private Transform itemHolder;

    public static bool itemAttached, isBomb = false, fishNet;
    public int fishNetGold;
    public bool owned;

    private Vector3 initalScale;

    private HookMovement hookMovement;

    private SoundManager soundManager;

    private GameplayManager gameplayManager;

    private BoosterManager boosterManager;

    private Transform[] objChild = new Transform[20];

    private PlayerAnimation playerAnim;

    private int curChild;



    void Awake() {
        hookMovement = GetComponentInParent<HookMovement>();
        playerAnim = GetComponentInParent<PlayerAnimation>();
        soundManager = FindObjectOfType<SoundManager>();
        gameplayManager = FindObjectOfType<GameplayManager>();
        boosterManager = FindObjectOfType<BoosterManager>();
        initalScale = transform.localScale;
        fishNet = false;
        fishNetGold = 0;
    }

    private void Update()
    {
        if (boosterManager.fishNet == true)
        {
            fishNet = true;
            boosterManager.fishNet = false;
        }
        if (HookMovement.canRotate)
        {
            if (itemAttached)
            {
                if (!fishNet)
                    itemAttached = false;

                playerAnim.IdleAnimation();
                soundManager.PullSound(false);
                
                if (itemHolder.childCount == 0)
                    return;
                else
                {
                    curChild = itemHolder.childCount;
                    for (int i = 0; i < curChild; i++)
                    {
                        objChild[i] = itemHolder.GetChild(0);
                        objChild[i].parent = null;
                        objChild[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (owned)
            return;
        else
        {
            if (target.tag == Tags.SMALL_GOLD || target.tag == Tags.MIDDLE_GOLD ||
                target.tag == Tags.LARGE_GOLD || target.tag == Tags.LARGE_STONE ||target.tag == Tags.DIAMOND ||
                target.tag == Tags.MIDDLE_STONE || target.tag == Tags.EXPLODE || 
                target.tag == Tags.DYNAMITEPICK || target.tag == Tags.THAIROPE || 
                target.tag == Tags.TIMER || target.tag == Tags.SUPERMAN ||
                target.tag == Tags.DNK || target.tag == Tags.FISHNET ||
                target.tag == Tags.MWALL || target.tag == Tags.SOCKET || target.tag == Tags.CEILING_FAN ||
                target.tag == Tags.PIG || target.tag == Tags.PIGDIA)
            {
                if (target.tag == Tags.SOCKET)
                {
                    gameplayManager.DieGame();
                    return;
                }
                if (target.tag == Tags.CEILING_FAN)
                {
                    hookMovement.moveDown = false;
                    return;
                }
                target.GetComponent<ItemScript>().beingCollected = true;
                if (fishNet)
                {
                    if (target.tag == Tags.EXPLODE)
                        return;
                }
                transform.localScale = initalScale;
                if (target.tag == Tags.EXPLODE)
                    isBomb = true;
                else
                    isBomb = false;

                if (target.tag == Tags.PIG || target.tag == Tags.PIGDIA)
                {
                    target.GetComponent<MovingPig>().notCaptured = false;
                }



                itemAttached = true;

                if (!fishNet)
                {
                    owned = true;
                    fishNetGold = 0;
                }
                else
                {
                    transform.localScale *= 2;
                }

                target.transform.parent = itemHolder;
                target.transform.position = itemHolder.position;
                if (fishNet)
                {
                    fishNetGold += target.GetComponent<ItemScript>().scoreValue;
                    target.GetComponent<ItemScript>().once = true;
                    HookMovement.move_Speed -= target.GetComponent<ItemScript>().hook_Speed;
                }
                else
                {
                    HookMovement.move_Speed = (HookMovement.playerStrength - target.GetComponent<ItemScript>().hook_Speed) / 2;
                }


                if (HookMovement.move_Speed < 0.5)
                    HookMovement.move_Speed = 0.5f;

                hookMovement.HookAttachedItem();

                

                // animate player
                playerAnim.PullingItemAnimation();

                if (target.tag == Tags.SMALL_GOLD || target.tag == Tags.MIDDLE_GOLD ||
                    target.tag == Tags.LARGE_GOLD)
                {
                    
                    soundManager.HookGrab_Gold();

                }
                else if (target.tag == Tags.MIDDLE_STONE || target.tag == Tags.LARGE_STONE)
                {

                    soundManager.HookGrab_Stone();

                }
                soundManager.PullSound(true);

                //if (target.tag == Tags.PIG)
                //    MovingPig.notCaptured = false;
                //this.tag = Tags.OWNED;
            } // if target is an item
            
        }
            // deliver item

    }

    public void Delivered()
    {
        owned = false;
        this.tag = Tags.READY;
        if ((fishNet) && (fishNetGold != 0))
        {
            fishNet = false;
        }
        transform.localScale = initalScale;
    }

    public void SuperManAnim()
    {
        playerAnim.CheerAnimation();
        itemAttached = false;
        TouchArea.tapable = false;
        Invoke(nameof(Chear), 0.5f);
    }

    void Chear()
    {
        itemAttached = true;
        TouchArea.tapable = true;
    }
    // on trigger enter


} // class
































