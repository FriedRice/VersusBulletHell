using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    ParticleSystem ps;
    SpriteRenderer sr;
    Collider2D col;
    public bool ISBEAR = false;
    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
        StartCoroutine(animate());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (ISBEAR)
        {
            if(collision.tag == "FishAllyBullet")
            {
                BulletSprite bs = collision.GetComponent<BulletSprite>();
                bs.Dissipate();

            } else if (collision.tag == "FishAlly")
            {
                Destroy(collision.gameObject);
            }
            else if (collision.tag == "FishPlayer")
            {
                Player p = collision.GetComponent<Player>();
                p.loseLife();
            }
        } else
        {
            if (collision.tag == "BearAllyBullet")
            {
                BulletSprite bs = collision.GetComponent<BulletSprite>();
                bs.Dissipate();

            }
            else if (collision.tag == "BearAlly")
            {
                Destroy(collision.gameObject);
            }
            else if (collision.tag == "BearPlayer")
            {
                Player p = collision.GetComponent<Player>();
                p.loseLife();
            }
        }
    }

    IEnumerator animate()
    {

        HUB.S.PlaySound("Alarm", 1f);
        for (int c = 0; c < 5; ++c)
        {
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
        //yield return new WaitForSeconds(0.5f);
        ps.Play();

        HUB.S.PlaySound("Laser", 1f);
        col.enabled = true;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
