using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource _audioSource;
    AudioClip _audioClip;
    string _songName;
    bool _played;

    void Start()
    {
        _songName = "maou_short_14_shining_star";
        _audioSource = GetComponent<AudioSource>();
        _audioClip = (AudioClip)Resources.Load("Musics/" + _songName);
        _audioSource.clip = _audioClip;
        _played = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_played)
        {
            GameManager.instance._start = true;
            GameManager.instance._startTime = Time.time;
            _played = true;
            _audioSource.Play();
        }
    }
}
