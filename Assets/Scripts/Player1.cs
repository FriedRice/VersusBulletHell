using UnityEngine;
using System.Collections;

public class Player1 : Player {
    const string ENEMY_ALLY_TAG = "BearAlly";
    const string ENEMY_ALLY_BULLET_TAG = "BearAllyBullet";

    public static Player1 S;

    const float X_MIN = -4.4f;
    const float X_MAX = 4.4f;
    const float Y_MIN = -4.75f;
    const float Y_MAX = 4.75f;

    struct controls {
        public string up, vert, hor;
        public string fire1, fire2, fire3;
    };

    controls my_inputs;
    void Awake() {
        S = this;
        Player.players[0] = S;
        Player.player_go_dict[S.gameObject] = S;
        my_number = 1;
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
        idle = sprite_renderer.sprite;
        slash = Resources.Load("FishFrontSlash")as Sprite;
    }

    protected override bool isEnemyAllyTag(string tag) {
        return tag == ENEMY_ALLY_TAG || tag == ENEMY_ALLY_BULLET_TAG;
    }

    protected override void toggleInvincible(bool enable) {
        base.invincible = enable;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(ENEMY_ALLY_TAG), enable);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(ENEMY_ALLY_BULLET_TAG), enable);
    }

    protected override Vector2 getInputMovementVector() {
        Vector2 move_vector = Vector2.zero;
        move_vector.x += Input.GetAxis(my_inputs.hor);
        move_vector.y += Input.GetAxis(my_inputs.vert);

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
