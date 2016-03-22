using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : MonoBehaviour {
    const string FISH_LAYER_PREFIX = "Fish";
    const string BEAR_LAYER_PREFIX = "Bear";
    const string ALLY_LAYER_SUFFIX = "Ally";
    const string BULLET_LAYER_SUFFIX = "AllyBullet";
    const string ENEMY_BULLET_SUFFIX = "PlayerBullet";

    Color FISH_BULLET_COLOR = Color.red;
    Color BEAR_BULLET_COLOR = Color.white;

    public float base_speed = 15f;
    public float base_bullet_speed = 4f;
    public float Speed = 15f;
    public float bulletSpeed = 4f;
    public float base_animespeed = 0.5f;
    public float animspeed = 0.1f;
    public float health;
    public float time;
    public Vector3 spawn_position;
    public float side = 0; //-1=left,0=both,1 = right
    protected Color bullet_color;

    int bullet_layer;
    int block_layer;
    string bullet_tag;
    string enemy_bullet_tag;
    protected SpriteRenderer sr;
    GameObject upgradeBlock;
    GameObject powerBlock;

    void Awake() {
        upgradeBlock = (GameObject) Resources.Load("Pblue");
        powerBlock = (GameObject) Resources.Load("Pgreen");
    }

    IEnumerator Flash() {
        sr.enabled = false;
        yield return new WaitForSeconds(0.1f);
        sr.enabled = true;
    }

    public void GetHurtFlash() {
        StartCoroutine(Flash());
    }

    public void Initialize(Vector3 new_spawn_pos) {
        string layer_prefix;
        spawn_position = new_spawn_pos;
        transform.position = spawn_position;
        sr = GetComponent<SpriteRenderer>();

        if (new_spawn_pos.y < 0) {
            transform.Rotate(Vector3.forward, 180);
            layer_prefix = FISH_LAYER_PREFIX;
            enemy_bullet_tag = BEAR_LAYER_PREFIX + ENEMY_BULLET_SUFFIX;
            bullet_color = FISH_BULLET_COLOR;
            block_layer = LayerMask.NameToLayer("BearEnemyDrops");
        } else {
            layer_prefix = BEAR_LAYER_PREFIX;
            enemy_bullet_tag = FISH_LAYER_PREFIX + ENEMY_BULLET_SUFFIX;
            bullet_color = BEAR_BULLET_COLOR;
            block_layer = LayerMask.NameToLayer("FishEnemyDrops");
        }

        gameObject.tag = layer_prefix + ALLY_LAYER_SUFFIX;
        gameObject.layer = LayerMask.NameToLayer(layer_prefix + ALLY_LAYER_SUFFIX);
        bullet_tag = layer_prefix + BULLET_LAYER_SUFFIX;
        bullet_layer = LayerMask.NameToLayer(bullet_tag);
    }

    protected void setSprite(Sprite sprite) {
        sr.sprite = sprite;
        transform.Find("AllySprite").gameObject.GetComponent<AllySprite>().setSprite(sr);
    }

    protected void setBulletLayerAndTag(GameObject bullet) {
        bullet.layer = bullet_layer;
        bullet.tag = bullet_tag;

        SpriteRenderer sr = bullet.GetComponent<SpriteRenderer>();
        sr.color = bullet_color;
        bullet.transform.Find("AllySprite").gameObject.GetComponent<AllySprite>().setSprite(sr);
    }

    public void Die() {
        float ranx, rany;
        int rng = Mathf.CeilToInt(Random.Range(0f, 10f));
        for (int c = 0; c < rng; ++c) {
            ranx = Random.Range(-1f, 1f);
            rany = Random.Range(-1f, 1f);
            GameObject upgrade_block_go = Instantiate(upgradeBlock,
                new Vector3(transform.position.x + ranx, transform.position.y + rany, transform.position.z),
                transform.rotation) as GameObject;
            upgrade_block_go.layer = block_layer;

            ranx = Random.Range(-1f, 1f);
            rany = Random.Range(-1f, 1f);
            GameObject power_block_go = Instantiate(powerBlock,
                new Vector3(transform.position.x + ranx, transform.position.y + rany, transform.position.z),
                transform.rotation) as GameObject;
            power_block_go.layer = block_layer;
        }
        Destroy(gameObject);
    }

    protected bool isEnemyBulletTag(string tag) {
        return tag == enemy_bullet_tag;
    }
}
