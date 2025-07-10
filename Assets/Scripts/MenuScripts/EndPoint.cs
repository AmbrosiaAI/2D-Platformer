using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem _particleSystem;
    private bool hasTriggerd = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggerd)
        {
            hasTriggerd = true;
            Messenger.Broadcast(EventList.EndpointReached);
            _particleSystem.Play();
            other.GetComponent<PlayerHP>().setSpawnPosition(this.transform.position);
        }
    }

    private void Start()
    {
        _particleSystem.Stop();
    }
}
