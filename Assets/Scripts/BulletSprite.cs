using UnityEngine;
using System.Collections;

public class BulletSprite : MonoBehaviour {
    public Sprite[] DieAnim;
    public Sprite[] NormalAnim;


    public float animPlaySpeed = 0.1f;
    Collider2D col;
    bool alive = true;
    bool LEFTBULLET = false;
    SpriteRenderer sr;
    // Use this for initialization
    void Start() {
        if (transform.position.x < 0) {
            LEFTBULLET = true;
        }

        if (LEFTBULLET) {
            Global.DestroyLeftBullets += Dissipate;
        }
        else {
            Global.DestroyRightBullets += Dissipate;
        }
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(anim());
    }

    void OnDisable() {
        if (LEFTBULLET) {
            Global.DestroyLeftBullets -= Dissipate;
        } else {
            Global.DestroyRightBullets -= Dissipate;
        }
    }

    IEnumerator anim() {
        while (alive) {
            for (int c = 0; c < NormalAnim.Length; ++c) {
                sr.sprite = NormalAnim[c];
                yield return new WaitForSeconds(animPlaySpeed);
            }
        }
    }

    IEnumerator dieanim() {
        col.enabled = false;
        for (int c = 0; c < DieAnim.Length; ++c) {
            sr.sprite = DieAnim[c];
            yield return new WaitForSeconds(animPlaySpeed);
        }
        Destroy(gameObject);
    }

    public void Dissipate() {
        alive = false;
        StartCoroutine(dieanim());
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "Divider") {
            Destroy(gameObject);
        }
    }





    // Update is called once per frame
    void Update() {

    }
}
