using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip Background_Music;
    private AudioSource[] AudioSource;
    private void Start()
    {
        AudioSource = transform.parent.GetComponents<AudioSource>();
        AudioSource[0].clip = Background_Music;
        AudioSource[0].Play();
        Messenger<AudioClip>.AddListener(EventList.Player_Jump, Player_Jump);
        Messenger<AudioClip>.AddListener(EventList.Player_Get_Damage, Player_Damaged);
        Messenger<int>.AddListener(EventList.Item_Collected, Item_Colleted);
        Messenger.AddListener(EventList.EndpointReached, Endpoint_Reached);
        Messenger.AddListener(EventList.EnemyGetDamage, Enemy_GetDamage);
        Messenger.AddListener(EventList.EnemyDeath, Enemy_Die);
        Messenger.AddListener(EventList.Button_Click, Button_Click);
    }

    private void OnDestroy()
    {
        Messenger<AudioClip>.RemoveListener(EventList.Player_Jump, Player_Jump);
        Messenger<AudioClip>.RemoveListener(EventList.Player_Get_Damage, Player_Damaged);
        Messenger<int>.RemoveListener(EventList.Item_Collected, Item_Colleted);
        Messenger.RemoveListener(EventList.EndpointReached, Endpoint_Reached);
        Messenger.RemoveListener(EventList.EnemyGetDamage, Enemy_GetDamage);
        Messenger.RemoveListener(EventList.EnemyDeath, Enemy_Die);
        Messenger.RemoveListener(EventList.Button_Click, Button_Click);
    }

    //Player
    private void Player_Jump(AudioClip jumpSound) => AudioSource[1].PlayOneShot(jumpSound);

    private void Player_Damaged(AudioClip damagedSound) => AudioSource[1].PlayOneShot(damagedSound);

    //UI
    private void Item_Colleted(int points)
    {
        AudioClip CollectSound = null;
        if (points <= 5) CollectSound = Managers.SoundList.CollectSmall;
        else if (points > 5 && points < 25) CollectSound = Managers.SoundList.CollectMedium;
        else CollectSound = Managers.SoundList.CollectBig;

        AudioSource[1].PlayOneShot(CollectSound);
    }
    private void Button_Click() => AudioSource[1].PlayOneShot(Managers.SoundList.ButtonClick);
    private void Endpoint_Reached() => AudioSource[1].PlayOneShot(Managers.SoundList.EndpointReached);

    //Enemy
    private void Enemy_GetDamage() => AudioSource[1].PlayOneShot(Managers.SoundList.EnemyGetDamage);
    private void Enemy_Die() => AudioSource[1].PlayOneShot(Managers.SoundList.EnemyDie);

}
