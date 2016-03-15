using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    ParticleSystem ps;
    SpriteRenderer sr;
    Collider2D col;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
        StartCoroutine(animate());
	}

    IEnumerator animate()
    {
        for(int c = 0; c < 3; ++c)
        {
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        ps.Play();
        col.enabled = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
