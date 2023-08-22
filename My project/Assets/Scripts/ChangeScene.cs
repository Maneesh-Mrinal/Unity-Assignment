using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Gototask2()
    {
        SceneManager.LoadSceneAsync("Task 2 Scene");
    }
    public void Gototask1()
    {
        SceneManager.LoadSceneAsync("Task 1 Scene");
    }
}
