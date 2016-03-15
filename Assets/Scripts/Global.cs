using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
    public static Global S;

    public delegate void DestroyBullets();
    public static event DestroyBullets DestroyLeftBullets;
    public static event DestroyBullets DestroyRightBullets;

    public void DestroyLevelEnemies(Vector2 position) {
        bool on_left = position.x < 0;
        if (on_left) {
            DestroyLeftBullets();
        } else {
            DestroyRightBullets();
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            if (on_left && enemy.transform.position.x < 0) {
                Destroy(enemy);
            } else if (!on_left && enemy.transform.position.x > 0) {
                Destroy(enemy);
            }
        }
    }

    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
