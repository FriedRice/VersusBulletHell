using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : MonoBehaviour {
    const string FISH_LAYER_PREFIX = "Fish";
    const string BEAR_LAYER_PREFIX = "Bear";
    const string ALLY_LAYER_SUFFIX = "Ally";
    const string BULLET_LAYER_SUFFIX = "AllyBullet";
    const string ENEMY_BULLET_SUFFIX = "PlayerBullet";

    public float health;
    public float time;
    public Vector3 spawn_position;
    public float side = 0; //-1=left,0=both,1 = right
    int bullet_layer;
    string bullet_tag;
    string enemy_bullet_tag;

    public void Initialize(Vector3 new_spawn_pos) {
        string layer_prefix;
        spawn_position = new_spawn_pos;
        transform.position = spawn_position;

        if (new_spawn_pos.y < 0) {
            transform.Rotate(Vector3.forward, 180);
            layer_prefix = FISH_LAYER_PREFIX;
            enemy_bullet_tag = BEAR_LAYER_PREFIX + ENEMY_BULLET_SUFFIX;
        } else {
            layer_prefix = BEAR_LAYER_PREFIX;
            enemy_bullet_tag = FISH_LAYER_PREFIX + ENEMY_BULLET_SUFFIX;
        }

        gameObject.tag = layer_prefix + ALLY_LAYER_SUFFIX;
        gameObject.layer = LayerMask.NameToLayer(layer_prefix + ALLY_LAYER_SUFFIX);
        bullet_tag = layer_prefix + BULLET_LAYER_SUFFIX;
        bullet_layer = LayerMask.NameToLayer(bullet_tag);
    }

    protected void setBulletLayerAndTag(GameObject bullet) {
        bullet.layer = bullet_layer;
        bullet.tag = bullet_tag;
    }

    protected bool isEnemyBulletTag(string tag) {
        return tag == enemy_bullet_tag;
    }
}
