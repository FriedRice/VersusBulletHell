using UnityEngine;
using System.Collections;



public class Enemy : MonoBehaviour {
    public float health;
    public float time;
    public Vector3 spawn_position;
    public float side = 0; //-1=left,0=both,1 = right



    public void Initialize(Vector3 new_spawn_pos) {
        spawn_position = new_spawn_pos;
        if (side != 0) {
           spawn_position.x = spawn_position.x * side;
        }
        transform.position = spawn_position;
    }

}
