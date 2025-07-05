using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private TextMeshProUGUI Score;
    private int DestroyedCount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, distance);
            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Destroy"))
                {
                    DestroyedCount++;
                    Score.text = "”ничт: " + DestroyedCount;
                    Destroy(hit.collider.gameObject);
                    break;
                }
            }
        }
    }
}
