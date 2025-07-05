using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private int price=1;
    [SerializeField] private bool isHeal =false;
    private Animator animator;
    bool isCollected =false;
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isCollected)
            {
                if (isHeal)
                {
                    int[] playerHP = collision.GetComponent<PlayerHP>().getHP();
                    if (playerHP[0] < playerHP[1]) collision.GetComponent<PlayerHP>().getHeal();
                    else InventoryManager.Instance.AddItem(price);
                }
                else
                {
                    InventoryManager.Instance.AddItem(price);
                }
                isCollected = true;
                animator.SetTrigger("Collected");
                yield return new WaitForSeconds(0.3f);
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
