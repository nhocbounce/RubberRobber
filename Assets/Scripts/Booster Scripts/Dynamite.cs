using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    private GameObject itemAttached;

    private float step;

    public float throwSpeed = 25f;

    private Vector3 endPos;

    private string currentItem;

    private ItemScript itemScript;

    private HookScript hookScript;

    //private float desiredDuration = 10f;
    //private float elapsedTime, percentageComplete;

    private bool lerpOK;
    public bool throwAble = true;
    // Start is called before the first frame update

    void Start()
    {
        itemAttached = GameObject.FindGameObjectWithTag("ItemAtt");
        hookScript = itemAttached.GetComponentInParent<HookScript>();
    }

    // Update is called once per frame
    void Update()
    {
        step = throwSpeed * Time.deltaTime;

        if (lerpOK)
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (itemAttached.transform.childCount == 0)
            return;
        else if (HookScript.fishNet)
        {
            foreach (Transform child in itemAttached.transform)
                Destroy(child.gameObject);
            Destroy(this.gameObject);
            HookMovement.move_Speed = HookMovement.initialSpeed;
            HookScript.fishNet = false;
            hookScript.owned = true;
        }
        else if (collision.tag == currentItem)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            HookMovement.move_Speed = HookMovement.initialSpeed;
        }

        
    }

    public void TriggerDynamite()
    {
        if (itemAttached.transform.childCount == 0)
            return;
        else if (!throwAble)
            return;
        else if (HookScript.fishNet)
        {
            BoosterManager.dynamiteNum--;
            HookMovement.move_Speed = 0f;
            foreach (Transform child in itemAttached.transform)
            {
                itemScript = child.GetComponent<ItemScript>();
                itemScript.beingCollected = false;
            }
            endPos = itemAttached.transform.position;
            lerpOK = true;
        }
        else
        {
            BoosterManager.dynamiteNum--;
            HookMovement.move_Speed = 0f;
            itemScript = itemAttached.GetComponentInChildren<ItemScript>();
            itemScript.beingCollected = false;
            currentItem = itemAttached.transform.GetChild(0).tag;
            endPos = itemAttached.transform.position;
            lerpOK = true;
        }
    }
}
