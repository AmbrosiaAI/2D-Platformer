using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    private Animator animator;
    private BoxCollider2D box;

    public int getHp()
    {
        return hp;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                if (contactPoint.normal.y < -0.70)
                {
                    getDamage();
                    Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();
                    Vector2 currentVelocity = body.velocity;
                    body.velocity = Vector2.zero;
                    body.AddForce(new Vector2(0, 4.5f), ForceMode2D.Impulse);
                }
            }
        } else if (collision.gameObject.CompareTag("Player"))
        {
            getDamage(15);
        }
    }

    private void getDamage(int damage = 1)
    {
        hp -= damage;
        animator.SetInteger("HP", hp);
        animator.SetTrigger("Hit");
        if (hp <= 0) StartCoroutine(death());
    }

    private IEnumerator death()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 4), ForceMode2D.Impulse);
        box.enabled = false;
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
