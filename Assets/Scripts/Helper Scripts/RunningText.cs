using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunningText : MonoBehaviour
{
    public float point;

    private float displayScore = 0;


    public TMP_Text thisText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        StartCoroutine(nameof(ScoreUpdater));
    }

    // Update is called once per frame
    void Update()
    {


    }
    private IEnumerator ScoreUpdater()
    {
        while (displayScore < point)
        {
            displayScore += (Time.deltaTime * 2*point); // or whatever to get the speed you like
            displayScore = Mathf.Clamp(displayScore, 0f, point);
            thisText.text = displayScore.ToString("F0");
            yield return null;
        }
    }

    private void OnDisable()
    {
        displayScore = 0;
    }
}
