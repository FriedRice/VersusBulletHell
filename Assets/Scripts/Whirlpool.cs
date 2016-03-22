using UnityEngine;
using System.Collections;

public class Whirlpool : MonoBehaviour {
    public Collider2D[] inRange;
    public float pullRadius = 2;
    public float pullForce = 1;
    // Use this for initialization
    void Start () {
        StartCoroutine(lifetime());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        inRange = Physics2D.OverlapCircleAll(transform.position, pullRadius);
        foreach (Collider2D collider in inRange) {
            // calculate direction from target to me
            Vector2 forceDirection = transform.position - collider.transform.position;
            
            // apply force on target towards me
            float inverse = 1f / Mathf.Pow(forceDirection.magnitude, 1.5f);
            if (collider.gameObject.tag != "Whirlpool" && collider.gameObject.tag != "other_side" && collider.gameObject.tag != "LevelBounds")
            {
                collider.GetComponent<Rigidbody2D>().AddForce(10f * inverse * forceDirection * pullForce * Time.fixedDeltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "FishPlayerBullet" || coll.gameObject.tag == "BearPlayerBullet" || coll.gameObject.tag == "FishAllyBullet" || coll.gameObject.tag == "BearAllyBullet")
        {
            Destroy(coll.gameObject);
        }

    }

    IEnumerator lifetime()
    {
        yield return new WaitForSeconds(12f);
        Destroy(this.gameObject);
    }

}
