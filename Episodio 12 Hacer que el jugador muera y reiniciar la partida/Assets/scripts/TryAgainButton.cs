using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{
  public void LoadSameScene()
  {
      Scene scene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(scene.name);
  }
}
