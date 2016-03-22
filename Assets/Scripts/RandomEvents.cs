using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomEvents : MonoBehaviour {
	public List<GameObject> tidalWaveList;
	public GameObject tidalWave;
	public Transform leftSpawn;
	public Transform rightSpawn;
	public int maxWaves = 4;
	public float delay = 10;
	public float timer = 0;
    public static bool enableEvents;
    GameObject spawner1;
    GameObject spawner2;
    public bool player1tut;
    public bool player2tut;

	// Use this for initialization
	void Start () {
		timer = delay - 3;
        enableEvents = false;
        spawner1 = GameObject.Find("Spawner");
        spawner2 = GameObject.Find("Spawner (1)");
    }
	
	// Update is called once per frame
	void Update () {
        player1tut = spawner1.GetComponent<Spawner>().tutorial;
        player2tut = spawner2.GetComponent<Spawner>().tutorial;
        if (!player1tut && !player2tut) enableEvents = true;
        if (!enableEvents) return;
		timer += Time.deltaTime;
		if (timer > delay) {
			timer = 0;
			GameObject leftWave = Instantiate (tidalWave, leftSpawn.position, Quaternion.identity) as GameObject;
			GameObject rightWave = Instantiate(tidalWave, rightSpawn.position, Quaternion.identity) as GameObject;
			tidalWaveList.Add (leftWave);
			tidalWaveList.Add (rightWave);

			if (tidalWaveList.Count > maxWaves) {
				Destroy (tidalWaveList [0]);
				Destroy (tidalWaveList [1]);
				tidalWaveList.RemoveAt(0);
				tidalWaveList.RemoveAt(1);
			}
		}
	}
}
