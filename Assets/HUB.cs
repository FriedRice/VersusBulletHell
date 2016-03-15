using UnityEngine;
using System.Collections;

public class HUB : MonoBehaviour {

    public static HUB S;

    public GameObject[] hearts1;
    public GameObject[] hearts2;

    public void UpdateLives()
    {
    }

    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
	
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
