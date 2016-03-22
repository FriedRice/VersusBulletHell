using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    const int NUMBER_OF_PLAYERS = 2;
    public static Dictionary<GameObject, Player> player_go_dict = new Dictionary<GameObject, Player>();
    public static Player[] players = new Player[NUMBER_OF_PLAYERS];

    public GameObject[] weapon_upgrades;
    public int lives = 3;
    public int upgrade_interval = 20;
    public float blink_time = 3f;
    public float blink_interval = 0.2f;
    public int upgrade_level;
    public int upgrade_points;
    public int powerup_points;
    public bool invincible;
    public float katana_duration = 15f;
    public bool immortal = false;

    public bool controlsAreReversed = false;
    public float move_speed = 10f;
    public float move_slow_speed = 4f;
    public GameObject hit_box_marker;
    public GameObject other_side_sprite;
    protected Bounds level_bounds;
    Rigidbody2D rigid;
    protected SpriteRenderer sprite_renderer;
    SpriteRenderer other_side_sprite_renderer;
    float blink_start_time;
    string enemy_layer_prefix;
    Color base_sprite_color;
    bool ripper_mode = false;
    bool slashing = false;
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;
    protected GameObject other_side;
    public AudioClip fireSound1;
    public AudioClip fireSound2;
    public AudioClip powerupSound;
    public AudioClip upgradeSound;
    protected int my_number;
    public Sprite idle, idle_back, twinkle, slash, slash_back;
    public List<string> Enemy_tags; //enemy player followed by enemy minions followed by enemy bullets

    // Use this for initialization
    protected virtual void Start() {
        rigid = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        other_side_sprite_renderer = other_side_sprite.GetComponent<SpriteRenderer>();
        other_side = transform.Find("other_side").gameObject;
        upgrade_level = 0;
        invincible = false;
        blink_start_time = -9999999;
        hit_box_marker.SetActive(false);
        base_sprite_color = sprite_renderer.color;
        for (int i = 1; i < weapon_upgrades.Length; ++i) {
            weapon_upgrades[i].SetActive(false);
        }

        if (gameObject.tag.StartsWith("Fish")) {
            enemy_layer_prefix = "Bear";
        } else {
            enemy_layer_prefix = "Fish";
        }
        if (immortal)
            invincible = true;
    }

    // Update is called once per frame
    void Update() {
        updateMovement();
        updateUpgrade();

        if (getInputFire() && fireDelegate != null) {
            if (!SoundManager.instance.efxSource.isPlaying) {
                SoundManager.instance.RandomizeSfx(fireSound1, fireSound2);
            }
            fireDelegate();
        }

    }

    public int POWERUPTHRESHOLD = 25;
    public bool hasPowerup = false;
    public string PowerupName = "None";
    public GameObject LASER;
    void updateMovement() {
        Vector2 move_vector = getInputMovementVector();
        if (controlsAreReversed) {
            move_vector = -move_vector;
        }
        if (!level_bounds.Contains(this.transform.position)) {
            this.transform.position = fitInLevelBounds(this.transform.position);
            rigid.velocity = Vector2.zero;
        } else {
            if (getInputMoveSlow()) {
                rigid.velocity = move_vector * move_slow_speed;
                if (!hit_box_marker.activeSelf) {
                    hit_box_marker.SetActive(true);
                }
            } else {
                rigid.velocity = move_vector * move_speed;
                if (hit_box_marker.activeSelf) {
                    hit_box_marker.SetActive(false);
                }
            }
        }


        if (powerup_points >= POWERUPTHRESHOLD && !hasPowerup) {
            hasPowerup = true;
            int rng = Random.Range(-1, 2);
        //    rng = 2;
            if (rng == 0) {
                PowerupName = "Laser";
            } else if (rng == 1) { PowerupName = "Reverse Controls"; } else {
                PowerupName = "Katana";
            }
        } else {
            if (powerup_points < POWERUPTHRESHOLD) {
                hasPowerup = false;
                PowerupName = "None";
            }
        }

        if (getInputPower()) {
            if (hasPowerup) {
                if (PowerupName == "Laser") {
                    FireLaser();
                } else if (PowerupName == "Reverse Controls") {
                    ReverseControlsOther();
                } else if (PowerupName == "Katana") {
                    enableKatana();
                }
            }
        }
        HUB.S.UpdatePowerup();
    }

    public void reset() {
        for (int i = 1; i <= upgrade_level; ++i) {
            weapon_upgrades[i].SendMessage("disable");
            weapon_upgrades[i].SetActive(false);
        }
        upgrade_points = 0;
        upgrade_level = 0;
        powerup_points = 0;
    }

    void ReverseControlsOther() {
        HUB.S.PlaySound("Discord", 1f);
        PowerupName = "None";
        powerup_points = 0;
        hasPowerup = false;
        if (this == players[0]) {
            HUB.S.FishUsedPowerupEffect();
            // u r fish
            GameObject g = Instantiate(Resources.Load("Reverser"), transform.position, transform.rotation) as GameObject;
            g.GetComponent<ReverseControls>().Fish = false;
        } else {
            HUB.S.BearUsedPowerupEffect();
            // u r bear
            GameObject g = Instantiate(Resources.Load("Reverser"), transform.position, transform.rotation) as GameObject;
            g.GetComponent<ReverseControls>().Fish = true;
        }
    }

    void FireLaser() {
        PowerupName = "None";
        powerup_points = 0;
        hasPowerup = false;
        GameObject g = Instantiate(LASER, new Vector3(transform.position.x, 0, 0), transform.rotation) as GameObject;
        if (this == players[0]) {
            // u r fish
            g.GetComponent<Laser>().ISBEAR = false;
            HUB.S.FishUsedPowerupEffect();
            g.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = Color.red;
        } else {
            // u r ber
            g.GetComponent<Laser>().ISBEAR = true;
            HUB.S.BearUsedPowerupEffect();
        }
    }

    void updateUpgrade() {
        int current_upgrade_level = Mathf.Min(upgrade_points / upgrade_interval, weapon_upgrades.Length - 1);
        if (current_upgrade_level == upgrade_level) {
            return;
        }

        for (int i = 0; i <= current_upgrade_level; ++i) {
            weapon_upgrades[i].SetActive(true);
            weapon_upgrades[i].SendMessage("reset");
        }
        upgrade_level = current_upgrade_level;
    }

    public Bounds getLevelBounds() {
        return level_bounds;
    }

    public Vector2 fitInLevelBounds(Vector2 position) {
        Vector2 new_position = position;
        if (position.x > level_bounds.max.x) {
            new_position.x = level_bounds.max.x;
        } else if (position.x < level_bounds.min.x) {
            new_position.x = level_bounds.min.x;
        }

        if (position.y > level_bounds.max.y) {
            new_position.y = level_bounds.max.y;
        } else if (position.y < level_bounds.min.y) {
            new_position.y = level_bounds.min.y;
        }
        return new_position;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "UpgradeBlock") {
            HUB.S.PlaySound("PowerupGet", 0.1f);
            ++upgrade_points;
            Destroy(coll.gameObject);
        } else if (coll.gameObject.tag == "PowerupBlock") {
            HUB.S.PlaySound("PowerupGet", 0.1f);
            ++powerup_points;
            HUB.S.UpdateWeapon();
            Destroy(coll.gameObject);
        } else if (isEnemyAllyTag(coll.gameObject.tag) && !invincible) {
            loseLife();
        }
    }

    public void loseLife() {
        Global.S.destroyLevelEnemies(this.transform.position, enemy_layer_prefix);
        --lives;
        HUB.S.UpdateLives();
        for (int i = 1; i <= upgrade_level; ++i) {
            weapon_upgrades[i].SendMessage("disable");
            weapon_upgrades[i].SetActive(false);
        }
        upgrade_points = 0;
        upgrade_level = 0;
        HUB.S.PlaySound("BearDies", 1f);
        if (this == players[0]) {
            // fish
            GameObject g = Instantiate(Resources.Load("LoseLife")) as GameObject;
            g.GetComponent<PlayerFollowLife>().FollowsFish = true;
        } else {
            GameObject g = Instantiate(Resources.Load("LoseLife")) as GameObject;
            g.GetComponent<PlayerFollowLife>().FollowsFish = false;

        }

        if (lives == 0) {
            Destroy(SoundManager.instance.gameObject);
            if (this == players[0]) {
                // bear win
                Application.LoadLevel(3);
            } else {
                Application.LoadLevel(2);
            }
        } else {
            if ((Time.time - blink_start_time) < blink_time) {
                blink_start_time = Time.time;
            } else {
                toggleInvincible(true);
                blink_start_time = Time.time;
                StartCoroutine(blinkAvatar());
            }
        }
    }

    IEnumerator blinkAvatar() {
        Color sprite_color = base_sprite_color;
        while ((Time.time - blink_start_time) < blink_time) {
            if (sprite_renderer.color.a == 0) {
                sprite_color.a = 255;
            } else {
                sprite_color.a = 0;
            }
            sprite_renderer.color = sprite_color;
            other_side_sprite_renderer.color = sprite_color;
            yield return new WaitForSeconds(blink_interval);
        }

        sprite_renderer.color = base_sprite_color;
        other_side_sprite_renderer.color = base_sprite_color;
        toggleInvincible(false);
    }

    protected virtual void toggleInvincible(bool enable) {
        return;
    }

    void enableKatana() {
        if (!ripper_mode) {
            ripper_mode = true;
            if (this == players[0]) {
                HUB.S.FishUsedPowerupEffect();
                // u r fish
            } else {
                HUB.S.BearUsedPowerupEffect();
                // u r bear
            }
            Invoke("disableKatana", katana_duration);
        } else {
            HUB.S.PlaySound("Sharpen", Random.Range(0.5f, 1f));
            beginKatanaSlash();
        }
    }
    void beginKatanaSlash() {
        if (!slashing) {
            slashing = true;
            invincible = true;
            other_side.GetComponent<SpriteRenderer>().sprite = twinkle;
            Invoke("performSlash", 0.1f);
        }
    }

    void performSlash() {
        // GameObject katana = transform.Find("katana").gameObject;
        //    katana.transform.rot
        //  katana.GetComponent<BoxCollider2D>().enabled = true;
        other_side.GetComponent<CircleCollider2D>().enabled = true;
        List<GameObject> enemies = new List<GameObject>();
        enemies.Add(GameObject.FindGameObjectWithTag(Enemy_tags[0]));
       
        transform.Rotate(Vector3.up, 180);
        foreach (string tag in Enemy_tags) {
            GameObject[] enemy_objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject enemy_object in enemy_objects)
                enemies.Add(enemy_object);
        }
        for (int i = enemies.Count - 1; i > 0; i--) {
            if (other_side.GetComponent<CircleCollider2D>().bounds.Contains(enemies[i].transform.position)) {
                GameObject target = enemies[i].gameObject;
                if (target.tag == Enemy_tags[0]) {
                    target.GetComponent<Player>().loseLife();
                } else if (target.tag == Enemy_tags[1])
                    target.GetComponent<Enemy>().Die();
                else if (target.tag == Enemy_tags[2])
                    target.GetComponent<BulletSprite>().Dissipate();
            }
        }
        other_side.GetComponent<SpriteRenderer>().sprite = slash;
        sprite_renderer.sprite = slash_back;
        Invoke("idleAnimation", 0.2f);
    }
    void disableKatana() {
        PowerupName = "None";
        powerup_points = 0;
        hasPowerup = false;
        ripper_mode = false;
        if (my_number == 1) {
            transform.rotation.Set(0, 0, 0, 0);
            other_side.transform.rotation.Set(0, 0, 180, 0);
        } else {
            transform.rotation.Set(0, 0, 180, 0);
            other_side.transform.rotation.Set(0, 0, 180, 0);
        }
    }
    void idleAnimation() {
        slashing = false;
        other_side.GetComponent<CircleCollider2D>().enabled = false;
        other_side.GetComponent<SpriteRenderer>().sprite = idle_back;
        if (!immortal)
            invincible = false;
        sprite_renderer.sprite = idle;
    }

    protected virtual bool isEnemyAllyTag(string tag) {
        return false;
    }

    protected virtual Vector2 getInputMovementVector() {
        return Vector2.zero;
    }

    protected virtual bool getInputFire() {
        return false;
    }

    protected virtual bool getInputMoveSlow() {
        return false;
    }

    protected virtual bool getInputPower() {
        return false;
    }
}
