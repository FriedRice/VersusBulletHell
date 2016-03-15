using UnityEngine;
using System.Collections;

public class TidalWave : MonoBehaviour {
	public float directionXMin = -1;
	public float directionXMax = 1;
	public float directionYMin = -1;
	public float directionYMax = 0;
	public Vector3 direction;
	public float speedMin = 4;
	public float speedMax = 7;
	float speed;

	// Use this for initialization
	void Start () {
		if (direction == null) {
			direction = Vector3.down;
		}
		direction = new Vector3 (Random.Range (directionXMin, directionXMax), Random.Range (directionYMin, directionYMax), 0);

		speed = Random.Range (speedMin, speedMax);
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        GetComponent<Rigidbody2D>().velocity = speed * direction;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
