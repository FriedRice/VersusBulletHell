using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MoveTile : MonoBehaviour {

    public float speed = 1;
    public float end = -10;
    public float start = 10;
  public  List<GameObject> tiles;
    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        foreach (GameObject tile in tiles)
        {
            Vector3 temp = tile.transform.position;
            temp.y -= speed;
            if (temp.y < end)
                temp.y += start - end;
            tile.transform.position = temp;
        }
    }
}
