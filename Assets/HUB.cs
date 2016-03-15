using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUB : MonoBehaviour {

    public static HUB S;

    public Text livesLeft, livesRight;
    

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
