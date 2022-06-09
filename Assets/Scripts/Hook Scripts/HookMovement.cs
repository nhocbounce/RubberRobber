using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookMovement : MonoBehaviour {

    // rotation Z
    public float min_Z, max_Z;
    public float rotate_Speed;

    public float rotate_Angle;
    public bool rotate_Right;
    public static bool canRotate;

    public bool tutorialWall;

    private bool onceCollide =true;

    private float initialAngle;

    public static float move_Speed;
    public static float playerStrength;

    public static float initialSpeed;
    public float initialRotSpeed;


    public float min_Y = -2.9f;
    private float initial_Y;
    private bool nonCollide = false, nonCollide1 = false, nonCollide2 = false, ined;

    public bool moveDown;
    public bool skip = false;

    // FOR LINE RENDERER
    private RopeRenderer ropeRenderer;

    private HookScript hookScript;

    private BoosterManager boosterManager;

    private GameplayManager gameplayManager;

    private SoundManager soundManager;


    private Transform circleCollider;

    private PipeTeleport pipeTeleport;

    private LineRenderer lineRenderer;

    private Dynamite[] check;

    private Vector3 initialPos;




    void Awake() {
        ropeRenderer = GetComponent<RopeRenderer>();
        hookScript = GetComponentInChildren<HookScript>();
        boosterManager = FindObjectOfType<BoosterManager>();
        pipeTeleport = FindObjectOfType<PipeTeleport>();
        lineRenderer = GetComponent<LineRenderer>();
        gameplayManager = FindObjectOfType<GameplayManager>();
        soundManager = FindObjectOfType<SoundManager>();
        circleCollider = transform.GetChild(0);
        playerStrength = 3f;
        initialPos = transform.position;
        initialSpeed = 3f;
        initialRotSpeed = 30f;
    }

    void Start() {

        rotate_Angle = 0f;
        transform.rotation = Quaternion.AngleAxis(rotate_Angle, Vector3.forward);
        initial_Y = transform.position.y;
        canRotate = true;
            
    }

    void Update() 
    {
        this.tag = "Untagged";
        Rotate();
        if (!TouchArea.noInput)
            GetInput();
        MoveRope();
    }


    public void ResetAngle()
    {
        rotate_Angle = Random.Range(-80, 80);
        rotate_Right = (Random.value > 0.5f);
    }

    void Rotate() {

        if (!canRotate)
            return;
        onceCollide = true;
        if(rotate_Right) {

            rotate_Angle += rotate_Speed * Time.deltaTime;

        } else {

            rotate_Angle -= rotate_Speed * Time.deltaTime;
        }

        if ((rotate_Angle < min_Z) && (skip) && (!rotate_Right))
        {
            
                rotate_Angle -= 2 * min_Z;
                skip = false;
            
        }
        else if ((rotate_Angle > -min_Z) && (skip) && (rotate_Right))
        {
            
                rotate_Angle += 2 * min_Z;
                skip = false;
           

        }

        //Debug.Log(rotate_Angle);

        transform.rotation = Quaternion.AngleAxis(rotate_Angle, Vector3.forward);

        

        if (rotate_Angle >= max_Z) {

            rotate_Right = false;
            skip = true;

        } else if(rotate_Angle <= -max_Z) {

            rotate_Right = true;
           skip = true;

        }

    } // can rotate

    public void GetInput() {

            if(canRotate) {
                canRotate = false;
                moveDown = true;
                initialAngle = rotate_Angle;

                soundManager.RopeStretch(true);

            //if (moveDown)
            //{
            //    rotate_Angle *= -1;
            //    transform.rotation = Quaternion.AngleAxis(rotate_Angle, Vector3.forward);
            //}

        }

    } // get input

    void MoveRope() {

        if (canRotate)
        {
            move_Speed = initialSpeed;
            //move_Speed = initial_Move_Speed;
            return;
        }

        if(!canRotate) {
            Vector3 temp = transform.position;

            if(moveDown) {

                temp -= transform.up * Time.deltaTime * move_Speed;

            } else {

                temp += transform.up * Time.deltaTime * move_Speed * 2;

            }

            transform.position = temp;

            if(temp.y <= min_Y) {
                moveDown = false;
            }

            if(temp.y > initial_Y) {
                canRotate = true;
                TouchArea.noInput = true;

                // deactivate line renderer
                ropeRenderer.RenderLine(temp, false);
                ResetAngle();

                

                //reset move speed
                if (boosterManager.DNK && gameplayManager.countdownTimer <=0)
                    boosterManager.DNK = false;

                soundManager.RopeStretch(false);

                hookScript.Delivered();
            }

            ropeRenderer.RenderLine(transform.position, true);

        } // cannot rotate


    } // move rope

    public void HookAttachedItem() {
        moveDown = false;
    }

    public void CollideWithBoundary()
    {
        rotate_Angle *= -1;
        transform.rotation = Quaternion.AngleAxis(rotate_Angle, Vector3.forward);
        if (boosterManager.magneticWall && moveDown)
            circleCollider.localScale *= 1.5f;

    }
    public void CollideWithBoundary2()
    {
        rotate_Angle = 180f - rotate_Angle;
        transform.rotation = Quaternion.AngleAxis(rotate_Angle, Vector3.forward);
        if (boosterManager.magneticWall && moveDown)
            circleCollider.localScale *= 1.5f;
    }

    public void Conti()
    {
        Time.timeScale = 1;
    }

    public void CollideWithTeleportIn()
    {
        if (moveDown)
        {
            ined = true;
            pipeTeleport.AddTelePoint(lineRenderer);
            if (Mathf.Abs(rotate_Angle) < 90)
                transform.position -= (pipeTeleport.teleportDis - pipeTeleport.offset);
            else
            {
                transform.position -= (pipeTeleport.teleportDis + pipeTeleport.offset);
            }
            ropeRenderer.addTele();
        }
        else
        {
            if (!ined)
                return;
            if (Mathf.Abs(rotate_Angle) < 90)
                transform.position -= (pipeTeleport.teleportDis + pipeTeleport.offset);
            else
            {
                transform.position -= (pipeTeleport.teleportDis - pipeTeleport.offset);
            }
            ropeRenderer.removeTele();
            pipeTeleport.RemoveTelePoint();
            ined = false;
        }
    }
    public void CollideWithTeleportOut()
    {
        if (moveDown)
        {
            ined = true;
            pipeTeleport.AddTelePoint(lineRenderer);
            if (Mathf.Abs(rotate_Angle) < 90)
                transform.position += (pipeTeleport.teleportDis - pipeTeleport.offset);
            else
            {
                transform.position += (pipeTeleport.teleportDis + pipeTeleport.offset);
            }
            ropeRenderer.addTele();
        }
        else
        {
            if (!ined)
                return;
            if (Mathf.Abs(rotate_Angle) < 90)
                transform.position += (pipeTeleport.teleportDis - pipeTeleport.offset);
            else
            {
                transform.position += (pipeTeleport.teleportDis + pipeTeleport.offset);
            }
            ropeRenderer.removeTele();
            pipeTeleport.RemoveTelePoint();
            ined = false;

        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.BOUNDARY)
        {
            tutorialWall = true;
            //if (nonCollide1)
            //    return;
            //nonCollide1 = true;
            //Invoke(nameof(EnableCollide1), 0.05f);
            if (moveDown)
            {
                if (!onceCollide)
                {
                    PullNothing();
                }
                else
                {
                    ropeRenderer.addLine();
                    CollideWithBoundary();
                    onceCollide = false;
                }
            }
            else
            {
                ropeRenderer.removeLine();
                CollideWithBoundary();
            }
        }
        else if (collision.tag == Tags.BOUNDARY2)
        {
            tutorialWall = true;
            if (moveDown)
            {
                if (!onceCollide)
                {
                    PullNothing();
                }
                else
                {
                    ropeRenderer.addLine();
                    CollideWithBoundary2();
                    onceCollide = false;
                }
            }
            else
            {
                ropeRenderer.removeLine();
                CollideWithBoundary2();
            }
        }
    

        
        if (collision.tag == Tags.WALL2)
        {
            if (nonCollide1)
                return;
            nonCollide2 = true;
            Invoke(nameof(EnableCollide2), 0.1f);
            if (moveDown)
                ropeRenderer.addLine();
            else
                ropeRenderer.removeLine();
            CollideWithBoundary();
        }
        else if (collision.tag == Tags.WALL1)
        {
            if (nonCollide2)
                return;
            nonCollide1 = true;
            Invoke(nameof(EnableCollide1), 0.1f);
            if (moveDown)
                ropeRenderer.addLine();
            else
                ropeRenderer.removeLine();
            CollideWithBoundary2();
        }
        if (nonCollide)
            return;
        nonCollide = true;
        Invoke(nameof(EnableCollide), 0.05f);
        if (collision.tag == Tags.PIPE_IN)
        {
            CollideWithTeleportIn();
        }
        else if (collision.tag == Tags.PIPE_OUT)
            CollideWithTeleportOut();
    }

    private void PullNothing()
    {
        //hookScript.gameObject.tag = Tags.OWNED;
        hookScript.owned = true;
        moveDown = false;
    }

    private void EnableCollide()
    {
        nonCollide = false;
    }
    
    private void EnableCollide1()
    {
        nonCollide1 = false;
    }
    private void EnableCollide2()
    {
        nonCollide2 = false;
    }
 }




































