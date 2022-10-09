using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    Animator animator;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        StartCoroutine(LoadLevel(currentSceneIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }
}
