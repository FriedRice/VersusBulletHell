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
        int num = Player.players[0].upgrade_level + 1;
        weaponLeft.text = "Weapon Lv: " + num;
        num = Player.players[1].upgrade_level + 1;
        weaponRight.text = "Weapon Lv: " + num;
    }

    public void UpdatePowerup()
    {
        powerupLeft.text = "Powerup Progress: " + (Player.players[0].powerup_points /(float) Player.players[0].POWERUPTHRESHOLD * 100f).ToString("F2") + "%\nAttack: " + Player.players[0].PowerupName;
        powerupRight.text = "Powerup Progress: " + (Player.players[1].powerup_points / (float)Player.players[1].POWERUPTHRESHOLD * 100f).ToString("F2") + "%\nAttack: " + Player.players[1].PowerupName;

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
