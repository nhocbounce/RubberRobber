using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchArea : MonoBehaviour
{

    static public bool noInput, tapable = true;
    // Start is called before the first frame update
    private void Start()
    {
        noInput = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (tapable)
            GetComponent<Image>().raycastTarget = true;
        else
            GetComponent<Image>().raycastTarget = false;
    }

    private void OnMouseDown()
    {
        noInput = false;
    }

    public void OnInput()
    {
        Time.timeScale = 1;
        noInput = false;
    }
}
