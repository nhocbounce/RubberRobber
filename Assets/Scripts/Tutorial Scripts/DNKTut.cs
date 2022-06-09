using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNKTut : MonoBehaviour
{

    [SerializeField]
    GameObject tut;

    GameplayManager gameplayManager;

    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
    }

    public void On()
    {
        gameplayManager.Pause();
        if (tut)
            tut.SetActive(true);
    }

    public void Off()
    {
        gameplayManager.Resume();
        if (tut)
            tut.SetActive(false);
    }
}
