using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(ButtonManager))]
[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(SoundManager))]
[RequireComponent (typeof(SoundList))]
public class Managers : MonoBehaviour
{
    public static InventoryManager InventoryManager {  get; set; }
    public static ButtonManager ButtonManager { get; set; }
    public static ScoreManager ScoreManager { get; set; }
    public static SoundManager SoundManager { get; set; }
    public static SoundList SoundList { get; set; }
    private void Awake()
    {
        InventoryManager = GetComponent<InventoryManager>();
        ButtonManager = GetComponent<ButtonManager>();
        ScoreManager = GetComponent<ScoreManager>();
        SoundManager = GetComponent<SoundManager>();
        SoundList = GetComponent<SoundList>();
    }
}
