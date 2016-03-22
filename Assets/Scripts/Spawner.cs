using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public List<GameObject> fish_list;
    public List<GameObject> units;
    
    // Use this for initialization
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

    
    public float difficultyIncRate;
    public float startSpawnRate;
    public float spawnRateDec;
    public int maxAlliesAllowed;
    public bool ___________________;
    public int difficultyLevel;
    public float lastDiffInc;
    public float currentSpawnRate;
    public int currentAlliesAllowed;
    public float lastSpawnTime;

    void Start() {
        difficultyLevel = 0;
        currentSpawnRate = startSpawnRate;
        currentAlliesAllowed = 1;
        lastSpawnTime = -999;
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
        GameObject enemy = DifficultyManager.getEnemy(difficultyLevel);
        float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
        float y = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
        Vector3 temp = new Vector3(x, y, 0);
        units.Add(enemy);
        enemy.GetComponent<Enemy>().Initialize(temp);
        lastSpawnTime = Time.time;      
    }

    void TutorialMakeFish(int i)
    {
        GameObject enemy = Instantiate(fish_list[i]) as GameObject;
        enemy.GetComponent<Enemy>().Speed = 10f;
        float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
        float y = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
        Vector3 temp = new Vector3(x, y, 0);
        units.Add(enemy);
        enemy.GetComponent<Enemy>().Initialize(temp);
    }

    // Update is called once per frame
    void Update() {
        UpdateAllyList();
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
                lastDiffInc = 0;
                foreach (Player p in Player.players) {
                    p.reset();
                }
            }
        }
        else
        {

            if (Time.time - lastDiffInc > difficultyIncRate) IncreaseDifficulty();
            if (Time.time - lastSpawnTime < currentSpawnRate) return;
            if (units.Count >= currentAlliesAllowed) return;
            MakeFish();
        }          
            
    }

    void IncreaseDifficulty()
    {
        lastDiffInc = Time.time;
        if (difficultyLevel >= DifficultyManager.MAX_DIFFICULTY_LEVEL) return;
        difficultyLevel++;
        currentSpawnRate -= spawnRateDec;
        if (currentAlliesAllowed < maxAlliesAllowed) currentAlliesAllowed++;
    }

    void UpdateAllyList()
    {
        for (int i = units.Count - 1; i > -1; i--)
        {
            if (units[i] == null)
            {
                units.RemoveAt(i);
            }
        }
    }
}
