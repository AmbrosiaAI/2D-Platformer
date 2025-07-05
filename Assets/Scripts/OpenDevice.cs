using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDevice : MonoBehaviour
{
    [SerializeField] private Vector2 dPos;
    private bool _open = false;

    public void Operate()
    {
        Debug.Log("Дверь");
        if (_open)
            Close();
        else
            Open();
    }

    public void Open()
    {
        if (!_open)
        {
            transform.position += (Vector3)dPos;
            _open = true;
        }
    }

    public void Close()
    {
        if (_open)
        {
            transform.position -= (Vector3)dPos;
            _open = false;
        }
    }
}
