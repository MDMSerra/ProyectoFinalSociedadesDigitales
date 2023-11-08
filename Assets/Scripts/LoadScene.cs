using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string scene;
    public int sceneNumber = 0;

    public void LoadSceneNumber()
    {
        SceneManager.LoadScene(sceneNumber);

    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(scene);
    }
}
