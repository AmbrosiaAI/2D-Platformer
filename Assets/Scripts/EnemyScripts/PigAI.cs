using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PigAI : MonoBehaviour
{
    [SerializeField] private float walkRadius;
    [SerializeField] private float walkRadiusCenter = float.NaN;
    [SerializeField] private float speed = 150f;
    private Animator animator;
    private Rigidbody2D body;
    private bool isWalk;
    private bool isAngry = false;

    private void Start()
    {
        speed *= Time.fixedDeltaTime;
        if (float.IsNaN(walkRadiusCenter)) walkRadiusCenter = transform.position.x;
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(Walk());
    }
    private void Update()
    {
        Collider2D[] hit = null;
        if (!isAngry)
            hit = Physics2D.OverlapCircleAll(transform.position, 5);

        if (!isWalk && !isAngry) 
        {
            StartCoroutine(Walk());
        }
        if (hit != null)
            foreach (Collider2D collider in hit) 
            {
                if (collider.CompareTag("Player") && !isAngry)
                {
                    if (isWalk) StopCoroutine(Walk());

                    StartCoroutine(Angry());
                }
            }
        if (GetComponent<EnemyHP>().getHp() <= 0) StopAllCoroutines();
    }
    private IEnumerator Walk()
    {
        isWalk = true;
        float walkPosition = Random.Range(walkRadiusCenter-walkRadius, walkRadiusCenter+walkRadius);
        float seconds = Random.Range(0.1f, 5f);

        if (walkPosition < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
        animator.SetBool("isWalk", true);
        
        while (true)
        {
            if (Vector2.Distance(transform.position, new Vector2(walkPosition, transform.position.y)) < 0.1f)
            {
                body.velocity = Vector2.zero;
                animator.SetBool("isWalk", false);
                isWalk = false;
                yield return new WaitForSeconds(seconds);
                yield break;
            }
            if (walkPosition < transform.position.x) body.velocity = new Vector2(-(speed/2.5f), body.velocity.y + 0.1f*Time.fixedDeltaTime);
            else body.velocity = new Vector2(speed/2.5f, body.velocity.y + 0.1f * Time.fixedDeltaTime);
            yield return null;
        }
    }

    private IEnumerator Angry()
    {
        Collider2D[] hit = null;
        isAngry = true;
        Vector2 currentVelocity = Vector2.zero;
        float acceleration = 7f;
        float deceleration = 7f;
        Vector2 targetVelocity; 

        while (true)
        {
            int currentHP = GetComponent<EnemyHP>().getHp();
            hit = Physics2D.OverlapCircleAll(transform.position, 10);
            foreach (Collider2D collider in hit)
            {
                if (collider.CompareTag("Player"))
                {
                    float walkPosition = collider.transform.position.x;

                    if (walkPosition < transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        targetVelocity = new Vector2(-speed, 0);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                        targetVelocity = new Vector2(speed, 0);
                    }
                    if (!animator.GetBool("isWalk"))
                        animator.SetBool("isWalk", true);
                    currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, (targetVelocity.magnitude > 0 ? acceleration : deceleration) * Time.deltaTime);
                    if (Physics2D.IsTouching(collider, GetComponent<Collider2D>()))
                    {
                        body.velocity = -targetVelocity;
                        yield return new WaitForSeconds(0.2f);
                    }
                    else
                    {
                        body.velocity = currentVelocity;
                    }
                    Debug.Log("Distance: " + Vector2.Distance(transform.position, collider.transform.position));
                    if (currentHP > GetComponent<EnemyHP>().getHp())
                    {
                        speed *= 1.5f;
                        acceleration *= 1.5f;
                        deceleration *= 1.5f;
                    }
                }
            }
            yield return null;
        }
    }
}
