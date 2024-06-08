using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] Text _score;
    [SerializeField] Text _perfect;
    [SerializeField] Text _great;
    [SerializeField] Text _good;
    [SerializeField] Text _miss;

    private void OnEnable()
    {
        _score.text = GameManager.instance._score.ToString();
        _perfect.text = GameManager.instance._perfect.ToString();
        _great.text = GameManager.instance._great.ToString();
        _good.text = GameManager.instance._good.ToString();
        _miss.text = GameManager.instance._miss.ToString();
    }
}
