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

	// Use this for initialization
	void Start () {
		timer = delay - 3;
	}
	
	// Update is called once per frame
	void Update () {
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
