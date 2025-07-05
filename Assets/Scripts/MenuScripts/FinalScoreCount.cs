using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreCount : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private uint maxTime;
    private uint[] score;
    private bool hasTriggerd = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggerd)
        {
            hasTriggerd = true;
            canvas.transform.GetChild(0).GetComponent<InventoryManager>().StopTimer();
            score = canvas.GetComponentInChildren<InventoryManager>().getScore();
            canvas.transform.GetChild(2).gameObject.SetActive(true);
            canvas.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "—чЄт: " + score[0].ToString();
            canvas.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text = "”рон: " + score[1].ToString();
            canvas.transform.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = "¬рем€: " + TimeTransform(score[2]);
            canvas.transform.GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "»тог: " + FinalScore(score).ToString();
        }
    }

    private string TimeTransform(uint seconds)
    {
        return (seconds/60).ToString() + ":" + (seconds%60).ToString();
    }

    private uint FinalScore(uint[] score)
    {
        if ((int)score[0] - (int)(25 * score[1]) + (int)((float)maxTime / (float)score[2] * 5) < 0)
        {
            return 0;
        }
        else
        {
            return score[0] - (25 * score[1]) + (uint)((float)maxTime / (float)score[2] * 5);
        }
    }
}
