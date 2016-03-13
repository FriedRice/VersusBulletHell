using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    const int NUMBER_OF_PLAYERS = 2;
    public static Player[] players = new Player[NUMBER_OF_PLAYERS];

    public float move_speed = 10f;

    Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {
        updateMovement();
    }

    void updateMovement() {
        Vector2 move_vector = getInputMovementVector();
        rigid.velocity = move_vector * move_speed;
    }

    protected virtual Vector2 getInputMovementVector() {
        return Vector2.zero;
    }
}
