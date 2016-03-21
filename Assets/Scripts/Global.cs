using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Global : MonoBehaviour {
    const string ALLY_LAYER_SUFFIX = "Ally";
    const string ALLY_BULLET_LAYER_SUFFIX = "AllyBullet";

    public static Global S;

    public GameObject wave;
    public GameObject warning;

    public void destroyLevelEnemies(Vector2 position, string enemy_layer_prefix) {
        GameObject[] enemy_bullets = GameObject.FindGameObjectsWithTag(enemy_layer_prefix + ALLY_BULLET_LAYER_SUFFIX);
        foreach (GameObject enemy_bullet in enemy_bullets) {
            enemy_bullet.SendMessage("Dissipate");
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemy_layer_prefix + ALLY_LAYER_SUFFIX);
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }

    IEnumerator makewaves()
    {
        while (true)
        {
            float interval = Random.Range(15f, 30f);
            yield return new WaitForSeconds(interval);
            warning.SetActive(true);
            EventIndicator.Panels.SetPanel(0);
            yield return new WaitForSeconds(2f);
            int rand = Random.Range(10, 20);

            for (int c = 0; c < rand; ++c)
            {
                float xpos = Random.Range(-9f, 9f);
                float ypos = Random.Range(5f, 7f);
                Instantiate(wave, new Vector3(xpos, ypos, 0f), transform.rotation);
            }
            warning.SetActive(false);
        }
    }

    void Awake()
    {
        Debug.Log("Displays connected: " + Display.displays.Length);
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
