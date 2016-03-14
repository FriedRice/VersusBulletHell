using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    const int NUMBER_OF_PLAYERS = 2;
    public static Dictionary<GameObject, Player> player_go_dict = new Dictionary<GameObject, Player>();

    public float move_speed = 10f;
    Rigidbody2D rigid;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {
        updateMovement();
        if (getInputFire() && fireDelegate != null) {
            fireDelegate();
        }
    }

    void updateMovement() {
        Vector2 move_vector = getInputMovementVector();
        rigid.velocity = move_vector * move_speed;
    }

    public virtual Bounds getLevelBounds() {
        return new Bounds();
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
