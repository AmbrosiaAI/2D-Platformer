using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        if (startButton == null || exitButton == null)
        {
            Debug.Log("Кнопки не привязаны");
        }
        else
        {
            exitButton.onClick.AddListener(ExitGame);
            startButton.onClick.AddListener(StartGame);
        }
    }
    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
