using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {
    public string bullet_prefab_name;
    public float delay_between_shots;
    public float bullet_velocity = 10f;

    protected Player weapon_player;
    float last_shot_time;
    Object bullet_prefab;
    Rigidbody2D player_rigid;
    bool added_to_fire_delegate;

	// Use this for initialization
	protected virtual void Start () {
        bullet_prefab = Resources.Load(bullet_prefab_name);
        weapon_player = Player.player_go_dict[transform.parent.gameObject];
        player_rigid = weapon_player.GetComponent<Rigidbody2D>();
        last_shot_time = 0;
        added_to_fire_delegate = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.activeSelf && !added_to_fire_delegate) {
            weapon_player.fireDelegate += Fire;
            added_to_fire_delegate = true;
        }
        updatePosition();	
	}

    protected virtual void updatePosition() {
        return;
    }

    public virtual void reset() {
        return;
    }

    public void disable() {
        weapon_player.fireDelegate -= Fire;
        added_to_fire_delegate = false;
    }

    public void Fire() { 
        if (Time.time - last_shot_time < delay_between_shots) {
            return;
        }
        GameObject new_bullet = Instantiate(bullet_prefab) as GameObject;
        new_bullet.transform.position = this.transform.position;
        new_bullet.GetComponent<Rigidbody2D>().velocity = (Vector2) this.transform.up * bullet_velocity;
        last_shot_time = Time.time;
    }
}
