using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUB : MonoBehaviour {

    public static HUB S;

    public Text livesLeft, livesRight;
    

    public void UpdateLives()
    {
        int lives1 = Player.players[0].lives;
        int lives2 = Player.players[1].lives;
        livesLeft.text = "x" + lives1;
        livesRight.text = "x" + lives2;

    }

    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
        UpdateLives();
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
