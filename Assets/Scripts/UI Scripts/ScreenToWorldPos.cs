using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToWorldPos : MonoBehaviour
{
    public Transform dynaThrow;
    [SerializeField]
    private Vector3 offset;

    public bool testOffset;

    private void OnEnable()
    {
        transform.position = Camera.main.WorldToScreenPoint(dynaThrow.position) + offset;
    }

    void Update()
    {
        if(testOffset) 
         transform.position = Camera.main.WorldToScreenPoint(dynaThrow.position) + offset;
    }
}
