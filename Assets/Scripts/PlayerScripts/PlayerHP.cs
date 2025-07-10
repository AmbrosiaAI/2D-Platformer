using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHp = 5;
    [SerializeField] private float InvincibleTime = 1f;
    private TextMeshProUGUI hpIndicator;
    [SerializeField] private Canvas canvas;
    private Vector2 savepointPosition;
    private bool Invincible = false;
    [SerializeField] private int hp;
    [SerializeField] private AudioClip DamageSound;
    [SerializeField] private AudioClip DeathSound;

    public int[] getHP()
    {
        int[] giveHp = new int[2];
        giveHp[0] = hp;
        giveHp[1] = maxHp;
        return giveHp;
    }

    public void getHeal(int heal = 1)
    {
        hp += heal;
        hpIndicator.text = "HP:" + hp;
    }
    public IEnumerator getDamage(int damage = 1)
    {
        if (!Invincible)
        {
            Invincible = true;
            hp -= damage;
            canvas.transform.GetChild(0).GetComponent<InventoryManager>().AddHpLost();
            hpIndicator.text = "HP:" + hp;
            if (hp <= 0)
            {
                Messenger<AudioClip>.Broadcast(EventList.Player_Get_Damage, DeathSound);
                StartCoroutine(respawn());
                Invincible = false;
            } else
            {
                Messenger<AudioClip>.Broadcast(EventList.Player_Get_Damage, DamageSound);
                Rigidbody2D body = GetComponent<Rigidbody2D>();
                Vector2 currentVelocity = body.velocity;
                body.velocity = Vector2.zero;
                body.AddForce(new Vector2(0, 4.5f), ForceMode2D.Impulse);
                StartCoroutine(BlinkColor(InvincibleTime));

                yield return new WaitForSeconds(InvincibleTime);
                Invincible = false;
            }
        }
    }

    private IEnumerator BlinkColor(float duration)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color currentColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            spriteRenderer.color = Color.Lerp(Color.red, currentColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator respawn()
    {
        PlayerControl playerControl = GetComponent<PlayerControl>();
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("IsDead");
        rigidbody2D.simulated = false;
        playerControl.enabled = false;
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.4f);
        transform.position = savepointPosition;
        rigidbody2D.simulated = true;
        playerControl.enabled = true;
        hp = maxHp;
        hpIndicator.text = "HP:" + hp;
    }

    public void setSpawnPosition(Vector2 position)
    {
        savepointPosition = position;
    }

    public void Start()
    {
        savepointPosition = transform.position;
        hp = maxHp;
        hpIndicator = canvas.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        hpIndicator.text = "HP:" + hp;
    }
}
