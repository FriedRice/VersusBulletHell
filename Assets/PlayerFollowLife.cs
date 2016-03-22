using UnityEngine;
using System.Collections;

public class PlayerFollowLife : MonoBehaviour {
    public bool FollowsFish = true;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(animate());
    }

    IEnumerator animate()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowsFish)
        {
            transform.position = Player.players[0].transform.position + Vector3.up;
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            transform.position =(Player.players[1].transform.position) - Vector3.up;
        }
    }
}
