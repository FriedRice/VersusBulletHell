using UnityEngine;
using System.Collections;

public class AllySprite : MonoBehaviour {
    const float ALPHA_LEVEL = 0.25f;

    int FISH_ONLY_LAYER = LayerMask.NameToLayer("OnlyFishSide");
    int BEAR_ONLY_LAYER = LayerMask.NameToLayer("OnlyBearSide");
    SpriteRenderer sr = null;

    public string parent_tag;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void setSprite(SpriteRenderer parent_sr) {
        if (sr == null) {
            sr = GetComponent<SpriteRenderer>();
        }
        sr.sprite = parent_sr.sprite;
        sr.color = parent_sr.color;
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector2.one;

        Color sprite_color = sr.color;
        sprite_color.a = ALPHA_LEVEL;
        sr.color = sprite_color;

        if (transform.parent.tag.StartsWith("Fish")) {
            gameObject.layer = FISH_ONLY_LAYER;
        } else {
            gameObject.layer = BEAR_ONLY_LAYER;
        }
    }

    public void makeInvisible() {
        sr.color = new Color(1f, 1f, 1f, 0f);
    }
}
