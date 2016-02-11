using UnityEngine;
using System.Collections;

public class BonusJauge : MonoBehaviour {

    public GameManager _gm;
    private SpriteRenderer spriterender;
    public Color MaxJaugeColor = new Color(255 / 255f, 63 / 255f, 255f);
    public Color MinJaugeColor = new Color(64 / 255f, 137 / 255f, 255f);

    // Use this for initialization
    void Start () {
        spriterender = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        var jaugepercent = _gm.getTimerBonus() / _gm._timerBonus;
        if (jaugepercent > 0)
        {
            spriterender.color = Color.Lerp(MaxJaugeColor, MinJaugeColor, jaugepercent);
        }
        transform.localScale = new Vector3(jaugepercent, 1, 1);            
	}
}
