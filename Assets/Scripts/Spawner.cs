using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public List<GameObject> fish_list;
    public List<GameObject> units;
    public int max_fish = 6;
    float more_fish_delay = 15;
    float more_fish_counter = 0;
    // Use this for initialization
    public float delay = 10;
    public float timer = 0;
    public bool tutorial = true;
    public int tutFishCount = 0;
    public bool mode_3d_left = false;
    public bool mode_3d_right = false;
    bool left_in_3d = false;
    bool right_in_3d = false;
    public float rot_3d = 80;
    public float height_3d = -1;
    GameObject left_cam, right_cam;
    const float BOUND_X = 5;
    const float BOUND_Y = 10;

    void Start() {
        timer = delay - 3;
    }
    void Awake() {
        left_cam = GameObject.Find("CamLeft");
        right_cam = GameObject.Find("CamRight");
    }

    /*void Toggle3d_left() {
        left_cam.GetComponent<Cam3d>().toggle_3d(mode_3d_left);
        if (left_in_3d) {
            for (int i = units.Count - 1; i > -1; i--) {
                if (units[i] == null || units[i].transform.position.y < -7) {
                    if (units[i] != null)
                        Destroy(units[i]);
                    units.RemoveAt(i);
                    continue;
                }
                units[i].transform.Rotate(Vector3.right, rot_3d);
                Vector3 temp = units[i].transform.position;
                temp.z = 0;
                units[i].transform.position = temp;

            }


            left_in_3d = false;
        } else {
            for (int i = units.Count - 1; i > -1; i--) {
                if (units[i] == null || units[i].transform.position.y < -7) {
                    if (units[i] != null)
                        Destroy(units[i]);
                    units.RemoveAt(i);
                    continue;
                }
                units[i].transform.Rotate(Vector3.right, -rot_3d);
                Vector3 temp = units[i].transform.position;
                temp.z = height_3d;
                units[i].transform.position = temp;

            }
            left_in_3d = true;

        }
    }
    void Toggle3d_right() {
        right_cam.GetComponent<Cam3d>().toggle_3d(mode_3d_right);
        if (right_in_3d) {
            for (int i = all_bear.Count - 1; i > -1; i--) {
                if (all_bear[i] == null || all_bear[i].transform.position.y < -7) {
                    if (all_bear[i] != null)
                        Destroy(all_bear[i]);
                    all_bear.RemoveAt(i);
                    continue;
                }
                all_bear[i].transform.Rotate(Vector3.right, rot_3d);
                Vector3 temp = all_bear[i].transform.position;
                temp.z = 0;
                all_bear[i].transform.position = temp;

            }


            right_in_3d = false;
        } else {
            for (int i = all_bear.Count - 1; i > -1; i--) {
                if (all_bear[i] == null || all_bear[i].transform.position.y < -7) {
                    if (all_bear[i] != null)
                        Destroy(all_bear[i]);
                    all_bear.RemoveAt(i);
                    continue;
                }
                all_bear[i].transform.Rotate(Vector3.right, -rot_3d);
                Vector3 temp = all_bear[i].transform.position;
                temp.z = height_3d;
                all_bear[i].transform.position = temp;

            }
            right_in_3d = true;

        }
    }
     */

    void MakeFish() {
       // print("making fish");
        GameObject enemy = Instantiate(fish_list[Random.Range(0, fish_list.Count)]) as GameObject;
        float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
        float y = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
        Vector3 temp = new Vector3(x, y, 0);

        if (units.Count < max_fish) {
            units.Add(enemy);
            enemy.GetComponent<Enemy>().Initialize(temp);
            /*
            if (left_in_3d) {
                Vector3 temp2 = temp + Vector3.forward * height_3d;
                left_fish.transform.Rotate(Vector3.right, -rot_3d);
                left_fish.GetComponent<Enemy>().Initialize(temp2);
            } else {
                left_fish.GetComponent<Enemy>().Initialize(temp);
            }
             */
        }
        else {
            Destroy(enemy);
        }

        if (delay > 1) {
            delay -= 0.3f;
        }

        if (more_fish_counter < more_fish_delay) {
            more_fish_counter++;
        }
        else {
            //  max_fish++;
            more_fish_counter = 0;
        }
    }

    void TutorialMakeFish(int i)
    {
        // print("making fish");
        GameObject enemy = Instantiate(fish_list[i]) as GameObject;

        switch (i)
        {
            case 0:
                enemy.GetComponent<Fish1>().Speed = 10f;
                break;
            case 1:
                enemy.GetComponent<Squid>().Speed = 10f;
                break;
            case 2:
                enemy.GetComponent<Octopus>().Speed = 10f;
                break;
        }
            
        float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
        float y = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
        Vector3 temp = new Vector3(x, y, 0);
        units.Add(enemy);
        enemy.GetComponent<Enemy>().Initialize(temp);
        
        //if (delay > 1)
        //{
        //    delay -= 0.3f;
        //}

    }

    // Update is called once per frame
    void Update() {
        if (tutorial)
        {
            if (units.Count == 0)
            {
                if (tutFishCount < 3)
                {
                    TutorialMakeFish(tutFishCount);
                    tutFishCount++;
                }
            }
            if (tutFishCount == 3 && units.Count == 0)
            {
                tutorial = false;
            }
        }
        //else
        //{
            timer += Time.deltaTime;
            if (timer > delay)
            {
                timer = 0;
                if(!tutorial) MakeFish();
            }
            for (int i = units.Count - 1; i > -1; i--)
            {
                if (units[i] == null || Mathf.Abs(units[i].transform.position.y) > BOUND_Y)
                {
                    if (units[i] != null)
                        Destroy(units[i]);
                    units.RemoveAt(i);
                }
            }
        //}
    }
}
