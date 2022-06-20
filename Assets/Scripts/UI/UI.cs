using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
