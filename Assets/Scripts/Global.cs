using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Global : MonoBehaviour {
    const string ALLY_LAYER_SUFFIX = "Ally";
    const string ALLY_BULLET_LAYER_SUFFIX = "AllyBullet";

    public static Global S;

    public GameObject wave;
    public GameObject whirlpool;
    public GameObject tidalWaveWarning;
    public GameObject whirlpoolWarning;
    public bool enableEvents;
    GameObject spawner1;
    GameObject spawner2;
    public bool player1tut;
    public bool player2tut;

    public float gameTimer;

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

    IEnumerator pickRandomEvent()
    {
        while (true)
        {
            float interval = Random.Range(15f, 30f);
            yield return new WaitForSeconds(interval);
            
            int rand = Random.Range(1, 3);
            switch (rand)
            {
                case 1:
                    StartCoroutine(makewaves());
                    break;
                case 2:
                    StartCoroutine(makeWhirlpools());
                    break;
            }
        }
    }

    IEnumerator makewaves()
    {
        HUB.S.PlaySound("Siren", 1f);
        tidalWaveWarning.SetActive(true);
            EventIndicator.Panels.SetPanel(0);
            yield return new WaitForSeconds(2f);
            int rand = Random.Range(10, 20);
        HUB.S.PlaySound("TidalWaves", 1f);
        GameObject measurer = new GameObject();
        measurer.transform.position = new Vector3(0, 0, 0);
        measurer.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));
        measurer.transform.position += measurer.transform.up * 10f;
        print(measurer.transform.position);
        for (int c = 0; c < rand; ++c)
            {
            float rnjesus = Random.Range(-5f, 5f);
            Vector3 slammer = measurer.transform.position + measurer.transform.right * rnjesus;
                GameObject g = Instantiate(wave, new Vector3(slammer.x, slammer.y, 0f), transform.rotation) as GameObject;
            
                TidalWave tw = g.GetComponent<TidalWave>();
            Vector3 dir = ((Vector3.zero + Vector3.up * Random.Range(-6f,6f) + Vector3.right * Random.Range(-6f,6f) )- g.transform.position);
            tw.direction = dir;
            g.GetComponent<Rigidbody2D>().velocity = dir * Random.Range(0.9f,1.4f);
            float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            g.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90f);
        }
        Destroy(measurer);
            tidalWaveWarning.SetActive(false);
    }

    IEnumerator makeWhirlpools()
    {
        HUB.S.PlaySound("Siren", 1f);
        whirlpoolWarning.SetActive(true);
            EventIndicator.Panels.SetPanel(0);
            yield return new WaitForSeconds(2f);
            int rand = Random.Range(1, 3);

        HUB.S.PlaySound("TidalWaves", 1f);
        for (int c = 0; c < rand; ++c)
            {
                float xpos = Random.Range(-3.5f, 3.5f);
                float ypos = Random.Range(-2.5f, 2.5f);
                Instantiate(whirlpool, new Vector3(xpos, ypos, 0f), transform.rotation);
            }
            whirlpoolWarning.SetActive(false);
    }

    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
        gameTimer = 0;
        spawner1 = GameObject.Find("Spawner");
        spawner2 = GameObject.Find("Spawner (1)");
        spawner1.SetActive(false);
        spawner2.SetActive(false);
        StartCoroutine(pickRandomEvent());
        Instantiate(Resources.Load("FullFish"));
        Instantiate(Resources.Load("FullBear"));
	}
	
	// Update is called once per frame
	void Update () {
        gameTimer += Time.deltaTime;
        if(gameTimer < 1)
        {
            HUB.S.countdownLeft.text = "3";
            HUB.S.countdownRight.text = "3";
        }
        if(gameTimer >= 1 && gameTimer < 2)
        {
            HUB.S.countdownLeft.text = "2";
            HUB.S.countdownRight.text = "2";
        }
        if (gameTimer >= 2 && gameTimer < 3)
        {
            HUB.S.countdownLeft.text = "1";
            HUB.S.countdownRight.text = "1";
        }
        if (gameTimer > 3)
        {
            HUB.S.countdownLeft.text = "GO!";
            HUB.S.countdownRight.text = "GO!";
            spawner1.SetActive(true);
            spawner2.SetActive(true);
        }
        if(gameTimer > 4)
        {
            HUB.S.countdownLeft.text = "";
            HUB.S.countdownRight.text = "";
        }
        player1tut = spawner1.GetComponent<Spawner>().tutorial;
        player2tut = spawner2.GetComponent<Spawner>().tutorial;
        if (!player1tut && !player2tut) enableEvents = true;
    }
}
