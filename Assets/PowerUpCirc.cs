using UnityEngine;
using System.Collections;

public class PowerUpCirc : MonoBehaviour {
    public Sprite s0, s25, s50, s75, s100;
    public bool isFish = false;
    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update() {
        if (isFish)
        {
            transform.position = Player.players[0].gameObject.transform.position;
            float percentage = Player.players[0].powerup_points / (float)Player.players[0].POWERUPTHRESHOLD;
                if (percentage < 0.25f)
            {
                sr.sprite = s0;
            } else if (percentage < 0.5f)
            {
                sr.sprite = s25;
            } else if (percentage < 0.75f)
            {
                sr.sprite = s50;
            } else if (percentage < 1f)
            {
                sr.sprite = s75;
            } else
            {
                sr.sprite = s100;
            }
        } else
        {
            transform.position = Player.players[1].gameObject.transform.position;
            float percentage = Player.players[1].powerup_points / (float)Player.players[1].POWERUPTHRESHOLD;
            if (percentage < 0.25f)
            {
                sr.sprite = s0;
            }
            else if (percentage < 0.5f)
            {
                sr.sprite = s25;
            }
            else if (percentage < 0.75f)
            {
                sr.sprite = s50;
            }
            else if (percentage < 1f)
            {
                sr.sprite = s75;
            }
            else
            {
                sr.sprite = s100;
            }
        }
	}
}
