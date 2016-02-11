using UnityEngine;
using System.Collections;

public class PositionFromScreen : MonoBehaviour {

    public bool centerImage = false;   

	// Use this for initialization
	void Start () {
        if (centerImage)
        {
            transform.position = Vector3.zero;
        }
	}
	
	// Update is called once per frame
	void Update () {
        functionResizeSpriteToScreen();
	}

    void    functionResizeSpriteToScreen()
    {
        SpriteRenderer _sr = GetComponent<SpriteRenderer>();

        if (_sr == null)
            return;
        transform.localScale = new Vector3(1, 1, 1);

        float width = _sr.sprite.bounds.size.x;
        float height = _sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, transform.localScale.z);
    }
}
