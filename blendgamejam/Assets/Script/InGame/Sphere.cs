using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour {

	// Use this for initialization
	void Start () {    
        _square = GameObject.FindGameObjectWithTag("Square");
        myCollider = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();
        _myState = myState.STAY;
        _attraction = 0.1f;
        _circleSpeed = 2f;
        _shootSpeed = 0.075f;
        _isActive = true;
        _anim.SetBool("ActiveOrNot", true);
        _mySFX = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_myState == myState.MOVETO)
            OnMoveTo();
        else if (_myState == myState.TURNAROUND)
            OnTurnArround();
        else if (_myState == myState.MOVEFAR)
            OnMoveFar();
        else if (_myState == myState.DEAD)
            onDeath();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Mur")
        {
            _mySFX.clip = _deathClip;
            _mySFX.Play();
            GameObject.Find("GameManager").GetComponent<GameManager>()._gameState = GameManager.GameState.GameOver;
            _myState = myState.DEAD;
            _isActive = false;
        }
    }

    void onDeath()
    {
        Instantiate(_particleDeath, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    void OnMoveTo()
    {
        float distance = Vector3.Distance(this.transform.position, _square.transform.position);
        if (distance > 1.75f)
            transform.position = Vector3.MoveTowards(transform.position, _square.transform.position, distance * _attraction);
        else if (distance <= 1.75f)
        {
            _angle = Mathf.Atan2(transform.position.y - _square.transform.position.y, transform.position.x - _square.transform.position.x) * 180 / Mathf.PI;
            if (_angle < 0)
                _angle = 360 - Mathf.Abs(_angle);
            _myState = myState.TURNAROUND;
        }
    }

    void OnTurnArround()
    {
        if (_square.GetComponent<Square>().getActive())
        {
            float x = _square.transform.position.x + (1.5f * Mathf.Cos(_angle * Mathf.Deg2Rad));
            float y = _square.transform.position.y + (1.5f * Mathf.Sin(_angle * Mathf.Deg2Rad));
            transform.position = new Vector3(x, y, 0);
            _angle -= _circleSpeed;
            if (_angle <= 0)
                _angle = 360;
        }
        else
            _myState = myState.MOVEFAR;
    }

    void OnMoveFar()
    {
        _anim.SetBool("ActiveOrNot", true);
        _isActive = true;
        myCollider.enabled = true;
        float x = transform.position.x + (_shootSpeed * Mathf.Cos(_angle * Mathf.Deg2Rad));
        float y = transform.position.y + (_shootSpeed * Mathf.Sin(_angle * Mathf.Deg2Rad));
        transform.position = new Vector3(x, y, 0);
    }

    public void OnTouch()
    {
        if (_isActive)
        {
            _myState = myState.STAY;
            _square.GetComponent<Square>().setState(Square.myState.MOVETO);
        }
    }

    public void OnRelease()
    {
        _mySFX.Play();
        _isActive = false;
        if (_anim)
            _anim.SetBool("ActiveOrNot", false);
        myCollider.enabled = false;
    }

    public bool getActive() { return _isActive; }

    public void setState(myState arg) { _myState = arg; }
    public GameObject _particleDeath;
    public AudioClip _deathClip;

    public enum myState
    {
        STAY,
        MOVETO,
        TURNAROUND,
        MOVEFAR,
        DEAD
    };

    private AudioSource _mySFX;
    private myState _myState;
    private GameObject _square;
    private Collider2D myCollider;
    private Animator _anim;
    private bool _isActive;
    private float _circleSpeed;
    private float _shootSpeed;
    private float _attraction;
    private float _angle;

}
