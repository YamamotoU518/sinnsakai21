using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;
}

[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour
{
    /// <summary> 総ノーツ数 </summary>
    public int _noteNum; // 総ノーツ数
    private string _songName;

    public List<int> LaneNum = new List<int>(); // どのレーンにノーツが落ちてくるか
    public List<int> NoteType = new List<int>(); // ノーツの種類
    public List<float> NotesTime = new List<float>(); // ノーツが判定線と重なる時間
    public List<GameObject> NotesObj = new List<GameObject>();

    float _notesSpeed; // ノーツのスピード
    [SerializeField] GameObject _noteObj; // ノーツのプレハブ

    private void OnEnable()
    {
        _notesSpeed = GameManager.instance._noteSpeed;
        _noteNum = 0;
        _songName = "maou_short_14_shining_star";
        Load(_songName);
    }

    private void Load(string SongName)
    {
        string _inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data _inputJson = JsonUtility.FromJson<Data>(_inputString);

        _noteNum = _inputJson.notes.Length;
        GameManager.instance._maxScore = _noteNum * 5;

        for (int i = 0; i < _inputJson.notes.Length; i++)
        {
            // 一小節の長さ
            float _interval = 60 / (_inputJson.BPM * (float)_inputJson.notes[i].LPB);
            // ノーツ間の長さ
            float _beatSec = _interval * (float)_inputJson.notes[i].LPB;
            // ノーツの降ってくる時間
            float _time = (_beatSec * _inputJson.notes[i].num / (float)_inputJson.notes[i].LPB) + _inputJson.offset * 0.01f;
            NotesTime.Add(_time);
            LaneNum.Add(_inputJson.notes[i].block);
            NoteType.Add(_inputJson.notes[i].type);

            // ノーツを生成
            float z = NotesTime[i] * _notesSpeed + 5.3f;
            NotesObj.Add(Instantiate(_noteObj, new Vector3(3f * _inputJson.notes[i].block - 4.75f, 0.1f, z), Quaternion.identity));
        }
    }
}
