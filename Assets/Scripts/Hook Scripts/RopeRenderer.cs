using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditorInternal;
#endif

public class RopeRenderer : MonoBehaviour {

    public LineRenderer lineRenderer;

    private PipeTeleport pipeTeleport;

    [SerializeField]
    public GameObject startPosition;

    private Vector3 initialPos, lineStart;

    public float check;

    private float line_Width = 0.05f;

    private GameObject[] linePoint = new GameObject[100];
    private int countP = 0;
    public GameObject pointPrefabs, player;





    void Awake() {

        lineRenderer = GetComponent<LineRenderer>();

        pipeTeleport = FindObjectOfType<PipeTeleport>();

        lineRenderer.startWidth = line_Width;
        lineRenderer.endWidth = line_Width;

        lineRenderer.enabled = false;

        initialPos = transform.position;
        lineStart = startPosition.transform.position;


    }

    void Start() {
        
    }

    private void Update()
    {
        lineRenderer.startWidth = line_Width;
        lineRenderer.endWidth = line_Width;
    }

    public void addLine()
    {
        linePoint[countP] = Instantiate<GameObject>(pointPrefabs);
        linePoint[countP].transform.SetParent(player.transform);
        linePoint[countP].transform.position = transform.position;
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(countP + 1, linePoint[countP].transform.position - Vector3.forward * check);
        countP++;
    }

    public void addTele()
    {
        countP = 0;
        startPosition.transform.position = transform.position;
        //lineRenderer.SetPosition(0, transform.position);
    }

    public void removeTele()
    {
        pipeTeleport.CopyComp(this.gameObject);

        startPosition.transform.position = GetComponent<LineRenderer>().GetPosition(0);

        countP = GetComponent<LineRenderer>().positionCount - 2;
    }

    public void removeLine()
    {
        countP--;
        lineRenderer.positionCount--;
    }

    public void RenderLine(Vector3 endPosition, bool enableRenderer) { 
    
        if(enableRenderer) {

            if (!lineRenderer.enabled) { lineRenderer.enabled = true; }

            lineRenderer.positionCount = countP + 2;


        } 
        else {
            transform.position = initialPos;
            startPosition.transform.position = lineStart;
            lineRenderer.positionCount = 0;
            countP = 0;
            GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
            foreach (GameObject p in points)
                GameObject.Destroy(p);
            GameObject[] telepoint = GameObject.FindGameObjectsWithTag("TelePoint");
            foreach (GameObject p in telepoint)
                GameObject.Destroy(p);
            if (lineRenderer.enabled) { lineRenderer.enabled = false; }
        }

        if(lineRenderer.enabled) {

            lineRenderer.SetPosition(0, startPosition.transform.position - Vector3.forward * check);
            lineRenderer.SetPosition(lineRenderer.positionCount -1, endPosition - Vector3.forward * check);
        }

    } // draw line
} // class

































