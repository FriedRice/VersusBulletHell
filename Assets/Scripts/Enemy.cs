using UnityEngine;
using System.Collections;



public class Enemy : MonoBehaviour {
    public float health;
    public float time;
    public Vector3 spawn_position;
    public float side = 0; //-1=left,0=both,1 = right



    public void Initialize(Vector3 new_spawn_pos) {
        spawn_position = new_spawn_pos;
        transform.position = spawn_position;
      if(new_spawn_pos.y > 0)  transform.Rotate(Vector3.forward, 180);
    }

}
