using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Spawner : MonoBehaviour {
    public List<GameObject> fish_list;
    public List<GameObject> all_fish_left;
    public List<GameObject> all_fish_right;
    public int max_fish = 6;
    float more_fish_delay = 5;
    float more_fish_counter = 0;
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
            GameObject right_fish = enemy.transform.Find("RightFish").gameObject;
            GameObject left_fish = enemy.transform.Find("LeftFish").gameObject;
            float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
            float y = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
            Vector2 temp = new Vector2(x, y);

            if (all_fish_right.Count < max_fish) {
                all_fish_right.Add(right_fish);
                right_fish.GetComponent<Enemy>().Initialize(temp);
            } else
                Destroy(right_fish);

            if (all_fish_left.Count < max_fish) {
                all_fish_left.Add(left_fish);
                left_fish.GetComponent<Enemy>().Initialize(temp);
            } else
                Destroy(left_fish);
            if (delay > 1)
                delay -= 0.3f;
            if (more_fish_counter < more_fish_delay)
                more_fish_counter++;
            else {
              //  max_fish++;
                more_fish_counter = 0;
            }
        }
        for (int i = all_fish_left.Count - 1; i > -1; i--) {
            if (all_fish_left[i] == null || all_fish_left[i].transform.position.y < -7) {
                if (all_fish_left[i] != null)
                    Destroy(all_fish_left[i]);
                all_fish_left.RemoveAt(i);
            }

        }
        for (int i = all_fish_right.Count - 1; i > -1; i--) {
            if (all_fish_right[i] == null || all_fish_right[i].transform.position.y < -7) {
                if (all_fish_right[i] != null)
                    Destroy(all_fish_right[i]);
                all_fish_right.RemoveAt(i);
            }
        }

    }
}
