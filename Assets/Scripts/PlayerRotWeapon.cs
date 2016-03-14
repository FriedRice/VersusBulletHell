using UnityEngine;
using System.Collections;

public class PlayerRotWeapon : PlayerWeapon {
    public float rotations_per_minute = 20f;
    public float max_width = 0.5f;
    public float start_position = 1f;

    float current_angle = 0f; // in radians
    Bounds level_bounds;

    protected override void Start() {
        base.Start();
        current_angle = Mathf.PI * start_position;
        level_bounds = weapon_player.getLevelBounds();
    }

    protected override void updatePosition() {
        current_angle += ((2 * Mathf.PI) / 60.0f) * rotations_per_minute * Time.deltaTime;
        if (current_angle > (2 * Mathf.PI)) {
            current_angle %= (2 * Mathf.PI);
        }

        this.transform.localPosition = new Vector2(Mathf.Cos(current_angle) * max_width, this.transform.localPosition.y);
        if (!level_bounds.Contains(this.transform.position)) {
            if (this.transform.position.x - level_bounds.center.x > 0f) {
                this.transform.position = new Vector2(level_bounds.max.x, this.transform.position.y);
            } else {
                this.transform.position = new Vector2(level_bounds.min.x, this.transform.position.y);
            }
        }
    }

    public override void reset() {
        current_angle = Mathf.PI * start_position;
    }
}
