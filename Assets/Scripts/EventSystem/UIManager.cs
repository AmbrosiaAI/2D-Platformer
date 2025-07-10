using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button menuButton;

    private void Start()
    {
        if (menuButton == null)
        {
            Debug.Log("Кнопка не привязана");
        }
        else
        {
            menuButton.onClick.AddListener(OpenMenu);
        }
    }

    private void OpenMenu()
    {
        Messenger.Broadcast(EventList.Button_Click);
        SceneManager.LoadScene(0);
    }
}
