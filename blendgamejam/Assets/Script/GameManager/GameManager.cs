using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;


public class GameManager : MonoBehaviour {

    //public Attribute
    #region Public_Vars
    public GameObject _playerOne;
    public GameObject _playerTwo;
    public float _distanceCam = 1f;
    public float _rate = 0.3f;

    public enum GameState
    {
        Start,
        Game,
        GameOver,
    };

    public GameState _gameState { get; set; }
    #endregion

    #region Private_Vars
    //private Attribute
    private Camera      MainCamera;
    private Vector3     vel = Vector3.zero;
    private int         _level { get; set; }
    private GameObject  _playerFastest;
    private float       _possmg;
    private Vector3     _camMax;
    public float        _timerBonus = 100;
    private float       _actualTime = 0;
 
    private float       _actualdistance;
    private List<GameObject> _UICanvas = new List<GameObject>();
    private float _deathDelay = 0f;
    #endregion

    #region UnityStartGame
    void Awake()
    {
        _level = 1;
        MainCamera = Camera.main;
        if (_playerOne.transform.position.x > _playerTwo.transform.position.x)
            _playerFastest = _playerOne;
        else
            _playerFastest = _playerTwo;
        _camMax = _playerFastest.transform.position;
        _actualdistance = _playerFastest.transform.position.x + UnityEngine.Random.Range(2.0f, 8.0f);
        _UICanvas.Add(GameObject.Find("StartUI"));
        _UICanvas.Add(GameObject.Find("InGameUI"));
        _UICanvas.Add(GameObject.Find("gameOverUI"));
        _UICanvas[0].SetActive(true);
        _UICanvas[1].SetActive(false);
        _UICanvas[2].SetActive(false);
       
    }

	void Start () {
        _possmg = transform.position.x;
        
	}
    #endregion

    #region UnityUpdate
    // Update is called once per frame
	void Update () {        
       if (_gameState == GameState.GameOver)
            gameOverEnable();
#if UNITY_STANDALONE_WIN
		if (Input.GetMouseButton (0) == true && _gameState == GameState.Start)
        {
            GetComponent<AudioSource>().Play();
            _gameState = GameState.Game;
            _UICanvas[0].gameObject.SetActive(false);
            _UICanvas[1].gameObject.SetActive(true);
        }
#elif UNITY_ANDROID
       foreach (Touch touch in Input.touches)
       {
           if (touch.phase == TouchPhase.Began && _gameState == GameState.Start)
           {
               GetComponent<AudioSource>().Play();
               _gameState = GameState.Game;
               _UICanvas[0].gameObject.SetActive(false);
               _UICanvas[1].gameObject.SetActive(true);
           }
       }
#endif
    }

	void FixedUpdate()
	{
        if (_gameState == GameState.Game)
            UpdatePlayerPosition();

	}
    #endregion

    void UpdatePlayerPosition()
    {
        if (_playerOne.transform.position.x > _playerTwo.transform.position.x)
            _playerFastest = _playerOne;
        else
            _playerFastest = _playerTwo;

        

        UpdateCam();
        if (_actualTime <= 0)
        {
            _actualTime = 0;
            Scoring.resetMultiplicator();
        }
        else
        {
            _actualTime -= (1f * Time.deltaTime);
            Debug.Log("Actual Time = " + _actualTime);
        }
        if (_actualdistance < _playerFastest.transform.position.x)
        {
            _actualdistance = _playerFastest.transform.position.x + UnityEngine.Random.Range(2.0f, 8.0f);
        }
        updateScoring();
    }

    public void UpdateCam()
    {
        if (_playerFastest)
        {           
            if (_playerFastest.transform.position.x > _camMax.x)
            {
                Vector3 point = MainCamera.WorldToViewportPoint(_playerFastest.transform.position);
                Vector3 smoothCam = (new Vector3(_playerFastest.transform.position.x + 3f, _playerFastest.transform.position.y, _playerFastest.transform.position.z)) - MainCamera.ViewportToWorldPoint(new Vector3(0.5f, point.y, point.z));
                Vector3 destination = MainCamera.transform.position + smoothCam;
                MainCamera.transform.position = Vector3.SmoothDamp(MainCamera.transform.position, destination, ref vel, 0.2f);
                _camMax = _playerFastest.transform.position + new Vector3(3f, 0f, 0f);
            }
            else
            {
                Vector3 point = MainCamera.WorldToViewportPoint(_playerFastest.transform.position);
                Vector3 destination = MainCamera.transform.position + new Vector3(0.25f, 0f, 0f);
                MainCamera.transform.position = Vector3.SmoothDamp(MainCamera.transform.position, destination, ref vel, _rate);
                _camMax = MainCamera.transform.position - new Vector3(2.0f, 0f, 0f);
            }
        }
    }

 
  
    public void gameOverEnable()
    {
        _deathDelay += Time.deltaTime;
        if (_deathDelay > 1f)
        {
            Scoring.UpdateHighscore();
            _UICanvas[1].gameObject.SetActive(false);
            _UICanvas[2].gameObject.SetActive(true);            
        }
    } 

    public void fullBonus()
    {
        _actualTime = _timerBonus;
        Scoring.incMultiplicator();
    }

    public float getTimerBonus()
    {        
        return (_actualTime);
    }

    public void updateScoring()
    {
        if (_playerFastest.transform.position.x > (_possmg + 1))
        {
            _possmg = _playerFastest.transform.position.x;
            Scoring.AddToScore(1);
        }

        long tmpscore = Scoring.getScore();

        if (tmpscore > 5 && tmpscore < 20)
            _level = 2;
        else if (tmpscore > 20 && tmpscore < 55)
            _level = 3;
        else if (tmpscore > 55 && tmpscore < 90)
            _level = 4;
        else if (tmpscore > 90 && tmpscore < 150)
            _level = 5;
        else if (tmpscore > 150)
            _level = 6;
        else
            _level = 1;
    }

    public int getLevel()
    {
        return (_level);
    }

 
}
