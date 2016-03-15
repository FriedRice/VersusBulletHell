using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    const int NUMBER_OF_PLAYERS = 2;
    public static Dictionary<GameObject, Player> player_go_dict = new Dictionary<GameObject, Player>();

    public GameObject[] weapon_upgrades;
    public int upgrade_interval = 20;
    public int upgrade_level;
    public int upgrade_points;
    public int powerup_points;

    public float move_speed = 10f;
    protected Bounds level_bounds;
    Rigidbody2D rigid;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    // Use this for initialization
    protected virtual void Start () {
        rigid = GetComponent<Rigidbody2D>();
        upgrade_level = 0;
        for (int i = 1; i < weapon_upgrades.Length; ++i) {
            weapon_upgrades[i].SetActive(false);
        }
	}

    // Update is called once per frame
    void Update() {
        updateMovement();
        updateUpgrade();
        if (getInputFire() && fireDelegate != null) {
            fireDelegate();
        }
    }

    void updateMovement() {
        Vector2 move_vector = getInputMovementVector();
        if (!level_bounds.Contains(this.transform.position)) {
            this.transform.position = fitInLevelBounds(this.transform.position);
        } else {
            rigid.velocity = move_vector * move_speed;
        }
    }

    void updateUpgrade() {
        int current_upgrade_level = upgrade_points / upgrade_interval;
        if (current_upgrade_level == upgrade_level) {
            return;
        }

        for (int i = 0; i <= current_upgrade_level ; ++i) {
            weapon_upgrades[i].SetActive(true);
            weapon_upgrades[i].SendMessage("reset");
        }
        upgrade_level = current_upgrade_level;
    }

    public Bounds getLevelBounds() {
        return level_bounds;
    }

    public Vector2 fitInLevelBounds(Vector2 position) {
        Vector2 new_position = position;
        if (position.x > level_bounds.max.x) {
            new_position.x = level_bounds.max.x;
        } else if (position.x < level_bounds.min.x) {
            new_position.x = level_bounds.min.x;
        }

        if (position.y > level_bounds.max.y) {
            new_position.y = level_bounds.max.y;
        } else if (position.y < level_bounds.min.y) {
            new_position.y = level_bounds.min.y;
        }
        return new_position;
    }

    protected virtual Vector2 getInputMovementVector() {
        return Vector2.zero;
    }

    protected virtual bool getInputFire() {
        return false;
    }

    protected virtual bool getInputMoveSlow() {
        return false;
    }

    protected virtual bool getInputPower() {
        return false;
    }
}
