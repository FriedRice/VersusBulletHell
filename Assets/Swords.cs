using UnityEngine;
using System.Collections;

public class Swords : MonoBehaviour {
    public bool Fish = true;
    SpriteRenderer sr;
    // Use this for initialization
    void Start() {
        sr = GetComponent<SpriteRenderer>();
        cr = StartCoroutine(Blink());
    }
    Coroutine cr;
    IEnumerator Blink() {
        for (int c = 0; c < 5; ++c) {
            yield return new WaitForSeconds(0.5f);
            sr.enabled = false;
            yield return new WaitForSeconds(1f);
            sr.enabled = true;
        }
        Destroy(gameObject);
    }

    public void OnDestroy() {
        StopCoroutine(cr);
    }



    // Update is called once per frame
    void Update() {
        if (Fish) {
            transform.position = Player.players[0].gameObject.transform.position;
            if (!Player.players[1].ripper_mode) {
                Destroy(gameObject);
            }
        } else {
            transform.position = Player.players[1].gameObject.transform.position;
            if (!Player.players[0].ripper_mode) {
                Destroy(gameObject);
            }
        }
    }
}
