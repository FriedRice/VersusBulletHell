using UnityEngine;
using System.Collections;

public class PowerupDestroyer : MonoBehaviour {
    SpriteRenderer sr;
	// Use this for initialization
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
	}

    IEnumerator animate()
    {
        yield return new WaitForSeconds(10f);
        for(int c = 0; c < 10; ++c)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
