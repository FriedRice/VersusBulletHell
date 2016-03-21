using UnityEngine;
using System.Collections;

public class ReverseControls : MonoBehaviour
{
    public bool Fish = true;
    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cr =  StartCoroutine(Blink());
    }
    Coroutine cr;
    IEnumerator Blink()
    {
        for (int c = 0; c < 10; ++c)
        {
            yield return new WaitForSeconds(0.5f);
            if (Fish)
            {
                Player.players[0].controlsAreReversed = true;
            }
            else
            {
                Player.players[1].controlsAreReversed = true;
            }
            sr.enabled = false;
            yield return new WaitForSeconds(1f);
            sr.enabled = true;
        }
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        StopCoroutine(cr);
        if (Fish)
        {
            Player.players[0].controlsAreReversed = false;
        } else
        {
            Player.players[1].controlsAreReversed = false;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Fish)
        {
            transform.position = Player.players[0].gameObject.transform.position;
        }
        else
        {

            transform.position = Player.players[1].gameObject.transform.position;
        }
    }
}
