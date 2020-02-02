using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] GameObject credits;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCredits()
    {
        GetComponent<Animator>().SetBool("openCredits", !credits.activeInHierarchy);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
