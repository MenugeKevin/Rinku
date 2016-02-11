using UnityEngine;
using System.Collections;

public class ScrollSide : MonoBehaviour {

    public float _scrollspeed = 5.0f;
    public float _tileSize;
    public GameObject _player;

    private Vector3 _startpos;

    // Mur sol : x: 4.5/ y = -4.5 | Scale x = 18
    // Use this for initialization
    void Start()
    {
        _startpos = _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float newpos = Mathf.Repeat(Time.time * _scrollspeed, _tileSize);
        Debug.Log("New Pos Repeat = " + newpos);
        transform.position = _startpos + Vector3.left * newpos;
        if (newpos < 1)
            _startpos = _player.transform.position;

    }
}
