using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes_new : MonoBehaviour
{
    int _noteSpeed;
    bool _start;

    private void Start()
    {
        _noteSpeed = GameManager.instance._noteSpeed;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _start = true;
        if (_start)
            transform.position -= transform.forward * Time.deltaTime * _noteSpeed;
    }
}
