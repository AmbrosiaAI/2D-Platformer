using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    public float speed = 250f;
    public float jumpPower = 12f;

    private Animator animator;
    private bool isRun = false;
    private Rigidbody2D body;
    private new Collider2D collider;
    private bool[] isWallNear = new bool[2];
    bool grounded = false;
    void Start()
    {
        isWallNear[0] = false;
        isWallNear[1] = false;
       animator = GetComponent<Animator>();
       body = GetComponent<Rigidbody2D>();
       collider = GetComponent<Collider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Level"))
        {
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                if (contactPoint.normal.x > 0.70) isWallNear[0] = true;
                else isWallNear[0] = false;
                if (contactPoint.normal.x < -0.70) isWallNear[1] = true;
                else isWallNear[1] = false;
                if (contactPoint.normal.y > 0.50) grounded = true;
                else grounded = false;
            }
        }
    }

    private IEnumerator OnCollisionExit2D(Collision2D collision)
    {
        isWallNear[0] = false;
        isWallNear[1] = false;
        if (grounded) 
        {
            yield return new WaitForSeconds(0.07f);
            grounded = false;
        }
    }

    void Update()
    {
        float dx = 0;
        if (!isWallNear[0] && Input.GetAxis("Horizontal") < 0) 
        {
            dx = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        } else if (!isWallNear[1] && Input.GetAxis("Horizontal") > 0)
        {
            dx = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        }

            isRun = Mathf.Abs(dx) >= 0.2;
        animator.SetBool("isRun", isRun);

        if (dx < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (dx > 0) transform.localScale = new Vector3(1, 1, 1);

        if (grounded && Input.GetKeyDown(KeyCode.Space)) //Jump
        {
            body.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
        body.velocity = new Vector2 (dx, body.velocity.y);
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
