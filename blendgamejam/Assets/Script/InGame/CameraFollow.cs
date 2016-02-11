using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        _camSpeed = 4f;
        _square = GameObject.FindGameObjectWithTag("Square");
        _sphere = GameObject.FindGameObjectWithTag("Sphere");
	}
	
	// Update is called once per frame
	void Update () {
        if (_square != null && _sphere != null)
        {
            if (_sphere.GetComponent<Sphere>().getActive())
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(_sphere.transform.position.x, _sphere.transform.position.y, -10), _camSpeed * Time.deltaTime);
            else if (_square.GetComponent<Square>().getActive())
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(_square.transform.position.x, _square.transform.position.y, -10), _camSpeed * Time.deltaTime);
        }
	
	}

    private GameObject _square;
    private GameObject _sphere;
    private float _camSpeed;
}
