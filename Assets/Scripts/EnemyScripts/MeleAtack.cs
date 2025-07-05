using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAtack : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] bool up = false;
    [SerializeField] bool down = false;
    [SerializeField] bool left = false;
    [SerializeField] bool right = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHP player = collision.gameObject.GetComponent<PlayerHP>();
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                if (contactPoint.normal.y < -0.70 && up) StartCoroutine(player.getDamage(damage));
                if (contactPoint.normal.y > 0.70 && down) StartCoroutine(player.getDamage(damage));
                if (contactPoint.normal.x < -0.70 && left) StartCoroutine(player.getDamage(damage));
                if (contactPoint.normal.x > 0.70 && right) StartCoroutine(player.getDamage(damage));
            }
        }
    }
}
