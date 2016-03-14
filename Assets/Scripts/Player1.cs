using UnityEngine;
using System.Collections;

public class Player1 : Player {
    const KeyCode P1_MOVE_UP_KEY = KeyCode.T;
    const KeyCode P1_MOVE_LEFT_KEY = KeyCode.F;
    const KeyCode P1_MOVE_RIGHT_KEY = KeyCode.H;
    const KeyCode P1_MOVE_DOWN_KEY = KeyCode.G;

    public static Player1 S;

    void Awake() {
        S = this;
        Player.players[0] = S;
    }

    protected override Vector2 getInputMovementVector() {
        Vector2 move_vector = Vector2.zero;
        if (Input.GetKey(P1_MOVE_UP_KEY)) {
            move_vector.y += 1;
        }  
        if (Input.GetKey(P1_MOVE_DOWN_KEY)) {
            move_vector.y -= 1;
        }  
        if (Input.GetKey(P1_MOVE_RIGHT_KEY)) {
            move_vector.x += 1;
        }  
        if (Input.GetKey(P1_MOVE_LEFT_KEY)) {
            move_vector.x -= 1;
        }
        return move_vector.normalized;
    }
}
