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
    int my_number = 1;
    struct controls {
        public string up, vert, hor;
        public string fire1, fire2, fire3;
    };
    controls my_inputs;
    void Awake() {
        S = this;
        Player.players[0] = S;
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
        move_vector.x += Input.GetAxis(my_inputs.hor);
        move_vector.y += Input.GetAxis(my_inputs.vert);

    /*
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
     * */
        //return move_vector.normalized;
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
