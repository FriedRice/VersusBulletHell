using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUB : MonoBehaviour {

    public static HUB S;

    public Text livesLeft, livesRight, weaponLeft, weaponRight, powerupLeft, powerupRight;
    

    public void UpdateLives()
    {
        int lives1 = Player.players[0].lives;
        int lives2 = Player.players[1].lives;
        livesLeft.text = "x" + lives1;
        livesRight.text = "x" + lives2;

    }

    public void UpdateWeapon()
    {
        int num = Player.players[0].upgrade_points;
        weaponLeft.text = "POWER: " + num;
        num = Player.players[1].upgrade_points;
        weaponRight.text = "POWER: " + num;
    }

    public void UpdatePowerup()
    {
        int num = Player.players[0].powerup_points;
        powerupLeft.text = "POWER: " + num;
        num = Player.players[1].powerup_points;
        powerupRight.text = "POWER: " + num;
    }

    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
        UpdateLives();
        UpdateWeapon();
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
