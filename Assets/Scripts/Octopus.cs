using UnityEngine;
using System.Collections;

public class Octopus : MonoBehaviour
{

    public Sprite[] idle;
    public float animspeed = 0.1f;

    public GameObject bullet;
    public GameObject greenPowerup, bluePowerup;
    public Transform ShootLoc;

    public float Speed = 15f;

    public float movementBoundXLeft, movementBoundXright, movementBoundYTop, movementBoundYBot;

    public float bulletSpeed = 4f;

    public int HEALTH = 15;

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet")
        {
            HEALTH -= 1;
            Destroy(collision.gameObject);
            if(HEALTH <= 0)
            {
                Die();
            }
        }
    }

    Rigidbody2D rb;
    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
        rb.AddForce(Vector3.down * Time.deltaTime * Speed, ForceMode2D.Impulse);
        //CalculateNextPosition();
    }

    IEnumerator animate()
    {
        while (true)
        {
            float ANGLE = 0f;
            for (int c = 0; c < 25; ++c)
            {
                GameObject g = Instantiate(bullet, ShootLoc.transform.position, Quaternion.Euler(0f, 0f, ANGLE)) as GameObject;
                // g.transform.SetParent(gameObject.transform);
                g.GetComponent<Rigidbody2D>().AddForce(g.transform.up * bulletSpeed);
                ANGLE += 14.4f;
            }

            yield return new WaitForSeconds(1f);
            ANGLE = 90f;
            for (int c = 0; c < 25; ++c)
            {
                GameObject g = Instantiate(bullet, ShootLoc.transform.position, Quaternion.Euler(0f, 0f, ANGLE)) as GameObject;
                // g.transform.SetParent(gameObject.transform);
                g.GetComponent<Rigidbody2D>().AddForce(g.transform.up * bulletSpeed);
                ANGLE += 14.4f;
            }


            yield return new WaitForSeconds(2.5f);
            //g.GetComponent<Rigidbody2D>().AddForce(Vector2.down * bulletSpeed + Vector2.right * rng * bulletSpeed, ForceMode2D.Impulse);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
