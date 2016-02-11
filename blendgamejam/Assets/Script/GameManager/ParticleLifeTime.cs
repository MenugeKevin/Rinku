using UnityEngine;
using System.Collections;

public class ParticleLifeTime : MonoBehaviour {

    public float _lifetime = 2f;
    private float deathtime;
        
	// Use this for initialization
	void Start () {
        deathtime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        deathtime += Time.deltaTime;
        if (deathtime >= _lifetime)
        {
            Destroy(gameObject);
        }
	}
}
