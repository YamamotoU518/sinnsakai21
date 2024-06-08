using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public float _maxScore;
    public float _ratioScore;
    public string _songId;
    public int _noteSpeed;
    public bool _start;
    public bool _finish;
    /// <summary> Space‚ª‰Ÿ‚³‚ê‚½ŽžŠÔ‚ð‹L˜^ </summary>
    public float _startTime;
    public int _combo;
    public int _score;
    public int _perfect;
    public int _great;
    public int _good;
    public int _miss;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
