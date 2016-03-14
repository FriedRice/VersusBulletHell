using UnityEngine;
using System.Collections;

public class Squid : MonoBehaviour {
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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
       //CalculateNextPosition();
    }

    IEnumerator animate()
    {
        while (true)
        {
            float ANGLE = 0f;
            for(int c = 0; c < 25; ++c)
            {
                GameObject g = Instantiate(bullet, ShootLoc.transform.position, Quaternion.Euler(0f,0f,ANGLE)) as GameObject;
                g.transform.SetParent(gameObject.transform);
                g.GetComponent<Rigidbody2D>().AddForce(g.transform.up * bulletSpeed);
                ANGLE += 14.4f;
                yield return new WaitForSeconds(0.15f);
            }
            //g.GetComponent<Rigidbody2D>().AddForce(Vector2.down * bulletSpeed + Vector2.right * rng * bulletSpeed, ForceMode2D.Impulse);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.down *  Time.deltaTime * Speed, ForceMode2D.Force);

    }
}
