using UnityEngine;
using System.Collections;

public class BulletSprite : MonoBehaviour
{
    public Sprite[] DieAnim;
    public Sprite[] NormalAnim;

    public float animPlaySpeed = 0.1f;

    bool alive = true;

    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(anim());
    }

    IEnumerator anim()
    {
        while (alive)
        {
            for (int c = 0; c < NormalAnim.Length; ++c)
            {
                sr.sprite = NormalAnim[c];
                yield return new WaitForSeconds(animPlaySpeed);
            }
        }
    }

    IEnumerator dieanim()
    {

        for (int c = 0; c < DieAnim.Length; ++c)
        {
            sr.sprite = DieAnim[c];
            yield return new WaitForSeconds(animPlaySpeed);
        }
    }

    public void Dissipate()
    {
        alive = false;
        StartCoroutine(dieanim());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Divider")
        {
            Destroy(gameObject);
        }
    }





    // Update is called once per frame
    void Update()
    {

    }
}
