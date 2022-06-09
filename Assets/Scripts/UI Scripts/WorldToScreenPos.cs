using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreenPos : MonoBehaviour
{
    [SerializeField]
    private GameObject thePoint;

    private void Awake()
    {
        transform.position = Camera.main.ScreenToWorldPoint(thePoint.transform.position);

    }

    void Update()
    {
        
    }
}
