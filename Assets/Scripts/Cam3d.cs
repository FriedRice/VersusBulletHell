using UnityEngine;
using System.Collections;

public class Cam3d : MonoBehaviour {
    Vector3 position;
    Quaternion rotation;
    Quaternion rotation2;
    bool in_3d = false;
    float speed = 4f;
    Quaternion startrot;
    Quaternion lookrot;

    public GameObject target;
    public float FollowDistance = 4f;
    public float distThres = .01f;
    public Vector3 targetPos;
    public float MAXz = -1;
    Vector3 new_pos;
    Vector3 cam_target_position;
    Vector3 start_pos;
    float start_time;

    // Use this for initialization
    void Start() {
        position = transform.position;
        rotation = transform.rotation;
        lookrot = rotation;
        startrot = rotation;
        Transform temp = transform;
        temp.Rotate(Vector3.right, -80);
        rotation2 = temp.rotation;
        if (transform.position.x < 0) {
            target = GameObject.Find("Player 1");
        } else {
            target = GameObject.Find("Player 2");
        }

    }

    // Update is called once per frame
    public void toggle_3d(bool to_3d) {
        if (to_3d) {
            if (!in_3d) {

                lookrot = rotation2;
                startrot = transform.rotation;
                in_3d = true;
            }
            gameObject.GetComponent<Camera>().orthographic = false;
        } else {
            startrot = transform.rotation;
            lookrot = rotation;
            in_3d = false;
            gameObject.GetComponent<Camera>().orthographic = true;
        }
    }
    void baboonCam() {
        start_time = Time.time;
        targetPos = Camera.main.transform.position;
        targetPos = target.transform.position;
        targetPos = targetPos - target.transform.up * FollowDistance - .5f * target.transform.forward;
        
        if (targetPos.z > MAXz - 1) {
            targetPos.z = MAXz - 1;
        }
         
        cam_target_position = targetPos;
        Vector3 to_player = transform.position - cam_target_position;
        start_pos = transform.position;
        if (to_player.magnitude > 0) {
            new_pos = transform.position;
            Vector3 displacement = cam_target_position - transform.position;
            float multiplier = 1;
            //displacement.z = 0;
            if (displacement.magnitude > 4)
                multiplier = displacement.magnitude / 4;
            displacement.Normalize();
            displacement = displacement * 5f * multiplier * multiplier * Time.deltaTime *speed/2;
            while (to_player.magnitude < displacement.magnitude)
                displacement = displacement * 0.9f;
            new_pos += displacement;
            targetPos = new_pos;
            transform.position = new_pos;
        } 
    }
    void FixedUpdate() {
        if (in_3d && target != null) {
            lookrot = rotation2;
            baboonCam();
        } else if(transform.position != position) {
            Vector3 to_pos = position - transform.position;
            if (to_pos.magnitude < speed)
                transform.position = position;
            else
            transform.position += Vector3.Normalize(to_pos)*speed;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrot, speed * Time.deltaTime);
    }

}
