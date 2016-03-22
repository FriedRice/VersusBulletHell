using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapon : MonoBehaviour {
    const string BULLET_LAYER_SUFFIX = "Bullet";

    Dictionary<string, Color> TAG_BULLET_COLORS = new Dictionary<string, Color>() {
        {"FishPlayer", Color.red },
        {"BearPlayer", Color.white }
    };

    public string bullet_prefab_name;
    public float delay_between_shots;
    public float bullet_velocity = 10f;
    public int bullet_layer;
    public string bullet_tag;

    protected Player weapon_player;
    float last_shot_time;
    Object bullet_prefab;
    Rigidbody2D player_rigid;
    bool added_to_fire_delegate;
    Color bullet_color;

	// Use this for initialization
	protected virtual void Start () {
        bullet_prefab = Resources.Load(bullet_prefab_name);
        weapon_player = Player.player_go_dict[transform.parent.gameObject];
        player_rigid = weapon_player.GetComponent<Rigidbody2D>();
        bullet_tag = LayerMask.LayerToName(weapon_player.gameObject.layer) + BULLET_LAYER_SUFFIX;
        bullet_layer = LayerMask.NameToLayer(bullet_tag);
        last_shot_time = 0;
        added_to_fire_delegate = false;

        bullet_color = TAG_BULLET_COLORS[weapon_player.gameObject.tag];
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
        HUB.S.PlaySound("Fire", Random.Range(0.05f, 0.4f));
        GameObject new_bullet = Instantiate(bullet_prefab) as GameObject;
        new_bullet.layer = bullet_layer;
        new_bullet.tag = bullet_tag;
        new_bullet.transform.position = this.transform.position;
        new_bullet.GetComponent<Rigidbody2D>().velocity = (Vector2) this.transform.up * bullet_velocity;
        new_bullet.GetComponent<SpriteRenderer>().color = bullet_color;
        last_shot_time = Time.time;
    }
}
