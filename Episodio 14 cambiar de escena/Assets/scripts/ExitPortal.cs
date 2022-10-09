using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    GameObject levelTransition;
    LevelTransition levelTransitionScript;

    void Start()
    {
        levelTransition = GameObject.Find("LevelTransition");
        levelTransitionScript = levelTransition.GetComponent<LevelTransition>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        levelTransitionScript.LoadNextLevel();
    }
}
