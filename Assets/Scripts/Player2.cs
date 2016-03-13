using UnityEngine;
using System.Collections;

public class Player2 : Player {
    public static Player2 S;
    const KeyCode P2_MOVE_UP_KEY = KeyCode.UpArrow;
    const KeyCode P2_MOVE_LEFT_KEY = KeyCode.LeftArrow;
    const KeyCode P2_MOVE_RIGHT_KEY = KeyCode.RightArrow;
    const KeyCode P2_MOVE_DOWN_KEY = KeyCode.DownArrow;

    void Awake() {
        S = this;
        Player.players[1] = S;
    }

    protected override Vector2 getInputMovementVector() {
        Vector2 move_vector = Vector2.zero;
        if (Input.GetKey(P2_MOVE_UP_KEY)) {
            move_vector.y += 1;
        }  
        if (Input.GetKey(P2_MOVE_DOWN_KEY)) {
            move_vector.y -= 1;
        }  
        if (Input.GetKey(P2_MOVE_RIGHT_KEY)) {
            move_vector.x += 1;
        }  
        if (Input.GetKey(P2_MOVE_LEFT_KEY)) {
            move_vector.x -= 1;
        }

        return move_vector.normalized;
    }
}
