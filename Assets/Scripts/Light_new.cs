using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_new : MonoBehaviour
{
    [Header("Light‚ªÁ‚¦‚é‘¬“x")]
    [SerializeField] float _speed = 3;
    [SerializeField] int _num = 0;
    Renderer _rend;
    float _alfa = 0;

    void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!(_rend.material.color.a < 0.01f))
        {
            _rend.material.color = new Color(_rend.material.color.r, _rend.material.color.g, _rend.material.color.b, _alfa);
        }

        if (_num == 1)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                ColorChange();
            }
        }if (_num == 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ColorChange();
            }
        }if (_num == 3)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                ColorChange();
            }
        }if (_num == 4)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                ColorChange();
            }
        }

        _alfa -= _speed * Time.deltaTime;
    }

    void ColorChange()
    {
        _alfa = 0.3f;
        _rend.material.color = new Color(_rend.material.color.r, _rend.material.color.g, _rend.material.color.b, _alfa);
    }
}
