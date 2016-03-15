using UnityEngine;
using System.Collections;

public class debugTrail : MonoBehaviour {
    Vector3 last_pos;
	// Use this for initialization
	void Start () {
        last_pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
      //  if(last_pos.y<transform.position.y)
        Debug.DrawLine(transform.position, last_pos, Color.red, 20);
       
        last_pos = transform.position;
	}
}
