using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchScreenRinku : MonoBehaviour {

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;
    private GameObject _square;
    private GameObject _sphere;
    private RaycastHit2D hit;

    void Start()
    {
        _form = form.NOTHING;
        _canRelease = false;
        _sphere = GameObject.FindGameObjectWithTag("Sphere");
        _square = GameObject.FindGameObjectWithTag("Square");
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_square.GetComponent<Square>().getActive())
            {
                _form = form.SQUARE;
                _distance = Vector3.Distance(new Vector3(screenRay.origin.x, screenRay.origin.y, 0), _square.transform.position);
            }
            else if (_sphere.GetComponent<Sphere>().getActive())
            {
                _form = form.SPHERE;
                _distance = Vector3.Distance(new Vector3(screenRay.origin.x, screenRay.origin.y, 0), _sphere.transform.position);
            }
            else
                _form = form.NOTHING;
            if (Input.GetMouseButtonDown(0))
            {
                if (_distance < 1.5f)
                {
                    if (_form == form.SQUARE)
                        _square.GetComponent<Square>().OnTouch();
                    else
                        _sphere.GetComponent<Sphere>().OnTouch();
                    _canRelease = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (_canRelease)
                {

                    if (_form == form.SQUARE)
                        _square.GetComponent<Square>().OnRelease();
                    else
                        _sphere.GetComponent<Sphere>().OnRelease();
                    _canRelease = false;
                }
            }
        }
#endif

#if UNITY_ANDROID
        touchesOld = new GameObject[touchList.Count];
        touchList.CopyTo(touchesOld);
        touchList.Clear();
        foreach (Touch touch in Input.touches)
        {
            Ray screenRay = Camera.main.ScreenPointToRay(touch.position);
            if (_square.GetComponent<Square>().getActive())
            {
                _form = form.SQUARE;
                _distance = Vector3.Distance(new Vector3(screenRay.origin.x, screenRay.origin.y, 0), _square.transform.position);
            }
            else if (_sphere.GetComponent<Sphere>().getActive())
            {
                _form = form.SPHERE;
                _distance = Vector3.Distance(new Vector3(screenRay.origin.x, screenRay.origin.y, 0), _sphere.transform.position);
            }
            else
                _form = form.NOTHING;
            if (_form != form.NOTHING)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (_distance < 1.5f)
                        {
                            if (_form == form.SQUARE)
                                _square.GetComponent<Square>().OnTouch();
                            else
                                _sphere.GetComponent<Sphere>().OnTouch();
                            _canRelease = true;
                        }
                        break;
                    case TouchPhase.Ended:
                        if (_canRelease)
                        {
                            if (_form == form.SQUARE)
                                _square.GetComponent<Square>().OnRelease();
                            else
                                _sphere.GetComponent<Sphere>().OnRelease();
                            _canRelease = false;
                        }
                        break;
                }
            }
        }
#endif
    }

    private enum form
    {
        SQUARE,
        SPHERE,
        NOTHING
    };
    private form _form;
    private float _distance;
    private bool _canRelease;
}
