using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPig : MonoBehaviour
{
    public float maxX, minX, pigSpeed;
    private bool changeDir = false, dirChanged = false;
    public bool notCaptured = true;
    private float angle, offset = 1;
    private HookMovement hookMovement;

    // Start is called before the first frame update
    void Start()
    {
        hookMovement = FindObjectOfType<HookMovement>();
        angle = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (notCaptured)
        {
            if ((transform.position.x >= maxX) || (transform.position.x <= minX))
            {
                if (!dirChanged)
                {
                    changeDir = true;
                }
            }
            
            if ((transform.position.x < (maxX -offset)) && (transform.position.x > (minX +offset)))
            {
                dirChanged = false;
            }

            if (changeDir)
            {
                angle += 180;
                transform.eulerAngles = new Vector3(0, angle, 0);
                changeDir = false;
                dirChanged = true;
            } 


            transform.position += transform.right * Time.deltaTime * pigSpeed;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hookMovement.moveDown && (collision.tag == Tags.READY || collision.tag == Tags.OWNED))
            if (HookScript.fishNet)
                notCaptured = false;
    }
}
