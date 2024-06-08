using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour
{
    [Tooltip("プレイヤーに判定を伝えるオブジェクト")] 
    [SerializeField] GameObject[] _messageObj;
    [Tooltip("NotesManagerを入れる")]
    [SerializeField] NotesManager _notesManager;

    [SerializeField] Text _comboText;
    [SerializeField] Text _scoreText;
    [SerializeField] GameObject _finishText;

    AudioSource _audio;
    [SerializeField] AudioClip _hitSound;

    float _endTime = 0;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _endTime = _notesManager.NotesTime[_notesManager.NotesTime.Count - 1];
    }

    void Update()
    {
        if (GameManager.instance._start)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (_notesManager.LaneNum[0] == 0) // 押されたボタンがレーンとあっているか
                {
                    // ノーツをたたく場所と実際にたたいた場所がどれくらいずれているかを求め、
                    // その絶対値をJudgement関数に送る
                    Judgement(GetABS(Time.time - (_notesManager.NotesTime[0] + GameManager.instance._startTime)), 0);
                }
                else
                {
                    if (_notesManager.LaneNum[1] == 0)
                    {
                        Judgement(GetABS(Time.time - (_notesManager.NotesTime[1] + GameManager.instance._startTime)), 1);
                    }
                    else
                    {
                        if (_notesManager.LaneNum[2] == 0)
                        {
                            Judgement(GetABS(Time.time - (_notesManager.NotesTime[2] + GameManager.instance._startTime)), 2);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_notesManager.LaneNum[0] == 1)
                {
                    Judgement(GetABS(Time.time - (_notesManager.NotesTime[0] + GameManager.instance._startTime)), 0);
                }
                else
                {
                    if (_notesManager.LaneNum[1] == 1)
                    {
                        Judgement(GetABS(Time.time - (_notesManager.NotesTime[1] + GameManager.instance._startTime)), 1);
                    }
                    else
                    {
                        if (_notesManager.LaneNum[2] == 1)
                        {
                            Judgement(GetABS(Time.time - (_notesManager.NotesTime[2] + GameManager.instance._startTime)), 2);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (_notesManager.LaneNum[0] == 2)
                {
                    Judgement(GetABS(Time.time - (_notesManager.NotesTime[0] + GameManager.instance._startTime)), 0);
                }
                else
                {
                    if (_notesManager.LaneNum[1] == 2)
                    {
                        Judgement(GetABS(Time.time - (_notesManager.NotesTime[1] + GameManager.instance._startTime)), 1);
                    }
                    else
                    {
                        if (_notesManager.LaneNum[2] == 2)
                        {
                            Judgement(GetABS(Time.time - (_notesManager.NotesTime[2] + GameManager.instance._startTime)), 2);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                if (_notesManager.LaneNum[0] == 3)
                {
                    Judgement(GetABS(Time.time - (_notesManager.NotesTime[0] + GameManager.instance._startTime)), 0);
                }
                else
                {
                    if (_notesManager.LaneNum[1] == 3)
                    {
                        Judgement(GetABS(Time.time - (_notesManager.NotesTime[1] + GameManager.instance._startTime)), 1);
                    }
                    else
                    {
                        if (_notesManager.LaneNum[2] == 3)
                        {
                            Judgement(GetABS(Time.time - (_notesManager.NotesTime[2] + GameManager.instance._startTime)), 2);
                        }
                    }
                }
            }

            if (Time.time > _endTime + GameManager.instance._startTime + 8.0f)
            {
                _finishText.SetActive(true);
                if (Time.time > _endTime + GameManager.instance._startTime + 10.0f)
                    GameManager.instance._finish = true;
                return;
            }

            if (Time.time > _notesManager.NotesTime[0] + 0.15f + GameManager.instance._startTime)
            {
                Debug.Log("Miss");
                Message(3);
                GameManager.instance._miss++;
                GameManager.instance._combo = 0;
                DeleteDate(0);
            }
        }
    }

    void Judgement(float timeLag, int numOffSet)
    {
        _audio.PlayOneShot(_hitSound);
        if (timeLag <= 0.05)
        {
            Debug.Log("Parfect");
            Message(0);
            GameManager.instance._ratioScore += 5;
            GameManager.instance._perfect++;
            GameManager.instance._combo++;
            DeleteDate(numOffSet);
        }
        else if (timeLag <= 0.08)
        {
            Debug.Log("Great");
            Message(1);
            GameManager.instance._ratioScore += 3;
            GameManager.instance._great++;
            GameManager.instance._combo++;
            DeleteDate(numOffSet);
        }
        else if (timeLag <= 0.15)
        {
            Debug.Log("Good");
            Message(2);
            GameManager.instance._ratioScore += 1;
            GameManager.instance._good++;
            GameManager.instance._combo++;
            DeleteDate(numOffSet);
        }
    }

    /// <summary> 引数の絶対値を返す関数 </summary>
    float GetABS(float num)
    {
        if (num >= 0) return num;
        else return -num;
    }

    /// <summary> すでにたたいたノーツを削除する関数 </summary>
    void DeleteDate(int numOffSet)
    {
        _notesManager.NotesTime.RemoveAt(numOffSet);
        _notesManager.LaneNum.RemoveAt(numOffSet);
        _notesManager.NoteType.RemoveAt(numOffSet);
        // プレイヤーの成績 / 理論値 * 10000000;
        GameManager.instance._score = (int)Math.Round(10000000 * Math.Floor(GameManager.instance._ratioScore / GameManager.instance._maxScore * 10000000) / 10000000);
        _comboText.text = GameManager.instance._combo.ToString();
        _scoreText.text = GameManager.instance._score.ToString();
    }

    /// <summary> 判定を表示する </summary>
    void Message(int judge)
    {
        Instantiate(_messageObj[judge], new Vector3(_notesManager.LaneNum[0] * 3f - 4.75f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }
}
