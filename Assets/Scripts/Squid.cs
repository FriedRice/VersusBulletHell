using UnityEngine;
using System.Collections;

public class Squid : Enemy {
    public Sprite[] idle;
    public float animspeed = 0.1f;

    public GameObject bullet;
    public Transform ShootLoc;

    public float Speed = 15f;

    public float movementBoundXLeft, movementBoundXright, movementBoundYTop, movementBoundYBot;

    public float bulletSpeed = 4f;
    public Sprite bear_sprite;
    public GameObject greenPowerup, bluePowerup;
    public int HEALTH = 55;

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Die() {
        int rng = Mathf.CeilToInt(Random.Range(0f, 10f));
        for (int c = 0; c < rng; ++c) {
            float ranx = Random.Range(-1f, 1f);
            float rany = Random.Range(-1f, 1f);
            Instantiate(greenPowerup, new Vector3(transform.position.x + ranx, transform.position.y + rany, transform.position.z), transform.rotation);
        }
        for (int c = 0; c < rng; ++c) {
            float ranx = Random.Range(-1f, 1f);
            float rany = Random.Range(-1f, 1f);
            Instantiate(bluePowerup, new Vector3(transform.position.x + ranx, transform.position.y + rany, transform.position.z), transform.rotation);
        }
        Destroy(gameObject);
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

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(animate());

        if (transform.position.y > 0) {
            base.setSprite(bear_sprite);
        } else {
            base.setSprite(sr.sprite);
        }
        //CalculateNextPosition();
    }

    IEnumerator animate() {
        while (true) {
            float ANGLE = 0f;
            for (int c = 0; c < 25; ++c) {
                GameObject g = Instantiate(bullet, ShootLoc.transform.position, Quaternion.Euler(0f, 0f, ANGLE)) as GameObject;
                base.setBulletLayerAndTag(g);
                //g.transform.SetParent(gameObject.transform);
                g.GetComponent<Rigidbody2D>().AddForce(g.transform.up * bulletSpeed);
                ANGLE += 14.4f;
                yield return new WaitForSeconds(0.15f);
            }
            //g.GetComponent<Rigidbody2D>().AddForce(Vector2.down * bulletSpeed + Vector2.right * rng * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update() {

        if (spawn_position.y > 0) {
            rb.AddForce(Vector3.down * Time.deltaTime * Speed, ForceMode2D.Force);
        } else {
            rb.AddForce(Vector3.up * Time.deltaTime * Speed, ForceMode2D.Force);
        }


    }
}
