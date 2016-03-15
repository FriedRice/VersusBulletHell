using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public GameObject wave;
    public GameObject warning;
    IEnumerator makewaves()
    {
        while (true)
        {
            warning.SetActive(true);
            yield return new WaitForSeconds(2f);
            int rand = Random.Range(9, 20);
            float interval = Random.Range(3f, 20f);

            for (int c = 0; c < rand; ++c)
            {
                float xpos = Random.Range(-9f, 9f);
                float ypos = Random.Range(5f, 7f);
                Instantiate(wave, new Vector3(xpos, ypos, 0f), transform.rotation);
            }
            warning.SetActive(false);
            yield return new WaitForSeconds(interval - 2f);
        }
    }

    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(makewaves());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
