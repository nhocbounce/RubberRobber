using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenToScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] target;

    public static bool world;
    int i = 0;
    void Start()
    {
        
    }

    private void OnEnable()
    {

    }

    public void ChangeFocus()
    {
        if (i >= target.Length - 2)
            i = target.Length -1;
        else
            i++;
    }

    // Update is called once per frame
    void Update()
    {
        if (world)
            transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("hole-01");
        else
            transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Focus");
        transform.position = target[i].transform.position;
    }
}
