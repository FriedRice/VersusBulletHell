using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Fish1 : Enemy {
    public Sprite[] idle;

    public GameObject bullet;
    public Transform ShootLoc;

    public float movementBoundXLeft, movementBoundXright, movementBoundYTop, movementBoundYBot;

    public Sprite bear_sprite;
    public Vector3 nextdest;

    Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
        CalculateNextPosition();
        rb = GetComponent<Rigidbody2D>();

        if (transform.position.y > 0) {
            base.setSprite(bear_sprite);
        } else {
            base.setSprite(sprite_renderer.sprite);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (base.isEnemyBulletTag(collision.gameObject.tag))
        {
            GetHurtFlash();
            HEALTH -= 1;
            Destroy(collision.gameObject);
            if (HEALTH <= 0) {
                Die();
            }
        }
    }

    IEnumerator animate() {
        while (true) {
            bool foxes_have_animation = false;
            if (foxes_have_animation) {
                for (int c = 0; c < idle.Length; ++c) {
                    sr.sprite = idle[c];
                    yield return new WaitForSeconds(animspeed);
                }

                for (int c = idle.Length - 2; c > 0; --c) {
                    sr.sprite = idle[c];
                    yield return new WaitForSeconds(animspeed);
                }
            } else {
                yield return new WaitForSeconds(animspeed);
            }

            float rng = Random.Range(-1f, 1f);
            GameObject g = Instantiate(bullet, ShootLoc.transform.position, transform.rotation) as GameObject;
            base.setBulletLayerAndTag(g);
            //g.transform.SetParent(gameObject.transform);
            Vector2 shoot_dir = Vector2.zero;
            shoot_dir.y = transform.up.y * -1;

            g.GetComponent<Rigidbody2D>().AddForce(shoot_dir * bulletSpeed + Vector2.right * rng * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    void CalculateNextPosition() {
        float x = Random.Range(movementBoundXLeft, movementBoundXright);
        float y = Random.Range(movementBoundYBot, movementBoundYTop);
        nextdest = new Vector3(x, y, transform.position.z);
    }

    // Update is called once per frame
    void Update() {
        if ((transform.position - nextdest).magnitude < 0.2) {
            CalculateNextPosition();
        }
        Vector3 to_point = nextdest - transform.position;
        if (to_point.magnitude > 0.5) {
            transform.position += Vector3.Normalize(to_point) / 1000 * Speed;
        } else {
            transform.position += Vector3.Normalize(to_point) * to_point.magnitude / 2f / 1000 * Speed;
        }
    }

    void FixedUpdate() {
        // rb.AddForce( Vector3.Normalize(nextdest - transform.position)  * Speed, ForceMode2D.Force);
    }
}
