using UnityEngine;
using System.Collections;

public class BonusManager : MonoBehaviour {

    public bool _col;
    public GameManager _gm;
    Camera MainCamera;
    public GameObject _particle;

	// Use this for initialization
	void Start () {
        _col = false;
        MainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	if (_col)
            Destroy(gameObject);
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Mur")
        {
            _col = true;
        }
        else if (col.tag == "Player")
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
            _col = true;
            _gm.fullBonus();  
        }
    }
}
