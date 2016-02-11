using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpawnWall : MonoBehaviour {

    public List<GameObject> _WallLvlOne;
    public List<GameObject> _WallLvlTwo;
    public List<GameObject> _WallLvlTree;
    public List<GameObject> _WallLvlFour;
    public GameObject _background;
    public GameObject       _gm;
    private float            _space = 36f;
    private List<List<GameObject>> _AllWall;
    private bool _transition = true;

    // Use this for initialization
    void Start () {
        _AllWall = new List<List<GameObject>>();
        _AllWall.Add(_WallLvlOne);
        _AllWall.Add(_WallLvlTwo);
        _AllWall.Add(_WallLvlTree);
        _AllWall.Add(_WallLvlFour);
        Instantiate(_AllWall[0][0], Vector3.zero, Quaternion.identity);
        Instantiate(_background, Vector3.zero, Quaternion.identity);
        Instantiate(_background, new Vector3(transform.position.x + 18, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(_AllWall[1][UnityEngine.Random.Range(0, _AllWall[1].Count)], new Vector3(transform.position.x + 18, transform.position.y, transform.position.z), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public bool InstantiateWall()
    {
        GameObject go = null;

        switch (_gm.GetComponent<GameManager>().getLevel())
        {
            case 1:
                go = _AllWall[0][UnityEngine.Random.Range(0, _AllWall[0].Count)];
                break;
            case 2:
                if (!_transition)
                    go = _AllWall[1][UnityEngine.Random.Range(0, _AllWall[1].Count)];
                else
                    go = _AllWall[0][UnityEngine.Random.Range(0, _AllWall[0].Count)];
                _transition = !_transition;
                break;
            case 3:
                if (!_transition)
                   go = _AllWall[2][UnityEngine.Random.Range(0, _AllWall[2].Count)];
                else
                   go = _AllWall[0][UnityEngine.Random.Range(0, _AllWall[0].Count)];
                _transition = !_transition;
                break;
            case 4:
                if (!_transition)
                    go = _AllWall[3][UnityEngine.Random.Range(0, _AllWall[3].Count)];
                else
                    go = _AllWall[1][UnityEngine.Random.Range(0, _AllWall[1].Count)];
                _transition = !_transition;
                break;
            case 5:
                if (!_transition)
                    go = _AllWall[3][UnityEngine.Random.Range(0, _AllWall[3].Count)];
                else
                    go = _AllWall[2][UnityEngine.Random.Range(0, _AllWall[1].Count)];
                _transition = !_transition;
                break;
            case 6:
                if (!_transition)
                    go = _AllWall[3][UnityEngine.Random.Range(0, _AllWall[3].Count)];
                else
                    go = _AllWall[UnityEngine.Random.Range(0, _AllWall.Count)][UnityEngine.Random.Range(0, _AllWall[1].Count)];
                _transition = !_transition;
                break;
            default:
                return false;
        }
        Vector3 posngo = new Vector3(transform.position.x + _space, transform.position.y, 0);
        Instantiate(_background, posngo, Quaternion.identity);
        Instantiate(go, posngo, Quaternion.identity);
        return (true);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Sphere" || col.tag == "Square")
        {
            InstantiateWall();
            transform.position = new Vector3(transform.position.x + (_space / 2), transform.position.y, transform.position.z);
        }
    }
}
