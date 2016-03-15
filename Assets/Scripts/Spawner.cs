using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Spawner : MonoBehaviour {
    public List<GameObject> fish_list;
    // Use this for initialization
    public float delay = 10;
    public float timer = 0;
    void Start() {
        timer = delay - 3;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > delay) {
            timer = 0;
            GameObject enemy = Instantiate(fish_list[Random.Range(0, fish_list.Count)]) as GameObject;
            float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
            float y = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
            Vector2 temp = new Vector2(x, y);
            enemy.transform.Find("RightFish").gameObject.GetComponent<Enemy>().Initialize(temp);
            enemy.transform.Find("LeftFish").gameObject.GetComponent<Enemy>().Initialize(temp);
            if (delay > 1)
                delay -= 0.3f;
        }

    }
}
