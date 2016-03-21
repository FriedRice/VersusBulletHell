using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {
    public bool FollowsFish = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (FollowsFish)
        {
            transform.position = Camera.allCameras[0].WorldToScreenPoint(Player.players[0].transform.position);
        } else
        {

            transform.position = Camera.allCameras[1].WorldToScreenPoint(Player.players[1].transform.position);
        }
	}
}
