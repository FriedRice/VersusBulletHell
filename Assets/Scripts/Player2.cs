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

    const float X_MIN = -4.4f;
    const float X_MAX = 4.4f;
    const float Y_MIN = -4.75f;
    const float Y_MAX = 4.75f;
    int my_number = 2;
    struct controls {
        public string up, vert, hor;
        public string fire1, fire2, fire3;
    };
    controls my_inputs;
    void Awake() {
        S = this;
        Player.players[1] = S;
        Player.player_go_dict[S.gameObject] = S;
    }

    protected override void Start() {
        base.Start();
        level_bounds = new Bounds(new Vector3((X_MAX + X_MIN) / 2, (Y_MAX + Y_MIN) / 2, 0f),
            new Vector3(Mathf.Abs(X_MAX - X_MIN), Mathf.Abs(Y_MAX - Y_MIN), 1f));
        my_inputs.vert = string.Format("Vertical{0}", my_number);
        my_inputs.hor = string.Format("Horizontal{0}", my_number);
        my_inputs.fire1 = string.Format("Fire1{0}", my_number);
        my_inputs.fire2 = string.Format("Fire2{0}", my_number);
        my_inputs.fire3 = string.Format("Fire3{0}", my_number);
    }

    protected override Vector2 getInputMovementVector() {
        Vector2 move_vector = Vector2.zero;
        move_vector.x += Input.GetAxis(my_inputs.hor)*-1;
        move_vector.y += Input.GetAxis(my_inputs.vert)*-1;
/*
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
 * */
        return move_vector;
    }

    protected override bool getInputFire() {
        return //Input.GetKey(P1_FIRE_KEY);
        Input.GetAxis(my_inputs.fire2) > 0;

    }

    protected override bool getInputMoveSlow() {
        return Input.GetAxis(my_inputs.fire1) > 0;
    }

    protected override bool getInputPower() {
        return Input.GetAxis(my_inputs.fire3) > 0;
    }
}
