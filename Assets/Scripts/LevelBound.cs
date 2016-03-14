using UnityEngine;
using System.Collections;

public class LevelBound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "PlayerBullet" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "EnemyBullet") {
            Destroy(coll.gameObject);
        }
    }
}
