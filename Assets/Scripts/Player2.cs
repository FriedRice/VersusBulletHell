using UnityEngine;
using System.Collections;

public class Player2 : Player {
    public static Player2 S;

    const KeyCode P2_MOVE_UP_KEY = KeyCode.UpArrow;
    const KeyCode P2_MOVE_LEFT_KEY = KeyCode.LeftArrow;
    const KeyCode P2_MOVE_RIGHT_KEY = KeyCode.RightArrow;
    const KeyCode P2_MOVE_DOWN_KEY = KeyCode.DownArrow;
    const KeyCode P2_MOVE_SLOW_KEY = KeyCode.Period;
    const KeyCode P2_FIRE_KEY = KeyCode.Slash;
    const KeyCode P2_POWER_KEY = KeyCode.RightShift;

    const float X_MIN = 0.15f;
    const float X_MAX = 8.75f;
    const float Y_MIN = -4.75f;
    const float Y_MAX = 4.75f;

    void Awake() {
        S = this;
        Player.players[1] = S;
        Player.player_go_dict[S.gameObject] = S;
    }

    protected override void Start() {
        base.Start();
        level_bounds = new Bounds(new Vector3((X_MAX + X_MIN) / 2, (Y_MAX + Y_MIN) / 2, 0f),
            new Vector3(Mathf.Abs(X_MAX - X_MIN), Mathf.Abs(Y_MAX - Y_MIN), 1f));
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

    protected override bool getInputFire() {
        return Input.GetKey(P2_FIRE_KEY);
    }

    protected override bool getInputMoveSlow() {
        return Input.GetKey(P2_MOVE_SLOW_KEY);
    }

    protected override bool getInputPower() {
        return Input.GetKey(P2_POWER_KEY);
    }
}
