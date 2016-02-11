using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    private Rigidbody2D rigid;
    public float _speed = 5f;
    public float _automove = 0.1f;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        if (Mathf.Abs(moveX) > 0 || Mathf.Abs(moveY) > 0)
            rigid.velocity = new Vector2(moveX * _speed, moveY * _speed);
        else
            rigid.velocity = new Vector2(_automove * _speed, rigid.velocity.y);
	}
}
