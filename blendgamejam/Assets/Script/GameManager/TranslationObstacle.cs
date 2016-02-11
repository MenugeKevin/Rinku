using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TranslationObstacle : MonoBehaviour {

    public List<GameObject> _pointList = new List<GameObject>();    
    public float _speed = 2.5f;
    private int _index = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position == _pointList[_index].transform.position)
        {
            _index++;
        }
        if (_index == _pointList.Count)
            _index = 0;
        translateObs();
	}

    void translateObs()
    {
       float step = _speed * Time.deltaTime;
       transform.position = Vector3.MoveTowards(transform.position, _pointList[_index].transform.position, step);
    }
}
