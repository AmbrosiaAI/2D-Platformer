using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float smootTime = 0.2f;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(
            target.position.x < minX
            ? minX
            : (target.position.x > maxX ? maxX : target.position.x), 
            target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smootTime);
    }
}
