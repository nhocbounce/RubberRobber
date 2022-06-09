
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTeleport : MonoBehaviour
{

    public Vector3 teleportDis;

    public Vector3 offset;

    private GameObject[] telePoint = new GameObject[100];

    private LineRenderer lineRenderer;

    private RopeRenderer ropeRenderer;
    private int countP = 0;
    public GameObject pointPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        ropeRenderer = FindObjectOfType<RopeRenderer>();
        teleportDis = transform.position - transform.GetChild(0).transform.position;
        offset = new Vector3(0f, -0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        teleportDis = transform.position - transform.GetChild(0).transform.position;
    }

    public void AddTelePoint(LineRenderer lineRen)
    {
        telePoint[countP] = Instantiate<GameObject>(pointPrefabs);
        CopyComponent(lineRen, telePoint[countP]);
        telePoint[countP].tag = "TelePoint";
        Vector3[] positions = new Vector3[lineRen.positionCount];
        lineRen.GetPositions(positions);
        telePoint[countP].GetComponent<LineRenderer>().SetPositions(positions);
        countP++;
    }


    public void RemoveTelePoint()
    {
        countP--;
        Destroy(telePoint[countP]);
    }

    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        var dst = destination.GetComponent(type) as T;
        if (!dst) dst = destination.AddComponent(type) as T;
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            if (field.IsStatic) continue;
            field.SetValue(dst, field.GetValue(original));
        }
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
            prop.SetValue(dst, prop.GetValue(original, null), null);
        }
        return dst as T;
    }


    public void CopyComp(GameObject game)
    {
        int i = countP - 1;
        lineRenderer = telePoint[i].GetComponent<LineRenderer>();
        CopyComponent(lineRenderer, game);
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        game.GetComponent<LineRenderer>().SetPositions(positions);
    }

}
