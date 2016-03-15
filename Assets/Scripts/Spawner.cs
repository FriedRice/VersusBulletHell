using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Spawner : MonoBehaviour {
    public List<GameObject> fish_list;
    public List<GameObject> all_fish_left;
    public List<GameObject> all_fish_right;
    public int max_fish = 6;
    float more_fish_delay = 15;
    float more_fish_counter = 0;
    // Use this for initialization
    public float delay = 10;
    public float timer = 0;
    public bool mode_3d_left = false;
    public bool mode_3d_right = false;
    bool left_in_3d = false;
    bool right_in_3d = false;
    public float rot_3d = 80;
    public float height_3d = -1;
    GameObject left_cam,right_cam;
    void Start() {
        timer = delay - 3;
    }
    void Awake() {
        left_cam = GameObject.Find("Cam3dLeft");
        right_cam = GameObject.Find("Cam3dRight");
    }
    void Toggle3d_left() {
        left_cam.GetComponent<Cam3d>().toggle_3d(mode_3d_left);
        if (left_in_3d) {
            for (int i = all_fish_left.Count - 1; i > -1; i--) {
                if (all_fish_left[i] == null || all_fish_left[i].transform.position.y < -7) {
                    if (all_fish_left[i] != null)
                        Destroy(all_fish_left[i]);
                    all_fish_left.RemoveAt(i);
                    continue;
                }
                all_fish_left[i].transform.Rotate(Vector3.right, rot_3d);
                Vector3 temp = all_fish_left[i].transform.position;
                temp.z = 0;
                all_fish_left[i].transform.position = temp;

            }


            left_in_3d = false;
        } else {
            for (int i = all_fish_left.Count - 1; i > -1; i--) {
                if (all_fish_left[i] == null || all_fish_left[i].transform.position.y < -7) {
                    if (all_fish_left[i] != null)
                        Destroy(all_fish_left[i]);
                    all_fish_left.RemoveAt(i);
                    continue;
                }
                all_fish_left[i].transform.Rotate(Vector3.right, -rot_3d);
                Vector3 temp = all_fish_left[i].transform.position;
                temp.z = height_3d;
                all_fish_left[i].transform.position = temp;

            }
            left_in_3d = true;

        }
    }

    void Toggle3d_right() {
        right_cam.GetComponent<Cam3d>().toggle_3d(mode_3d_right);
        if (right_in_3d) {
            for (int i = all_fish_right.Count - 1; i > -1; i--) {
                if (all_fish_right[i] == null || all_fish_right[i].transform.position.y < -7) {
                    if (all_fish_right[i] != null)
                        Destroy(all_fish_right[i]);
                    all_fish_right.RemoveAt(i);
                    continue;
                }
                all_fish_right[i].transform.Rotate(Vector3.right, rot_3d);
                Vector3 temp = all_fish_right[i].transform.position;
                temp.z = 0;
                all_fish_right[i].transform.position = temp;

            }


            right_in_3d = false;
        } else {
            for (int i = all_fish_right.Count - 1; i > -1; i--) {
                if (all_fish_right[i] == null || all_fish_right[i].transform.position.y < -7) {
                    if (all_fish_right[i] != null)
                        Destroy(all_fish_right[i]);
                    all_fish_right.RemoveAt(i);
                    continue;
                }
                all_fish_right[i].transform.Rotate(Vector3.right, -rot_3d);
                Vector3 temp = all_fish_right[i].transform.position;
                temp.z = height_3d;
                all_fish_right[i].transform.position = temp;

            }
            right_in_3d = true;

        }
    }
    void MakeFish() {
        GameObject enemy = Instantiate(fish_list[Random.Range(0, fish_list.Count)]) as GameObject;
        GameObject right_fish = enemy.transform.Find("RightFish").gameObject;
        GameObject left_fish = enemy.transform.Find("LeftFish").gameObject;
        float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
        float y = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
        Vector3 temp = new Vector3(x, y, 0);

        if (all_fish_right.Count < max_fish) {
            all_fish_right.Add(right_fish);
            if (right_in_3d) {
                Vector3 temp2 = temp + Vector3.forward * height_3d;
                right_fish.transform.Rotate(Vector3.right, -rot_3d);
                right_fish.GetComponent<Enemy>().Initialize(temp2);
            } else {
                right_fish.GetComponent<Enemy>().Initialize(temp);
            }
        } else
            Destroy(right_fish);

        if (all_fish_left.Count < max_fish) {
            all_fish_left.Add(left_fish);
            if (left_in_3d) {
                Vector3 temp2 = temp + Vector3.forward * height_3d;
                left_fish.transform.Rotate(Vector3.right, -rot_3d);
                left_fish.GetComponent<Enemy>().Initialize(temp2);
            } else {
                left_fish.GetComponent<Enemy>().Initialize(temp);
            }
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
    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (mode_3d_left != left_in_3d)
            Toggle3d_left();
        if (mode_3d_right != right_in_3d)
            Toggle3d_right();
        if (timer > delay) {
            timer = 0;
            MakeFish();

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
