using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugStatus : MonoBehaviour {

    private static string _status = "";
    private Text _text;
    // Use this for initialization
	void Start () {
        _text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _text.text = _status;
	}

    public static void changeStatus(string newStatus)
    {
        _status = newStatus;
    }
}
