using UnityEngine;
using System.Collections;

public class Player1 : Player {
    public static Player1 S;

    const KeyCode P1_MOVE_UP_KEY = KeyCode.T;
    const KeyCode P1_MOVE_LEFT_KEY = KeyCode.F;
    const KeyCode P1_MOVE_RIGHT_KEY = KeyCode.H;
    const KeyCode P1_MOVE_DOWN_KEY = KeyCode.G;
    const KeyCode P1_MOVE_SLOW_KEY = KeyCode.LeftShift;
    const KeyCode P1_FIRE_KEY = KeyCode.Z;
    const KeyCode P1_POWER_KEY = KeyCode.X;

    const float X_MIN = -8.75f;
    const float X_MAX = -0.15f;
    const float Y_MIN = -4.75f;
    const float Y_MAX = 4.75f;

    void Awake() {
        S = this;
        Player.player_go_dict[S.gameObject] = S;
    }

    protected override void Start() {
        base.Start();
        level_bounds = new Bounds(new Vector3((X_MAX + X_MIN) / 2, (Y_MAX + Y_MIN) / 2, 0f),
            new Vector3(Mathf.Abs(X_MAX - X_MIN), Mathf.Abs(Y_MAX - Y_MIN), 1f));
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

    protected override bool getInputFire() {
        return Input.GetKey(P1_FIRE_KEY);
    }

    protected override bool getInputMoveSlow() {
        return Input.GetKey(P1_MOVE_SLOW_KEY);
    }

    protected override bool getInputPower() {
        return Input.GetKey(P1_POWER_KEY);
    }
}
