using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour
{
    public  LayerMask touchInputMask;
    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;
    private RaycastHit2D hit;
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(screenRay.origin, screenRay.direction);
            if(hit && hit.collider != null)
            {
                GameObject recipient = hit.transform.gameObject;
                if (Input.GetMouseButtonDown(0))
                    recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                if (Input.GetMouseButtonUp(0))
                    recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                if (Input.GetMouseButton(0))
                    recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
            }
        }
#endif

#if UNITY_STANDALONE_OSX
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(screenRay.origin, screenRay.direction);
            if(hit && hit.collider != null)
            {
                GameObject recipient = hit.transform.gameObject;
                if (Input.GetMouseButtonDown(0))
                    recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                if (Input.GetMouseButtonUp(0))
                    recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                if (Input.GetMouseButton(0))
                    recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
            }
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();
            foreach (Touch touch in Input.touches)
            {
                Ray screenRay = Camera.main.ScreenPointToRay(touch.position);
                hit = Physics2D.Raycast(screenRay.origin, screenRay.direction);
                if (hit && hit.collider != null)
                {
                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                            break;
                        case TouchPhase.Ended:
                            recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                            break;
                        case TouchPhase.Stationary:
                            recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                            break;
                        case TouchPhase.Canceled:
                            recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                            break;
                    }
                }
            }
        }
#endif
    }
}
