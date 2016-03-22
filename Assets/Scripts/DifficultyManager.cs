using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DifficultyManager : MonoBehaviour {

    public static int MAX_DIFFICULTY_LEVEL = 6;

    static GameObject[] enemys = new GameObject[] {
        Resources.Load("Enemy/Fish1") as GameObject,
        Resources.Load("Enemy/Squid") as GameObject,
        Resources.Load("Enemy/Octopus") as GameObject
    };

    static float[,] spawn_probabilities = new float[,] {
        { 1.0f, 0f, 0f},
        { 1.0f, 0f, 0f},
        { 0.8f, 0.2f, 0f},
        { 0.6f, 0.2f, 0.2f},
        { 0.4f, 0.3f, 0.3f},
        { 0.25f, 0.4f, 0.35f},
        { 0.2f, 0.4f, 0.4f}
    };

    static float[] difficulty_modifiers = new float[] {
        0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f, 1.1f
    };

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public GameObject getEnemy(int difficulty_level) {
        int enemy_index = getRandomEnemyIndex(difficulty_level);
        float difficulty_mod = difficulty_modifiers[difficulty_level];
        GameObject enemy = Instantiate(enemys[enemy_index]) as GameObject;
        Enemy enemy_comp = enemy.GetComponent<Enemy>();

        enemy_comp.Speed = enemy_comp.base_speed * difficulty_mod;
        enemy_comp.bulletSpeed = enemy_comp.base_bullet_speed * difficulty_mod;
        enemy_comp.animspeed = enemy_comp.base_animespeed * (1f / difficulty_mod);
        return enemy;
    }

    static int getRandomEnemyIndex(int difficulty_level) {
        float p = Random.Range(0f, 1f);
        float cummulative_probabilty = 0f;
        for (int i = 0; i < spawn_probabilities.GetLength(1); ++i) {
            cummulative_probabilty += spawn_probabilities[difficulty_level, i];
            if (cummulative_probabilty >= p) {
                return i;
            }
        }
        return -1;
    }
}
