﻿using UnityEngine;
using System.Collections;

public class Fish1 : Enemy {
    public Sprite[] idle;
    public float animspeed = 0.1f;

    public GameObject bullet;
    public Transform ShootLoc;

    public float Speed = 15f;

    public float movementBoundXLeft, movementBoundXright, movementBoundYTop, movementBoundYBot;

    public float bulletSpeed = 4f;


    Rigidbody2D rb;
    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
        CalculateNextPosition();
        rb = GetComponent<Rigidbody2D>();
	}
    void Die()
    {
        int rng = Mathf.CeilToInt(Random.Range(0f, 10f));
        for (int c = 0; c < rng; ++c)
        {
            float ranx = Random.Range(-1f, 1f);
            float rany = Random.Range(-1f, 1f);
            Instantiate(greenPowerup, new Vector3(transform.position.x + ranx, transform.position.y + rany, transform.position.z), transform.rotation);
        }
        for (int c = 0; c < rng; ++c)
        {
            float ranx = Random.Range(-1f, 1f);
            float rany = Random.Range(-1f, 1f);
            Instantiate(bluePowerup, new Vector3(transform.position.x + ranx, transform.position.y + rany, transform.position.z), transform.rotation);
        }
        Destroy(gameObject);
    }
    public int HEALTH = 15;
    public GameObject greenPowerup, bluePowerup;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            HEALTH -= 1;
            Destroy(collision.gameObject);
            if (HEALTH <= 0)
            {
                Die();
            }
        }
    }
    IEnumerator animate()
    {
        while (true)
        {
            for(int c = 0; c < idle.Length; ++c)
            {
                sr.sprite = idle[c];
                yield return new WaitForSeconds(animspeed);
            }

            for (int c = idle.Length - 2; c > 0; --c)
            {
                sr.sprite = idle[c];
                yield return new WaitForSeconds(animspeed);
            }
            float rng = Random.Range(-1f, 1f);
            GameObject g = Instantiate(bullet, ShootLoc.transform.position, transform.rotation) as GameObject;
            g.transform.SetParent(gameObject.transform);
            g.GetComponent<Rigidbody2D>().AddForce(Vector2.down * bulletSpeed + Vector2.right * rng * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    public Vector3 nextdest;

    void CalculateNextPosition()
    {
        float x = Random.Range(movementBoundXLeft, movementBoundXright);
        float y = Random.Range(movementBoundYBot, movementBoundYTop);
        nextdest = new Vector3(x, y, transform.position.z);
        if (side == -1) {
            nextdest = transform.parent.Find("RightFish").gameObject.GetComponent<Fish1>().nextdest;
            nextdest.x = nextdest.x * -1;
        }
    }

	// Update is called once per frame
	void Update () {
	    if((transform.position - nextdest).magnitude < 0.2)
        {
            CalculateNextPosition();
        }
        if (side == -1) {
            Vector3 temp = transform.parent.Find("RightFish").transform.position;
            temp.x = temp.x * -1;
            transform.position = temp;
            return;

        }
        Vector3 to_point = nextdest - transform.position;
       if(to_point.magnitude> 0.5) transform.position += Vector3.Normalize(to_point) / 1000 *Speed ;
       else {
           transform.position += Vector3.Normalize(to_point) * to_point.magnitude/2f/1000*Speed;
       }


	}
    void FixedUpdate() {
       // rb.AddForce( Vector3.Normalize(nextdest - transform.position)  * Speed, ForceMode2D.Force);
    }
}
