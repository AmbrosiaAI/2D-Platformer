using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private AudioClip ButtonSound;
    [SerializeField] private AudioSource AudioSource;

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

    private void PlayButtonSound() => AudioSource.PlayOneShot(ButtonSound);
    private void StartGame()
    {
        PlayButtonSound();
        SceneManager.LoadScene(1);
    }

    private void ExitGame()
    {
        PlayButtonSound();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
