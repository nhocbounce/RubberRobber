using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    [SerializeField]
    private Vector3 endPos;
    [SerializeField]
    private Transform[] bound = new Transform[2];

    public float runSpeed = 10;
    public bool checking = false;

    [SerializeField]
    private bool PoLi;

    // Start is called before the first frame update


    private void Awake()
    {

    }

    private void Start()
    {

    }

    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, runSpeed * Time.deltaTime);
        if (PoLi)
        {
            if (transform.position.x > bound[1].position.x)
            {
                checking = false;
                return;
            }

            if (transform.position.x > bound[0].position.x)
                checking = true;
        }
        else
        {
            if (transform.position.x < bound[0].position.x)
            {
                checking = false;
                return;
            }

            if (transform.position.x < bound[1].position.x)
                checking = true;
        }
 

    }
}
