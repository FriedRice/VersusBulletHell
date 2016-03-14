using UnityEngine;
using System.Collections;

public class Fish1 : MonoBehaviour {
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
            g.GetComponent<Rigidbody2D>().AddForce(Vector2.down * bulletSpeed + Vector2.right * rng * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    public Vector3 nextdest;

    void CalculateNextPosition()
    {
        float x = Random.Range(movementBoundXLeft, movementBoundXright);
        float y = Random.Range(movementBoundYBot, movementBoundYTop);
        nextdest = new Vector3(x, y, transform.position.z);
    }

	// Update is called once per frame
	void Update () {
	    if((transform.position - nextdest).magnitude < 5)
        {
            CalculateNextPosition();
        }
        rb.AddForce((nextdest - transform.position) * Time.deltaTime * Speed, ForceMode2D.Force);
	}
}
