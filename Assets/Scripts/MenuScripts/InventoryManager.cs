using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private uint itemScore = 0;
    private uint hpLost = 0;
    private uint time = 0;
    public static InventoryManager Instance { get; private set; }
    private TextMeshProUGUI score;
    private Canvas canvas;

    private void Start()
    {
        canvas = transform.parent.GetComponent<Canvas>();
        score = canvas.transform.GetChild(1).transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        Instance = this;
        InvokeRepeating("Timer", 0f, 1f);
    }

    private void Timer()
    {
        time++;
        if (time <= 999)
        {
            string timeS = time.ToString();
            for (int i = timeS.Length; i < 3; i++)
            {
                timeS = timeS.Insert(0, "0");
            }
            TextMeshProUGUI timeGUI = canvas.transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            timeGUI.text = "Âðåìÿ: " + timeS;
        }
    }
    public void StopTimer()
    {
        CancelInvoke("Timer");
    }
    public void AddItem(int price=1)
    {
        itemScore=(uint)((int)itemScore+price);
        score.text = "Ñ÷¸ò: " + itemScore;
    }

    public void AddHpLost()
    {
        hpLost++;
    }

    public uint[] getScore()
    {
        uint[] result = new uint[3];
        result[0] = itemScore;
        result[1] = hpLost;
        result[2] = time;

        return result;
    }
}
