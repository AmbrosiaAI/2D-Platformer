using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Canvas canvas;
    void Start()
    {
        canvas = transform.parent.GetComponent<Canvas>();
        canvas.transform.GetChild(2).GetChild(5).GetComponent<Button>().onClick.AddListener(NextLevel);
        canvas.transform.GetChild(2).GetChild(6).GetComponent<Button>().onClick.AddListener(RestartLevel);
        canvas.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(ExitButton);
        canvas.transform.GetChild(1).GetChild(4).GetComponent<Button>().onClick.AddListener(RestartLevel);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
