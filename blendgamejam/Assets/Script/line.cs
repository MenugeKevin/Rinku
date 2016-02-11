using UnityEngine;
using System.Collections;

public class line : MonoBehaviour {

    public GameObject _sphere;
    public GameObject _square;
    private LineRenderer _line;
	// Use this for initialization
	void Start () {
        _line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        _line.SetPosition(0, _square.transform.position);
        _line.SetPosition(1, _sphere.transform.position);
        if (!_sphere.active || !_square.active)
        {
            Destroy(gameObject);
        }
    }
}
