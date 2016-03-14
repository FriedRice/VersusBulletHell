using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
    Material mat;
    public float speed = 1;
    // Use this for initialization
    void Start() {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate() {
        mat.SetTextureOffset("_MainTex", mat.mainTextureOffset + new Vector2(0, Time.deltaTime * speed));
    }
}
